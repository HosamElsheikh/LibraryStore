using LibraryStore.DTOs;
using LibraryStore.Models;
using LibraryStore.Services.Interfaces;
using LibraryStore.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryStore.Services
{
    public class BookService : IBooksService
    {
        private readonly LibraryDbContext _db;

        public BookService(LibraryDbContext db)
        {
            _db = db;
        }

        public async Task<GenericResponse> GetAllAsync(int pageNum = 1)
        {
            int pageSize = 50;

            var data = from b in _db.Books
                       join a in _db.Authors on b.AuthorId equals a.Id
                       orderby b.Id
                       select new BooksListDTOs
                       {
                           Id = b.Id,
                           Title = b.Title,
                           AuthorName = a.Name
                       };

            var result = await data.ToPagedListAsync(pageNum, pageSize);

            return GenericResponse.Success(result);
        }

        public async Task<GenericResponse> GetBookById(int id)
        {
            var data = await _db.Books
                             .Include(b => b.Author)
                             .Select(b => new BooksListDTOs
                             {
                                 Id = b.Id,
                                 Title = b.Title,
                                 AuthorName = b.Author.Name
                             })
                             .FirstOrDefaultAsync(b => b.Id == id);

            if (data == null)
            {
                return GenericResponse.Failure();
            }

            return GenericResponse.Success(data);
        }


        public async Task<GenericResponse> CreateAsync(BooksFormDTOs dto)
        {
            var errors = await _ValidateBooks(dto);
            if (errors.Any()) return GenericResponse.Failure(errors);

            var book = _BuildBookEntity(dto);

            _db.Books.Add(book);
            _db.SaveChanges();

            return GenericResponse.Success(null, "The Book has been added successfully");
        }

        public async Task<GenericResponse> UpdateAsync(int id, BooksFormDTOs dto)
        {
            var bookResponse = await _getById(id);

            var errors = await _ValidateBooks(dto, ignoreId: id);
            if (errors.Any()) return GenericResponse.Failure(errors);

            _FillBookFromDto(dto, bookResponse);

            await _db.SaveChangesAsync();

            return GenericResponse.Success(null, "Book has been updated successfully");
        }

        public async Task<GenericResponse> DeleteAsync(int id)
        {
            var book = await _getById(id);

            _db.Remove(book);
            _db.SaveChanges();

            return GenericResponse.Success(null, $"Book number {id} has been removed successfully!");
        }

        public async Task<GenericResponse> GetBooksByAuthorIdAsync(int pageNumber, int authorId)
        {
            int pageSize = 20;

            var data = from b in _db.Books
                       join a in _db.Authors
                       on b.AuthorId equals a.Id
                       orderby b.Id
                       select new
                       {
                           b.Title,
                           b.AuthorId
                       };

            var results = await data.ToPagedListAsync(pageNumber, pageSize);
            return GenericResponse.Success(results);
        }

        #region Helpers

        private static void _FillBookFromDto(BooksFormDTOs BookDto, Book book)
        {
            book.Title = BookDto.Title;
            book.Price = BookDto.Price;
            book.Quantity = BookDto.Quantity;
            book.AuthorId = BookDto.AuthorId;
            book.UpdatedAt = DateTime.Now;
        }
        private Book _BuildBookEntity(BooksFormDTOs BookDto)
        {
            return new Book
            {
                Title = BookDto.Title,
                Price = BookDto.Price,
                Quantity = BookDto.Quantity,
                CreatedAt = DateTime.Now,
                AuthorId = BookDto.AuthorId,
            };
        }

        private async Task<List<FieldError>> _ValidateBooks(BooksFormDTOs dto, int ignoreId = 0)
        {
            var Errors = new List<FieldError>();

            if (string.IsNullOrWhiteSpace(dto.Title))
                Errors.Add(new FieldError("Title", "Title is required!"));

            if (dto.Quantity < 0)
                Errors.Add(new FieldError("Quantity", "Quantity must be positive!"));

            if (dto.Price <= 0)
                Errors.Add(new FieldError("Price", "Price must be greater than 0!"));

            if (dto.Title.Length < 3)
                Errors.Add(new FieldError("Title", "At least 3 letters in the Title"));

            if (Errors.Any()) return Errors;

            var existingBook = await _db.Books.FirstOrDefaultAsync(b => b.Title == dto.Title && b.Id != ignoreId);
            if (existingBook != null)
                Errors.Add(new FieldError("Title", "Book already exists!"));

            var author = await _db.Authors.FindAsync(dto.AuthorId);
            if (author is null)
                Errors.Add(new FieldError("AuthorId", "Author Not Found!"));

            return Errors;
        }

        private async Task<Book> _getById(int id)
        {
            var data = await _db.Books.FindAsync(id);

            if (data is null)
                throw new Exception("Book not found");

            return data;
        }

        #endregion
    }
}
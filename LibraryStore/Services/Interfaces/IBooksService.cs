using LibraryStore.DTOs;

namespace LibraryStore.Services.Interfaces
{
    public interface IBooksService
    {
        Task<GenericResponse> CreateAsync(BooksFormDTOs book);

        Task<GenericResponse> DeleteAsync(int id);

        Task<GenericResponse> GetAllAsync(int pageNum = 1);

        Task<GenericResponse> GetBookById(int id);

        Task<GenericResponse> UpdateAsync(int id, BooksFormDTOs dto);

        Task<GenericResponse> GetBooksByAuthorIdAsync(int pageNumber, int id);
    }
}

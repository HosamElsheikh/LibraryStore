namespace LibraryStore.DTOs
{
    public class BooksListDTOs
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string AuthorName { get; set; }
    }

    public class BooksFormDTOs
    {
        public string Title { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public int AuthorId { get; set; }
    }
}
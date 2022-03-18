namespace MidAssignment.DTO
{
    public class BookDTO
    {
        public string? BookName { get; set; }
        public string? Author { get; set; }
        public int CategoryId { get; set; }
    }
    public class BookWithIdDTO : BookDTO
    {
        public int BookId { get; set; }
    }

}
namespace MidAssignment.DTO
{
    public class BookRequestDetailDTO
    {
        public int RequestId { get; set; }
        public int BookId { get; set; }
    }

    public class BookRequestDetailWithBookNameDTO : BookRequestDetailDTO
    {
        public string? BookName { set; get; }
    }
}
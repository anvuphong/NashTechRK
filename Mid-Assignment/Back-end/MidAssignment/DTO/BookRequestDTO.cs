using MidAssignment.Enums;

namespace MidAssignment.DTO
{
    public class BookRequestDTO
    {
        public int RequestByUserId { get; set; }
        public DateTime DateOfRequest { get; set; }
        public int? ProcessByUserId { get; set; }
        public Status Status { get; set; }
    }

    public class BookRequestWithIdDTO : BookRequestDTO
    {
        public int RequestId { get; set; }
    }

    public class BookRequestChangeStatusDTO
    {
        public int RequestId { get; set; }
        public int? ProcessByUserId { get; set; }
        public Status Status { get; set; }
    }
}
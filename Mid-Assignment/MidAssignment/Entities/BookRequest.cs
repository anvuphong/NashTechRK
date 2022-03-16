using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using MidAssignment.Enums;

namespace MidAssignment.Entities
{
    [Table("BookRequest")]
    public class BookRequest
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public int RequestId { get; set; }
        public DateTime DateOfRequest { get; set; }
        public Status Status { get; set; }
        public int StatusUserId { get; set; }
        public int UserId { get; set; }
        [JsonIgnore]
        public User? User { get; set; }
        [JsonIgnore]
        public ICollection<BookRequestDetail>? BookRequestDetails { get; set; }
    }
}
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MidAssignment.Entities
{
    [Table("BookRequestDetail")]
    public class BookRequestDetail
    {
        public int RequestId { get; set; }
        public int BookId { get; set; }
        [JsonIgnore]
        public BookRequest? BookRequest { get; set; }
        [JsonIgnore]
        public Book? Book { get; set; }
        
    }
}
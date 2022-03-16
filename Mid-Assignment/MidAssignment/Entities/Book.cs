using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MidAssignment.Entities
{
    [Table("Book")]
    public class Book
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public int BookId { get; set; }
        public string? BookName { get; set; }
        public string? Author { get; set; }
        public int CategoryId { get; set; }
        [JsonIgnore]
        public Category? Category { get; set; }
        // [JsonIgnore]
        // public BookRequestDetail? BookRequestDetail { get; set; }
    }
}
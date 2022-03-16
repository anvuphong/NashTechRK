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

        [Required, MaxLength(100)]
        public string? BookName { get; set; }

        [Required, MaxLength(100)]
        public string? Author { get; set; }

        [Required]
        public int CategoryId { get; set; }
        [JsonIgnore]
        public virtual Category? Category { get; set; }
        
        [JsonIgnore]
        public ICollection<BookRequestDetail>? Details { get; set; }
    }
}
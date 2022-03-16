using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MidAssignment.Entities
{
    [Table("Category")]
    public class Category
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public int CategoryId { get; set; }

        [Required, MaxLength(100)]
        public string? CategoryName { get; set; }
        
        [JsonIgnore]
        public ICollection<Book>? Books { get; set; }
    }
}
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MidAssignment.Entities
{
     [Table("BookRequestDetail")]
    public class BookRequestDetail
    {
        [Required]
         public int RequestId { get; set; }
         [JsonIgnore]
         public virtual BookRequest? BookRequest { get; set; }

         [Required]
        public int BookId { get; set; }
         [JsonIgnore]
         public virtual Book? Book { get; set; }
        
    }
}
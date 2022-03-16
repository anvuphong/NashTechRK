using System.ComponentModel;
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

        [Required]
        public int RequestByUserId { get; set; }
        [JsonIgnore]
        public virtual User? RequestBy { get; set; }

        public int? ProcessByUserId { get; set; }
        [JsonIgnore]
        public virtual User? ProcessBy { get; set; }

        [Required]
        public DateTime DateOfRequest { get; set; }

        [Required, DefaultValue(Status.Waiting)]
        public Status Status { get; set; }

        [JsonIgnore]
        public ICollection<BookRequestDetail>? Details { get; set; }
    }
}
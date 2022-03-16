using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MidAssignment.Entities
{
    [Table("User")]
    public class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public int UserId { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public int RoleId { get; set; }
        [JsonIgnore]
        public ICollection<BookRequest>? BookRequests { get; set; }
    }
}
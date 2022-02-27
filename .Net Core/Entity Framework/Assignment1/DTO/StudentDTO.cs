using System.ComponentModel.DataAnnotations;

namespace Assignment1.DTO
{
    public class StudentDTO
    {
        [MaxLength(100)]
        [Required]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string City { get; set; }
        public string State { get; set; }
    }
}
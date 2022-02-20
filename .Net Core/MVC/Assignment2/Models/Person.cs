using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Assignment2.Models
{
    public class Person
    {
        [HiddenInput]
        public int? Id { get; set; }
        [Required(ErrorMessage = "Required.")]
        public string? FirstName { get; set; }
        [Required(ErrorMessage = "Required.")]
        public string? LastName { get; set; }
        public Gender Gender { get; set; }
        [Required(ErrorMessage = "Required.")]
        public DateTime DateOfBirth { get; set; }
        [Required(ErrorMessage = "Required.")]
        public string? PhoneNumber { get; set; }
        [Required(ErrorMessage = "Required.")]
        public string? BirthPlace { get; set; }
        public bool IsGraduated { get; set; }
    }

    public enum Gender { Male, Female, Other }
}
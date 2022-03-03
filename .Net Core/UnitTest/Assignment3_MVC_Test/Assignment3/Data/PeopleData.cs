using Assignment3.Models;

namespace Assignment3.Data
{
    public class PeopleData
    {
        public static List<Person> People = new List<Person>{
            new Person{
                Id = 1,
                FirstName = "Vu",
                LastName = "Minh Duc",
                Gender = Gender.Male,
                DateOfBirth = new DateTime(2000,08,09),
                PhoneNumber = "0395429489",
                BirthPlace = "Ha Noi",
                IsGraduated = false
            },
            new Person{
                Id = 2,
                FirstName = "Vu",
                LastName = "Minh Ngoc",
                Gender = Gender.Female,
                DateOfBirth = new DateTime(2005,06,23),
                PhoneNumber = "0395429489",
                BirthPlace = "Ha Noi",
                IsGraduated = false
            },
            new Person{
                Id = 3,
                FirstName = "Do",
                LastName = "Long Quan",
                Gender = Gender.Male,
                DateOfBirth = new DateTime(1996,08,09),
                PhoneNumber = "0395429489",
                BirthPlace = "Ha Noi",
                IsGraduated = true
            },
            new Person{
                Id = 4,
                FirstName = "Nguyen",
                LastName = "Thi Tam",
                Gender = Gender.Female,
                DateOfBirth = new DateTime(2000,06,18),
                PhoneNumber = "0395429489",
                BirthPlace = "Ha Noi",
                IsGraduated = false
            },
            new Person{
                Id = 5,
                FirstName = "Ngo",
                LastName = "Van Phuong",
                Gender = Gender.Male,
                DateOfBirth = new DateTime(2000,12,09),
                PhoneNumber = "0395429489",
                BirthPlace = "Ha Noi",
                IsGraduated = false
            },
        };
    }
}
using Assignment1.Models;

namespace Assignment1.Services
{
    public class PersonManipulation : IPersonManipulation
    {
        public List<Person> CreatePeople()
        {
            return new List<Person>{
            new Person{
                FirstName = "Vu",
                LastName = "Minh Duc",
                Gender = Gender.Male,
                DateOfBirth = new DateTime(2000,08,09),
                PhoneNumber = "0395429489",
                BirthPlace = "Ha Noi",
                Age = 22,
                IsGraduated = false
            },
            new Person{
                FirstName = "Vu",
                LastName = "Minh Ngoc",
                Gender = Gender.Female,
                DateOfBirth = new DateTime(2005,06,23),
                PhoneNumber = "0395429489",
                BirthPlace = "Ha Noi",
                Age = 17,
                IsGraduated = false
            },
            new Person{
                FirstName = "Do",
                LastName = "Long Quan",
                Gender = Gender.Male,
                DateOfBirth = new DateTime(1996,08,09),
                PhoneNumber = "0395429489",
                BirthPlace = "Ha Noi",
                Age = 26,
                IsGraduated = true
            },
            new Person{
                FirstName = "Nguyen",
                LastName = "Thi Tam",
                Gender = Gender.Female,
                DateOfBirth = new DateTime(2000,06,18),
                PhoneNumber = "0395429489",
                BirthPlace = "Ha Noi",
                Age = 22,
                IsGraduated = false
            },
            new Person{
                FirstName = "Ngo",
                LastName = "Van Phuong",
                Gender = Gender.Male,
                DateOfBirth = new DateTime(2000,12,09),
                PhoneNumber = "0395429489",
                BirthPlace = "Ha Noi",
                Age = 22,
                IsGraduated = false
            },
        };
        }

        public List<Person> GetMemberByGender(List<Person> members, Gender gender)
        {
            var list = new List<Person>();
            foreach (var member in members)
            {
                if (member.Gender == gender)
                {
                    list.Add(member);
                }
            }
            return list;
        }

        public List<String> GetMemberFullName(List<Person> members)
        {
            return members.Select(member => member.Fullname).ToList();
        }

        public Person GetOldestMember(List<Person> members)
        {
            var maxAge = members.Max(member => member.Age);
            return members.FirstOrDefault(member => member.Age == maxAge);
        }
    }
}
using System;
using System.Collections.Generic;

public class MembersUntil
{
    public List<Member> Members;
    public MembersUntil() {
        Members = InitMembers();
        
     }
    private List<Member> InitMembers()
    {
        return new List<Member>{
            new Member{
                FirstName = "Vu",
                LastName = "Minh Duc",
                Gender = Gender.Male,
                DateOfBirth = new DateTime(2000,08,09),
                PhoneNumber = "0395429489",
                BirthPlace = "Ha Noi",
                Age = 22,
                IsGraduated = false
            },
            new Member{
                FirstName = "Vu",
                LastName = "Minh Ngoc",
                Gender = Gender.Female,
                DateOfBirth = new DateTime(2005,06,23),
                PhoneNumber = "0395429489",
                BirthPlace = "Ha Noi",
                Age = 17,
                IsGraduated = false
            },
            new Member{
                FirstName = "Do",
                LastName = "Long Quan",
                Gender = Gender.Male,
                DateOfBirth = new DateTime(1996,08,09),
                PhoneNumber = "0395429489",
                BirthPlace = "Ha Noi",
                Age = 26,
                IsGraduated = true
            },
            new Member{
                FirstName = "Nguyen",
                LastName = "Thi Tam",
                Gender = Gender.Female,
                DateOfBirth = new DateTime(2000,06,18),
                PhoneNumber = "0395429489",
                BirthPlace = "Ha Noi",
                Age = 22,
                IsGraduated = false
            },
            new Member{
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

}
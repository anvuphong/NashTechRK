using System;
public class Member
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DefineGender Gender { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string PhoneNumber { get; set; }
    public string BirthPlace { get; set; }
    public int Age { get; set; }
    public bool IsGraduated { get; set; }
    public Member(string firstName, string lastName, DefineGender gender, DateTime dateOfBirth, string phoneNumber, string birthPlace, int age, bool isGraduated)
    {
        FirstName = firstName;
        LastName = lastName;
        Gender = gender;
        DateOfBirth = dateOfBirth;
        PhoneNumber = phoneNumber;
        BirthPlace = birthPlace;
        Age = age;
        IsGraduated = isGraduated;
    }

    public Member()
    {
    }
    public enum DefineGender { Male, Female, Other }

    public override string ToString()
    {
        return "Firstname: " + FirstName + ", Lastname: " + LastName + ", Gender: " + Gender + ", DateOfBirth: " + DateOfBirth + ", PhoneNumber: " + PhoneNumber + ", BirthPlace: " + BirthPlace + ", Age: " + Age + ", IsGraduated?: " + (IsGraduated ? "Yes" : "No");
    }
}
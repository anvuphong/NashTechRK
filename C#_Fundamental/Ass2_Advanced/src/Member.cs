using System;
public class Member
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public Gender Gender { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string PhoneNumber { get; set; }
    public string BirthPlace { get; set; }
    public int Age { get; set; }
    public bool IsGraduated { get; set; }
    public string MemberInfo { get { return String.Format("{0} {1} {2}", FirstName, LastName, Gender.ToString()); } }
    public string Fullname { get { return String.Format("{0} {1}", FirstName, LastName); } }
}
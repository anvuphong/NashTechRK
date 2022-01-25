namespace Assignment1.Models
{
    public class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Gender Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public string BirthPlace { get; set; }
        public int Age { get; set; }
        public bool IsGraduated { get; set; }
        public string MemberInfo { get { return String.Format("{0} {1} {2} {3} {4} {5} {6} {7}", FirstName, LastName, Gender.ToString(), DateOfBirth, PhoneNumber, BirthPlace, Age, (IsGraduated? "Is Graduated":"Isn't Graduated") ); } }
        public string Fullname { get { return String.Format("{0} {1}", FirstName, LastName); } }

    }
    public enum Gender { Male, Female, Other }
}
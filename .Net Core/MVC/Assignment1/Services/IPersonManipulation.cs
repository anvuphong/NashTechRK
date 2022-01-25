using Assignment1.Models;

namespace Assignment1.Services
{
    public interface IPersonManipulation
    {
        public List<Person> GetMemberByGender(List<Person> members, Gender gender);
        public List<Person> CreatePeople();
        public Person GetOldestMember(List<Person> members);
        public List<String> GetMemberFullName(List<Person> members);
    }
}
using System;
using System.Linq;

namespace Ass2_Advanced
{
    class Program
    {
        static void Main(string[] args)
        {
            MembersUntil membersUntil = new MembersUntil();
            var members = membersUntil.Members;
            //1
            var maleMembers = members.Where(m => m.Gender == Gender.Male);
            Console.WriteLine("List of member who is Male:");
            foreach (Member member in maleMembers)
            {
                Console.WriteLine("{0}", member.MemberInfo);
            }
            Console.WriteLine("----------------------------------------");
            //2
            var maxAge = members.Max(member => member.Age);
            Console.WriteLine("List of member based on Age:");
            Console.WriteLine(members.FirstOrDefault(member => member.Age == maxAge).MemberInfo);
            Console.WriteLine("----------------------------------------");
            //3
            Console.WriteLine("List Fullname of member:");
            var fullnameList = members.Select(member => member.Fullname).ToList();
            fullnameList.ForEach(m => { Console.WriteLine("Full name: " + m); });
            Console.WriteLine("----------------------------------------");
            //4
            Console.WriteLine("List member base on BirthYear:");
            Console.WriteLine("Born in 2000:");
            members.Where(m => m.DateOfBirth.Year == 2000).Select(x => x.MemberInfo).ToList().ForEach(x => Console.WriteLine(x));
            Console.WriteLine("Born before 2000:");
            members.Where(m => m.DateOfBirth.Year < 2000).Select(x => x.MemberInfo).ToList().ForEach(x => Console.WriteLine(x));
            Console.WriteLine("Born after 2000");
            members.Where(m => m.DateOfBirth.Year > 2000).Select(x => x.MemberInfo).ToList().ForEach(x => Console.WriteLine(x));
            Console.WriteLine("----------------------------------------");
            //5
            Console.WriteLine("Firsr member who was born in Ha Noi: ");
            var firstBornAtHanoi = members.Where(m => m.BirthPlace == "Ha Noi").FirstOrDefault();
            Console.WriteLine(firstBornAtHanoi.MemberInfo);

        }
    }
}

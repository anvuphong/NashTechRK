using System;
using System.Collections.Generic;

namespace Day1_Basic
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Hello World!");
            List<Member> members = new List<Member>();
            members.Add(new Member("Vu", "Minh Duc", Member.DefineGender.Male, new DateTime(2000, 9, 8), "0395429489", "Ha Noi", 22, false));
            members.Add(new Member("Vu", "Minh Ngoc", Member.DefineGender.Female, new DateTime(2006, 6, 23), "0395429489", "Ha Noi", 17, false));
            members.Add(new Member("Do", "Long Quan", Member.DefineGender.Male, new DateTime(1996, 12, 10), "0395429489", "Hai Phong", 26, true));
            members.Add(new Member("Nguyen", "Tam", Member.DefineGender.Female, new DateTime(2000, 6, 18), "0395429489", "Bac Giang", 22, false));
            members.Add(new Member("Ngo", "Hoang Phuong", Member.DefineGender.Male, new DateTime(2000, 4, 20), "0395429489", "Ha Noi", 22, false));

            Controller controller = new Controller();
            //1
            Console.WriteLine("List of member who is Male:");
            controller.PrintData(controller.GetMaleList(members));
            Console.WriteLine("----------------------------------------");
            //2
            Console.WriteLine("List of member based on Age:");
            Console.WriteLine(controller.GetOldestMember(members).ToString());
            Console.WriteLine("----------------------------------------");
            //3
            Console.WriteLine("List Fullname of member:");
            var FullnameList = controller.GetFullnameList(members);
            foreach (var name in FullnameList)
            {
                Console.WriteLine(name);
            }
            Console.WriteLine("----------------------------------------");
            //4
            Console.WriteLine("List member base on BirthYear:");
            var results = controller.SplitMembersByBirthYear(members);
            Console.WriteLine("Equal 2000:");
            controller.PrintData(results.Item1);
            Console.WriteLine("Greater than 2000:");
            controller.PrintData(results.Item2);
            Console.WriteLine("Less than 2000");
            controller.PrintData(results.Item3);
            Console.WriteLine("----------------------------------------");
            //5
            Console.WriteLine("Firsr member who was born in Ha Noi: ");
            Console.WriteLine(controller.GetBornPlace(members).ToString());
        }


    }
}

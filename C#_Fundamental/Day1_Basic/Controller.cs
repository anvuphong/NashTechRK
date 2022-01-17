using System;
using System.Collections.Generic;

class Controller
{
    //Output
    public void PrintData(List<Member> members)
    {
        foreach (var member in members)
        {
            Console.WriteLine(member.ToString());
        }
    }
    //Return a list of members who is Male
    public List<Member> GetMaleList(List<Member> members)
    {
        List<Member> maleList = new List<Member>();
        foreach (Member member in members)
        {
            if (member.Gender == Member.DefineGender.Male)
            {
                maleList.Add(member);
                //Console.WriteLine(member.ToString());
            }
        }
        return maleList;
    }
    //Return the oldest one based on Age
    public Member GetOldestMember(List<Member> members)
    {
        var maxAge = members[0].Age;
        var maxAgeIndex = 0;
        for (var i = 1; i < members.Count; i++)
        {
            if (members[i].Age > maxAge)
            {
                maxAge = members[i].Age;
                maxAgeIndex = i;
            }
        }
        return members[maxAgeIndex];
    }
    //Return a new list contains Fullname
    public List<string> GetFullnameList(List<Member> members)
    {
        List<string> fullnameList = new List<string>();
        foreach (Member member in members)
        {
            fullnameList.Add(member.FirstName + " " + member.LastName);
        }
        return fullnameList;
    }
    //Return 3 list base on BirthYear
    public Tuple<List<Member>, List<Member>, List<Member>> SplitMembersByBirthYear(List<Member> members, int year = 2000)
    {
        var equal = new List<Member>();
        var greater = new List<Member>();
        var less = new List<Member>();
        foreach (var member in members)
        {
            var birthYear = member.DateOfBirth.Year;
            switch (birthYear)
            {
                case 2000:
                    equal.Add(member);
                    break;
                case >2000:
                    greater.Add(member);
                    break;
                case <2000:
                    less.Add(member);
                    break;
            }
        }
        return Tuple.Create(equal,greater,less);
    }
    //Return the first person who was born in Ha Noi
    public Member GetBornPlace(List<Member> members, string Place = "Ha Noi")
    {
        Member firstBornInHanoi = new Member();
        foreach (Member member in members)
        {
            if (member.BirthPlace.ToLower().Equals(Place.ToLower()))
            {
                return member;
            }
        }
        return firstBornInHanoi;
    }

}
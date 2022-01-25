using Microsoft.AspNetCore.Mvc;
using Assignment1.Services;
using System.Data;
using Assignment1.CustomActionResults;
using Assignment1.Models;

namespace Assignment1.Controllers;

//[Route("NashTech")]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IPersonManipulation _personManipulation;
    private readonly List<Person> _people;
    private readonly PersonCsvResult _peopleCSVResult;

    public HomeController(ILogger<HomeController> logger, IPersonManipulation personManipulation)
    {
        _logger = logger;
        _personManipulation = personManipulation;
        _people = personManipulation.CreatePeople();
    }

    //[Route("Rookies")]
    public IActionResult MaleMember()
    {
        var people = _personManipulation.CreatePeople();
        var malePeople = _personManipulation.GetMemberByGender(people, Gender.Male);
        return View(malePeople);
    }

    public IActionResult OldestMember()
    {
        var people = _personManipulation.CreatePeople();
        var oldestPerson = _personManipulation.GetOldestMember(people);
        return View(oldestPerson);
    }

    public IActionResult MemberFullname()
    {
        var people = _personManipulation.CreatePeople();
        var fullnameList = _personManipulation.GetMemberFullName(people);
        return View(fullnameList);
    }

    public IActionResult MemberByBirthYear([FromQuery(Name = "comparator")] string comparator)
    {
        switch (comparator)
        {
            case "equal":
                return RedirectToAction("BirthyearEqual2000");
            case "greater":
                return RedirectToAction("BirthyearGreaterThan2000");
            case "less":
                return RedirectToAction("BirthyearLessThan2000");
            default: return View("Index");
        }
    }

    public IActionResult BirthyearEqual2000()
    {
        var people = _personManipulation.CreatePeople();
        var birthyearEqual2000 = people.Where(m => m.DateOfBirth.Year == 2000).ToList();
        return View(birthyearEqual2000);
    }

    public IActionResult BirthyearGreaterThan2000()
    {
        var people = _personManipulation.CreatePeople();
        var birthyearGreaterThan2000 = people.Where(m => m.DateOfBirth.Year > 2000).ToList();
        return View(birthyearGreaterThan2000);
    }

    public IActionResult BirthyearLessThan2000()
    {
        var people = _personManipulation.CreatePeople();
        var birthyearLessThan2000 = people.Where(m => m.DateOfBirth.Year < 2000).ToList();
        return View(birthyearLessThan2000);
    }

    public async Task<IActionResult> ListPeopleCSV()
    {
        return new PersonCsvResult(_people, "people.csv");
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

}

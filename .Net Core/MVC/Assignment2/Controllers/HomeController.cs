using Microsoft.AspNetCore.Mvc;
using Assignment2.Models;
using Assignment2.Services;

namespace Assignment2.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IPersonManipulation _personManipulation;

    public static List<Person> People = new List<Person>{
            new Person{
                Id = 1,
                FirstName = "Vu",
                LastName = "Minh Duc",
                Gender = Gender.Male,
                DateOfBirth = new DateTime(2000,08,09),
                PhoneNumber = "0395429489",
                BirthPlace = "Ha Noi",
                IsGraduated = false
            },
            new Person{
                Id = 2,
                FirstName = "Vu",
                LastName = "Minh Ngoc",
                Gender = Gender.Female,
                DateOfBirth = new DateTime(2005,06,23),
                PhoneNumber = "0395429489",
                BirthPlace = "Ha Noi",
                IsGraduated = false
            },
            new Person{
                Id = 3,
                FirstName = "Do",
                LastName = "Long Quan",
                Gender = Gender.Male,
                DateOfBirth = new DateTime(1996,08,09),
                PhoneNumber = "0395429489",
                BirthPlace = "Ha Noi",
                IsGraduated = true
            },
            new Person{
                Id = 4,
                FirstName = "Nguyen",
                LastName = "Thi Tam",
                Gender = Gender.Female,
                DateOfBirth = new DateTime(2000,06,18),
                PhoneNumber = "0395429489",
                BirthPlace = "Ha Noi",
                IsGraduated = false
            },
            new Person{
                Id = 5,
                FirstName = "Ngo",
                LastName = "Van Phuong",
                Gender = Gender.Male,
                DateOfBirth = new DateTime(2000,12,09),
                PhoneNumber = "0395429489",
                BirthPlace = "Ha Noi",
                IsGraduated = false
            },
        };
    public HomeController(ILogger<HomeController> logger, IPersonManipulation personManipulation)
    {
        _logger = logger;
        _personManipulation = personManipulation;
    }

    public IActionResult Index()
    {
        return View(People.OrderBy(x => x.FirstName).ToList());
    }

    public IActionResult CreateNewPerson()
    {
        var person = new Person();
        return View(person);
    }


    public IActionResult EditPerson([FromQuery] int personId)
    {
        var person = People.Find(p => p.Id == personId);
        if (person == null)
        {
            return RedirectToAction("PersonNotFound");
        }
        return View(person);
    }


    [HttpPost]
    public IActionResult SavePerson(Person person)
    {
        //Add new person to list of person
        //No handle for invalid ModelState, because we have server & client side validation
        if (ModelState.IsValid)
        {
            if (person.Id == 0)
            {
                var newId = People.Max(x => x.Id);
                person.Id = newId + 1;
                People.Add(person);
            }
            else
            {
                People.RemoveAll(x => x.Id == person.Id);
                People.Add(person);
            }
        }
        return Redirect("Index");
    }

    [HttpPost]
    public IActionResult DeletePerson(int personId)
    {
        var person = People.Find(p => p.Id == personId);
        if (person == null)
        {
            return RedirectToAction("PersonNotFound");
        }

        People.RemoveAll(p => p.Id == personId);
        return Json(new { status = "OK" });
    }

    public IActionResult PersonNotFound()
    {
        return View();
    }
}

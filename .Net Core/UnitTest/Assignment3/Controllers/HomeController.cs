using Assignment3.Data;
using Assignment3.Models;
using Assignment3.Services;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Assignment3.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IPersonManipulation _personManipulation;

    public HomeController(ILogger<HomeController> logger, IPersonManipulation personManipulation)
    {
        _logger = logger;
        _personManipulation = personManipulation;
    }

    public IActionResult Index()
    {
        List<Person> people = _personManipulation.GetAllMembers();
        return View(people);
    }

    public IActionResult Add()
    {
        return View();
    }
    [HttpPost]
    public IActionResult Add(Person person)
    {
        _personManipulation.CreateNewPerson(person);
        return RedirectToAction("Index");
    }

    public IActionResult Edit([FromQuery] int personId)
    {
        var person = _personManipulation.GetPersonByID(personId);
        return View(person);
    }
    [HttpPost]
    public IActionResult Edit(Person person)
    {
        _personManipulation.UpdatePersonInfo(person);
        return RedirectToAction("Index");
    }
    [HttpPost]
    public IActionResult DeletePerson(int personId)
    {
        var person = _personManipulation.GetPersonByID(personId);
        HttpContext.Session.SetString("DELETED_USER_NAME", person.LastName);
        _personManipulation.DeletePerson(personId);
        //return RedirectToAction("Result");
        return Json(new { status = "OK" });
    }

    public IActionResult Detail([FromQuery] int personId)
    {
        var person = _personManipulation.GetPersonByID(personId);
        return View(person);
    }

    public IActionResult Result()
    {
        return View();
    }

}

using Assignment3.Data;
using Assignment3.Models;
using Assignment3.Services;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using ClosedXML.Excel;

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
        return View(PeopleData.People.OrderBy(x => x.FirstName).ToList());
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
        return Json(new { status = "OK" });
    }

    public IActionResult Detail([FromQuery] int personId){
        var person = _personManipulation.GetPersonByID(personId);
        return View(person);
    }

    public IActionResult Result()
    {
        
        return View();
    }

    public IActionResult ExportToExcell()
        {
            using (var wookbook = new XLWorkbook())
            {
                var wooksheet = wookbook.Worksheets.Add("PersonInfo");
                var currentRow = 1;
                wooksheet.Cell(currentRow, 1).Value = "ID";
                wooksheet.Cell(currentRow, 2).Value = "First Name";
                wooksheet.Cell(currentRow, 3).Value = "Last Name";
                wooksheet.Cell(currentRow, 4).Value = "Gender";
                wooksheet.Cell(currentRow, 5).Value = "Date of Birth";
                wooksheet.Cell(currentRow, 6).Value = "Birth Address";
                wooksheet.Cell(currentRow, 7).Value = "Phone Number";
                wooksheet.Cell(currentRow, 8).Value = "Is Granduated";
                foreach (var person in PeopleData.People)
                {
                    currentRow++;
                    wooksheet.Cell(currentRow, 1).Value = person.Id;
                    wooksheet.Cell(currentRow, 2).Value = person.FirstName;
                    wooksheet.Cell(currentRow, 3).Value = person.LastName;
                    wooksheet.Cell(currentRow, 4).Value = (person.Gender == Gender.Male) ? "Male" : "Female";
                    wooksheet.Cell(currentRow, 5).Value = person.DateOfBirth.ToString("dd-MM-yyyy");
                    wooksheet.Cell(currentRow, 6).Value = person.BirthPlace;
                    wooksheet.Cell(currentRow, 7).Value ="'"+ person.PhoneNumber;
                    wooksheet.Cell(currentRow, 8).Value = string.Format("{0}", person.IsGraduated ? "Yes" : "No");
                }
                using (var stream = new MemoryStream())
                {
                    
                    wookbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "person.xlsx");
                }

            }

        }

}

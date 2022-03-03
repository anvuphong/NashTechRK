using NUnit.Framework;
using Moq;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;
using System.Linq;
using Assignment3.Controllers;
using Assignment3.Services;
using Assignment3.Models;

namespace Assignment3.Test;

public class HomeControllerTest
{
    private Mock<ILogger<HomeController>> _loggerMock;
    private Mock<IPersonManipulation> _serviceMock;
    private HomeController _controller;

    static List<Person> _people = new List<Person>{
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
            }
            };

    [SetUp]
    public void Setup()
    {
        _loggerMock = new Mock<ILogger<HomeController>>();
        _serviceMock = new Mock<IPersonManipulation>();
        _controller = new HomeController(_loggerMock.Object, _serviceMock.Object);

        //GetAll
        _serviceMock.Setup(x => x.GetAllMembers()).Returns(_people);
    }

    [Test]
    public void Index_ReturnView_ListPeople()
    {
        //Arrange
        var expectedCount = _people.Count;

        //Act
        var result = _controller.Index();

        //Assert
        Assert.IsInstanceOf<ViewResult>(result, "Invalid return type.");

        var view = (ViewResult)result;
        Assert.IsAssignableFrom<List<Person>>(view.ViewData.Model, "Invalid view data model.");

        var model = view.ViewData.Model as List<Person>;
        Assert.IsNotNull(model, "View data model must not be Null");
        Assert.AreEqual(expectedCount, model?.Count, "Model count is not equal to expected count.");
    }

    [Test]
    public void Detail_InputId_ReturnView()
    {
        //Setup
        const int id = 1;
        _serviceMock.Setup(x => x.GetPersonByID(id)).Returns(_people[id - 1]);
        var expected = _people.FirstOrDefault(x => x.Id == id);

        //Act
        var result = _controller.Detail(id);

        //Assert
        Assert.IsInstanceOf<ViewResult>(result, "Invalid return type.");

        var view = (ViewResult)result;
        Assert.IsAssignableFrom<Person>(view.ViewData.Model, "Invalid view data model.");

        var model = view.ViewData.Model as Person;
        Assert.IsNotNull(model, "View data model must not be null.");
        Assert.AreEqual(expected, model, "Model is not equal to expected count.");
    }

    [Test]
    public void Add_ReturnView()
    {

        //Action
        var result = _controller.Add();
        //Asert
        Assert.IsInstanceOf<ViewResult>(result);
        var view = result as ViewResult;
    }

    [Test]
    public void Add_InputValidPersonInfo_RedirectToAction()
    {
        //Setup
        var expected = _people.Count + 1;
        var person = new Person
        {
            FirstName = "Vu",
            LastName = "Minh Duc",
            Gender = Gender.Male,
            DateOfBirth = new DateTime(2000, 8, 9),
            PhoneNumber = "0395429489",
            BirthPlace = "Ha Noi",
            IsGraduated = false
        };
        _serviceMock.Setup(x => x.CreateNewPerson(person)).Callback<Person>((Person p) => _people.Add(person));

        //Act
        var result = _controller.Add(person);

        //Assert
        Assert.IsInstanceOf<RedirectToActionResult>(result, "Invalid return type.");

        var view = (RedirectToActionResult)result;
        Assert.AreEqual("Index", view.ActionName, "Invalid action name.");

        var actual = _people.Count;
        Assert.AreEqual(expected, actual, "Model count is not equal to expected count.");

        var actualPerson = _people.Last();
        Assert.AreEqual(person, actualPerson, "Model is not equal to expected.");
    }

    [Test]
    public void Edit_InputId_ReturnView()
    {
        //Setup
        const int id = 1;
        _serviceMock.Setup(x => x.GetPersonByID(id)).Returns(_people[id - 1]);
        var expected = _people[id - 1];

        //Act
        var result = _controller.Edit(id);

        //Assert
        Assert.IsInstanceOf<ViewResult>(result, "Invalid return type.");

        var view = (ViewResult)result;
        Assert.IsAssignableFrom<Person>(view.ViewData.Model, "Invalid view data model.");

        var model = view.ViewData.Model as Person;
        Assert.IsNotNull(model, "View data model must not be null.");
        Assert.AreEqual(expected, model, "Model is not equal to expected.");
    }

    [Test]
    public void Edit_InputValidPersonInfo_RedirectToAction()
    {
        //Setup
        var person = new Person
        {
            Id = 1,
            FirstName = "Nguyen",
            LastName = "Thi Tam",
            Gender = Gender.Female,
            DateOfBirth = new DateTime(2000, 8, 9),
            PhoneNumber = "0395429489",
            BirthPlace = "Bac Giang",
            IsGraduated = false
        };

        _serviceMock.Setup(x => x.UpdatePersonInfo(person)).Callback<Person>((Person p) =>
        {
            var updatePerson = _people.FirstOrDefault(x => x.Id == person.Id);
            updatePerson.FirstName = person.FirstName;
            updatePerson.LastName = person.LastName;
            updatePerson.Gender = person.Gender;
            updatePerson.DateOfBirth = person.DateOfBirth;
            updatePerson.PhoneNumber = person.PhoneNumber;
            updatePerson.BirthPlace = person.BirthPlace;
            updatePerson.IsGraduated = person.IsGraduated;
        });
        var actual = _people.FirstOrDefault(x => x.Id == person.Id);

        //Act
        var result = _controller.Edit(person);

        //Assert
        Assert.IsInstanceOf<RedirectToActionResult>(result, "Invalid return type.");

        var view = (RedirectToActionResult)result;
        Assert.AreEqual("Index", view.ActionName, "Invalid action name.");

        //Assert.AreEqual(person, actual, "Model is not equal to expected.");
    }

    [Test]
    public void Delete_InputId_ReturnView()
    {
        //Setup
        const int id = 1;
        var deletePerson = _people.FirstOrDefault(x => x.Id == id);
        _serviceMock.Setup(x => x.DeletePerson(id)).Callback(() => _people.Remove(deletePerson));

        //Act
        var result = _controller.Result();

        //Assert
        Assert.IsInstanceOf<ViewResult>(result, "Invalid return type.");
    }
}
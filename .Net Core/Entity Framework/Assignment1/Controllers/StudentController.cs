using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Assignment1.DTO;
using Assignment1.Entities;
using Assignment1.Service;
using Microsoft.AspNetCore.Mvc;

namespace Assignment1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController : ControllerBase
    {
        IStudentService _service;
        public StudentController(IStudentService service)
        {
            _service = service;
        }

        [HttpPost("add-student")]
        public void CreateNewStudent(StudentDTO studentDTO)
        {
            _service.CreateNewStudent(studentDTO);
        }
        [HttpGet("get-all")]
        public List<Student> GetAllStudents()
        {
            return _service.GetAllStudents();
        }
        [HttpGet("get-one-student")]
        public Student GetStudentById(int id)
        {
            return _service.GetStudentById(id);
        }
        [HttpDelete("delete-student")]
        public void DeleteStudent(int id)
        {
            _service.DeleteStudent(id);
        }
        [HttpPut("update-student")]
        public void UpdateStudent([FromBody] Student student)
        {
            _service.UpdateStudentInfo(student);
        }
    }
}
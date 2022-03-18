using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MidAssignment.DTO;
using MidAssignment.Entities;
using MidAssignment.Interfaces;

namespace MidAssignment.Controllers
{
    [ApiController]
    [Route("api/")]
    public class BookController : ControllerBase
    {
        private readonly ILibraryServices<Book> _bookService;
        private readonly IMapper _mapper;

        public BookController(ILibraryServices<Book> bookService, IMapper mapper)
        {
            _bookService = bookService;
            _mapper = mapper;
        }

        [HttpPost("book")]
        public IActionResult AddBook(BookDTO bookDTO)
        {
            if (bookDTO == null) return BadRequest(ModelState);
            var book = _mapper.Map<Book>(bookDTO);
            if (!_bookService.IsValidForeignKey(book.CategoryId))
            {
                ModelState.AddModelError("InvalidFK", "Invalid ForeignKey");
                return StatusCode(422, ModelState);
            }
            if (ModelState.IsValid)
            {
                _bookService.Add(book);
                return Ok(book);
            }
            return BadRequest();
        }

        [HttpGet("book")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<BookWithIdDTO>))]
        public IActionResult GetAllBooks()
        {
            var books = _mapper.Map<List<BookWithIdDTO>>(_bookService.GetAll());
            if (!ModelState.IsValid) return BadRequest(ModelState);
            return Ok(books);
        }

        [HttpGet("book/{id}")]
        [ProducesResponseType(200, Type = typeof(BookWithIdDTO))]
        public IActionResult GetBookById(int id)
        {
            if (!_bookService.GetAll().Any(b => b.BookId == id)) return NotFound();
            var book = _mapper.Map<BookWithIdDTO>(_bookService.GetById(id));
            if (!ModelState.IsValid) return BadRequest(ModelState);
            return Ok(book);
        }

        [HttpPut("book")]
        public IActionResult UpdateBook([FromBody]BookWithIdDTO bookDTO)
        {
            if (bookDTO == null) return BadRequest(ModelState);
            var book = _mapper.Map<Book>(bookDTO);
            if (!_bookService.GetAll().Any(b => b.BookId == book.BookId)) return NotFound();
            if (!_bookService.IsValidForeignKey(book.CategoryId))
            {
                ModelState.AddModelError("InvalidFK", "Invalid ForeignKey");
                return StatusCode(422, ModelState);
            }
            if (ModelState.IsValid)
            {
                _bookService.Update(book);
                return Ok(book);
            }
            return BadRequest(ModelState);
        }

        [HttpDelete("book")]
        [ProducesResponseType(200, Type = typeof(BookDTO))]
        public IActionResult DeleteBook(int id)
        {
            var book = _bookService.GetById(id);
            if (!_bookService.GetAll().Any(b => b.BookId == book.BookId)) return NotFound();
            if (ModelState.IsValid)
            {
                _bookService.Remove(book);
            }
            return Ok(book);
        }
    }
}
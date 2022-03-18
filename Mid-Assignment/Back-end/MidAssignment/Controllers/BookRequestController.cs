using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MidAssignment.DTO;
using MidAssignment.Entities;
using MidAssignment.Interfaces;

namespace MidAssignment.Controllers
{
    [ApiController]
    [Route("api")]
    public class BookRequestController : ControllerBase
    {
        private readonly ILibraryServices<BookRequest> _bookRequestService;
        private readonly IMapper _mapper;

        public BookRequestController(ILibraryServices<BookRequest> bookRequestService, IMapper mapper)
        {
            _bookRequestService = bookRequestService;
            _mapper = mapper;
        }

        [HttpPost("book-request")]
        public IActionResult CreateBookRequest(BookRequestDTO bookRequestDTO)
        {
            if (bookRequestDTO == null) return BadRequest(ModelState);
            var bookRequest = _mapper.Map<BookRequest>(bookRequestDTO);
            if (!_bookRequestService.IsValidForeignKey(bookRequest.RequestByUserId))
            {
                ModelState.AddModelError("InvalidFK", "Invalid ForeignKey");
                return StatusCode(422, ModelState);
            }
            if (ModelState.IsValid)
            {
                _bookRequestService.Add(bookRequest);
                return Ok(bookRequest);
            }
            return BadRequest(ModelState);
        }

        [HttpGet("book-request")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<BookRequestWithIdDTO>))]
        public IActionResult GetAllBooks()
        {
            var bookRequests = _mapper.Map<List<BookRequestWithIdDTO>>(_bookRequestService.GetAll());
            if (!ModelState.IsValid) return BadRequest(ModelState);
            return Ok(bookRequests);
        }

        [HttpGet("book-request/{id}")]
        [ProducesResponseType(200, Type = typeof(BookRequestWithIdDTO))]
        public IActionResult GetBookById(int id)
        {
            if (!_bookRequestService.GetAll().Any(b => b.RequestId == id)) return NotFound();
            var book = _mapper.Map<BookRequestWithIdDTO>(_bookRequestService.GetById(id));
            if (!ModelState.IsValid) return BadRequest(ModelState);
            return Ok(book);
        }

        [HttpPut("book-request")]
        public IActionResult UpdateBook([FromBody]BookRequestChangeStatusDTO bookRequestDTO)
        {
            if (bookRequestDTO == null) return BadRequest(ModelState);
            var bookRequest =  _mapper.Map<BookRequest>(bookRequestDTO);
            if (!_bookRequestService.GetAll().Any(br => br.RequestId == bookRequest.RequestId)) return NotFound();
            if (!_bookRequestService.IsValidForeignKey(bookRequestDTO.ProcessByUserId))
            {
                ModelState.AddModelError("InvalidFK", "Invalid User Process");
                return StatusCode(422, ModelState);
            }
            if (ModelState.IsValid)
            {
                _bookRequestService.Update(bookRequest);
                return Ok(bookRequest);
            }
            return BadRequest(ModelState);
        }

        [HttpDelete("book-request")]
        [ProducesResponseType(200, Type = typeof(BookRequestDTO))]
        public IActionResult DeleteBook(int id)
        {
            var bookRequest = _bookRequestService.GetById(id);
            if (!_bookRequestService.GetAll().Any(b => b.RequestId == bookRequest.RequestId)) return NotFound();
            if (ModelState.IsValid)
            {
                _bookRequestService.Remove(bookRequest);
            }
            return Ok(bookRequest);
        }
    }
}
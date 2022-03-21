using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using MidAssignment.DTO;
using MidAssignment.Entities;
using MidAssignment.Interfaces;

namespace MidAssignment.Controllers
{
    [ApiController]
    [Route("api")]
    [EnableCors]
    public class BookRequestController : ControllerBase
    {
        private readonly IRequestService _bookRequestService;
        private readonly IDetailServices _detailService;
        private readonly IMapper _mapper;

        public BookRequestController(IRequestService bookRequestService, IDetailServices detailServices, IMapper mapper)
        {
            _detailService = detailServices;
            _bookRequestService = bookRequestService;
            _mapper = mapper;
        }

        // [Authorize]
        [HttpPost("book-request")]
        public IActionResult CreateBookRequest(BookRequestWithDetailDTO bookRequestDTO)
        {
            if (bookRequestDTO == null) return BadRequest(ModelState);
            var bookRequest = _mapper.Map<BookRequest>(bookRequestDTO);
            var detail = new BookRequestDetail();
            if (!_bookRequestService.IsValidForeignKey(bookRequest.RequestByUserId))
            {
                ModelState.AddModelError("InvalidFK", "Invalid ForeignKey");
                return StatusCode(422, ModelState);
            }
            if (ModelState.IsValid)
            {
                _bookRequestService.Add(bookRequest);
                foreach (int id in bookRequestDTO.ListBookId)
                {
                    detail.RequestId = bookRequest.RequestId;
                    detail.BookId = id;
                    _detailService.Add(detail);
                }
                return Ok(bookRequest);
            }
            return BadRequest(ModelState);
        }

        //[Authorize]
        [HttpGet("book-request")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<BookRequestWithIdDTO>))]
        public IActionResult GetAllBookRequests()
        {
            var bookRequests = _mapper.Map<List<BookRequestWithIdDTO>>(_bookRequestService.GetAll());
            if (!ModelState.IsValid) return BadRequest(ModelState);
            return Ok(bookRequests);
        }

        //[Authorize]
        [HttpGet("book-request/{id}")]
        public IActionResult GetBookRequestByUserId(int id)
        {
            if (!_bookRequestService.GetAll().Any(b => b.RequestByUserId == id)) return NotFound();
            var bookRequests = _mapper.Map<List<BookRequestWithIdDTO>>(_bookRequestService.GetByUserId(id));
            var details = new List<BookRequestDetailWithBookNameDTO>();
            foreach (var request in bookRequests)
            {
                details.AddRange(_detailService.GetRequestsById(request.RequestId));
            }
            if (!ModelState.IsValid) return BadRequest(ModelState);
            return Ok(new
            {
                Requests = bookRequests,
                Details = details
            });
        }

        [Authorize]
        [HttpPut("book-request")]
        public IActionResult UpdateBookRequest([FromBody] BookRequestChangeStatusDTO bookRequestDTO)
        {
            if (bookRequestDTO == null) return BadRequest(ModelState);
            var bookRequest = _mapper.Map<BookRequest>(bookRequestDTO);
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

        //[Authorize]
        [HttpDelete("book-request")]
        [ProducesResponseType(200, Type = typeof(BookRequestDTO))]
        public IActionResult DeleteBookRequest(int id)
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
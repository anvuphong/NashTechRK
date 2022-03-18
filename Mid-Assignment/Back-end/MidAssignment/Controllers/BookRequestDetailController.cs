using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MidAssignment.DTO;
using MidAssignment.Entities;
using MidAssignment.Interfaces;
using MidAssignment.Services;

namespace MidAssignment.Controllers
{
    [ApiController]
    [Route("api/")]
    public class BookRequestDetailController : ControllerBase
    {
        private readonly IDetailServices _detailService;
        private readonly IMapper _mapper;

        public BookRequestDetailController(IDetailServices detailService, IMapper mapper)
        {
            _detailService = detailService;
            _mapper = mapper;
        }

        [HttpPost("book-request-detail")]
        public IActionResult CreateBookRequestDetail(BookRequestDetailDTO detailDTO)
        {
            if (detailDTO == null) return BadRequest(ModelState);
            var detail = _mapper.Map<BookRequestDetail>(detailDTO);
            if (!_detailService.IsValidForeignKey(detail.BookId))
            {
                ModelState.AddModelError("InvalidFK", "Invalid ForeignKey");
                return StatusCode(422, ModelState);
            }
            if (ModelState.IsValid)
            {
                _detailService.Add(detail);
                return Ok(detail);
            }
            return BadRequest(ModelState);
        }

        [HttpGet("book-request-detail/{id}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<BookRequestWithIdDTO>))]
        public IActionResult GetBookRequestDetailById(int id)
        {
            var details = _mapper.Map<List<BookRequestDetailDTO>>(_detailService.GetRequestsById(id));
            if (!ModelState.IsValid) return BadRequest(ModelState);
            return Ok(details);
        }

        [HttpDelete("book-request-detail")]
        public IActionResult DeleteBookRequestDetail(int id)
        {
            if (_detailService.GetRequestsById(id) == null) return NotFound();
            if (ModelState.IsValid)
            {
                _detailService.Remove(id);
            }
            return Ok();
        }

    }
}
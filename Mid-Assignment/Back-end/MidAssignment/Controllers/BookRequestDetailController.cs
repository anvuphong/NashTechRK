using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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

        //[Authorize]
        [HttpGet("book-request-detail/{id}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<BookRequestDetailWithBookNameDTO>))]
        public IActionResult GetBookRequestDetailById(int id)
        {
            var details = _detailService.GetRequestsById(id);
            if (!ModelState.IsValid) return BadRequest(ModelState);
            return Ok(details);
        }

        [Authorize]
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
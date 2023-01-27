using Project.Dto;
using Project.Interfaces;
using Project.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Project.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MarkController : ControllerBase
    {
        private readonly IMarkInterface _markInterface;
        private readonly IMapper _mapper;

        public MarkController(IMarkInterface markInterface, IMapper mapper)
        {
            _markInterface = markInterface;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(ICollection<Mark>))]
        public async Task<IActionResult> GetMarks()
        {
            var marks = await _markInterface.GetMarks();
            var marksDto = _mapper.Map<IEnumerable<MarkDto>>(marks);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(marksDto);
        }

        [HttpGet("{markId}")]
        [ProducesResponseType(200, Type = typeof(Mark))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetMark(Guid markId)
        {
            if (!await _markInterface.MarkExists(markId))
                return NotFound("Mark with the given Id was not found.");

            var mark = await _markInterface.GetMark(markId);
            var markDto = _mapper.Map<Mark>(mark);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(markDto);
        }

        [HttpPost]
        [ProducesResponseType(200, Type = typeof(Mark))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateMark([FromBody] MarkDto createMark)
        {
            if (createMark == null)
                return BadRequest(ModelState);

            var markMap = _mapper.Map<Mark>(createMark);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!await _markInterface.CreateMark(markMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving the mark.");
                return StatusCode(500, ModelState);
            }

            return Ok(markMap);
        }

        [HttpPut("{markId}")]
        [ProducesResponseType(200, Type = typeof(Mark))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateMark(Guid markId, [FromBody] MarkDto updateMark)
        {
            if (updateMark == null)
                return BadRequest(ModelState);

            if (markId != updateMark.Id)
                return BadRequest(ModelState);

            if (!await _markInterface.MarkExists(markId))
                return NotFound("Mark with the given Id was not found.");

            var markMap = _mapper.Map<Mark>(updateMark);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!await _markInterface.UpdateMark(markMap))
            {
                ModelState.AddModelError("", "Something went wrong while updating the mark.");
                return StatusCode(500, ModelState);
            }

            return Ok(markMap);
        }

        [HttpDelete("{markId}")]
        [ProducesResponseType(200, Type = typeof(Mark))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteMark(Guid markId)
        {
            if (!await _markInterface.MarkExists(markId))
                return NotFound("Mark with the given Id was not found.");

            var mark = await _markInterface.GetMark(markId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!await _markInterface.DeleteMark(mark))
            {
                ModelState.AddModelError("", "Something went wrong while deleting the mark.");
                return StatusCode(500, ModelState);
            }

            return Ok(mark);
        }
    }
}
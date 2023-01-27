using Project.Dto;
using Project.Interfaces;
using Project.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Project.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ModelController : ControllerBase
    {
        private readonly IModelInterface _modelInterface;
        private readonly IMarkInterface _markInterface;
        private readonly IMapper _mapper;

        public ModelController(IModelInterface modelInterface, IMarkInterface markInterface, IMapper mapper)
        {
            _modelInterface = modelInterface;
            _markInterface = markInterface;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(ICollection<Model>))]
        public async Task<IActionResult> GetModels()
        {
            var models = await _modelInterface.GetModels();
            var modelsMap = _mapper.Map<IEnumerable<GetModelDto>>(models);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(modelsMap);
        }

        [HttpGet("{modelId}")]
        [ProducesResponseType(200, Type = typeof(Model))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetModel(Guid modelId)
        {
            if (!await _modelInterface.ModelExists(modelId))
                return NotFound("Model with the given Id was not found.");

            var model = await _modelInterface.GetModel(modelId);
            var modelMap = _mapper.Map<GetModelDto>(model);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(modelMap);
        }

        [HttpPost]
        [ProducesResponseType(200, Type = typeof(Model))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateModel([FromBody] CreateModelDto createModel)
        {
            if (createModel == null)
                return BadRequest(ModelState);

            if (!await _markInterface.MarkExists(createModel.MarkId))
                return NotFound("Mark with the given Id was not found.");

            var modelMap = _mapper.Map<Model>(createModel);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!await _modelInterface.CreateModel(modelMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving the model.");
                return StatusCode(500, ModelState);
            }

            return Ok(modelMap);
        }

        [HttpPut("{modelId}")]
        [ProducesResponseType(200, Type = typeof(Model))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateModel(Guid modelId, [FromBody] CreateModelDto updateModel)
        {
            if (updateModel == null)
                return BadRequest(ModelState);

            if (modelId != updateModel.Id)
                return BadRequest(ModelState);

            if (!await _modelInterface.ModelExists(modelId))
                return NotFound("Model with the given Id was not found.");

            if (!await _markInterface.MarkExists(updateModel.MarkId))
                return NotFound("Mark with the given Id was not found.");

            var modelMap = _mapper.Map<Model>(updateModel);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!await _modelInterface.UpdateModel(modelMap))
            {
                ModelState.AddModelError("", "Something went wrong while updating the model.");
                return StatusCode(500, ModelState);
            }

            return Ok(modelMap);
        }

        [HttpDelete("{modelId}")]
        [ProducesResponseType(200, Type = typeof(Model))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteModel(Guid modelId)
        {
            if (!await _modelInterface.ModelExists(modelId))
                return NotFound("Model with the given Id was not found.");

            var model = await _modelInterface.GetModel(modelId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!await _modelInterface.DeleteModel(model))
            {
                ModelState.AddModelError("", "Something went wrong while deleting the model.");
                return StatusCode(500, ModelState);
            }

            return Ok(model);
        }
    }
}
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserManagementApi.Interface;
using UserManagementApi.Interfaces;

namespace UserManagementApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class GenericController<T> : ControllerBase where T : class, IGenericModel
    {
        private readonly IGenericService<T> _service;

        public GenericController(IGenericService<T> service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<T>>> GetAll()
        {
            var entities = await _service.GetAll();
            return Ok(entities);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<T>> GetById(int id)
        {
            var entity = await _service.GetById(id);
            if (entity == null)
            {
                return NotFound();
            }
            return entity;
        }

        [Authorize(Policy = "ApenasAdministrador")]
        [HttpPost]
        public async Task<ActionResult<T>> Create(T entity)
        {
            await _service.Add(entity);
            return CreatedAtAction(nameof(GetById), new { id = entity.Id }, entity);
        }

        [Authorize(Policy = "ApenasAdministrador")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, T entity)
        {
            if (id != entity.Id)
            {
                return BadRequest();
            }

            await _service.Update(entity);

            return NoContent();
        }

        [Authorize(Policy = "ApenasAdministrador")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var entity = await _service.GetById(id);
            if (entity == null)
            {
                return NotFound();
            }

            await _service.Delete(id);

            return NoContent();
        }
    }

}

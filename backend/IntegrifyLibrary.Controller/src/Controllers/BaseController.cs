using IntegrifyLibrary.Domain;
using IntegrifyLibrary.Business;

using Microsoft.AspNetCore.Mvc;

namespace IntegrifyLibrary.Controllers;

[ApiController]
[Route("api/v1/[controller]s")]

public class BaseController<T, TCreateDto, TGetDto, TUpdateDto> : ControllerBase
{
    protected readonly IBaseService<TCreateDto, TGetDto, TUpdateDto> _service;
    public BaseController(IBaseService<TCreateDto, TGetDto, TUpdateDto> service)
    {
        _service = service;
    }

    [HttpGet]
    [ProducesResponseType(statusCode: 200)]
    [ProducesResponseType(statusCode: 404)]
    public virtual ActionResult<List<TGetDto>> GetAll()
    {
        var items = _service.GetAll();
        if (items == null)
        {
            return NotFound();
        }
        return Ok(items);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(statusCode: 200)]
    [ProducesResponseType(statusCode: 404)]
    public virtual ActionResult<TGetDto> GetOne([FromRoute] Guid id)
    {
        var item = _service.GetOne(id);
        if (item == null)
        {
            return NotFound();
        }
        return Ok(item);
    }

    [HttpPost]
    [ProducesResponseType(statusCode: 201)]
    [ProducesResponseType(statusCode: 400)]
    public virtual ActionResult<TGetDto> CreateOne([FromBody] TCreateDto dto)
    {
        var createdObject = _service.CreateOne(dto);
        return CreatedAtAction(nameof(CreateOne), createdObject);
    }

    [HttpPatch("{id}")]
    [ProducesResponseType(statusCode: 200)]
    [ProducesResponseType(statusCode: 400)]
    [ProducesResponseType(statusCode: 404)]
    public virtual ActionResult<TGetDto> UpdateOne([FromRoute] Guid id, [FromBody] TUpdateDto dto)
    {
        var item = _service.UpdateOne(id, dto);
        if (item == null)
        {
            return NotFound();
        }
        return Ok(item);
    }


    [HttpDelete("{id}")]
    [ProducesResponseType(statusCode: 200)]
    [ProducesResponseType(statusCode: 404)]
    public virtual ActionResult<bool> DeleteOne([FromRoute] Guid id)
    {
        var item = _service.DeleteOne(id);
        if (item == false)
        {
            return NotFound();
        }
        return NoContent();
    }
}
using IntegrifyLibrary.Domain;
using IntegrifyLibrary.Business;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

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
    public virtual async Task<ActionResult<List<TGetDto>>> GetAll([FromQuery] QueryOptions queryOptions)
    {
        var items = await _service.GetAll(queryOptions);
        if (items == null)
        {
            return NotFound();
        }
        return Ok(items);
    }

    [HttpGet("{id:Guid}")]
    [ProducesResponseType(statusCode: 200)]
    [ProducesResponseType(statusCode: 404)]
    public virtual async Task<ActionResult<TGetDto>> GetOne([FromRoute] Guid id)
    {
        try
        {
            var item = await _service.GetOne(id);
            if (item == null)
            {
                return NotFound();
            }
            return Ok(item);
        }
        catch (CustomException e)
        {
            return StatusCode(e.StatusCode, e.ErrorMessage);
        }
    }

    [HttpPost]
    [ProducesResponseType(statusCode: 201)]
    [ProducesResponseType(statusCode: 400)]
    public virtual async Task<ActionResult<TGetDto>> CreateOne([FromBody] TCreateDto dto)
    {
        try
        {
            var createdObject = await _service.CreateOne(dto);
            return CreatedAtAction(nameof(CreateOne), createdObject);
        }
        catch (CustomException e)
        {
            return StatusCode(e.StatusCode, e.ErrorMessage);
        }
    }

    [Authorize(Roles = "Librarian")]
    [HttpPatch("{id:Guid}")]
    [ProducesResponseType(statusCode: 200)]
    [ProducesResponseType(statusCode: 400)]
    [ProducesResponseType(statusCode: 404)]
    public virtual async Task<ActionResult<TGetDto>> UpdateOne([FromRoute] Guid id, [FromBody] TUpdateDto dto)
    {
        try
        {
            await _service.UpdateOne(id, dto);
            var item = await _service.GetOne(id);
            if (item == null)
            {
                return NotFound();
            }
            return Ok(item);
        }
        catch (CustomException e)
        {
            return StatusCode(e.StatusCode, e.ErrorMessage);
        }


    }

    [Authorize(Roles = "Librarian")]
    [HttpDelete("{id}")]
    [ProducesResponseType(statusCode: 200)]
    [ProducesResponseType(statusCode: 404)]
    public virtual async Task<ActionResult<bool>> DeleteOne([FromRoute] Guid id)
    {
        try
        {
            var item = await _service.DeleteOne(id);
            if (item == false)
            {
                return NotFound();
            }
            return NoContent();
        }
        catch (CustomException e)
        {
            return StatusCode(e.StatusCode, e.ErrorMessage);
        }


    }
}
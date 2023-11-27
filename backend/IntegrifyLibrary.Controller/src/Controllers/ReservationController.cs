using IntegrifyLibrary.Business;
using IntegrifyLibrary.Domain;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace IntegrifyLibrary.Controllers;

[ApiController]
public class ReservationController : BaseController<Reservation, CreateReservationDto, ReservationDto, ReservationDto>
{
    private readonly IReservationService _reservationService;
    public ReservationController(IReservationService service) : base(service)
    {
        _reservationService = service;
    }

    [Authorize(Roles = "User")]
    [HttpPost]
    [ProducesResponseType(statusCode: 201)]
    [ProducesResponseType(statusCode: 400)]
    public override async Task<ActionResult<ReservationDto>> CreateOne([FromBody] CreateReservationDto dto)
    {
        var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        var createdObject = await _reservationService.CreateReservation(dto, userId);
        return CreatedAtAction(nameof(CreateOne), createdObject);
    }

    [Authorize(Roles = "User")]
    [HttpGet("own-reservations")]
    [ProducesResponseType(statusCode: 200)]
    [ProducesResponseType(statusCode: 404)]
    public async Task<ActionResult<List<ReservationDto>>> GetOwnReservations()
    {
        var userIdClaim = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        var result = await _reservationService.GetOwnReservations(userIdClaim);
        return Ok(result);
    }

    [Authorize(Roles = "User")]
    [HttpDelete("own-reservations/{id}")]
    [ProducesResponseType(statusCode: 204)]
    [ProducesResponseType(statusCode: 404)]
    public async Task<ActionResult<bool>> DeleteOwnReservation([FromRoute] Guid id)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
        Console.WriteLine(userIdClaim);
        var result = await _reservationService.DeleteOne(id);
        if (result == null)
        {
            return NotFound();
        }
        return NoContent();
    }
}
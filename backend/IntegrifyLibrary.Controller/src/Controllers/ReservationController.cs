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
}
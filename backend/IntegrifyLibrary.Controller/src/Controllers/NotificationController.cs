using IntegrifyLibrary.Domain;
using IntegrifyLibrary.Business;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace IntegrifyLibrary.Controllers;

public class NotificationController : BaseController<Notification, NotificationDto, NotificationDto, NotificationDto> 
{
    private readonly INotificationService _notificationService;
    public NotificationController(INotificationService notificationService) : base(notificationService)
    {
        _notificationService = notificationService;
    }

    [Authorize(Roles = "User, Librarian")]
    [HttpGet("own-notifications")]
    [ProducesResponseType(statusCode: 200)]
    [ProducesResponseType(statusCode: 404)]
    public async Task<ActionResult<List<NotificationDto>>> GetOwnNotifications()
    {
        var userIdClaim = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        var result = await _notificationService.GetOwnNotifications(userIdClaim);
        return Ok(result);
    }

}
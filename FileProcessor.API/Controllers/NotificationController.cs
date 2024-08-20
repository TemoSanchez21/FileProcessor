using FileProcessor.Application.Areas.Notification.Commands.SendEmailNotification;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FileProcessor.API.Controllers;

[ApiController]
[Route("[controller]")]
public class NotificationController(IMediator mediator) : Controller
{

    [HttpPost("email")]
    public async Task<IActionResult> SendEmailNotification([FromBody] SendEmailNotificationCommand command)
    {
        var resultado = await mediator.Send(command);
        return Ok(resultado);
    }
}

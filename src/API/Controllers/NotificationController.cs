using Application.UseCases.Commands.UpdateNotification;
using Application.UseCases.Queries;
using Domain.Common;
using Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace API.Controllers
{
    [ApiController]
    //[Route("[controller]")]
    public class NotificationController : CustomControllerBase
    {
        private readonly IMediator _mediator;
        public NotificationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("get-notifications")]
        public async Task<IActionResult> GetNotifications(
               [FromQuery] Guid userId,
               [FromQuery][DefaultValue(NotificationStatus.Unread)] NotificationStatus status = NotificationStatus.Unread,
               [FromQuery][DefaultValue(1)] int pageNumber = 1,
               [FromQuery][DefaultValue(10)] int pageSize = 10)
        {
            try
            {
                var query = new GetAllNotificationQuery
                {
                    UserId = userId,
                    Status = status,
                    PageNumber = pageNumber,
                    PageSize = pageSize
                };

                var notifications = await _mediator.Send(query);
                return OkResponse(notifications);
            }
            catch (Exception ex)
            {
                return BadRequestResponse("An error occurred while processing your request: " + ex.Message);
            }
        }

        [HttpPost("mark-as-read")]
        public async Task<IActionResult> MarkNotificationAsRead([FromBody] UpdateNotificationCommand command)
        {
            try
            {
                var result = await _mediator.Send(command);
                return OkResponse(result);
            }
            catch (KeyNotFoundException ex)
            {
                return BadRequestResponse(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequestResponse($"An error occurred: {ex.Message}");
            }
        }
    }
}

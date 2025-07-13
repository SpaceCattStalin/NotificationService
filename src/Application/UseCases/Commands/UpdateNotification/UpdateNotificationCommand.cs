using Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Commands.UpdateNotification
{
    public class UpdateNotificationCommand : IRequest<ResponseNotification>
    {
        public Guid Id { get; set; }
        public DateTime ReadAt { get; set; }
    }
}

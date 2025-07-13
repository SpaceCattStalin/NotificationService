using Application.DTOs;
using Domain.Common;
using Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Queries
{
    public class GetAllNotificationQuery : IRequest<PaginatedList<ResponseNotification>>
    {
        public Guid? UserId { get; set; }

        public NotificationStatus? Status { get; set; }

        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}

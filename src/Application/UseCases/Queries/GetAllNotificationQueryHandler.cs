using Application.Common.Interfaces.Repositories;
using Application.Common.Interfaces.UnitOfWork;
using Application.DTOs;
using AutoMapper;
using Domain.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Queries
{
    public class GetAllNotificationQueryHandler : IRequestHandler<GetAllNotificationQuery, PaginatedList<ResponseNotification>>
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly IMapper _mapper;
        public GetAllNotificationQueryHandler(IMapper mapper, INotificationRepository notificationRepository)
        {
            _mapper = mapper;
            _notificationRepository = notificationRepository;
        }

        public async Task<PaginatedList<ResponseNotification>> Handle(GetAllNotificationQuery request, CancellationToken cancellationToken)
        {
            var filter = new NotificationFilter
            {
                Id = request.UserId.ToString(),
                Status = request.Status,
            };

            var allResults = await _notificationRepository.SearchAsync(filter);

            var totalCount = allResults.Count();

            var items = allResults
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToList();

            var responseList = new List<ResponseNotification>();
            foreach (var item in items)
            {
                var response = _mapper.Map<ResponseNotification>(item);
                responseList.Add(response);
            }

            return new PaginatedList<ResponseNotification>(responseList, totalCount, request.PageNumber, request.PageSize);
        }
    }
}

using Application.Common.Interfaces.UnitOfWork;
using Application.DTOs;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Commands
{
    public class CreateNotificationCommandHandler : IRequestHandler<CreateNotificationCommand, ResponseNotification>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CreateNotificationCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<ResponseNotification> Handle(CreateNotificationCommand request, CancellationToken cancellationToken)
        {
            var notificationRepository = _unitOfWork.GetRepository<Notification>();

            var notification = _mapper.Map<Notification>(request);
            notificationRepository.Add(notification);

            try
            {
                await _unitOfWork.SaveAsync();
                return _mapper.Map<ResponseNotification>(notification);
            }
            catch (Exception ex)
            {
                throw new Exception("Có lỗi khi tạo Notification.", ex);
            }
        }
    }
}

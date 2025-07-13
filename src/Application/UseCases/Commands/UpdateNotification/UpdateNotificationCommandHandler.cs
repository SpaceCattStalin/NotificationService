using Application.Common.Interfaces.UnitOfWork;
using Application.DTOs;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Commands.UpdateNotification
{
    public class UpdateNotificationCommandHandler : IRequestHandler<UpdateNotificationCommand, ResponseNotification>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public UpdateNotificationCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<ResponseNotification> Handle(UpdateNotificationCommand request, CancellationToken cancellationToken)
        {
            var notifcationRepository = _unitOfWork.GetRepository<Notification>();

            var notification = await notifcationRepository.GetByIdAsync(request.Id);

            if (notification == null)
            {
                throw new KeyNotFoundException($"Notification with ID {request.Id} not found.");
            }

            notification.ReadAt = request.ReadAt;
            notification.Status = NotificationStatus.Read;
            notifcationRepository.Update(notification);

            try
            {
                await _unitOfWork.SaveAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while updating the notification.", ex);
            }

            return _mapper.Map<ResponseNotification>(notification);
        }
    }
}

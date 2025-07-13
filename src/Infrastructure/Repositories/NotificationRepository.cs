using Application.Common.Interfaces.Repositories;
using Application.DTOs;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class NotificationRepository : GenericRepository<Notification>, INotificationRepository
    {
        public NotificationRepository(ApplicationDbContext context) : base(context)
        {

        }
        public async Task<List<Notification>> SearchAsync(NotificationFilter filter)
        {
            var query = _dbSet.AsQueryable();

            if (!string.IsNullOrEmpty(filter.Id))
            {
                if (Guid.TryParse(filter.Id, out var id))
                {
                    query = query.Where(n => n.UserId == id);
                }
            }


            if (filter.Status.HasValue)
            {
                query = query.Where(n => n.Status == filter.Status.Value);
            }

            return await query.OrderByDescending(n => n.CreatedAt).ToListAsync();
        }
    }
}

using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.SignalR
{
    public class CustomUserIdProvider : IUserIdProvider
    {
        public string? GetUserId(HubConnectionContext connection)
        {
            //return connection.User?.FindFirst("sub")?.Value ?? connection.User?.Identity?.Name;
            return connection.User?.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;
        }
    }
}

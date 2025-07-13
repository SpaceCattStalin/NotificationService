using Microsoft.AspNetCore.Http;
using System.Net;
using System.Text.Json;

namespace NotificationService.Domain.Utils
{
    public class Authentication
    {
        public static async Task HandleForbiddenRequest(HttpContext context)
        {
            int code = (int)HttpStatusCode.Forbidden;
            var errorResponse = new
            {
                data = "An unexpected error occurred.",
                ErrorMessage = "You don't have permission to access this feature",
                statusCode = StatusCodes.Status401Unauthorized,
                code = "Unauthorized!"
            };
            string result = JsonSerializer.Serialize(errorResponse);

            context.Response.ContentType = "application/json";
            context.Response.Headers.Append("Access-Control-Allow-Origin", "*");
            context.Response.StatusCode = code;

            await context.Response.WriteAsync(result);
        }

        public static async Task HandleUnAuthorized(HttpContext context)
        {
            int code = (int)HttpStatusCode.Unauthorized;
            var errorResponse = new
            {
                data = "An unexpected error occurred.",
                ErrorMessage = "vui lòng đăng nhập",
                statusCode = StatusCodes.Status401Unauthorized,
                code = "Unauthorized!"
            };
            string result = JsonSerializer.Serialize(errorResponse);

            context.Response.ContentType = "application/json";
            context.Response.Headers.Append("Access-Control-Allow-Origin", "*");
            context.Response.StatusCode = code;

            await context.Response.WriteAsync(result);
        }

        public static async Task HandleForbiddenRequest(IHttpContextAccessor context)
        {
            int code = (int)HttpStatusCode.Forbidden;
            var errorResponse = new
            {
                data = "An unexpected error occurred.",
                ErrorMessage = "You don't have permission to access this feature",
                statusCode = StatusCodes.Status401Unauthorized,
                code = "Unauthorized!"
            }; string result = JsonSerializer.Serialize(errorResponse);

            context.HttpContext!.Response.ContentType = "application/json";
            context.HttpContext.Response.Headers.Append("Access-Control-Allow-Origin", "*");
            context.HttpContext.Response.StatusCode = code;

            await context.HttpContext.Response.WriteAsync(result);
        }
    }
}

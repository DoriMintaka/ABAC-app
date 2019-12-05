using ABAC.DAL.Entities;
using ABAC.DAL.Services.Contracts;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ABAC.WebApp.Middleware
{
    public class AuthorizationMiddleware
    {
        private readonly RequestDelegate next;
        private IUserService userService;

        public AuthorizationMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context, IUserService service)
        {
            this.userService = service;

            if (!context.Session.IsAvailable && !context.Request.Path.StartsWithSegments("/api/auth"))
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;

            }
            else if (!await IsAuthorized(context.Request, context.Session.GetInt32("userId").GetValueOrDefault(-1)))
            {
                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                await context.Response.WriteAsync("Not enough permissions!");
            }
            else
            {
                await next(context);
            }
        }

        private async Task<bool> IsAuthorized(HttpRequest request, int userId)
        {
            if (request.Path.StartsWithSegments("/api/auth"))
            {
                return true;
            }

            if (request.Path.StartsWithSegments("/api/resources"))
            {
                return userId > 0;
            }

            if (!request.Path.StartsWithSegments("/api/rules") && !request.Path.StartsWithSegments("/api/management"))
            {
                return false;
            }

            try
            {
                var userAttributes = (await userService.GetAttributesAsync(userId)).ToList();
                var user = new User { Attributes = userAttributes };

                return user["role"] == "admin";
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}

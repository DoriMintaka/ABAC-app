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
        private readonly IUserService userService;

        public AuthorizationMiddleware(RequestDelegate next, IUserService userService)
        {
            this.next = next;
            this.userService = userService;
        }

        public async Task Invoke(HttpContext context)
        {
            if (!context.Session.IsAvailable && !context.Request.Path.StartsWithSegments("/api/auth"))
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                
            }
            else if(!await IsAuthorized(context.Request, context.Session.GetInt32("userId").GetValueOrDefault(-1)))
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
            if (request.Path.StartsWithSegments("/api/resources") || request.Path.StartsWithSegments("/api/auth"))
            {
                return true;
            }

            Func<User, Resource, bool> predicate;

            if (request.Path.StartsWithSegments("/api/resources") || request.Path.StartsWithSegments("/api/rules"))
            {
                predicate = (u, r) => u["role"] == "admin";
            }
            else
            {
                predicate = (u, r) => false;
            }

            try
            {
                var attributes = await userService.GetAttributesAsync(userId);
                var user = new User { Id = userId, Attributes = attributes.ToList() };
                return predicate(user, null);
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}

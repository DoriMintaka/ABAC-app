using ABAC.DAL.Context;
using ABAC.DAL.Entities;
using ABAC.DAL.Exceptions;
using ABAC.DAL.Mapping;
using ABAC.DAL.Repositories;
using ABAC.DAL.Repositories.Contracts;
using ABAC.DAL.Services;
using ABAC.DAL.Services.Contracts;
using ABAC.DAL.ViewModels;
using ABAC.WebApp.Middleware;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ABAC.WebApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(o => o.UseSqlServer(Configuration.GetConnectionString("ABAC_Connection")));

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });

            services.AddAutoMapper(typeof(MappingProfile));
            services.AddTransient<IEntityRepository<Resource>, ResourceRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IEntityRepository<Rule>, RuleRepository>();
            services.AddTransient<IService<ResourceInfo>, ResourceService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IRuleService, RuleService>();
            services.AddDistributedMemoryCache();
            services.AddSession(options =>
            {
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
                options.Cookie.Name = ".AbacCookie";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseExceptionHandler(errApp =>
            {
                errApp.Run(async context =>
                {
                    var features = context.Features.Get<IExceptionHandlerPathFeature>();
                    var exception = features.Error;
                    switch (exception)
                    {
                        case AlreadyExistsException e:
                            context.Response.StatusCode = 400;

                            break;
                        case InvalidCredentialsException e:
                            context.Response.StatusCode = 401;

                            break;
                        case ForbiddenException e:
                            context.Response.StatusCode = 403;

                            break;
                        case NotFoundException e:
                            context.Response.StatusCode = 404;

                            break;
                        default:
                            context.Response.StatusCode = 500;

                            break;
                    }

                    context.Response.ContentType = "application/json";
                    var errorMessage = context.Response.StatusCode == 500
                                           ? "Internal server error"
                                           : exception.Message;

                    var responseText = $@"{{
                                                ""errorCode"": {context.Response.StatusCode},
                                                ""errorMessage"": ""{errorMessage}""
                                            }}";

                    await context.Response.WriteAsync(responseText);
                });
            });

            app.UseStaticFiles();
            app.UseSpaStaticFiles();
            app.UseSession();
            app.UseMiddleware<AuthorizationMiddleware>();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });



            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";


                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
        }
    }
}

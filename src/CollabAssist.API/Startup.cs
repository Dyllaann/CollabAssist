using System.Security.Claims;
using System.Threading.Tasks;
using CollabAssist.API.Handlers;
using CollabAssist.Incoming.DevOps;
using CollabAssist.Output.Slack;
using CollabAssist.Services;
using idunno.Authentication.Basic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Formatting.Json;

namespace CollabAssist.API
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .CreateLogger();
            services.AddSingleton(Log.Logger);

            var configSection = _configuration.GetSection("BasicAuth");
            var config = configSection.Get<AuthenticationConfiguration>();
            services.AddSingleton(config);

            var settingsSection = _configuration.GetSection("Settings");
            var settings = configSection.Get<SettingsConfiguration>();
            services.AddSingleton(settings);

            services.AddSingleton<AuthenticationHandler>();
            AddBasicAuthentication(services);

            services.AddSingleton<PullRequestService>();
            services.AddSingleton<BuildService>();

            services.RegisterSlack(_configuration);
            services.RegisterDevOps(_configuration);

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseMiddleware<UnhandeledExceptionMiddleware>();

            app.UseMiddleware<RequestLoggingMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private static IServiceCollection AddBasicAuthentication(IServiceCollection services)
        {
            services.AddAuthentication(BasicAuthenticationDefaults.AuthenticationScheme)
                .AddBasic(options =>
                {
                    options.Realm = "CollabAssist";
                    options.Events = new BasicAuthenticationEvents
                    {
                        OnValidateCredentials = context =>
                        {
                            var validationService = context.HttpContext.RequestServices.GetService<AuthenticationHandler>();
                            if (validationService.IsAuthenticated(context.Username, context.Password))
                            {
                                var claims = new[]
                                {
                                    new Claim(ClaimTypes.NameIdentifier, context.Username, ClaimValueTypes.String, context.Options.ClaimsIssuer),
                                    new Claim(ClaimTypes.Name, context.Username, ClaimValueTypes.String, context.Options.ClaimsIssuer)
                                };

                                context.Principal = new ClaimsPrincipal(new ClaimsIdentity(claims, context.Scheme.Name));
                                context.Success();
                            }
                            return Task.CompletedTask;
                        }
                    };
                });

            return services;
        }

    }
}

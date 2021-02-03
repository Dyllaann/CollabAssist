using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace CollabAssist.External.Slack
{
    public static class SlackRegistration
    {
        public static IServiceCollection RegisterSlack(this IServiceCollection services, IConfiguration configuration)
        {
            var configSection = configuration.GetSection("Slack");
            var config = configSection.Get<SlackConfiguration>();

            services.AddSingleton(config);

            return services;
        }
    }
}

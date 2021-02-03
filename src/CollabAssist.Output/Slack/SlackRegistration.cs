using System;
using System.Collections.Generic;
using System.Text;
using CollabAssist.Output.Slack.Client;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace CollabAssist.Output.Slack
{
    public static class SlackRegistration
    {
        public static IServiceCollection RegisterSlack(this IServiceCollection services, IConfiguration configuration)
        {
            var configSection = configuration.GetSection("Slack");
            var config = configSection.Get<SlackConfiguration>();

            services.AddSingleton(config);
            services.AddHttpClient<ISlackClient, SlackClient>(c => c.BaseAddress = new Uri("https://slack.com/"));
            services.AddSingleton<IOutputHandler, SlackOutputHandler>();

            return services;
        }
    }
}

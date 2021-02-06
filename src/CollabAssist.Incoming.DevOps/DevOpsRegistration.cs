using System;
using System.Collections.Generic;
using System.Text;
using CollabAssist.Incoming.DevOps.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.TeamFoundation.SourceControl.WebApi;
using Microsoft.VisualStudio.Services.Common;
using Microsoft.VisualStudio.Services.WebApi;

namespace CollabAssist.Incoming.DevOps
{
    public static class DevOpsRegistration
    {
        public static IServiceCollection RegisterDevOps(this IServiceCollection services, IConfiguration configuration)
        {
            var configSection = configuration.GetSection("DevOps");
            var config = configSection.Get<DevOpsConfiguration>();

            services.AddSingleton(config);


            var credentials = new VssBasicCredential(string.Empty, config.PersonalAccessToken);
            var baseUri = new Uri(config.BaseUrl);
            var connection = new VssConnection(baseUri, credentials);

            var gitClient = connection.GetClient<GitHttpClient>();

            services.AddSingleton(connection);
            services.AddSingleton(gitClient);

            services.AddSingleton<IDevOpsClient, DevOpsClient>();
            services.AddSingleton<IInputHandler, DevOpsInputHandler>();

            return services;
        }
    }
}

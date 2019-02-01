using MediaBrowser.Controller;
using MediaBrowser.Model.System;
using Microsoft.Extensions.DependencyInjection;

namespace Emby.Server.Implementations
{
    public static class ServerImplementationsModule
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddSingleton<IEnvironmentInfo, EnvironmentInfo.EnvironmentInfo>();
        }
    }
}

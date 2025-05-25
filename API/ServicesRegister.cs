using API.IService;
using API.Service;
using Microsoft.Extensions.DependencyInjection;

namespace API
{
    public static class ServicesRegister
    {
        public static void AddAPI(this IServiceCollection serviceCollection)
        {
            // Service
            //serviceCollection.AddScoped<IInsideOLTService, InsideOLTService>();
            serviceCollection.AddScoped<IDiscordService, DiscordService>();

        }
    }
}

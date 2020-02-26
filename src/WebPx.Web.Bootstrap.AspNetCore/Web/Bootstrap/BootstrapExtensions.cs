using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using WebPx.Web.Bootstrap;
using WebPx.Web.Bootstrap.Configuration;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class BootstrapExtensions
    {
        public static IServiceCollection AddBootstrap(this IServiceCollection services)
        {
            services.ConfigureOptions<BootstrapOptions>();
            services.AddScoped<IBootstrapGenerator, BootstrapGenerator>();
            services.AddScoped<IBootstrap>((svcs) => svcs.GetRequiredService<IBootstrapGenerator>());
            return services;
        }
    }

    public class BootstrapOptions : IConfigureOptions<BootstrapSettings>
    {
        public BootstrapOptions(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void Configure(BootstrapSettings options)
        {
            Configuration.GetSection("Bootstrap").Bind(options);
        }
    }
}

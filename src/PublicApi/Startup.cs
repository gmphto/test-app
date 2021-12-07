using Microsoft.AspNetCore.Mvc;

namespace PublicApi;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfidureServices(IServiceCollection services)
    {
        services.AddApplication();
        services.AddInfrastructure(Configuration);

    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {

    }
}

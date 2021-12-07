using Microsoft.AspNetCore.Identity;
using PublicApi;

namespace Code.WebUI;

public class Program
{
    public async static Task Main(string[] args)

    {
        await CreateHostBuilder(args).Build().RunAsync();

    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
                webBuilder.UseStartup<Startup>());
}
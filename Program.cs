using Retorno;
using Retorno.Services;

using IHost host = Host.CreateDefaultBuilder(args)
    .UseWindowsService(options =>
    {
        options.ServiceName = ".NET Retorno Service";
    })
    .ConfigureServices(services =>
    {
        services.AddHostedService<WindowsBackgroundService>();
        services.AddHttpClient<RetornoService>();
        //services.AddHttpClient<JokeService>();
    })
    .Build();

await host.RunAsync();
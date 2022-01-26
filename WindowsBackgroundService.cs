using Retorno.Services;
using System.Net.Http;

namespace Retorno;

public class WindowsBackgroundService : BackgroundService
{
    // Informações para login
    private readonly string usu = @"admin";
    private readonly string pass = @"admin@01";


    private readonly RetornoService _retornoService;
    private readonly ILogger<WindowsBackgroundService> _logger;

//(RetornoService retornoService,ILogger<Worker> logger) => (_retornoService, _logger) = (retornoService, logger);
    public WindowsBackgroundService(
        RetornoService retornoService,
        ILogger<WindowsBackgroundService> logger) =>
        (_retornoService, _logger) = (retornoService, logger);

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            String resultado = _retornoService.GetRecords("http://187.87.242.13:8083/", usu, pass, 2).Result;
            _logger.LogInformation(resultado);

            resultado = _retornoService.GetRecords("http://187.87.242.13:8084/", usu, pass, 3).Result;
            _logger.LogInformation(resultado);

            resultado = _retornoService.GetRecords("http://187.87.242.13:8085/", usu, pass, 4).Result;
            _logger.LogInformation(resultado);

            await Task.Delay(TimeSpan.FromHours(12), stoppingToken);
        }
    }
}

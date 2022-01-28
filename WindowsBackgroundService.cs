using Retorno.Services;
using System.Net.Http;
using Retorno.lol;

namespace Retorno;

public class WindowsBackgroundService : BackgroundService
{
    // Informações para login
    private readonly string usu = @"admin";
    private readonly string pass = @"admin@01";

    //private Teste _te = new Teste();


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
            /*String resultado = _retornoService.GetRecords("http://187.87.242.13:8083/", usu, pass, 2).Result;
            _logger.LogInformation(resultado);

            resultado = _retornoService.GetRecords("http://187.87.242.13:8084/", usu, pass, 3).Result;
            _logger.LogInformation(resultado);

            resultado = _retornoService.GetRecords("http://187.87.242.13:8085/", usu, pass, 4).Result;
            _logger.LogInformation(resultado);*/

            foreach(var item in Teste.ReadIniFile("Teste/teste.ini")) {
                Console.WriteLine(string.Format("{0}, {1}", item.Key, item.Value));
            }

            await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
        }
    }
}

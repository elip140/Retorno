using Retorno.Services;
using System.Net.Http;
using IniParser;
using IniParser.Model;
using Retorno.Models;

namespace Retorno;

public class WindowsBackgroundService : BackgroundService
{
    


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
            
            
            _retornoService.GetRecords();
           


            await Task.Delay(10000, stoppingToken);
            //TimeSpan.FromMinutes(1);
        }
    }
}

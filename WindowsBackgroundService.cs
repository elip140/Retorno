using Retorno.Services;
using System.Net.Http;
using IniParser;
using IniParser.Model;
using Retorno.Models;

namespace Retorno;

public class WindowsBackgroundService : BackgroundService
{
    // Informações para login
    private readonly string usu = @"admin";
    private readonly string pass = @"admin@01";

    FileIniDataParser parser = new FileIniDataParser();


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
            
            IniData data = parser.ReadFile("config.ini");

            foreach (var s in data.Sections)
            {  
                try
                {
                    Console.WriteLine(s.SectionName);
                    Camera cam = new Camera(data[s.SectionName]["CamID"], data[s.SectionName]["URL"], data[s.SectionName]["Usuario"], data[s.SectionName]["Senha"]);

                    Console.WriteLine(_retornoService.GetRecords(cam).Result);
                }catch(Exception ex)
                {
                    Console.WriteLine("Erro ao enviar dados da Camera: "+s.SectionName+".\n");
                    Console.WriteLine("Erro: "+ex+"\n\n");

                }
                
            }

             /*String ;
            _logger.LogInformation(resultado);

            resultado = _retornoService.GetRecords("http://187.87.242.13:8084/", usu, pass, 3).Result;
            _logger.LogInformation(resultado);

            resultado = _retornoService.GetRecords("http://187.87.242.13:8085/", usu, pass, 4).Result;
            _logger.LogInformation(resultado);*/
           


            await Task.Delay(1000, stoppingToken);
        }
    }
}

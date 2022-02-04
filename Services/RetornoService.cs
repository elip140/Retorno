using System.Net;
using System.Text;
using System.Text.Json;
using Retorno.Models;
using IniParser;
using IniParser.Model;


namespace Retorno.Services;
public class RetornoService
{
    private readonly HttpClient _httpClient;

    FileIniDataParser parser = new FileIniDataParser();


    public RetornoService(HttpClient httpClient) => _httpClient = httpClient;

    public async void GetRecords()
    {
        IniData data = parser.ReadFile("config.ini");

        // Começo e final do dia atual em Unix
        Int32 StartTime = (Int32)(DateTime.Today.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
        Int32 EndTime = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;

        foreach (var s in data.Sections)
        {  
            try
            {
                //Cria uma nova camera com os dados do .ini
                Camera cam = new Camera(data[s.SectionName]["CamID"], data[s.SectionName]["URL"], data[s.SectionName]["Usuario"], data[s.SectionName]["Senha"]);

                
                await cam.GetRecords(StartTime, EndTime);
            }
            catch(Exception ex)
            {
                Logs.ErrorLog("Erro ao enviar dados da Camera: "+s.SectionName+".\nErro: "+ex+"\n\n");
                Console.WriteLine("Erro ao enviar dados da Camera: "+s.SectionName+".\n");
                Console.WriteLine("Erro: "+ex+"\n\n");

            }
        }
    }
}

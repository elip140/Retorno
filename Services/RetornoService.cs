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

                //await cam.GetRecords(StartTime, EndTime);
                //cam.SendAllNewRecords(_httpClient);

                //List<String> List = new List<string>{ "123", "234", "235", "124"};
                //Logs.RecordsLog(1, List);
                //Logs.RecordsLog(2, List);
                //Logs.RecordsLog(3, List);

                /*List<String> Lista = new List<string>();
                Lista = Logs.GetOldRecords(1);
                String teste = "";
                foreach(String r in Lista)
                {
                    teste = " "+r;
                }
                //Console.WriteLine(teste);*/
            }
            catch(Exception ex)
            {
                Logs.ErrorLog("Erro ao enviar dados da Camera: "+s.SectionName+".\nErro: "+ex+"\n\n", "ERRO DE ENVIO");
                Console.WriteLine("Erro ao enviar dados da Camera: "+s.SectionName+".\n");
                Console.WriteLine("Erro: "+ex+"\n\n");

            }
        }
    }
}

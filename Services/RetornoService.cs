using System.Net;
using System.Text;
using System.Text.Json;
using Retorno.Models;


namespace Retorno.Services;
public class RetornoService
{
    private readonly HttpClient _httpClient;


    public RetornoService(HttpClient httpClient) => _httpClient = httpClient;

    public async Task<String> GetRecords(Camera cam)
    {
        var credCache = cam.LoginInfo();

        // Começo e final do dia atual em Unix
        Int32 StartTime = (Int32)(DateTime.Today.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
        Int32 EndTime = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;

        StartTime = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
        EndTime = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;

        // Request para o site
        var httpNClient = new HttpClient(new HttpClientHandler { Credentials = credCache });
        var answer = await httpNClient.GetAsync(new Uri($"{cam.URL}cgi-bin/recordFinder.cgi?action=find&name=AccessControlCardRec&StartTime={StartTime}&EndTime={EndTime}"));
        var res = answer.Content.ReadAsStringAsync().Result;

        // Separando as linhas e descobrindo o número de registros
        String[] linhas = res.Split("\r\n", StringSplitOptions.RemoveEmptyEntries);

        
        switch(linhas.Count()) 
        {
            case 1:
                return "";
            case 2:
                return "Senha invalida";
        }


        var found = linhas[0].Split("=");
        var max = int.Parse(found[1]);

        String Lis = "\n-";
        Lis = Lis+"Camera ID: "+cam.CamID+"\n";

        // Separa cada registro
        for(int i=1; i<max; i++)
        {
            var inicio = res.IndexOf("records["+i+"]");
            var information = "";

            if(i!=(max-1)){
                var end = res.IndexOf("records["+(i+1)+"]");
                information = res.Substring(inicio, (end-inicio));
            }else{
                information = res.Substring(inicio);
            }

            information = information.Replace("records["+i+"].",null);

            var info = information.Split("\r\n", StringSplitOptions.RemoveEmptyEntries);

            //Cria o objeto com as informações e o id da camera     
            ControlCard c = new ControlCard(information, cam.CamID);

            // Manda o JSON e imprime o resultado
            Lis = Lis+c.toJSON()+"\n";
            //c.Send(httpNClient).Result
        }
        Lis = Lis+"-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-\n";

        return Lis;
    }
}

using System.Net;
using System.Text;
using System.Text.Json;
using Retorno.Models;


HttpClient client = new HttpClient();

// Informações para login
const string userName = @"admin";
const string password = @"admin@01";
string domain = "http://187.87.242.13:8084/";

var credCache = new CredentialCache();
credCache.Add(new Uri(domain), "Digest", new NetworkCredential(userName, password));

// Começo e final do dia atual em Unix
Int32 StartTime = (Int32)(DateTime.Today.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
Int32 EndTime = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;

// Request para o site
var httpNClient = new HttpClient(new HttpClientHandler { Credentials = credCache });
var answer = await httpNClient.GetAsync(new Uri($"{domain}cgi-bin/recordFinder.cgi?action=find&name=AccessControlCardRec&StartTime={StartTime}&EndTime={EndTime}"));
var res = answer.Content.ReadAsStringAsync().Result;

// Separando as linhas e descobrindo o número de registros
String[] linhas = res.Split("\r\n", StringSplitOptions.RemoveEmptyEntries);
var found = linhas[0].Split("=");
var max = int.Parse(found[1]);


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
                
    ControlCard c = new ControlCard(information);

    Console.WriteLine(c.Send(httpNClient).Result+"\n");

    //Lista.Add(RecNo = c.RecNo, Response = c.Send(httpNClient));
    //Lista.Add(new Information(){RecNo = c.RecNo, Response = "StatusCode: 69420, ReasonPhrase: 'Teste', Version: 1.1, Content: System.Net.Http.HttpConnectionResponseContent, Headers:\n{\nCache-Control: no-cache"});
    //Console.WriteLine(c.Send(httpNClient).Result);
}





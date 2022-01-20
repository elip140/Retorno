using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net;
using System.Text;
using System.Text.Json;
using Retorno.Models;

namespace Retorno.Pages;

public class IndexModel : PageModel
{
    public HttpClient client = new HttpClient();
    public List<ControlCard> CList = ControlList.GetAll();

    public string response = "";

    private readonly ILogger<IndexModel> _logger;

    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
    }


    public async Task<IActionResult> OnGetAsync()
    {
        const string userName = @"admin";
        const string password = @"admin@01";
        string domain = "http://187.87.242.13:8084/";

        var credCache = new CredentialCache();
        credCache.Add(new Uri(domain), "Digest", new NetworkCredential(userName, password));


        //Int32 StartTime = (Int32)(DateTime.Today.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
        //Int32 EndTime = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
        var StartTime="1640995200";
        var EndTime = "1643673599";

        var httpNClient = new HttpClient(new HttpClientHandler { Credentials = credCache });
        var answer = await httpNClient.GetAsync(new Uri($"{domain}cgi-bin/recordFinder.cgi?action=find&name=AccessControlCardRec&StartTime={StartTime}&EndTime={EndTime}"));
        var res = answer.Content.ReadAsStringAsync().Result;

        String[] linhas = res.Split("\r\n", StringSplitOptions.RemoveEmptyEntries);

        var found = linhas[0].Split("=");
        var max = int.Parse(found[1]);

        for(int i=1; i<max; i++)
        {
            var inicio = res.IndexOf("records["+i+"]");
            var information = "";

            // Separa cada grupo de informações pelo ID
            if(i!=(max-1)){
                var end = res.IndexOf("records["+(i+1)+"]");
                information = res.Substring(inicio, (end-inicio));
            }else{
                information = res.Substring(inicio);
            }

            // Remove "records[i]"
            information = information.Replace("records["+i+"].",null);


            ControlCard c = new ControlCard(information, 3);
            ControlList.Add(c);
                

                //Lista.Add(new Information(){RecNo = c.RecNo, Response = c.Send(httpNClient).Result});
                //Lista.Add(new Information(){RecNo = c.RecNo, Response = "StatusCode: 69420, ReasonPhrase: 'Teste', Version: 1.1, Content: System.Net.Http.HttpConnectionResponseContent, Headers:\n{\nCache-Control: no-cache"});
                //Console.WriteLine(c.Send(httpNClient).Result);
        }

        return Page();
    }

    public async Task<IActionResult> OnPostSendUnit()
    {
        response = "TESTESTTESTESTTESTESTTESTESTTESTESTTESTESTTESTESTTESTESTTESTESTTESTEST";
        return Page();
    }
}
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net;
using System.Text.Json;
using Retorno.Models;
using Retorno.List;

namespace Retorno.Pages;

public class IndexModel : PageModel
{
    public HttpClient client = new HttpClient();

    public String response = "";

    //public List<ControlCard> Lista = ControlList.GetAll();

    private readonly ILogger<IndexModel> _logger;

    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
    }


    public void OnGet()
    {
    
    }
    public async Task<IActionResult> OnPostAsync()
    {
        const string userName = @"admin";
        const string password = @"admin@01";
        string domain = "http://187.87.242.13:8084/";

        var credCache = new CredentialCache();
        credCache.Add(new Uri(domain), "Digest", new NetworkCredential(userName, password));

        var httpNClient = new HttpClient(new HttpClientHandler { Credentials = credCache });
        var answer = await httpNClient.GetAsync(new Uri($"{domain}cgi-bin/recordFinder.cgi?action=find&name=AccessControlCardRec&StartTime=1640995200&EndTime=1643673599"));
        var res = answer.Content.ReadAsStringAsync().Result;

        //Console.WriteLine(res);

        String[] linhas = res.Split("\r\n", StringSplitOptions.RemoveEmptyEntries);

        var found = linhas[0].Split("=");
        var max = int.Parse(found[1]);


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

                string jsonString = JsonSerializer.Serialize(c);

                Console.WriteLine(c.Send(httpNClient));
            }
    
        return Page();
    }

    /*
    const string userName = @"admin";
    const string password = @"admin@01";
    string domain = "http://187.87.242.13:8084/";

    var credCache = new CredentialCache();
    credCache.Add(new Uri(domain), "Digest", new NetworkCredential(userName, password));

    var httpNClient = new HttpClient(new HttpClientHandler { Credentials = credCache });
    var answer = await httpNClient.GetAsync(new Uri($"{domain}cgi-bin/recordFinder.cgi?action=find&name=AccessControlCardRec&StartTime=1640995200&EndTime=1643673599"));
    var allaccess = answer.Content.ReadAsStringAsync().Result
    */
}



        //response = await client.GetStringAsync("http://187.87.242.13:8084/cgi-bin/recordFinder.cgi?action=find&name=AccessControlCardRec&StartTime=1640995200&EndTime=1643673599");
        

        /*Usu = Request.Form["Usuario"];
        Pass = Request.Form["Senha"];
        var formContent = new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string, string>("userName", Request.Form["Usuario"]),
            new KeyValuePair<string, string>("password", Request.Form["Senha"]),
        });*/
    /*
    const string userName = @"admin";
    const string password = @"admin@01";
    string domain = "http://187.87.242.13:8084/";

    var credCache = new CredentialCache();
    credCache.Add(new Uri(domain), "Digest", new NetworkCredential(userName, password));

    var httpNClient = new HttpClient(new HttpClientHandler { Credentials = credCache });
    var answer = await httpNClient.GetAsync(new Uri($"{domain}cgi-bin/recordFinder.cgi?action=find&name=AccessControlCardRec&StartTime=1640995200&EndTime=1643673599"));
    var allaccess = answer.Content.ReadAsStringAsync().Result
    */

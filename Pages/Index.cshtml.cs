using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net;

namespace Retorno.Pages;

public class IndexModel : PageModel
{
    public HttpClient client = new HttpClient();

    public String response = "";

    public String Usu = "";
    
    public String Pass = "";

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
        //response = await client.GetStringAsync("http://187.87.242.13:8084/cgi-bin/recordFinder.cgi?action=find&name=AccessControlCardRec&StartTime=1640995200&EndTime=1643673599");
        

        /*Usu = Request.Form["Usuario"];
        Pass = Request.Form["Senha"];
        var formContent = new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string, string>("userName", Request.Form["Usuario"]),
            new KeyValuePair<string, string>("password", Request.Form["Senha"]),
        });*/

    const string userName = @"admin";
    const string password = @"admin@01";
    string domain = "http://187.87.242.13:8084/";

    var credCache = new CredentialCache();
    credCache.Add(new Uri(domain), "Digest", new NetworkCredential(userName, password));

    var httpNClient = new HttpClient(new HttpClientHandler { Credentials = credCache });
    var answer = await httpNClient.GetAsync(new Uri($"{domain}cgi-bin/recordFinder.cgi?action=find&name=AccessControlCardRec&StartTime=1640995200&EndTime=1643673599"));
    response = answer.Content.ReadAsStringAsync().Result;

    
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

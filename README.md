using System.Net;
using System.Text;
using System.Text.Json;
using Retorno.Models;


HttpClient client = new HttpClient();

// Informações para login
const string usu = @"admin";
const string pass = @"admin@01";

/*
porta 8083 ColetorID=2
porta 8084 ColetorID=3
porta 8085 ColetorID=4
*/

List<String> Resultados = Mandar("http://187.87.242.13:8083/", usu, pass, 2).Result;
foreach(String res in Resultados)
    Console.WriteLine(res);

Resultados = Mandar("http://187.87.242.13:8084/", usu, pass, 3).Result;
foreach(String res in Resultados)
    Console.WriteLine(res);

Resultados = Mandar("http://187.87.242.13:8085/", usu, pass, 4).Result;
foreach(String res in Resultados)
    Console.WriteLine(res);



async Task<List<String>> Mandar(String domain, String userName, String password, int CamId)
{
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

    List<String> Lista = new List<String>();
    Lista.Add("Camera ID: "+CamId+"\n");

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
        ControlCard c = new ControlCard(information, CamId);

        // Manda o JSON e imprime o resultado
        Lista.Add(c.Send(httpNClient).Result+"\n");
    }
    Lista.Add("-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-\n");

    return Lista;
}






using System.Text;
using System.Text.Json;

namespace Retorno.Models
{
    public class ControlCard
    {
        //public int AccessControlCardRecID { get; set; }
        public String AttendanceState { get; set; }
        public String CardName { get; set; }
        public String CardNo { get; set; }
        public String CardType { get; set; }
        public String CreateTime { get; set; }
        public String Door { get; set; }
        public String ErrorCode { get; set; }
        public String Mask { get; set; }
        public String Method { get; set; }
        public String Notes { get; set; }
        public String Password { get; set; }
        public String ReaderID { get; set; }
        public String RecNo { get; set; }
        public String RemainingTimes { get; set; }
        public String ReservedInt { get; set; }
        public String ReservedString { get; set; }
        public String RoomNumber { get; set; }
        public String Status { get; set; }
        public String Type { get; set; }
        public String URL { get; set; }
        public String UserID { get; set; }
        public String UserType { get; set; }
        public int ColetorID { get; set; }
        public int MovimentacaoPessoaID { get; set; }
        private DateTime CData { get; set; }
        public String Data {get; set;}

        public ControlCard(String res, int CamID){
            var info = res.Split("\r\n", StringSplitOptions.RemoveEmptyEntries);

            AttendanceState = info[0].Substring(info[0].IndexOf("=")+1);
            CardName = info[1].Substring(info[1].IndexOf("=")+1);
            CardNo = info[2].Substring(info[2].IndexOf("=")+1);
            CardType = info[3].Substring(info[3].IndexOf("=")+1);
            CreateTime = info[4].Substring(info[4].IndexOf("=")+1);
            Door = info[5].Substring(info[5].IndexOf("=")+1);
            ErrorCode = info[6].Substring(info[6].IndexOf("=")+1);
            Mask = info[7].Substring(info[7].IndexOf("=")+1);
            Method = info[8].Substring(info[8].IndexOf("=")+1);
            Notes = info[9].Substring(info[9].IndexOf("=")+1);
            Password = info[10].Substring(info[10].IndexOf("=")+1);
            ReaderID = info[11].Substring(info[11].IndexOf("=")+1);
            RecNo = info[12].Substring(info[12].IndexOf("=")+1);
            RemainingTimes = info[13].Substring(info[13].IndexOf("=")+1);
            ReservedInt = info[14].Substring(info[14].IndexOf("=")+1);
            ReservedString = info[15].Substring(info[15].IndexOf("=")+1);
            RoomNumber = info[16].Substring(info[16].IndexOf("=")+1);
            Status = info[17].Substring(info[17].IndexOf("=")+1);
            Type = info[18].Substring(info[18].IndexOf("=")+1);
            URL = info[19].Substring(info[19].IndexOf("=")+1);
            UserID = info[20].Substring(info[20].IndexOf("=")+1);
            UserType = info[21].Substring(info[21].IndexOf("=")+1);


            CData = DateTime.Now;
            Data = CData.ToString("yyyy-MM-ddTHH:mm:ss.fffffffK");

            ColetorID = CamID;
            MovimentacaoPessoaID = 0;
        }

        public string toJSON()
        {
            return JsonSerializer.Serialize(this);
        }


        public async Task<String> Send(HttpClient client)
        {
            var content = new StringContent(toJSON(), Encoding.UTF8, "application/json");
            var response = await client.PostAsync(new Uri($"https://www.adsportal.com.br/DirectCondo/api/AccessControlCardRecs/PostAccessControlCardRec"), content);

            return response.ToString();
        }
    }
}
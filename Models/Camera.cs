using System.Net;

namespace Retorno.Models
{
    public class Camera
    {
        public int CamID {get; set;}
        public String URL {get; set;}
        private String Usuario {get; set;}
        private String Senha {get; set;}

        private List<String> OldRecords {get; set;}

        private List<ControlCard> NewRecords {get; set;}


        public Camera(String _camID, String _url, String _usuario, String _senha)
        {
            CamID = Int32.Parse(_camID);
            URL = _url;

            if(String.IsNullOrEmpty(_usuario))
                Usuario = @"admin";
            else
                Usuario = _usuario;

            if(String.IsNullOrEmpty(_senha))
                Senha = @"admin@01";
            else
                Senha = _senha;

            NewRecords = new List<ControlCard>();

            OldRecords.DefaultIfEmpty("");
            
        }

        public async Task<String> GetRecords(Int32 StartTime, Int32 EndTime)
        {
            var credCache = new CredentialCache();
            credCache.Add(new Uri(URL), "Digest", new NetworkCredential(Usuario, Senha));

            var httpNClient = new HttpClient(new HttpClientHandler { Credentials = credCache });
            var answer = await httpNClient.GetAsync(new Uri($"{URL}cgi-bin/recordFinder.cgi?action=find&name=AccessControlCardRec&StartTime={StartTime}&EndTime={EndTime}"));
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
            Lis = Lis+"Camera ID: "+CamID+"\n";

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
                AddNewRec(new ControlCard(information));
            }

            return Lis;
        }

        public void AddNewRec(ControlCard c)
        {
            c.ColetorID = CamID;
            NewRecords.Add(c);
        }

        public void SendAllNewRecords()
        {
            
            
        }

        public Boolean CheckRecord(String RecNo)
        {
            var r = OldRecords.FirstOrDefault(r => r==RecNo);

            if(String.IsNullOrEmpty(r)){
                return true;
            }
            return false;
        }
        
    }
    
}

/*
porta 8083 ColetorID=2
porta 8084 ColetorID=3
porta 8085 ColetorID=4
*/
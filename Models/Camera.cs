using System.Net;

namespace Retorno.Models
{
    public class Camera
    {
        public int CamID {get; set;}
        public String URL {get; set;}
        private String Usuario {get; set;}
        private String Senha {get; set;}

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
        }

        public CredentialCache LoginInfo()
        {
            var credCache = new CredentialCache();
            credCache.Add(new Uri(URL), "Digest", new NetworkCredential(Usuario, Senha));
            return credCache;
        }
        
    }
    
}

/*
porta 8083 ColetorID=2
porta 8084 ColetorID=3
porta 8085 ColetorID=4
*/

namespace Retorno.Models
{
    public class Camera
    {
        public int CamID {get; set;}
        public String Porta {get; set;}
        public String URL {get; set;}
        private String Usuario {get; set;}
        private String Senha {get; set;}

        Camera()
        {
            Usuario = $"admin";
            Senha = $"admin@01";
        }
    }
}
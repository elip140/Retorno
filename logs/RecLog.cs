using Newtonsoft.Json;
namespace Retorno
{
    public class RecLog
    {
        [JsonProperty(PropertyName = "RecNo")]
        public String RecNo{get; set;}

        [JsonProperty(PropertyName = "ColetorID")]
        public int ColetorID {get; set;}

        public RecLog(String _recNo, int _coletorID)
        {
            RecNo = _recNo;
            ColetorID = _coletorID;
        }
    }
}
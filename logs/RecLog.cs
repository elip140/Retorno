
namespace Retorno
{
    public class RecLog
    {
        public String RecNo{get; set;}
        public int ColetorID {get; set;}

        public RecLog(String _recNo, int _coletorID)
        {
            RecNo = _recNo;
            ColetorID = _coletorID;
        }
    }
}
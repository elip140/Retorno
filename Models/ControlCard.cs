using System.Text;
using System.Text.Json;

namespace Retorno.Models
{
    public class ControlCard
    {
        //public int AccessControlCardRecID { get; set; }
        //public String CamID {get; set;}
        private int Id {get; set;}

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

        private int SendState {get; set;}

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

        public void SetId(int NewId)
        {
            Id = NewId;
        }
        public int GetId()
        {
            return Id;
        }


        public async Task<String> Send(HttpClient client)
        {
            string jsonString = JsonSerializer.Serialize(this);

            var content = new StringContent(jsonString, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(new Uri($"https://www.adsportal.com.br/DirectCondo/api/AccessControlCardRecs/PostAccessControlCardRec"), content);

            return response.ToString();

            //return jsonString;
        }
    }
}

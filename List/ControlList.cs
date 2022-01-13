using System.IO;
using System;
using Retorno.Models;

namespace Retorno.List
{
    public class ControlList
    {
        private List<ControlCard> Lista;
        private static int nextID = 0;

        public ControlList(String r)
        {
            
            String[] linhas = r.Split("\r\n", StringSplitOptions.RemoveEmptyEntries);

            var found = linhas[0].Split("=");
            var max = int.Parse(found[1]);


            for(int i=1; i<max; i++)
            {
                var inicio = r.IndexOf("records["+i+"]");
                var information = "";

                if(i!=(max-1)){
                    var end = r.IndexOf("records["+(i+1)+"]");
                    information = r.Substring(inicio, (end-inicio));
                }else{
                    information = r.Substring(inicio);
                }

                information = information.Replace("records["+i+"].",null);

                var info = information.Split("\r\n", StringSplitOptions.RemoveEmptyEntries);

                ControlCard c = new ControlCard();

                c.AttendanceState = info[0].Substring(info[0].IndexOf("=")+1);
                c.CardName = info[1].Substring(info[1].IndexOf("=")+1);
                c.CardNo = info[2].Substring(info[2].IndexOf("=")+1);
                c.CardType = info[3].Substring(info[3].IndexOf("=")+1);
                c.CreateTime = info[4].Substring(info[4].IndexOf("=")+1);
                c.Door = info[5].Substring(info[5].IndexOf("=")+1);
                c.ErrorCode = info[6].Substring(info[6].IndexOf("=")+1);
                c.Mask = info[7].Substring(info[7].IndexOf("=")+1);
                c.Method = info[8].Substring(info[8].IndexOf("=")+1);
                c.Notes = info[9].Substring(info[9].IndexOf("=")+1);
                c.Password = info[10].Substring(info[10].IndexOf("=")+1);
                c.ReaderID = info[11].Substring(info[11].IndexOf("=")+1);
                c.RecNo = info[12].Substring(info[12].IndexOf("=")+1);
                c.RemainingTimes = info[13].Substring(info[13].IndexOf("=")+1);
                c.ReservedInt = info[14].Substring(info[14].IndexOf("=")+1);
                c.ReservedString = info[15].Substring(info[15].IndexOf("=")+1);
                c.RoomNumber = info[16].Substring(info[16].IndexOf("=")+1);
                c.Status = info[17].Substring(info[17].IndexOf("=")+1);
                c.Type = info[18].Substring(info[18].IndexOf("=")+1);
                c.URL = info[19].Substring(info[19].IndexOf("=")+1);
                c.UserID = info[20].Substring(info[20].IndexOf("=")+1);
                c.UserType = info[21].Substring(info[21].IndexOf("=")+1);

                Console.WriteLine(String.IsNullOrEmpty(c.UserID));
                try
                {
                    Add(c);
                }
                catch (System.Exception)
                {
                    Console.WriteLine("Erro"+c.Id);
                    throw;
                }
                
            }
        }

        public List<ControlCard> GetAll() => Lista;

        public ControlCard Get(int id) => Lista.FirstOrDefault(p => p.Id == id);

        public void Add(ControlCard c)
        {
            c.Id = nextID++;
            Lista.Add(c);
        }

        public void Delete(int id)
        {
            var c = Get(id);
            if (c is null)
                return;

            Lista.Remove(c);
        }

        public void Update(ControlCard Control)
        {
            var index = Lista.FindIndex(p => p.Id == Control.Id);
            if (index == -1)
                return;

            Lista[index] = Control;
        }
    }
}
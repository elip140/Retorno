using System.IO;
using System;
using System.Regex;

namespace Retorno.Models
{
    public class ControlList
    {
        private static List<ControlCard> Lista{get; }
        private static int nextID = 0;

        public ControlList(String r)
        {
            
            String[] linhas = r.Split("\r\n", StringSplitOptions.RemoveEmptyEntries);

            var found = linhas[0].Split("=");
            var max = int.Parse(found[1]);

            String[] cac = new String[max];

            for(int i=1; i<max; i++)
            {
                var inicio = r.IndexOf("records["+i+"]");

                if(i!=(max-1)){
                    var end = r.IndexOf("records["+(i+1)+"]");
                    cac[i] = r.Substring(inicio, (end-inicio));
                }else{
                    cac[i] = r.Substring(inicio);
                }

                using(Regex regex = new Regex()){
                    cac[i] = regex.Replace(cac[i], "records["+(i+1)+"]");
                }
                

                Console.WriteLine(cac[i]);

                ControlCard c = new ControlCard();


            }
        }

        public static List<ControlCard> GetAll() => Lista;

        public static ControlCard Get(int id) => Lista.FirstOrDefault(p => p.Id == id);

        public static void Add(ControlCard c)
        {
            c.Id = nextID++;
            Lista.Add(c);
        }

        public static void Delete(int id)
        {
            var c = Get(id);
            if (c is null)
                return;

            Lista.Remove(c);
        }

        public static void Update(ControlCard c)
        {
            var index = Lista.FindIndex(p => p.Id == c.Id);
            if (index == -1)
                return;

            Lista[index] = c;
        }
    }
}
using System.IO;
using System;

namespace Retorno.Models
{
    public class ControlList
    {
        private static List<ControlCard> Lista{get; }
        private static int nextID = 0;

        public ControlList(String r)
        {
            String[] linhas = r.Split("\r\n");

            var found = linhas[0].Split("=");
            var max = int.Parse(found[1]);

            String[] records;

            for(int i=0; i<=max; i++)
            {
                
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
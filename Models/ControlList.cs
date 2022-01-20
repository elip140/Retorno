using System.IO;
using System;


namespace Retorno.Models
{
    public static class ControlList
    {
        private static List<ControlCard> Lista{get; }

        private static int nextID = 1;

        static ControlList()
        {
            Lista = new List<ControlCard>();
        }

        public static List<ControlCard> GetAll() => Lista;

        public static ControlCard Get(int id) => Lista.FirstOrDefault(c => c.GetId() == id);

        public static void Add(ControlCard c)
        {
            var i = Lista.FindIndex(p => p.RecNo == c.RecNo);

            if(i==-1){
                c.SetId(nextID);
                nextID++;
                Lista.Add(c);
            }
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
            var index = Lista.FindIndex(p => p.GetId == c.GetId);
            if (index == -1)
                return;

            Lista[index] = c;
        }
    }
}


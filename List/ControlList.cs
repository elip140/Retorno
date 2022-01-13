using System.IO;
using System;
using Retorno.Models;

namespace Retorno.List
{
    public static class ControlList
    {/*
        private static List<ControlCard> Lista{get; }
        private static int nextID = 0;

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

        public static void Update(ControlCard Control)
        {
            var index = Lista.FindIndex(p => p.Id == Control.Id);
            if (index == -1)
                return;

            Lista[index] = Control;
        }*/
    }
}
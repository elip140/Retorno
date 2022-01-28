/*using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;*/
namespace Retorno.lol {

    public static class Teste
    {
        public static Dictionary < string, string > ReadIniFile(string fileName) {
            return File.ReadLines(fileName).Select(s => s.Split('=')).Select(s => new {
                key = s[0], value = string.Join("=", s.Select((o, n) => new {
                    n,
                    o
                }).Where(o => o.n > 0).Select(o => o.o))
            }).ToDictionary(o => o.key, o => o.value);
        }
    }
}
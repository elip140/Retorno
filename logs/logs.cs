using System.IO;
//using System.Text.Json;
using Newtonsoft.Json;
namespace Retorno;

static class Logs
{
    private static String CreateLogsPath = @"logs\logs.txt";
    private static String ErrorLogsPath = @"logs\error.txt";
    private static String RecordsPath = @"logs\records.json";

    public static void ErrorLog(String message, String categoria)
    {
        // Cria um log de Erro com horario no erro.txt
        try
        {
            Console.WriteLine(message);

            StreamWriter errorlog = new StreamWriter(ErrorLogsPath, true);
            errorlog.WriteLine("["+categoria+"] ["+DateTime.Now+"] "+message);
            errorlog.Close();
        }
        catch(Exception e)
        {
            Console.WriteLine("Erro ao criar log de erro: " + e.Message);
        }
    }

    public static void CreateLog(String message, String categoria)
    {
        // Cria um log com o horario no lods.txt
        try
        {
            StreamWriter createlog = new StreamWriter(CreateLogsPath, true);
            
            createlog.WriteLine("["+categoria+"] ["+DateTime.Now+"] "+message);
            
            createlog.Close();
        }
        catch(Exception e)
        {
            Console.WriteLine("Erro ao criar log: " + e.Message, "ERRO");
        }
    }

    public static void RecordsLog(List<RecLog> NewRecords)
    {
        // Salva no records.json os novos records
        try
        {
            StreamReader reader = new StreamReader(RecordsPath);
            var json = reader.ReadToEnd();
            reader.Close();

            List<RecLog> OldRecords = new List<RecLog>();
            var teste = JsonConvert.DeserializeObject<List<RecLog>>(json);


            if(teste != null && teste.Count()!=0){
                OldRecords.AddRange(teste);
            }


            if(NewRecords != null && NewRecords.Count()!=0)
            {
                foreach(RecLog r in NewRecords)
                {
                    if(!(OldRecords.Contains(r))){
                        OldRecords.Add(r);
                    }
                }
            }


            string jsonString = JsonConvert.SerializeObject(OldRecords);

            StreamWriter writer = new StreamWriter(RecordsPath);
            writer.WriteLine(jsonString);
            writer.Close();
        }
        catch(Exception e)
        {
            ErrorLog("Erro ao atualizar registro no records.json: " + e.Message, "ERRO");
        }
        
    }
    

    public static List<RecLog>? GetOldRecords(int CamId)
    {// Pega todos os Registros do records.json e retorna uma lista com os que tem o mesmo ID da camera
        List<RecLog>? OldRecs = new List<RecLog>();

        try
        {
            StreamReader reader = new StreamReader(RecordsPath);

            OldRecs = JsonConvert.DeserializeObject<List<RecLog>>(reader.ReadToEnd());
            reader.Close();

            if(OldRecs==null || OldRecs.Count()==0)
                return null;
            

            List<RecLog> Recs = new List<RecLog>();
            foreach(RecLog item in OldRecs)
            {
                if(item.ColetorID==CamId)
                {
                    Recs.Add(item);
                }
            }

            return Recs;
        }
        catch(Exception e)
        {
            ErrorLog("Erro ao acessar as informações do records.txt: " + e.Message, "ERRO");
        }
        
        return OldRecs;
    }
}
using System.IO;
using System.Text.Json;
using 

namespace Retorno;

static class Logs
{
    private static String CreateLogsPath = @"logs\logs.txt";
    private static String ErrorLogsPath = @"logs\error.txt";
    private static String RecordsPath = @"logs\records.json";

    public static void ErrorLog(String message, String categoria)
    {
        try
        {
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
        try
        {
            StreamWriter createlog = new StreamWriter(CreateLogsPath, true);
            
            createlog.WriteLine("["+categoria+"] ["+DateTime.Now+"] "+message);
            
            createlog.Close();
        }
        catch(Exception e)
        {
            ErrorLog("Erro ao criar log: " + e.Message, "ERRO");
            Console.WriteLine("Erro ao criar log: " + e.Message);
        }
    }

    public static void RecordsLog(int CamID, List<RecLog> NewRecords)
    {
        try
        {
            StreamReader reader = new StreamReader(RecordsPath);

            var json = reader.ReadToEnd();

            List<RecLog> items = JsonSerializer.Deserialize<RecLog>(json);

            foreach(var j in items)
            {

            }
        }
        catch(Exception e)
        {
            ErrorLog("Erro ao atualizar registro no records.txt: " + e.Message, "ERRO");
            Console.WriteLine("Erro ao atualizar registro no records.txt: " + e.Message);
        }
        
    }
    
    public static List<String> GetOldRecords(int CamId)
    {
        List<String> OldRecs = new List<string>();

        try
        {
            

        }
        catch(Exception e)
        {
            ErrorLog("Erro ao acessar as informações do records.txt: " + e.Message, "ERRO");
            Console.WriteLine("Erro ao acessar as informações do records.txt: " + e.Message);
        }
        
        return OldRecs;
    }
}






/*
public static void RecordsLog(int CamID, List<String> NewRecords)
    {
        try
        {
            StreamReader reader = new StreamReader(RecordsPath);
            String records = reader.ReadToEnd();

            int start = records.IndexOf("}["+CamID+"](");
            int end = records.IndexOf(")", start);

            start = start+("}["+CamID+"]").Length;

            String num = records.Substring(start, end-start);
            
            Console.WriteLine(("}["+CamID+"]").Length);
            reader.Close();

            using (StreamWriter recLog = new StreamWriter(RecordsPath))
            {
                foreach(String r in NewRecords)
                {
                    records = records.Replace("}["+CamID+"]", r+", }["+CamID+"]");
                }

                recLog.Write(records);

                recLog.Close();
            }
        }
        catch(Exception e)
        {
            ErrorLog("Erro ao atualizar registro no records.txt: " + e.Message, "ERRO");
            Console.WriteLine("Erro ao atualizar registro no records.txt: " + e.Message);
        }
        
    }
    
    public static List<String> GetOldRecords(int CamId)
    {
        List<String> OldRecs = new List<string>();

        try
        {
            StreamReader reader = new StreamReader(RecordsPath);
            String input = reader.ReadToEnd();
            reader.Close();

            int start = input.IndexOf("["+CamId+"]{");

            if(start>=0)
            {
                int end = input.IndexOf(", }["+CamId+"]");

                int i = 0;

                
                while(start<end || i<=5){
                    //Console.WriteLine(start);
                    int endRec = input.IndexOf(", ", start+2);
                    OldRecs.Add(input.Substring(start, (endRec-start-2)));

                    //Console.WriteLine(endRec-start);
                    start = endRec;
                    

                    i++;
                }
                
            }
            else
            {
                StreamWriter recLog = new StreamWriter(RecordsPath, true);
                recLog.WriteLine("");
                recLog.WriteLine("["+CamId+"]{ }["+CamId+"](1)");
                recLog.Close();
            }

        }
        catch(Exception e)
        {
            ErrorLog("Erro ao acessar as informações do records.txt: " + e.Message, "ERRO");
            Console.WriteLine("Erro ao acessar as informações do records.txt: " + e.Message);
        }
        
        return OldRecs;
    }





*/
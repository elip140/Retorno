using System.IO;

namespace Retorno;

static class Logs
{
    private static String CreateLogsPath = @"logs\logs.txt";
    private static String ErrorLogsPath = @"logs\error.txt";

    public static void ErrorLog(String message)
    {
        try
        {
            StreamWriter errorlog = new StreamWriter(ErrorLogsPath, true);
            
            errorlog.WriteLine(message);
            
            errorlog.Close();
        }
        catch(Exception e)
        {
            Console.WriteLine("Erro ao criar log de erro: " + e.Message);
        }
    }

    public static void CreateLog(String message)
    {
        try
        {
            StreamWriter createlog = new StreamWriter(CreateLogsPath, true);
            
            createlog.WriteLine(message);
            
            createlog.Close();
        }
        catch(Exception e)
        {
            ErrorLog("Erro ao criar log: " + e.Message);
            Console.WriteLine("Erro ao criar log: " + e.Message);
        }
    }
}
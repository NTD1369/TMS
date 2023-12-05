using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeSheet.Intergration
{
    public class LogUtils
    {
        public static void WriteToFile(string Message)
        {
            Message = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + ": " + Message;
            Console.WriteLine(Message);
            string path = AppDomain.CurrentDomain.BaseDirectory + "\\Logs\\Auto\\";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string filepath = Path.Combine(path, "ServiceLog_" + DateTime.Now.ToString("yyyyMMdd") + ".txt");
            if (!File.Exists(filepath))
            {
                // Create a file to write to. 
                using (StreamWriter sw = File.CreateText(filepath))
                {
                    sw.WriteLine(Message);
                }
            }
            else
            {
                using (StreamWriter sw = File.AppendText(filepath))
                {
                    sw.WriteLine(Message);
                }
            }
        }
    }
}

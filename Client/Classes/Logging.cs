using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using static System.IO.Directory;
using static System.Console;

namespace Client.Classes
{
    public static class Logging
    {
        public static void WriteMessagelessLog(string entry, bool networklogging = false, string networkinfo = null, string logtype = "Scorched")
        {
            if (!Exists("Logs")) { CreateDirectory("Logs"); }
            string dir = "Logs/" + logtype + " " + DateTime.Now.ToString("yyyMMdd") + ".txt";
            string final = "[" + DateTime.Now.ToString("HH:mm:ss") + "] - " + entry;
            if (networklogging) { final += " " + networkinfo; }
            using (StreamWriter logFile = File.AppendText(dir))
            {
                logFile.WriteLine(final);
                logFile.Flush();
            }
        }

        public static void WriteLoglessMessage(string entry, bool addtime = true)
        {
            string final;
            if (addtime) { final = "[" + DateTime.Now.ToString("HHmmss") + "] - " + entry; }
            else { final = entry; }
            WriteLine(final);
        }

        public static void WriteMessageLog(string entry, bool networklogging = false, string networkinfo = null, string logtype = "Scorched")
        {
            if (!Exists("Logs")) { CreateDirectory("Logs"); }
            string dir = "Logs/" + logtype + " " + DateTime.Now.ToString("yyyMMdd") + ".txt";
            string final = "[" + DateTime.Now.ToString("HH:mm:ss") + "] - " + entry;
            if (networklogging) { final += " " + networkinfo; }
            using (StreamWriter logFile = File.AppendText(dir))
            {
                logFile.WriteLine(final);
                logFile.Flush();
            }
            WriteLine(final);
        }
    }
}

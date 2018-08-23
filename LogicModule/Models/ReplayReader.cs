using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using LogicModule.Interfaces;

namespace LogicModule.Models
{
    public static class ReplayReader
    {
        public static void DeleteReplay(string replayPath)
        {
            if (File.Exists(replayPath)) File.Delete(replayPath);
        }

        public static string ReadReplay(string path)
        {
            if (path != null)
                if (File.Exists(path))
                    return File.ReadAllText(path);
            return null;
        }

        public static string ReadReplay(string path, int lines)
        {
            
            if (path != null)
            {
                if (!File.Exists(path)) return null;
                string text = null;
                try
                {
                    using (StreamReader fs = new StreamReader(path))
                    {
                        for (int i = 0; i < lines; i++)
                        {
                            string temp = fs.ReadLine();
                            if (temp == null) break;
                            text += temp;
                        }
                    }

                    return text;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    return null;
                }

            }

            return null;
        }

        public static void ReplaceReplay(string replayPath, string destinationPath)
        {
            if (replayPath != null && File.Exists(replayPath))
            {
                File.Copy(replayPath, destinationPath, true);
            }
        }
    }
}

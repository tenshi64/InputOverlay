using System;
using System.Text.RegularExpressions;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using System.Reflection;

namespace InputOverlay
{
    static internal class FileManagement
    {
        private static readonly string path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Input Overlay\config.ini";
        static public void CreateFile()
        {
            if(!File.Exists(path))
            {
                var f = File.Create(path);
                f.Close();
                DefaultContent();
            }
        }

        static public void DefaultContent()
        {
            if (File.Exists(path))
            {
                string text = "X: 2" + System.Environment.NewLine + "Y: 545" + System.Environment.NewLine + "TopMost: true";

                File.WriteAllText(path, text);
            }
            else
            {
                CreateFile();
            }
        }

        static public string ReadFile()
        {
            if (File.Exists(path))
            {
                return File.ReadAllText(path);
            }
            else
            {
                CreateFile();
                return ReadFile();
            }
        }

        static public string[] ReadAllLinesFile()
        {
            if (File.Exists(path))
            {
                return File.ReadAllLines(path);
            }
            else
            {
                CreateFile();
                return ReadAllLinesFile();
            }
        }

        static public string GetValue(string text)
        {
            for (int i = 0; i < ReadAllLinesFile().Count(); i++)
            {
                if (ReadAllLinesFile()[i].Contains(text))
                {
                    if(text != "TopMost:")
                    {
                        return Regex.Match(ReadAllLinesFile()[i], @"\d+").Value;
                    }
                    else
                    {
                        if(ReadAllLinesFile()[i].ToLower().Contains("true"))
                        {
                            return "true";
                        }
                        else if (ReadAllLinesFile()[i].ToLower().Contains("false"))
                        {
                            return "false";
                        }
                    }
                }
            }
            return "";
        }

        static public void SaveTextInFile(string text, string value)
        {
            bool exists = false;
            int index = 0;
            for(int i = 0; i < ReadAllLinesFile().Count(); i++)
            {
                if (ReadAllLinesFile()[i].Contains(text))
                {
                    exists = true;
                    index = i;
                }
            }

            if(!exists)
            {
                File.WriteAllText(path, text);
            }
            else
            {
                text = ReadFile().Replace(ReadAllLinesFile()[index], text + value);
                File.WriteAllText(path, text);
            }
        }

        static public void CreateDirectory()
        {
            if(!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Input Overlay"))
            {
                Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Input Overlay");
            }
        }
    }
}

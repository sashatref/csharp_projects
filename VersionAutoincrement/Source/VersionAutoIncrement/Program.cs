using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using System.Reflection;

namespace VersionAutoIncrement
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            if(Environment.GetCommandLineArgs().Length == 2)
            {
                incBuild(System.Environment.GetCommandLineArgs()[1]);
            }
            else
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Form1());
            }
        }

        static public void incBuild(string fileName)
        {
            string result = "";
            int major_ver = 1;
            int minor_ver = 0;
            int maintanse = 0;
            int build = 0;

            if (File.Exists(fileName))
            {
                try
                {
                    result = File.ReadAllText(fileName);

                    string[] lines = result.Split('\n');

                    for (int i = 0; i < lines.Length; i++)
                    {
                        string[] subLine = lines[i].Split(' ');

                        if (subLine.Length == 3)
                        {
                            switch (subLine[1])
                            {
                                case "MAJOR_VERSION":
                                    major_ver = Convert.ToInt32(subLine[2]);
                                    break;
                                case "MINOR_VERSION":
                                    minor_ver = Convert.ToInt32(subLine[2]);
                                    break;
                                case "MAINTENANCE":
                                    maintanse = Convert.ToInt32(subLine[2]);
                                    break;
                                case "BUILD":
                                    build = Convert.ToInt32(subLine[2]);
                                    break;
                            }
                        }
                    }
                }
                catch
                {

                }
            }

            build++;

            string textToSave = "#define STRINGIZE2(x) #x" +
                                "\n#define STRINGIZE(x) STRINGIZE2(x)" +
                                "\n#define MAJOR_VERSION " + major_ver.ToString() +
                                "\n#define MINOR_VERSION " + minor_ver.ToString() +
                                "\n#define MAINTENANCE " + maintanse.ToString() +
                                "\n#define BUILD " + build.ToString() +
                                "\n\n#define FULL_VER STRINGIZE(MAJOR_VERSION) \".\" STRINGIZE(MINOR_VERSION) \".\" STRINGIZE(MAINTENANCE) \".\" STRINGIZE(BUILD)" + 
                                "\n\n#define PRODUCT_VER STRINGIZE(MAJOR_VERSION) \".\" STRINGIZE(MINOR_VERSION) \".\" STRINGIZE(MAINTENANCE)" +
                                "\n";
            
            File.WriteAllText(fileName, textToSave);

            return;
        }
    }
}

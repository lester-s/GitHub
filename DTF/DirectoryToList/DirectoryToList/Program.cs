using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectoryToList
{
    class Program
    {
        static string localFolder;
        static System.IO.StreamWriter output;
        static int index = 0;
        static void Main(string[] args)
        {
            //args = new string[1];
            var path = "";
            //args[0] = @"C:\Users\LE_STER\Desktop\test";
            if (args.Length != 0)
            {
                Console.WriteLine("Chemin d'accès du dossier à exporter");
                Console.WriteLine(args[0].ToString());
                path = args[0].ToString();
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("Chemin d'accès du dossier à exporter");
                path = Console.ReadLine();
                Console.WriteLine(path);
                Console.ReadLine();
            }
            

            localFolder = Environment.CurrentDirectory;
            output = new System.IO.StreamWriter(localFolder+"\\list.txt");
            output.WriteLine("\r\n******** Root ******** \r\n");
            Program.SeekFiles(path);
            
            Program.SeekDirectories(path);
            output.Close();

        }
        private static void SeekFiles(string path)
        {
            var files = Directory.GetFiles(path);
            foreach (var file in files)
            {
                Program.index++;
                var tab = file.Split('\\');

                output.WriteLine(Program.index + "-" + tab[tab.Length - 1]);

            }
        }

        private static void SeekDirectories(string path)
        {
            var directories = Directory.GetDirectories(path);

            foreach (var dir in directories)
            {
                var tab = dir.Split('\\');
                output.WriteLine("");
                output.WriteLine("******** " + tab[tab.Length - 1] + " ********");
                Program.SeekFiles(dir);
                Program.SeekDirectories(dir);
            }
        }
    }
}

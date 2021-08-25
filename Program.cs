using FileArchiver.utils;
using log4net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FileArchiver
{
    class Program
    {
        private static readonly ILog log
            = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        static void Main(string[] args)
        {
            Config cfg;
            string sJson = File.ReadAllText(@"config\cfg.json");
            cfg = JsonConvert.DeserializeObject<Config>(sJson);
            string menu = "**********************************************************\n" +
                          "**---------------   Elija una opción:  -----------------**\n" +
                          "**[------ 1.- Mover archivos de origen a destino  -----]**\n" +
                          "**[------ 2.- Eliminar los archivos del destino   -----]**\n" +
                          "**[------------ Escriba \"quit\" para salir.   ----------]**\n" +
                          "**********************************************************";
            Console.WriteLine(menu);
            string option = Console.ReadLine();
            while(!option.Equals("quit"))
            {
                switch (option)
                {
                    case "1":
                        Console.WriteLine("Se buscarán archivos para mover");
                        log.Debug("Se buscarán archivos para mover");
                        int op = Convert.ToInt32(option);
                        List<string> files = getFiles(cfg.source, op);
                        if (files.Count > 0)
                            processFiles(files, op, cfg.testMode, cfg.destiny);
                        else
                            Console.WriteLine("No se encontraron archivos que cumplan con los parametros establecidos.");
                        break;
                    case "2":
                        Console.WriteLine("Se buscarán archivos para eliminar");
                        log.Debug("Se buscarán archivos para eliminar");
                        op = Convert.ToInt32(option);
                        files = getFiles(cfg.destiny, op);
                        if (files.Count > 0)
                            processFiles(files, op, cfg.testMode);
                        else
                            Console.WriteLine("No se encontraron archivos que cumplan con los parametros establecidos.");
                        break;
                    default:
                        Console.WriteLine("Opción inválida");
                        break;
                }                
                Console.WriteLine(menu);
                option = Console.ReadLine();
                if (option.Equals("quit"))
                {
                    Console.WriteLine("Cerrando...");
                    Thread.Sleep(3000);
                }
            }
        }

        private static List<string> getFiles(string source, int option)
        {
            List<string> files = new List<string>();
            FileArchiver archiver = new FileArchiver(source, option, files);
            Task task = new Task(archiver.listDirectories);
            task.Start();
            Task.WaitAll(task);
            return files;
        }
        
        private static void processFiles(List<string> files, int option, bool mode, string dest = "")
        {
            Console.WriteLine(string.Format("Se {1} {0} archivos. Por favor espere...", files.Count, option == 1 ? "moverán" : "eliminarán"));
            log.Info(string.Format("Se {1} {0} archivos", files.Count, option == 1 ? "moverán" : "eliminarán"));
            int count = 0;
            foreach (string file in files)
            {
                if (option == 1)
                {
                    string result = mode ? "correctamente" : FileManager.moveFile(@"" + file, dest + @"\" + Util.getRecordName(file));
                    log.Debug(result);
                    if (result.Contains("correctamente"))
                        count++;
                }
                else
                {
                    string result = mode ? "correctamente" : FileManager.deleteFile(file);
                    log.Debug(result);
                    if (result.Contains("correctamente"))
                        count++;
                }
            }
            Console.WriteLine(string.Format("Se {1} {0} archivos.", count, option == 1 ? "movieron" : "eliminaron"));
            log.Info(string.Format("Se {1} {0} archivos.", count, option == 1 ? "movieron" : "eliminaron"));
        }
    }
}

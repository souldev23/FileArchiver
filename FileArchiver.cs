using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FileArchiver.utils;
using log4net;

namespace FileArchiver
{
    class FileArchiver
    {
        private static readonly ILog log
            = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        string source;
        int op;
        List<string> directories;
        List<string> list;
        public FileArchiver(string source, int op, List<string> list)
        {
            this.source = source;
            directories = new List<string>();
            this.op = op;
            this.list = list;
        }

        public void listDirectories()
        {
            browseDirectories(source);            
        }

        private void browseDirectories(string path)
        {
            browseFiles(path);
            if(FileManager.hasDirectories(path))
            {
                directories = FileManager.toList(FileManager.getDirectories(path));
                foreach(string dir in directories)
                {
                    FileArchiver archiver = new FileArchiver(dir, op, list);
                    Task task = new Task(archiver.listDirectories);
                    task.Start();
                    Task.WaitAll(task);
                }
            }
            
        }

        private void browseFiles(string path)
        {
            log.Info(path);
            Console.WriteLine(path);
            if (FileManager.hasFiles(path))
            {
                string[] files = FileManager.getFiles(path);
                foreach (string f in files)
                {
                    string name = Util.getRecordName(f);
                    
                    if(Validator.isValidDate(name) && !Validator.isException(name))
                    {
                        Console.WriteLine(string.Format("Se {1} el archivo {0}", name, op == 1 ? "moverá" : "eliminará"));
                        log.Debug(string.Format("Se {1} el archivo {0}", name, op == 1 ? "moverá" : "eliminará"));
                        list.Add(f);
                    }
                }
            }
        }        
    }
}

using System;
using System.Collections.Generic;
using System.IO;

namespace FileArchiver
{
    public class FileManager
    {
        public static string moveFile(string path, string dest)
        {
            try
            {
                if (File.Exists(path))
                {
                    File.Move(path, dest);
                    return string.Format("{0} se movió correctamente a {1}", path, dest);
                }
                else
                    return "No se encontró el archivo " + path;
            }
            catch(Exception e)
            {
                if(e.Message.Contains("Este archivo ya existe") || e.Message.Contains("This file already exists"))
                {
                    if (deleteFile(path).Contains("correctamente"))
                    {
                        return string.Format("{0} se eliminó correctamente, porque ya existía", path);
                    }
                    else
                    {
                        return string.Format("No se pudo mover, ni eliminar el archivo {0} que ya exitía en el destino.\nError: {1}", path, e.Message);
                    }
                }
                return string.Format("No se pudo mover el archivo {0}.\nError: {1}", path, e.Message);
            }
        }

        public static string deleteFile(string path)
        {
            try
            {
                if (File.Exists(path))
                {
                    File.Delete(path);
                    return string.Format("{0} se eliminó correctamente", path);
                }
                else
                    return "No se encontró el archivo " + path;
            }
            catch (Exception e)
            {
                return string.Format("No se pudo eliminar el archivo {0}.\nError: {}", path, e.Message);
            }
        }

        public static bool hasDirectories(string path)
        {
            try
            {
                return Directory.GetDirectories(path).Length > 0 ? true : false;
            }
            catch(Exception e)
            {
                return false;
            }
        }

        public static bool hasFiles(string path)
        {
            try
            {
                return Directory.GetFiles(path).Length > 0 ? true : false;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public static string[] getFiles(string path)
        {
            try
            {
                string[] files = Directory.GetFiles(path);
                return files;
            }
            catch(Exception e)
            {
                return null;
            }
        }

        public static string[] getDirectories(string path)
        {
            try
            {
                string[] directories = Directory.GetDirectories(path);
                return directories;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public static List<string> toList(string[] array)
        {
            List<string> list = new List<string>();
            for (int i = 0; i < array.Length; i++)
            {
                list.Add(array[i]);
            }
            return list;
        }
    }
}

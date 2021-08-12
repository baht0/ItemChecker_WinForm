using System.Text.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Diagnostics;
using System.Threading;

namespace ItemChecker.Updater
{
    class Program
    {
        static string path = AppDomain.CurrentDomain.BaseDirectory + @"\update";
        static void Main(string[] args)
        {
            try
            {
                if (!args.Any())
                    return;
                Console.WriteLine("Startup: success...");

                do
                    Thread.Sleep(1000);
                while (Process.GetProcessesByName("chromedriver").Any() | Process.GetProcessesByName("ItemChecker").Any());

                if (Directory.Exists(path))
                {
                    DirectoryInfo dirInfo = new(path);
                    dirInfo.Delete(true);
                }
                Directory.CreateDirectory(path);

                List<string> files = new();
                files.Add("ItemChecker.exe");
                files.Add("ItemChecker.dll");
                if (args[1] == "True")
                    files.Add("ItemChecker.Net.dll");
                if (args[2] == "True")
                    files.Add("ItemChecker.Support.dll");
                if (args[3] == "True")
                    files.Add("Newtonsoft.Json.dll");
                if (args[4] == "True")
                    files.Add("WebDriver.dll");
                if (args[5] == "True")
                    files.Add("WebDriver.Support.dll");
                if (args[6] == "True")
                    files.Add("chromedriver.exe");

                Console.WriteLine("Preparation: success");
                Console.WriteLine("\nDownloading...");
                Console.WriteLine("============================");
                foreach (string file in files)
                {
                    string link = TemporaryLinkDropbox(file);
                    DownloadFile(link, file);
                    Console.WriteLine($"{file}: done");
                }
                Console.WriteLine("============================");
                Console.WriteLine("Download: success");

                Console.WriteLine("\nUpdating...");
                foreach (string file in files)
                {
                    string newPath = AppDomain.CurrentDomain.BaseDirectory + @"\" + file;
                    File.Move($"{path}\\{file}", newPath, true);
                }
                Console.WriteLine("Update complete: success");
                Console.WriteLine("\nPress any key to close...");
            }
            catch (Exception exp)
            {
                Console.WriteLine("\nSomething went wrong :(");
                Console.WriteLine("***\n" + exp.Message + "\n***");
            }
            finally
            {
                if (Directory.Exists(path))
                {
                    DirectoryInfo dirInfo = new(path);
                    dirInfo.Delete(true);
                }
                Console.ReadKey();
            }
        }
        public static String TemporaryLinkDropbox(string file)
        {
            HttpWebRequest httpRequest = (HttpWebRequest)WebRequest.Create("https://api.dropboxapi.com/2/files/get_temporary_link");

            httpRequest.Method = "POST";
            httpRequest.ContentType = "application/json";
            httpRequest.Headers["Authorization"] = "Bearer a94CSH6hwyUAAAAAAAAAAf3zRyhyZknI9J8KM3VZihWEILAuv6Vr3ht_-4RQcJxs";
            var data = "{\"path\": \"/ItemChecker/" + file + "\"}";

            using (var streamWriter = new StreamWriter(httpRequest.GetRequestStream()))
            {
                streamWriter.Write(data);
            }

            var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                string json = streamReader.ReadToEnd();
                JsonDocument jDoc = JsonDocument.Parse(json);
                JsonElement root = jDoc.RootElement;
                string link = root.GetProperty("link").ToString();
                return link;
            }
        }
        public static void DownloadFile(string link, string fileName)
        {
            using (WebClient client = new WebClient())
            {
                client.DownloadFile(link, path + $"\\{fileName}");
            }
        }
    }
}

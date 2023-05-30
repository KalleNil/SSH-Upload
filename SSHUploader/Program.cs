using Renci.SshNet;
using System;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        string host = "ifkn";
        string username = "ifkn";
        string password = "ifkn";
        int port = 1337;

        string localDirectory = "path_to_local_directory";
        string remoteDirectory = "path_to_remote_directory";

        using (var client = new SftpClient(host, port, username, password))
        {
            client.Connect();

            string[] jsonFiles = Directory.GetFiles(localDirectory, "*.json");

            foreach (var file in jsonFiles)
            {
                string fileName = Path.GetFileName(file);

                using (var fileStream = new FileStream(file, FileMode.Open))
                {
                    client.UploadFile(fileStream, Path.Combine(remoteDirectory, fileName));
                    Console.WriteLine($"File '{fileName}' uploaded successfully.");
                }
            }

            client.Disconnect();
        }
    }
}
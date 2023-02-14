using System.Diagnostics;
using System.Text;

namespace cryptoSoftEasySave
{
    internal class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 4)
            {
                //Console.WriteLine("Usage : cryptoSoftEasySave.exe -c/d fichier_source fichier_destination key");
                Environment.ExitCode = -1;
            }

            string mode = args[0];
            string sourceFile = args[1];
            string destFile = args[2];
            byte[] key = Encoding.UTF8.GetBytes($"{args[3]}");
            int blockSize = 4096;
            try
            {
                if (mode == "-c")
                {
                    DateTime start = DateTime.Now;
                    using (var inputStream = File.OpenRead(sourceFile))
                    using (var outputStream = File.OpenWrite(destFile))
                    {
                        byte[] buffer = new byte[blockSize];
                        int bytesRead;
                        long totalBytesRead = 0;

                        while ((bytesRead = inputStream.Read(buffer, 0, blockSize)) > 0)
                        {
                            for (int i = 0; i < bytesRead; i++)
                            {
                                buffer[i] = (byte)(buffer[i] ^ key[totalBytesRead % key.Length]);
                                totalBytesRead++;
                            }

                            outputStream.Write(buffer, 0, bytesRead);
                        }
                    }
                    TimeSpan duration = DateTime.Now - start;
                    Environment.ExitCode = (int)duration.TotalMilliseconds;
                }
                else if (mode == "-d")
                {
                    DateTime start = DateTime.Now;
                    using (var inputStream = File.OpenRead(sourceFile))
                    using (var outputStream = File.OpenWrite(destFile))
                    {
                        byte[] buffer = new byte[blockSize];
                        int bytesRead;
                        long totalBytesRead = 0;

                        while ((bytesRead = inputStream.Read(buffer, 0, blockSize)) > 0)
                        {
                            for (int i = 0; i < bytesRead; i++)
                            {
                                buffer[i] = (byte)(buffer[i] ^ key[totalBytesRead % key.Length]);
                                totalBytesRead++;
                            }

                            outputStream.Write(buffer, 0, bytesRead);
                        }
                    }
                    TimeSpan duration = DateTime.Now - start;
                    Environment.ExitCode = (int)duration.TotalMilliseconds;
                }
                else
                {
                    Environment.ExitCode = -1;
                }

            }
            catch (Exception ex)
            {
                Environment.ExitCode = -1;
            }
        }
    }
}
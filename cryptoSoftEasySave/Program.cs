using System.Diagnostics;

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
            string key = args[3]; // clé de chiffrement de 64 bits

            try
            {
                byte[] source = File.ReadAllBytes(sourceFile);
                byte[] dest = new byte[source.Length];

                if (mode == "-c")
                {
                    DateTime start = DateTime.Now;
                    for (int i = 0; i < source.Length; i++)
                    {
                        dest[i] = (byte)(source[i] ^ key[i % key.Length]);
                    }

                    File.WriteAllBytes(destFile, dest);
                    TimeSpan duration = DateTime.Now - start;
                    Environment.ExitCode = (int)duration.TotalMilliseconds;
                }
                else if (mode == "-d")
                {
                    DateTime start = DateTime.Now;
                    for (int i = 0; i < source.Length; i++)
                    {
                        dest[i] = (byte)(source[i] ^ key[i % key.Length]);
                    }
                    File.WriteAllBytes(destFile, dest);
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
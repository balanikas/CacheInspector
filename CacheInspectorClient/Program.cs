using System;
using System.IO;
using System.IO.Pipes;

namespace CacheInspectorClient
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var client = new NamedPipeClientStream("CacheInspectorNamedPipeName"))
            {
                client.Connect();
                using (var reader = new StreamReader(client))
                {
                    var writer = new StreamWriter(client);

                    while (true)
                    {
                        var input = Console.ReadLine();
                        if (string.IsNullOrEmpty(input))
                        {
                            Console.WriteLine("exit? (y/n)");
                            var answer = Console.ReadLine() ?? "n";
                            if (answer.Equals("y", StringComparison.InvariantCultureIgnoreCase))
                            {
                                break;
                            }
                            continue;
                        }

                        if (input.Equals("cls", StringComparison.InvariantCultureIgnoreCase))
                        {
                            Console.Clear();
                            continue;
                        }

                        writer.WriteLine(input);
                        writer.Flush();

                        string response;
                        while ((response = reader.ReadLine()) != "SYNC")
                        {
                            Console.WriteLine(response);
                        }
                    }

                    writer.Dispose();
                }
                  
            }
            
        }
    }
}

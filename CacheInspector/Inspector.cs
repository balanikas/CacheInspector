using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CacheInspector
{
    public sealed class Inspector
    {
        private static readonly Lazy<Inspector> _lazy = new Lazy<Inspector>(() => new Inspector());

        public static Inspector Instance => _lazy.Value;
        internal readonly CacheConversion CacheConversion = new CacheConversion();
        private Inspector()
        {
            var pipeClient = new Process
            {
                StartInfo =
                {
                    FileName = @"C:\dev\commerce\Sources\CacheInspector\bin\Debug\CacheInspectorClient.exe",
                    UseShellExecute = true
                }
            };

            pipeClient.Start();

            Task.Factory.StartNew(() =>
            {
                using (var server = new NamedPipeServerStream("CacheInspectorNamedPipeName"))
                {
                    server.WaitForConnection();
                    var reader = new StreamReader(server);
                    var writer = new StreamWriter(server);

                    while (true)
                    {
                        var line = reader.ReadLine();
                        if (string.IsNullOrEmpty(line))
                        {
                            continue;
                        }
                        writer.WriteLine(CommandRunner.RunCommand(CommandParser.ParseCommand(line)));
                        writer.WriteLine("SYNC");
                        writer.Flush();
                    }
                }
                
            });
        }

        public void AddConversion(string cacheKey, Func<object, object> conversionFunc)
        {
            CacheConversion.Add(cacheKey, conversionFunc);
        }

    }
}

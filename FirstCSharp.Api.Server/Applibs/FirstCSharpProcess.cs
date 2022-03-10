using NLog;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace FirstCSharp.Api.Server.Applibs
{
    internal static class FirstCSharpProcess
    {
        private static ILogger logger = LogManager.GetLogger("FirstCSharp.Api.Server");

        public static void ProcessStart()
        {
            logger.Info("FirstCSharp.Api.Server Application_Start");
            var container = AutofacConfig.Container;

            Task.Run(() =>
            {
                while (!SpinWait.SpinUntil(() => false, 1000))
                {
                    Console.Clear();
                    Console.WriteLine($"Current Memory Usage:{(int)((GC.GetTotalMemory(true) / 1024f))}(KB)");
                    Console.WriteLine($"Process Memory Usage:{(int)((Process.GetCurrentProcess().PrivateMemorySize64 / 1024f))}(KB)");
                    Console.WriteLine($"Handle count:{Process.GetCurrentProcess().HandleCount}");
                    Console.WriteLine($"Thread count:{Process.GetCurrentProcess().Threads.Count}");
                }
            });

            logger.Info("FirstCSharp.Api.Server Started");
        }

        public static void ProcessStop()
        {
            logger.Info("FirstCSharp.Api.Server Ended");
        }
    }
}

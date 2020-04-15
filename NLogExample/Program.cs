using System;
using NLog;

namespace NLogExample
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            // create NLog configuration
            var config = new NLog.Config.LoggingConfiguration();

            // define targets
            var logfile = new NLog.Targets.FileTarget("logfile") { FileName = "log_file.txt" };
            var logconsole = new NLog.Targets.ConsoleTarget("logconsole");

            // specify minimum log level to maximum log level and target (console, file, etc.)
            config.AddRule(LogLevel.Trace, LogLevel.Fatal, logconsole);
            config.AddRule(LogLevel.Info, LogLevel.Fatal, logfile);

            // apply NLog configuration
            NLog.LogManager.Configuration = config;

            // create instance of LogManager
            var logger = NLog.LogManager.GetCurrentClassLogger();

            // log sample messages
            logger.Trace("Sample trace message");
            logger.Debug("Sample debug message");
            logger.Info("Sample informational message");
            logger.Warn("Sample warning message");
            logger.Error("Sample error message");
            logger.Fatal("Sample fatal error message");

            // NLog supports structured messages
            var fruit = new[] { "bananas", "apples", "pears" };
            logger.Info("I like to eat {Fruit}", fruit);

            // Example of logging an exception
            try
            {
                int x = 10;
                int y = 0;
                Console.WriteLine(x / y);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }
        }
    }
}

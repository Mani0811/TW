using Merchant_s_Guide_to_the_Galaxy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NLog;
using System;
using System.Collections;

namespace ConferenceTrackManagement
{
    class Program
    {
        public static Logger logger = LogManager.GetCurrentClassLogger();
        static void Main(string[] args)
        {
            try
            {
                var config = new ConfigurationBuilder()
                  .SetBasePath(System.IO.Directory.GetCurrentDirectory())
                  .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                  .Build();
                Utility.Println("Conference Track Management ! please provide input below and a blank new line to finish input");
                Conference conference = new ConferenceScheduler().Schedule();
                Utility.Println(conference.ToString());
                Console.Read();
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Stopped program because of exception");
            }
        }
    }
}

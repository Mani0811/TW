using Microsoft.Extensions.Logging;
using NLog;
using System;
using System.Collections;

namespace Merchant_s_Guide_to_the_Galaxy
{
    class Program
    {
        public static Logger logger = LogManager.GetCurrentClassLogger();
        static void Main(string[] args)
        {
            try
            {
                Utility.println("GalaxyMerchant ! please provide input below and a blank new line to finish input");

                // Initialize a new paragraph
                Paragraph paragraph = new Paragraph();

                // Read the input from console, validate and process
                ArrayList output = paragraph.Read();

                for (int i = 0; i < output.Count; i++)
                {
                    Utility.println((string)output[i]);
                }
            }
            catch ( Exception ex)
            {
                logger.Error(ex, "Stopped program because of exception");
            }
        }
    }
}

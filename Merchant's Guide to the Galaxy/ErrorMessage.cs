using Microsoft.Extensions.Logging;
using NLog;
using System;
using System.Collections.Generic;
using System.Text;

namespace Merchant_s_Guide_to_the_Galaxy
{
    public class ErrorMessage
    {
        public static Logger logger = LogManager.GetCurrentClassLogger();
        public ErrorMessage()
        {

        }

        /**
         * This method prints the message for the particular error code
         */
        public void PrintMessage(ErrorCodes error)
        {
            string message = GetMessage(error);
            logger.Info(message);
            if (message != null)
                Utility.println(message);

        }

        public string GetMessage(ErrorCodes error)
        {
           string message = null;

            switch (error)
            {
                case ErrorCodes.NO_INPUT:
                    message = "No input was specified ! Program exited";
                    break;
                case ErrorCodes.INVALID: message = "Input format is wrong ! input discarded";
                    break;

                case ErrorCodes.INVALID_ROMAN_CHARACTER: message = "Illegal character specified in roman number ! input discarded";
                    break;

                case ErrorCodes.INVALID_ROMAN_STRING: message = "wrong Roman number, voilated roman number format"; break;

                case ErrorCodes.INCORRECT_LINE_TYPE: message = "Exception caused during processing due to incorrect line type supplied"; break;

                case ErrorCodes.NO_IDEA: message = "I have no idea what you are talking about"; break;

                default: break;
            }
            logger.Error(message);
            return message;
        }
    }

}

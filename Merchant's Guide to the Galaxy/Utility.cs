using System;
using System.Collections.Generic;
using System.Text;

namespace Merchant_s_Guide_to_the_Galaxy
{
    class Utility
    {
        /**
	 * <p>This method prints the String message passed as an parameter.<br>
	 * The parameter must be string</p>
	 * @param msg String
	 * @see #println(String)
	 */
        public static void print(String msg)
        {
            Console.Write(msg);
        }

        /**
         * <p>This method prints the String message passed as an parameter with a new line.<br>
         * The parameter must be string</p>
         * @param msg String 
         * @see #print(String)
         */
        public static void println(String msg)
        {
            Console.WriteLine(msg);
        }
    }
}

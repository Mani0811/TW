using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Merchant_s_Guide_to_the_Galaxy
{
    public class RomanNumbers
    {

        ////public static final ErrorMessage eMessage = new ErrorMessage();

        /**
         * This regex string will validate whether roman number entered is valid or not
         */
        public static String romanNumberValidator = "^M{0,4}(CM|CD|D?C{0,3})(XC|XL|L?X{0,3})(IX|IV|V?I{0,3})$";

        enum Roman
        {
            I = 1,
            V = 5,
            X = 10,
            L = 50,
            C = 100,
            D = 500,
            M = 1000

            
        }



        private static int getValueFromRomanChar(char romanChar)
        {
            int value = -1;

            switch (romanChar)
            {
                case 'I':
                    value = (int)Roman.I;
                    break;
                case 'V':
                    value = (int)Roman.V;
                    break;
                case 'X':
                    value = (int)Roman.X;
                    break;
                case 'L':
                    value = (int)Roman.L;
                    break;

                case 'C':
                    value = (int)Roman.C;
                    break;

                case 'D':
                    value = (int)Roman.D;
                    break;

                case 'M':
                    value = (int)Roman.M;
                    break;

            }

            return value;
        }

        public static String romanToArabic(String roman)
        {
            String result = "";

            switch (validateRomanNumber(roman))
            {
                case 1:
                    result = convert(roman);
                    break;

            }

            return result;
        }


        /**
         * <p>This method validates a given roman number<br>
         * Return 1 when roman number is in correct format or 0 otherwise
         * </p>
         * @param roman String
         * @return boolean
         */
        private static int validateRomanNumber(String roman)
        {
            int result = 0;
            Regex regex = new Regex(@romanNumberValidator);
            Match match = regex.Match(roman);
            if (match.Success)
            {            
                result = 1;
            }

            return result;
        }


        /**
         * Converts the valid roman number to arabic number
         * @param roman
         * @return String
         */
        private static String convert(String roman)
        {
            int decimal_val = 0;
            int lastNumber = 0;

            for (int i = roman.Length - 1; i >= 0; i--)
            {
                char ch = roman[i];
                decimal_val = CheckRoman(getValueFromRomanChar(ch), lastNumber, decimal_val);
                lastNumber = getValueFromRomanChar(ch);
            }

            return decimal_val + "";

        }


        private static int CheckRoman(int TotalDecimal, int LastRomanLetter, int LastDecimal)
        {

            if (LastRomanLetter > TotalDecimal)
            {
                return LastDecimal - TotalDecimal;
            }
            else
            {
                return LastDecimal + TotalDecimal;
            }

        }
    }
}

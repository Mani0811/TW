using Microsoft.Extensions.Logging;
using NLog;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Merchant_s_Guide_to_the_Galaxy
{
    public class Paragraph
    {
        public static Logger logger = LogManager.GetCurrentClassLogger();
        private Scanner scan;
        private ConversationLine conversationLine;
        private ErrorMessage eMessage;
        /**
         * This is the hash map that will contain the value for each identifier
         */
        private Dictionary<String, String> constantAssignments;

        /**
         * This is the hash map for storing the value of the calculated literal
         */
        private Dictionary<String, String> computedLiterals;


        /**
         * <p>This variable of ArrayList type that will contain the answers for the questions asked in input from console.<br>
         * Read() method will return this object which in turn can be used to display the results</p>  
         */
        private ArrayList output;



        public Paragraph()
        {
            //this.scan = new Scanner(System.in);
            this.conversationLine = new ConversationLine();
            this.eMessage = new ErrorMessage();
            this.constantAssignments = new Dictionary<String, String>();
            this.computedLiterals = new Dictionary<String, String>();
            this.output = new ArrayList();
        }






        /**
         * <p>This method reads the paragraph from the input console.<br>
         * The input sequence can be terminated by a blank new line.<br>
         * Each input entered will be processed same time and if it contains any formatting error message will be shown immediately<br>
         * <b>Ex:</b> saket is 78 , error message : <i>Input format is wrong ! input discarded</i>
         * </p>
         * @return output List<String>
         * <p>Use this returned List<String> object to print the results for the question asked in the input.
         */
        public ArrayList Read()
        {
            string line = string.Empty;
            int count = 0;
            ErrorCodes error = new ErrorCodes();

            while (true) // Loop indefinitely
            {
                ////Console.WriteLine("Enter input:"); // Prompt
                line = Console.ReadLine(); // Get string from user
                if (line == "exit" || line == "" || line.Length <= 0) // Check string
                {
                    Console.WriteLine("input will end with exit or blank space");
                    break;
                }
                error = validate(line);

                switch (error)
                {
                    case ErrorCodes.NO_IDEA: this.output.Add(this.eMessage.GetMessage(error)); break;

                    default: this.eMessage.PrintMessage(error); break;
                }

                count++;
            }


            switch (count)
            {
                case 0:
                    error = ErrorCodes.NO_INPUT;
                    this.eMessage.PrintMessage(error);
                    break;

                default: break;
            }

            return this.output;

        }




        /**
         * <p>This method first determines the type of line<br>
         * Based on the type of line it process each input line
         * </p>
         * @param line
         * @return error Errorcodes
         * @see ConversationLine.Type
         * @see ConversationLine#getLineType(String)
         */

        private ErrorCodes validate(String line)
        {

            ErrorCodes error = ErrorCodes.SUCCESS_OK;

            ConversationLine.Type lineType = this.conversationLine.getLineType(line);

            switch (lineType)
            {
                case ConversationLine.Type.ASSIGNED:
                    processAssignmentLine(line);
                    break;

                case ConversationLine.Type.CREDITS:
                    processCreditsLine(line);
                    break;

                case ConversationLine.Type.QUESTION_HOW_MUCH:
                    processHowMuchQuestion(line);
                    break;

                case ConversationLine.Type.QUESTION_HOW_MANY:
                    processHowManyCreditsQuestion(line);
                    break;

                default: error = ErrorCodes.NO_IDEA; break;
            }

            return error;
        }





        /**
         * <p>This method process the assignment line<br>
         * It extracts the constant roman literal from input string and adds it constantAssignments hash map
         * <p>
         * @param line
         * @throws ArrayIndexOutOfBoundsException
         */
        private void processAssignmentLine(string line)
        {
            //Since the assignment line is like "glob is I"
            //we have to break the line based on space
            //The first part i.e splited[0] is identifier and second part is i.e splited[2] is the value
            String[] splited = line.Trim().Split(' ');//.trim().split("\\s+");

            //Since it is assignment line the first String will be constantIdentifier
            // and the last will be its roman value;
            try
            {
                //Add identifier and its value to the map
                constantAssignments.Add(splited[0], splited[2]);
            }
            catch (Exception e)
            {
                this.eMessage.PrintMessage(ErrorCodes.INCORRECT_LINE_TYPE);
                Utility.println(e.Message);
            }
        }






        /**
         * <p>This method process the line for type how_much question<br>
         * It extracts all the constant identifiers from line and calculates the value from the constantAssignments hashMap<br>
         * It will generate an error 
         * @param line
         * @throws Exception
         */
        private void processHowMuchQuestion(String line)
        {
            try
            {
                //Break the how much question line based on "is" keyword
                // the second part will contain the identifiers whooose values are to determined

                //  String formatted = line.split("\\sis\\s")[1].trim();
                String formatted = line.Split(" is ")[1].Trim();


                //Remove the question mark from the string
                formatted = formatted.Replace("?", "").Trim();

                //Now since the string will contain only identifiers,break them into words by splitting through space
                String[] keys = formatted.Split(" ");


                String romanResult = "";
                String completeResult = null;
                bool errorOccured = false;

                foreach (String key in keys)
                {
                    //For each identifier gets its value
                    String romanValue = constantAssignments.GetValueOrDefault(key);
                    if (romanValue == null)
                    {
                        // This means that user has entered something thats not in the hash map
                        completeResult = this.eMessage.GetMessage(ErrorCodes.NO_IDEA);
                        errorOccured = true;
                        break;
                    }
                    romanResult += romanValue;
                }

                if (!errorOccured)
                {
                    //Utility.println(romanResult.length()+"");
                    romanResult = RomanNumbers.romanToArabic(romanResult);
                    completeResult = formatted + " is " + romanResult;
                }

                output.Add(completeResult);

            }
            catch (Exception e)
            {
                this.eMessage.PrintMessage(ErrorCodes.INCORRECT_LINE_TYPE);
                Utility.println(e.Message);

            }
        }


        /**
         * <p>This method process the line for credit computation for line type CREDITS defined in ConversationLine.type<br>
         * It extracts the constant identifier from line and compute the variable identifier<br>
         * The variable identifier is assumed to be closest to 'is' keyword in the line
         * </p>
         * @param line String
         */

        private void processCreditsLine(String line)
        {
            try
            {
                //Remove the unwanted words like "is" and "credits"
                // String formatted = line.replaceAll("(is\\s+)|([c|C]redits\\s*)", "").trim();

                //string testString = "<b>Hello, <i>world</i></b>";
                Regex regex = new Regex("(is\\s+)|([c|C]redits\\s*)");
                string formatted = regex.Replace(line, "");

                //string formatted = line.Replace("is ","").Replace("Credit","").Replace("credit","").Trim();


                //Split the remaining based on space
                string[] keys = formatted.Split(" ");
                Array.Resize(ref keys, keys.Length - 1);
                //concatenate all keys to form roman number except the second last and last one. because the second last one is to be computed.
                // The last one is the value itself
                // get the value for that roman number

                String toBeComputed = keys[keys.Length - 2];
                float value = float.Parse(keys[keys.Length - 1]);

                //concatenate remaining initial strings


                String roman = "";

                for (int i = 0; i < keys.Length - 2; i++)
                {
                    roman += constantAssignments.GetValueOrDefault(keys[i]);
                }

                int romanNumber = int.Parse(RomanNumbers.romanToArabic(roman));
                float credit = (float)(value / romanNumber);


                computedLiterals.Add(toBeComputed, credit + "");
            }
            catch (Exception e)
            {

                this.eMessage.PrintMessage(ErrorCodes.INCORRECT_LINE_TYPE);
                Utility.println(e.Message);

            }
        }





        /**
         * This will calculate the answer for how many credits question.
         * @param line
         */
        private void processHowManyCreditsQuestion(String line)
        {

            try
            {
                //Remove the unwanted words like "is" and "?"
                String formatted = line.Split(" is ")[1];

                formatted = formatted.Replace("?", "").Trim();

                // search for all numerals for their values to compute the result
                String[] keys = formatted.Split(" ");

                bool found = false;
                String roman = "";
                String outputResult = null;
                Stack<float> cvalues = new Stack<float>();

                foreach (String key in keys)
                {
                    found = false;

                    String romanValue = constantAssignments.GetValueOrDefault(key);
                    if (romanValue != null)
                    {
                        roman += romanValue;
                        found = true;
                    }

                    String computedValue = computedLiterals.GetValueOrDefault(key);
                    if (!found && computedValue != null)
                    {
                        cvalues.Push(float.Parse(computedValue));
                        found = true;
                    }

                    if (!found)
                    {
                        outputResult = this.eMessage.GetMessage(ErrorCodes.NO_IDEA);
                        break;
                    }
                }

                if (found)
                {
                    float res = 1;
                    for (int i = 0; i < cvalues.Count; i++)
                        res *= cvalues.ToArray()[i];

                    int finalres = (int)res;
                    if (roman.Length > 0)
                        finalres = (int)(int.Parse(RomanNumbers.romanToArabic(roman)) * res);
                    outputResult = formatted + " is " + finalres + " Credits";
                }

                this.output.Add(outputResult);

            }
            catch (Exception e)
            {
                this.eMessage.PrintMessage(ErrorCodes.INCORRECT_LINE_TYPE);
                Utility.println(e.Message);
            }

        }
    }

}

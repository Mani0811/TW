using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Merchant_s_Guide_to_the_Galaxy
{
    public class ConversationLine
    {

        /**
         * <p>This Enumeration holds the different type of possible line types notation<br>
         * Used by other classes to check the type of particular line and perform certain action
         * </p>
         * @author Saket kumar
         *
         */
        public enum Type
        {

            /**
             * This represents that line is of Assignment type. Ex: glob is V
             */
            ASSIGNED,

            /**
             * This represents that line is of Credits type. Ex : glob glob Silver is 34 Credits
             */
            CREDITS,

            /**
             * This represents that line is question asking how much. Ex : how much is pish tegj glob glob ?
             */
            QUESTION_HOW_MUCH,

            /**
             * This represents that line is question asking how many. Ex: how many Credits is glob prok Iron ?
             */
            QUESTION_HOW_MANY,

            /**
             * This represents that line does not matched any of the line type mentioned above
             */
            NOMATCH

        }


        public class LineFilter
        {
            private ConversationLine.Type type;
            private String pattern;
            public LineFilter(ConversationLine.Type type, String pattern)
            {
                this.type = type;
                this.pattern = pattern;
            }

            public String getPattern()
            {
                return this.pattern;

            }

            public ConversationLine.Type getType()
            {
                return this.type;
            }
        }



        public static String patternAssigned = "^([A-Za-z]+) is ([I|V|X|L|C|D|M])$";
        public static String patternCredits = "^([A-Za-z]+)([A-Za-z\\s]*) is ([0-9]+) ([c|C]redits)$";
        public static String patternHowMuch = "^how much is (([A-Za-z\\s])+)\\?$";
        public static String patternHowMany = "^how many [c|C]redits is (([A-Za-z\\s])+)\\?$";
        private LineFilter[] linefilter;



        /**
         * <p>Initializes the line filters, i.e the four type of lines to be checked.<br>
         * If more filters are to be added then create as per shown</p>
         */
        public ConversationLine()
        {
            // Since we have have four type of lines
            this.linefilter = new LineFilter[4];
            this.linefilter[0] = new LineFilter(ConversationLine.Type.ASSIGNED, patternAssigned);
            this.linefilter[1] = new LineFilter(ConversationLine.Type.CREDITS, patternCredits);
            this.linefilter[2] = new LineFilter(ConversationLine.Type.QUESTION_HOW_MUCH, patternHowMuch);
            this.linefilter[3] = new LineFilter(ConversationLine.Type.QUESTION_HOW_MANY, patternHowMany);

        }




        /**
         * <p>This method returns the line type for the a particular line</p>
         * @param line String
         * @return lineType ConversationLine.Type
         */
        public ConversationLine.Type getLineType(String line)
        {
            line = line.Trim();
            ConversationLine.Type result = Type.NOMATCH;

            bool matched = false;

            for (int i = 0; i < linefilter.Length && !matched; i++)
            {
                Regex regex = new Regex(@linefilter[i].getPattern());
                Match match = regex.Match(line);
                if (match.Success)
                {
                    matched = true;
                    result = linefilter[i].getType();
                }

            }

            return result;

        }

    }
}

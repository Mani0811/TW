using System;
using System.Collections.Generic;
using System.Text;

namespace ConferenceTrackManagement
{
    public sealed class Time
    {
        private Time() { }

        /**
         * Takes the time in minutes and returns its string representation in AB:BC AM or AB:BC PM
         * format.
         *
         * @param minutes the time in minutes to be convereted.
         * @return the string representation of the minutes passed as argument.
         * @throws IllegalArgumentException if time in minutes exceeds 11:59 PM.
         */
        public static String minutesToDisplayTime(int minutes)
        {
            int maxSupportedTimeInMinutes = (12 * 60 + 12 * 60) - 1;
            if (minutes > maxSupportedTimeInMinutes)
            {
                throw new Exception("Time in minutes cannot be greater than"
                        + maxSupportedTimeInMinutes + " minutes.");
            }

            int hours = minutes / 60;
            String hoursToDisplay = hours.ToString();
            if (hours > 12)
            {
                hoursToDisplay = (hours - 12).ToString();
            }
            if (hoursToDisplay.Length == 1)
            {
                hoursToDisplay = "0" + hoursToDisplay;
            }

            minutes = minutes - (hours * 60);
            String minutesToDisplay = null;
            if (minutes == 0)
            {
                minutesToDisplay = "00";
            }
            else if (minutes < 10)
            {
                minutesToDisplay = "0" + minutes;
            }
            else
            {
                minutesToDisplay = "" + minutes;
            }

            String displayValue;
            if (hours < 12)
            {
                displayValue = " AM";
                if (hoursToDisplay.Equals("00"))
                {
                    hoursToDisplay = "12";
                }
            }
            else
            {
                displayValue = " PM";
            }
            displayValue = hoursToDisplay + ":" + minutesToDisplay + displayValue;

            return displayValue;
        }
    }

}

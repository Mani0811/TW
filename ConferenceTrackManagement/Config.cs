using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ConferenceTrackManagement
{
    public sealed class Config
    {
        private Config() { }

        public static Regex INPUT_LINE_PATTERN = new Regex("^(.+)\\s(\\d+)?\\s?((min)|(lightning))$");

        public static int EVENT_NAME_INDEX = 1;
        public static int EVENT_DURATION_INDEX = 2;
        public static int EVENT_DURATION_UNIT_INDEX = 3;

        public static int MORNING_SLOT_DURATION = 180;
        public static int LUNCH_SLOT_DURATION = 60;
        public static int AFTERNOON_SLOT_DURATION = 240;

        public static int MORNING_SLOT_START_TIME = 9 * 60;
        public static int LUNCH_SLOT_START_TIME = MORNING_SLOT_START_TIME + MORNING_SLOT_DURATION;
        public static int AFTERNOON_SLOT_START_TIME = LUNCH_SLOT_START_TIME + LUNCH_SLOT_DURATION;

        public static int MAX_EVENT_DURATION = new int[]{MORNING_SLOT_DURATION, LUNCH_SLOT_DURATION, AFTERNOON_SLOT_DURATION }.Max();

        public static string LUNCH_SLOT_NAME = "Lunch";
        public static string NETWORKING_EVENT_NAME = "Networking Event";
        public static int NETWORKING_EVENT_DURATION = 60;
        public static int NETWORKING_EVENT_MIN_START_TIME = (12 * 60) + (4 * 60); // 4 PM.
    }
}

using Microsoft.Extensions.Logging;
using NLog;
using System;
using System.Collections.Generic;

namespace ConferenceTrackManagement
{
    public sealed class ConferenceScheduler
    {
        public static Logger logger = LogManager.GetCurrentClassLogger();
        public ConferenceScheduler() { }


        public Conference Schedule()
        {

            String line = string.Empty;
            List<Event> events = new List<Event>();
            while (true) // Loop indefinitely
            {
                ////Console.WriteLine("Enter input:"); // Prompt
                line = Console.ReadLine(); // Get string from user
                if (line == "exit" || line == "" ||line.Length<=0) // Check string
                {
                    Console.WriteLine("input will end with exit or blank space");
                    break;
                }
                line = line.Trim();
                Event event_obj = ParseInputLine(line);
                if (event_obj == null)
                {
                    continue;
                }
                events.Add(event_obj);
            }

            Conference conference = new Conference();
            while (events.Count != 0)
            {
                DurationUnit unit = new DurationUnit();
                Slot morningSlot = new Slot(Config.MORNING_SLOT_DURATION, Config.MORNING_SLOT_START_TIME);
                fillSlotWithEvents(morningSlot, events);
                Slot lunchSlot = new Slot(Config.LUNCH_SLOT_DURATION, Config.LUNCH_SLOT_START_TIME);
                unit.Minutes();
                lunchSlot.addEvent(new Event("Lunch", Config.LUNCH_SLOT_DURATION,unit));
                Slot afternoonSlot = new Slot(Config.AFTERNOON_SLOT_DURATION, Config.AFTERNOON_SLOT_START_TIME);
                fillSlotWithEvents(afternoonSlot, events);
                Event networkingEvent = new Event(Config.NETWORKING_EVENT_NAME, Config.NETWORKING_EVENT_DURATION, unit);
                Slot networkingSlot = new Slot(networkingEvent.GetDurationInMinutes(),
                        Config.NETWORKING_EVENT_MIN_START_TIME);
                networkingSlot.addEvent(networkingEvent);
                afternoonSlot.addSupplementSlot(networkingSlot);
                Track track = new Track();
                track.addSlot(morningSlot);
                track.addSlot(lunchSlot);
                track.addSlot(afternoonSlot);
                conference.addTrack(track);
            }

            return conference;
        }

        private static void fillSlotWithEvents(Slot slot, List<Event> events)
        {
            foreach (var event_obj in events.ToArray())
            {
                if (slot.hasRoomFor(event_obj))
                {
                    slot.addEvent(event_obj);
                    events.Remove(event_obj);
                }
            }
        }

        public static Event ParseInputLine(string line)
        {
            if (line.Length == 0)
            {
                return null;
            }

            var match = Config.INPUT_LINE_PATTERN.Match(line);
            if (!match.Success)
            {
                logger.Warn("Invalid input line: " + line);
                return null;
            }

            DurationUnit unit = new DurationUnit();
            if (match.Groups[Config.EVENT_DURATION_UNIT_INDEX].Value.Equals("min", StringComparison.OrdinalIgnoreCase))
            {
                unit.Minutes();
            }
            else
            {
                unit.LIGHTENING();
            }

            string name = match.Groups[Config.EVENT_NAME_INDEX].Value;
            string durationInString = match.Groups[Config.EVENT_DURATION_INDEX].Value;
            if (durationInString == null || string.IsNullOrEmpty(durationInString))
            {
                durationInString = "1";
            }
            int duration = int.Parse(durationInString);

            Event event_obj = new Event(name, duration, unit);
            if (event_obj.GetDurationInMinutes() > Config.MAX_EVENT_DURATION)
            {
                logger.Warn("Duration of event '" + name + "' is more than the maximum duration"
                       + " allowed for an event. Dropping this event for scheduling.");
                return null;
            }

            return event_obj;
        }
    }
}

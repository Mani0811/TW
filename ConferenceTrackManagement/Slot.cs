using System;
using System.Collections.Generic;
using System.Text;

namespace ConferenceTrackManagement
{
    public class Slot
    {
        private List<Event> events;
        private int remainingDuration;
        private int startTime;
        private Slot supplement;

        public Slot(int duration, int startTime)
        {
            this.remainingDuration = duration;
            this.startTime = startTime;
            events = new List<Event>();
        }

        public void addEvent(Event event_obj) {
            if (remainingDuration < event_obj.GetDurationInMinutes()) {
            throw new Exception("Not enough room in this slot to fit the event: '"
                    + event_obj.Name + "'");
        }
        events.Add(event_obj);
        remainingDuration -= event_obj.GetDurationInMinutes();
    }

    public bool hasRoomFor(Event event_obj)
    {
        return remainingDuration >= event_obj.GetDurationInMinutes();
    }

    public void addSupplementSlot(Slot slot)
    {
        this.supplement = slot;
    }

    public override string ToString()
    {
        StringBuilder str = new StringBuilder();
        int nextEventStartTime = addEventsSchedule(events, startTime, str);
        if (supplement != null)
        {
            int supplementStartTime = supplement.startTime;
            if (nextEventStartTime > supplement.startTime)
            {
                supplementStartTime = nextEventStartTime;
            }
            nextEventStartTime = addEventsSchedule(supplement.events, supplementStartTime, str);
        }
        return str.ToString();
    }

    /**
     * Adds events to be added to the StringBuilder with their starting time and their duration.
     *
     * @param events the events to be added to the StringBuilder.
     * @param startTime the start time of the first event.
     * @param str the StringBuilder object to which the schedule must be added.
     * @return the time at which the events end.
     */
    private int addEventsSchedule(List<Event> events, int startTime, StringBuilder str)
    {
        int nextEventStartTime = startTime;
        foreach (Event event_obj in events)
        {
            str.Append(Time.minutesToDisplayTime(nextEventStartTime) + " " + event_obj.ToString() + Environment.NewLine);
            nextEventStartTime += event_obj.GetDurationInMinutes();
        }

        return nextEventStartTime;
    }
}
}

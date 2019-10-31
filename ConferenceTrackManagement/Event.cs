using System;
using System.Collections.Generic;
using System.Text;

namespace ConferenceTrackManagement
{
    public class Event
    {
        private string name;
        private int duration;
        private DurationUnit unit;

        public Event(string name, int duration, DurationUnit unit)
        {
            this.name = name;
            this.duration = duration;
            this.unit = unit;
        }

        public override string ToString()
        {
            if (unit.Equals("lightning"))
                return name + " " + unit;
            return name + " " + duration + unit;
        }

        public int GetDurationInMinutes()
        {
            return unit.InMinutes(duration);
        }

        public string Name
        {
            get

            {
                return name;
            }
        }

    }
}

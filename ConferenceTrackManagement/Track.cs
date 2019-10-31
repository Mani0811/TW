using System;
using System.Collections.Generic;
using System.Text;

namespace ConferenceTrackManagement
{
    public class Track
    {
        public List<Slot> Slots;

        public Track()
        {
            Slots = new List<Slot>();
        }

        public void addSlot(Slot slot)
        {
            Slots.Add(slot);
        }

        public override string ToString()
        {
            StringBuilder str = new StringBuilder();
            foreach (Slot slot in Slots)
            {
                str.Append(slot);
            }
            return str.ToString();
        }
    }
}

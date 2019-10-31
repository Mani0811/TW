using System;
using System.Collections.Generic;
using System.Text;

namespace ConferenceTrackManagement
{
    public  class DurationUnit
    {
        
        private int factor;
        private string stringRepresentation;

        private void DurationToUnit(int _factor, string stringRepresentation)
        {
            factor = _factor;
            this.stringRepresentation = stringRepresentation;
        }
        public void Minutes()
        {
            DurationToUnit(1, "min");
        }

        public void LIGHTENING()
        {
            DurationToUnit(5, "lightning");
        }
        public int InMinutes(int duration)
        {
            return factor * duration;
        }

        public override string ToString()
        {
            return stringRepresentation;
        }
    }
}


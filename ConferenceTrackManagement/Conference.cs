using System;
using System.Collections.Generic;
using System.Text;

namespace ConferenceTrackManagement
{
    public class Conference
    {
        private List<Track> tracks;

        public Conference()
        {
            tracks = new List<Track>();
        }

        public void addTrack(Track track)
        {
            tracks.Add(track);
        }

        public override string ToString()
        {
            StringBuilder str = new StringBuilder();
            str.Append("Conference Schedule:" + Environment.NewLine + Environment.NewLine);
            for (int i = 0; i < tracks.Count; i++)
            {
                str.Append("Track " + (i + 1) + ":" + Environment.NewLine);
                str.Append(tracks[i]);
                str.Append(Environment.NewLine);
            }
            return str.ToString();
        }
    }
}

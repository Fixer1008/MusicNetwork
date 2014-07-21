using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MusicSocialNetwork.Helpers
{
    public static class SongHelper
    {
        public static int ParseDuration(string duration)
        {
            int pointIndex = duration.IndexOf(':');
            string minutes = duration.Substring(0, pointIndex);
            string seconds = duration.Substring(pointIndex+1, duration.Length - (pointIndex + 1));

            int min = int.Parse(minutes);
            int sec = int.Parse(seconds);

            return (min * 60) + sec;
        }
    }
}
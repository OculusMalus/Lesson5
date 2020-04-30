using System;
using System.Collections.Generic;
using System.Text;

namespace Lesson5
{
    public class Movie : Media
    {
        public string director { get; set; }
        public TimeSpan runningTime { get; set; }

        public override string Display()
        {
            return $"Id: {mediaId}\nTitle: {title}\nDirector: {director}\nRun time: {runningTime}\nGenres: {string.Join(", ", genres)}\n";
        }
    }
}

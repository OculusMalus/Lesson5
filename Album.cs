using System;
using System.Collections.Generic;
using System.Text;

namespace Lesson5
{
    // Album class is derived from Media class
    public class Album : Media
    {
        public string artist { get; set; }
        public string recordLabel { get; set; }

        public override string Display()
        {
            return $"{base.Display()}Artist: {artist}\nLabel: {recordLabel}\n";
        }
    }
}

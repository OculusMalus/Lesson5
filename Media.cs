using System;
using System.Collections.Generic;
using System.Text;
using NLog;

namespace Lesson5
{
    public abstract class Media
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        // public properties
        public UInt64 mediaId { get; set; }
        public string title { get; set; }
        public List<string> genres { get; set; }

        // constructor
        public Media()
        {
            genres = new List<string>();
        }

        // public method
        public virtual string Display()
        {
            return $"Id: {mediaId}\nTitle: {title}\nGenres: {string.Join(", ", genres)}\n";
        }
        
    }
}

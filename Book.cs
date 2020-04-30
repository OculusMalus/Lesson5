using System;
using System.Collections.Generic;
using System.Text;

namespace Lesson5
{
    // Book class is derived from Media class
    public class Book : Media
    {
        public string author { get; set; }
        public string pageCount { get; set; }
        public string publisher { get; set; }

        public override string Display()
        {
            return $"{base.Display()}Author: {author}\nPages: {pageCount}\nPublisher: {publisher}\n";
        }
    }
}

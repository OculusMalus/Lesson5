using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Lesson5
{
    public class BookFile
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        // public property
        public string filePath { get; set; }
        public List<Book> Books { get; set; }

        public BookFile(string path)
        {
            Books = new List<Book>();
            filePath = path;
            // to populate the list with data, read from the data file
            try
            {
                StreamReader sr = new StreamReader(filePath);
                // first line contains column headers
                var bob = sr.ReadLine();
                while (!sr.EndOfStream)
                {
                    // create instance of Book class
                    Book book = new Book();
                    string line = sr.ReadLine();

                    string[] bookDetails = line.Split(',');
                    book.mediaId = UInt64.Parse(bookDetails[0]);
                    book.title = bookDetails[1];
                    book.genres = bookDetails[2].Split(',').ToList();
                    book.author = bookDetails[3];
                    book.pageCount = bookDetails[4];
                    book.publisher = bookDetails[5];

                    Books.Add(book);
                }
                // close file when done
                sr.Close();
                logger.Info("Books in file {Count}", Books.Count);
          
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }
        }
        

        public bool isUniqueTitle(string title)
        {
            if (Books.ConvertAll(b => b.title.ToLower()).Contains(title.ToLower()))
            {
                logger.Info("Duplicate book title {Title}", title);
                return false;
            }
            return true;
        }

        public void AddBook(Book book)
        {
            try
            {
                // first generate movie id
                book.mediaId = (Books.Last().mediaId + 1);

                StreamWriter sw = new StreamWriter(filePath, true);
                sw.WriteLine($"{book.mediaId},{book.title},{string.Join("|", book.genres)},{book.author},{book.pageCount},{book.publisher}");
                sw.Close();
                // add movie details to Lists
                Books.Add(book);
                // log transaction
                logger.Info("Book id {Id} added", book.mediaId);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }
        }
    }
}

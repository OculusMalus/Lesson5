using System;
using System.IO;
using NLog;
using System.Collections.Generic;
using System.Linq;

namespace Lesson5
{
    class MainClass
    {
        // create a class level instance of logger(can be used in methods other than Main)
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        public static void Main(string[] args)
        {
            // config is loaded using xml (NLog.config saved in debug folder)
            logger.Info("Program started");
            string scrubbedFile = FileScrubber.ScrubMovies("movies.csv");

            // path to movie data file
            string file = "movies.scrubbed.csv";
            // path to book data file
            string bookFilePath = "books.csv";
            // path to album data file;
            string albumFilePath = "albums.csv";

            //create instance of File classes
            MovieFile movieFile = new MovieFile(file);
            BookFile bookFile = new BookFile(bookFilePath);
            AlbumFile albumFile = new AlbumFile(albumFilePath);

            //AlbumFile albumFile = new AlbumFile(albumFilePath)


            string choice = "";
            do
            {
                // display choices to user
                Console.WriteLine("1) Add Movie");
                Console.WriteLine("2) Display All Movies");
                Console.WriteLine("3) Add Book");
                Console.WriteLine("4) Display All Books");
                Console.WriteLine("5) Add Album");
                Console.WriteLine("6) Display All Albums");
                Console.WriteLine("7) Search Movie Titles");
                Console.WriteLine("8) Search for a Book by Title");
                Console.WriteLine("9) Search for an Album by Title");

                Console.WriteLine("Enter to quit");
                // input selection
                choice = Console.ReadLine();
                logger.Info("User choice: {Choice}", choice);

                if (choice == "1")
                {
                    Movie movie = new Movie();
                    // ask user to input movie title
                    Console.WriteLine("Enter movie title");
                    // input title
                    movie.title = Console.ReadLine();
                    // verify title is unique
                    if (movieFile.isUniqueTitle(movie.title))
                    {
                        Console.WriteLine("Movie title is unique\n");
                        // input genres
                        string input;
                        do
                        {
                            // ask user to enter genre
                            Console.WriteLine("Enter genre (or done to quit)");
                            // input genre
                            input = Console.ReadLine();
                            // if user enters "done"
                            // or does not enter a genre do not add it to list
                            if (input != "done" && input.Length > 0)
                            {
                                movie.genres.Add(input);
                            }
                        } while (input != "done");
                        // specify if no genres are entered
                        if (movie.genres.Count == 0)
                        {
                            movie.genres.Add("(no genres listed)");
                        }

                        Console.Write("Enter movie director: ");
                        movie.director = Console.ReadLine();

                        Console.Write("Enter run time hours: ");
                        var hrs = Int16.Parse(Console.ReadLine());
                        Console.Write("Enter run time minutes: ");
                        var min = Int16.Parse(Console.ReadLine());
                        Console.Write("Enter run time seconds: ");
                        var sec = Int16.Parse(Console.ReadLine());
                        movie.runningTime = new TimeSpan(hrs, min, sec);

                        // add movie
                        movieFile.AddMovie(movie);
                    }
                    else
                    {
                        Console.WriteLine("Movie title already exists\n");
                    }
                }
                else if (choice == "2")
                {
                    // Display All Movies
                    foreach (Movie m in movieFile.Movies)
                    {
                        Console.WriteLine(m.Display());
                    }
                }
                else if (choice == "3")
                {
                    string input = "";
                    Book book = new Book();
                    // ask user to input book title
                    Console.WriteLine("Enter book title");
                    book.title = Console.ReadLine();
                    // verify title is unique
                    if (bookFile.isUniqueTitle(book.title))
                    {
                        Console.WriteLine("Book title is unique\n");

                        do
                        {
                            // ask user to enter genre
                            Console.WriteLine("Enter genre (or done to quit)");
                            // input genre
                            input = Console.ReadLine();
                            // if user enters "done"
                            // or does not enter a genre do not add it to list
                            if (input != "done" && input.Length > 0)
                            {
                                book.genres.Add(input);
                            }
                        } while (input != "done");
                        // specify if no genres are entered
                        if (book.genres.Count == 0)
                        {
                            book.genres.Add("(no genres listed)");
                        }

                        Console.WriteLine("Enter author: ");
                        book.author = Console.ReadLine();
                        Console.WriteLine("Enter page count: ");
                        book.pageCount = Console.ReadLine();
                        Console.WriteLine("Enter publisher: ");
                        book.publisher = Console.ReadLine();

                        // add movie
                        bookFile.AddBook(book);
                    }
                    else
                    {
                        Console.WriteLine("Book title already exists\n");
                    }


                }
                else if (choice == "4")
                {
                    // Display All Books
                    foreach (Book b in bookFile.Books)
                    {
                        Console.WriteLine(b.Display());
                    }
                }
                else if (choice == "5")
                {

                }
                else if (choice == "6")
                {
                    //Display All Albums
                    foreach (Album a in albumFile.Albums)
                    {
                        Console.WriteLine(a.Display());
                    }
                }

                else if (choice == "7")
                {
                    string movieTerm = "";
                    // ask user to input movie title
                    Console.WriteLine("Enter search term");
                    movieTerm = Console.ReadLine().ToLower();

                    var titles = movieFile.Movies.Where(m => m.title.ToLower().Contains(movieTerm)).Select(m => m.title);
                    Console.WriteLine("\nMovies with {0} in title", movieTerm);
                    foreach (string t in titles)
                    {
                        Console.WriteLine(t);
                    }
                    Console.WriteLine("\n");


                }

                else if (choice == "8")
                {
                    string bookTerm = "";
                    // ask user to input book title
                    Console.WriteLine("Enter search term");
                    bookTerm = Console.ReadLine().ToLower();

                    var titles = bookFile.Books.Where(m => m.title.ToLower().Contains(bookTerm)).Select(m => m.title);
                    Console.WriteLine("\nBooks with {0} in title", bookTerm);
                    foreach (string t in titles)
                    {
                        Console.WriteLine(t);
                    }
                    Console.WriteLine("\n");
                }

                else if (choice == "9")
                {
                    string albumTerm = "";
                    // ask user to input album title
                    Console.WriteLine("Enter search term");
                    albumTerm = Console.ReadLine().ToLower();

                    var titles = albumFile.Albums.Where(m => m.title.ToLower().Contains(albumTerm)).Select(m => m.title);
                    Console.WriteLine("\nMovies with {0} in title", albumTerm);
                    foreach (string t in titles)
                    {
                        Console.WriteLine(t);
                    }
                    Console.WriteLine("\n");
                }

            } while (choice == "1" || choice == "2" || choice == "3" || choice == "4" || choice == "5" || choice == "6" || choice == "7" || choice == "8" || choice == "9");
            
                                  

            logger.Info("Program ended");
        }
    }
}
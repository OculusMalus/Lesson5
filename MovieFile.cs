using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Lesson5
{
    public class MovieFile
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        // public property
        public string filePath { get; set; }
        public List<Movie> Movies { get; set; }
        public MovieFile(string path)
        {
            Movies = new List<Movie>();
            filePath = path;
            // to populate the list with data, read from the data file
            try
            {
                StreamReader sr = new StreamReader(filePath);
                // first line contains column headers
                ///sr.ReadLine();
                while (!sr.EndOfStream)
                {
                    // create instance of Movie class
                    Movie movie = new Movie();
                    string line = sr.ReadLine();
                    // first look for quote(") in string
                    // this indicates a comma(,) in movie title
                    int idx = line.IndexOf('"');
                    if (idx == -1)
                    {
                        // no quote = no comma in movie title
                        // movie details are separated with comma(,)
                        string[] movieDetails = line.Split(',');
                        movie.mediaId = UInt16.Parse(movieDetails[0]);
                        movie.title = movieDetails[1];
                        movie.genres = movieDetails[2].Split('|').ToList();
                        movie.director = movieDetails[3];
                        movie.runningTime = TimeSpan.Parse(movieDetails[4]);
                    }
                    else
                    {
                        // quote = comma in movie title
                        // extract the movieId
                        movie.mediaId = UInt16.Parse(line.Substring(0, idx - 1));
                        // remove movieId and first quote from string
                        line = line.Substring(idx + 1);
                        // find the next quote
                        idx = line.IndexOf('"');
                        // extract the movieTitle
                        movie.title = line.Substring(0, idx);
                        // remove title and last comma from the string
                        line = line.Substring(idx + 2);

                        // find the next comma which ends the list of genres
                        idx = line.IndexOf(',');
                        // extract the genres
                        var rawGenres = line.Substring(0, idx);                      
                        // replace the "|" with ", "
                        movie.genres = rawGenres.Split('|').ToList();

                        //extract the director and running time
                        string[] directorAndRunningTime = line.Split(',');
                        movie.director = directorAndRunningTime[1];
                        movie.runningTime = TimeSpan.Parse(directorAndRunningTime[2]);

                    }
                    Movies.Add(movie);
                }
                // close file when done
                sr.Close();
                logger.Info("Movies in file {Count}", Movies.Count);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }
        }

        public bool isUniqueTitle(string title)
        {
            if (Movies.ConvertAll(m => m.title.ToLower()).Contains(title.ToLower()))
            {
                logger.Info("Duplicate movie title {Title}", title);
                return false;
            }
            return true;
        }

        public void AddMovie(Movie movie)
        {
            try
            {
                // first generate movie id
                movie.mediaId = (Movies.Last().mediaId + 1);

                StreamWriter sw = new StreamWriter(filePath, true);
                sw.WriteLine($"{movie.mediaId},{movie.title},{string.Join("|", movie.genres)},{movie.director},{movie.runningTime}");
                sw.Close();
                // add movie details to Lists
                Movies.Add(movie);
                // log transaction
                logger.Info("Movie id {Id} added", movie.mediaId);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }
        }
    }
}
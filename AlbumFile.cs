using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Lesson5
{
    public class AlbumFile
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        // public property
        public string filePath { get; set; }
        public List<Album> Albums { get; set; }

        public AlbumFile(string path)
        {
            Albums = new List<Album>();
            filePath = path;
            // to populate the list with data, read from the data file
            try
            {
                StreamReader sr = new StreamReader(filePath);
                // first line contains column headers
                var bob = sr.ReadLine();
                while (!sr.EndOfStream)
                {
                    // create instance of Album class
                    Album album = new Album();
                    string line = sr.ReadLine();

                    string[] albumDetails = line.Split(',');
                    album.mediaId = UInt64.Parse(albumDetails[0]);
                    album.title = albumDetails[1];
                    album.genres = albumDetails[2].Split(',').ToList();
                    album.artist = albumDetails[3];
                    album.recordLabel = albumDetails[4];
                    
                    Albums.Add(album);
                }
                // close file when done
                sr.Close();
                logger.Info("Albums in file {Count}", Albums.Count);

            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }
        }


        public bool isUniqueTitle(string title)
        {
            if (Albums.ConvertAll(b => b.title.ToLower()).Contains(title.ToLower()))
            {
                logger.Info("Duplicate album title {Title}", title);
                return false;
            }
            return true;
        }

        public void AddAlbum(Album album)
        {
            try
            {
                // first generate movie id
                album.mediaId = (Albums.Last().mediaId + 1);

                StreamWriter sw = new StreamWriter(filePath, true);
                sw.WriteLine($"{album.mediaId},{album.title},{string.Join("|", album.genres)},{album.artist},{album.recordLabel}");
                sw.Close();
                // add movie details to Lists
                Albums.Add(album);
                // log transaction
                logger.Info("Album id {Id} added", album.mediaId);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }
        }


    }
}

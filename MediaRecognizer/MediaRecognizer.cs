using com.cyberinternauts.all.MediaRecognizer.Models.Metas;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.cyberinternauts.all.MediaRecognizer
{
    class MediaRecognizer
    {

        public async Task RecognizeMedias(string path)
        {   var watch = new System.Diagnostics.Stopwatch();
            watch.Start();

            using (var db = new Database.MediaRecognizerContext())
            {
                var first = db.MetaMovies.FromSqlRaw("SELECT * FROM MetaMovies WHERE Id = 2718064").ToList();
                var second = db.MetaMovies.FromSqlRaw("SELECT * FROM MetaMovies WHERE Id = 2718065").ToList();
            }

            /*
            */
            using (var db = new Database.MediaRecognizerContext())
            {
                var conn = (SqliteConnection)db.Database.GetDbConnection();
                conn.EnableExtensions(true);
                conn.LoadExtension(@"SQLite Extensions\spellfix1.dll");

                //FIXME: No entries in MetaTitles for this movie, but there were in the AKAs file
                var fuzzyQuery = @"SELECT mm.*, fuzzyTitles.distance FROM MetaMovies AS mm,  
(
	SELECT mt.MetaMovieId, MIN(fuzzyWords.distance) AS Distance FROM MetaTitles AS mt, 
		(SELECT DISTINCT word, distance FROM MetaTitlesFuzzy WHERE word MATCH 'Movie title 1' AND distance < 500) AS fuzzyWords
	WHERE mt.Title = fuzzyWords.word
	GROUP BY mt.MetaMovieId
) AS fuzzyTitles
WHERE mm.Id = fuzzyTitles.MetaMovieId;";

                var found = db.FuzzyMetaMovies.FromSqlRaw<FuzzyMetaMovie>(fuzzyQuery);

                var titles = found.ToList();

                //FIXME: This doesn't work... tried using a another "using" (below)
                /*
                fuzzyQuery = @"SELECT mm.*, fuzzyTitles.distance FROM MetaMovies AS mm,  
(
	SELECT mt.MetaMovieId, MIN(fuzzyWords.distance) AS Distance FROM MetaTitles AS mt, 
		(SELECT DISTINCT word, distance FROM MetaTitlesFuzzy WHERE word MATCH 'Movie title 2' AND distance < 500) AS fuzzyWords
	WHERE mt.Title = fuzzyWords.word
	GROUP BY mt.MetaMovieId
) AS fuzzyTitles
WHERE mm.Id = fuzzyTitles.MetaMovieId;";

                conn = (SqliteConnection)db.Database.GetDbConnection();
                conn.EnableExtensions(true);
                conn.LoadExtension(@"SQLite Extensions\spellfix1.dll");

                found = db.FuzzyMetaMovies.FromSqlRaw<FuzzyMetaMovie>(fuzzyQuery);

                titles = found.ToList();
                */

                //conn.EnableExtensions(false); // NO LUCK - Still the bug
                //conn.Close(); // NO LUCK - Still the bug
                var a = 1;

            }

            using (var db = new Database.MediaRecognizerContext())
            {
                var third = db.MetaMovies.FromSqlRaw("SELECT * FROM MetaMovies WHERE Id = 2718064").ToList();
                var fourth = db.MetaMovies.FromSqlRaw("SELECT * FROM MetaMovies WHERE Id = 2718065").ToList();
            }

            using (var db = new Database.MediaRecognizerContext())
            {
                var conn = (SqliteConnection)db.Database.GetDbConnection();
                conn.EnableExtensions(true);
                conn.LoadExtension(@"SQLite Extensions\spellfix1.dll");

                var fuzzyQuery = @"SELECT mm.*, fuzzyTitles.distance FROM MetaMovies AS mm,  
(
	SELECT mt.MetaMovieId, MIN(fuzzyWords.distance) AS Distance FROM MetaTitles AS mt, 
		(SELECT DISTINCT word, distance FROM MetaTitlesFuzzy WHERE word MATCH 'Movie title 2' AND distance < 500) AS fuzzyWords
	WHERE mt.Title = fuzzyWords.word
	GROUP BY mt.MetaMovieId
) AS fuzzyTitles
WHERE mm.Id = fuzzyTitles.MetaMovieId;";

                var found = db.FuzzyMetaMovies.FromSqlRaw<FuzzyMetaMovie>(fuzzyQuery);

                var titles = found.ToList(); //FIXME: Microsoft.Data.Sqlite.SqliteException: 'SQLite Error 1: 'error during initialization: '.'
                var b = 0;
            }

            watch.Stop();
            Console.WriteLine("Time:" + watch.ElapsedMilliseconds);
        }
    }
}

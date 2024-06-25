using System;
using System.Collections.Generic;

namespace productDemo.Data.Movie.impl.remote.model
{
    public class MovieResponse
    {
        public class Movie
        {
            public int Page { get; set; }
            public int Total_Pages { get; set; }
            public List<MovieResult> Results { get; set; }
        }

        public class MovieResult
        {
            public bool Adult { get; set; }
            public string BackdropPath { get; set; }
            public List<int> GenreIds { get; set; }
            public int Id { get; set; }
            public string OriginalLanguage { get; set; }
            public string OriginalTitle { get; set; }
            public string Overview { get; set; }
            public double Popularity { get; set; }
            public string Poster_Path { get; set; }
            public DateTime ReleaseDate { get; set; }
            public string Title { get; set; }
            public bool Video { get; set; }
            public double VoteAverage { get; set; }
            public int VoteCount { get; set; }
        }
    }
}
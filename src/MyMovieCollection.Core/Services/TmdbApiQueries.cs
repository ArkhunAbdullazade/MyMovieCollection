using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyMovieCollection.Core.Services
{
    public static class TmdbApiQueries
    {
        public static string Trending = "3/trending/movie/week";
        public static string TopRated = "3/movie/top_rated";
        public static string Popular = "3/movie/popular";
        public static string Upcoming = "3/movie/upcoming";
    }
}
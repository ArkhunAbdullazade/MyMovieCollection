using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyMovieCollection.Core.Services
{
    public static class TmdbApiQueries
    {
        public static string Trending = "trending/movie/week";
        public static string Popular = "movie/popular";
        public static string Upcoming = "movie/upcoming";
        public static string TopRated = "movie/top_rated";
        public static string Discover = "discover/movie";
    }
}
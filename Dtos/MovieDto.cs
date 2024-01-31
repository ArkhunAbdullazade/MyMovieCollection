using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyMovieCollection.Dtos
{
    public class MovieDto
    {
        public string? Title { get; set; }
        public string? OriginalTitle { get; set; }
        public string? PosterUrl { get; set; }
        public string? Description { get; set; }
        public float? Budget { get; set; }
        public float? ImbdScore { get; set; }
        public int? MetaScore { get; set; }
        public string? ReleaseDate { get; set; }
    }
}
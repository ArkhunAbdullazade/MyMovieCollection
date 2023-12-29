namespace MyMovieCollection.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public float? ImbdScore { get; set; }
        public int? MetaScore { get; set; }

        public Movie() { }
        public Movie(string? title, string? description, int? imbdScore, int? metaScore)
        {
            Title = title;
            Description = description;
            ImbdScore = imbdScore;
            MetaScore = metaScore;
        }
    }
}
use MyMovieCollectionDb;

create table Movies(
    [Id] int primary key identity,
    [Title] nvarchar(100),
    [OriginalTitle] nvarchar(100),
    [PosterUrl] nvarchar(200),
    [Description] text,
    [Budget] float,
    [ImbdScore] float,
    [MetaScore] int,
)
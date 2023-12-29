use MyMovieCollection;

create table Movies(
    [Id] int primary key identity,
    [Title] nvarchar(100),
    [Description] text,
    [ImbdScore] float,
    [MetaScore] int,
)
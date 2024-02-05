use MyMovieCollectionDb;

create table Movies(
    [Id] int primary key identity,
    [Title] nvarchar(100),
    [OriginalTitle] nvarchar(100),
    [ReleaseDate] nvarchar(50),
    [PosterUrl] nvarchar(200),
    [Description] text,
    [Budget] float,
    [ImbdScore] float,
    [MetaScore] int,
)

create table Users 
(
    [Id] int primary key identity,
    [Login] nvarchar(100),
    [Password] nvarchar(100),
)

create table Logs
(
    [Id] int primary key identity,
    [UserId] int,
    [Url] nvarchar(max),
    [MethodType] nvarchar(20),
    [StatusCode] int,
    [RequestBody] nvarchar(max),
    [ResponseBody] nvarchar(max)
)
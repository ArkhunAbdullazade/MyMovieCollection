use MyMovieCollectionDb;

create table Users 
(
    [Id] int primary key identity,
    [Email] nvarchar(100),
    [Login] nvarchar(100),
    [Password] nvarchar(100),
    [PhoneNumber] nvarchar(100),
)

create table Logs
(
    [Id] int primary key identity,
    [UserId] int,
    [Url] nvarchar(max),
    [MethodType] nvarchar(20),
    [StatusCode] int,
    [RequestBody] nvarchar(max),
    [ResponseBody] nvarchar(max),
)

create table UsersMovies
(
    [UserId] int,
    [MovieId] int,
    [Rating] float,
    [Review] text,
)
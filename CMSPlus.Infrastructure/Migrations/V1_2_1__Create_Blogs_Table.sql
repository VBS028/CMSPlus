CREATE TABLE Blogs(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    [CreatedOnUtc] [datetime] NOT NULL,
    [UpdatedOnUtc] [datetime] NOT NULL,
)
GO

CREATE TABLE BlogComments(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    BlogId INT NOT NULL REFERENCES Blogs(Id),
    UserId [nvarchar](450) NOT NULL,
    [Body] [nvarchar](3000) NOT NULL,
    [CreatedOnUtc] [datetime] NOT NULL,
    [UpdatedOnUtc] [datetime] NOT NULL,
)
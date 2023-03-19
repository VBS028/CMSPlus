ALTER TABLE Blogs
ADD [SystemName] [varchar](255) NOT NULL,
    [Title] [nvarchar](512) NOT NULL,
    [Body] [nvarchar](3000) NOT NULL;
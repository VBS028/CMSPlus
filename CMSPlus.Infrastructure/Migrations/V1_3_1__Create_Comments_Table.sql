CREATE TABLE [dbo].[TopicComments](
    [Id] [int] IDENTITY(1,1) NOT NULL,
    [TopicId] [int] NOT NULL REFERENCES Topics(Id),
    [Username] [nvarchar](450) NOT NULL,
    [Body] [nvarchar](3000) NOT NULL,
    [CreatedOnUtc] [datetime] NOT NULL,
    [UpdatedOnUtc] [datetime] NOT NULL,
    PRIMARY KEY CLUSTERED
(
[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
    ) ON [PRIMARY]
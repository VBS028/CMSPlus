ALTER TABLE Blogs
    ADD [CreatorId] [nvarchar](450) NOT NULL;

ALTER TABLE Blogs
ADD CONSTRAINT FK_Creator_Blog_Users FOREIGN KEY (CreatorId)
REFERENCES AspNetUsers(Id)
ON UPDATE CASCADE 
    
    
ALTER TABLE Topics
    ADD [CreatorId] [nvarchar](450) NOT NULL;

ALTER TABLE Topics
    ADD CONSTRAINT FK_Creator_Topic_Users FOREIGN KEY (CreatorId)
        REFERENCES AspNetUsers(Id)
        ON UPDATE CASCADE 
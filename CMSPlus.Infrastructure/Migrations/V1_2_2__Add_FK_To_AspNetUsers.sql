ALTER TABLE BlogComments
ADD CONSTRAINT FK_BlogComments_AspNet_Users FOREIGN KEY (UserId)
    REFERENCES AspNetUsers(Id)
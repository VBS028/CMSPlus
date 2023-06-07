ALTER TABLE BlogComments
    ADD ParentCommentId INT;
ALTER TABLE BlogComments
    ADD CONSTRAINT FK_Comments_Comments FOREIGN KEY (ParentCommentId)
        REFERENCES BlogComments (Id);

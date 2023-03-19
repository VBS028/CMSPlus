ALTER TABLE BlogComments
DROP CONSTRAINT FK_BlogComments_AspNet_Users;

ALTER TABLE BlogComments
DROP COLUMN UserId;

ALTER TABLE BlogComments
ADD Username nvarchar(450) NOT NULL
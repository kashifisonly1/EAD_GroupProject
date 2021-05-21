CREATE PROCEDURE [dbo].[AddMessageToContactUs]
	 @Email nvarchar(256)
	,@Subject nvarchar(256)
	,@Message nvarchar(max)
	,@Name nvarchar(256)

AS

INSERT INTO [ContactUs]([Email], [Subject], [Message], [Name])
VALUES(@Email, @Subject, @Message, @Name);
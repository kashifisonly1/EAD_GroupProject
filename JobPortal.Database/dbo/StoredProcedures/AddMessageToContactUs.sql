CREATE PROCEDURE [dbo].[AddMessageToContactUs]
	 @email nvarchar(256)
	,@subject nvarchar(256)
	,@message nvarchar(max)
	,@name nvarchar(256)

AS

INSERT INTO [ContactUs]([Email], [Subject], [Message], [Name])
VALUES(@email, @subject, @message, @name);
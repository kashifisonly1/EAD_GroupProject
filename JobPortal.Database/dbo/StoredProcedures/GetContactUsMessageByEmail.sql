CREATE PROCEDURE [dbo].[GetContactUsMessagesByEmail]
	@Email nvarchar(250)
AS
	SELECT * FROM [ContactUs]
	WHERE [Email] = @Email;

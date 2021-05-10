CREATE PROCEDURE [dbo].[GetContactUsMessageByEmail]
	@Email nvarchar(250)
AS
	SELECT TOP 1 * FROM [ContactUs]
	WHERE [Email] = @Email;

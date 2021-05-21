CREATE PROCEDURE [dbo].[DeleteContactUsMessageById]
	@Id int
AS
	DELETE FROM [dbo].[ContactUs]
	WHERE [Id] = @Id

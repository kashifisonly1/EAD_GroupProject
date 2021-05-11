CREATE PROCEDURE [dbo].[ResolveContactUsMessageById]
	@Id int
AS
	UPDATE [dbo].[ContactUs]
	SET [IsResponded] = 1
	WHERE [Id] = @Id

CREATE PROCEDURE [dbo].[GetAllUnResolvedMessages]
AS
	SELECT * FROM [dbo].[ContactUs]
	WHERE [IsResponded] = 0

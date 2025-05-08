IF OBJECT_ID('dbo.AddBook_SP', 'P') IS NOT NULL
    DROP PROCEDURE dbo.AddBook_SP;

GO
CREATE PROCEDURE AddBook_SP
    @Title NVARCHAR(200),
    @Author NVARCHAR(200),
    @ISBN NVARCHAR(50),
    @Genre BIT,
    @Format BIT,
    @Availability BIT,
    @PublishedDate DATE,
    @Description NVARCHAR(MAX)
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO Books (Title, Author, ISBN, Genre, Format, Availability, PublishedDate, Description)
    VALUES (@Title, @Author, @ISBN, @Genre, @Format, @Availability, @PublishedDate, @Description);

    SELECT CAST(SCOPE_IDENTITY() AS INT); -- return the newly inserted Id
END;
GO
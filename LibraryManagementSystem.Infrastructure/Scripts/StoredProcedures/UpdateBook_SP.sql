IF OBJECT_ID('dbo.UpdateBook_SP', 'P') IS NOT NULL
    DROP PROCEDURE dbo.UpdateBook_SP;

GO
CREATE PROCEDURE UpdateBook_SP
    @Id INT,
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
    UPDATE Books
    SET 
        Title = @Title,
        Author = @Author,
        ISBN = @ISBN,
        Genre = @Genre,
        Format = @Format,
        Availability = @Availability,
        PublishedDate = @PublishedDate,
        Description = @Description
    WHERE Id = @Id;
END;
GO
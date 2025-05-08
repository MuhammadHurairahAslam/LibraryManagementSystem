CREATE TABLE Books (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Title NVARCHAR(200) NULL,
    Author NVARCHAR(200) NULL,
    ISBN NVARCHAR(50) NULL,
    Genre INT NOT NULL,           -- Enum: BookGenre
    Format INT NOT NULL,          -- Enum: BookFormat
    Availability INT NOT NULL,    -- Enum: BookAvailability
    PublishedDate DATE NOT NULL,
    Description NVARCHAR(MAX) NULL
);

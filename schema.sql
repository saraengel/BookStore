USE [master];
GO

-- יצירת מסד הנתונים
IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'BookStore')
BEGIN
    CREATE DATABASE [BookStore];
END
GO

USE [BookStore];
GO

-- יצירת טבלת משתמשים
CREATE TABLE Users (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    UserName NVARCHAR(255) NOT NULL,
    Password NVARCHAR(255) NOT NULL,
    Email NVARCHAR(255) NOT NULL UNIQUE
);
GO

-- יצירת טבלת ספרים
CREATE TABLE Books (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Title NVARCHAR(255) NOT NULL,
    Description NVARCHAR(500) NOT NULL,
    PublishedDate DATETIME2 NOT NULL,
    Price DECIMAL(10,2) NOT NULL,
    Amount INT NOT NULL
);
GO

-- יצירת טבלת הזמנות (שינוי שם מ-Order כי זו מילה שמורה)
CREATE TABLE Orders (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    BookId INT NOT NULL,
    UserId INT NOT NULL,
    Amount INT NOT NULL,
    Status INT NULL,
    IsValid BIT NOT NULL,
    CONSTRAINT FK_Orders_Users FOREIGN KEY (UserId) REFERENCES Users(Id) ON DELETE CASCADE,
    CONSTRAINT FK_Orders_Books FOREIGN KEY (BookId) REFERENCES Books(Id) ON DELETE CASCADE
);
GO

-- הכנסת נתוני דוגמה
INSERT INTO Users (UserName, Password, Email) VALUES 
('moshe', 'm123', 'suriengel10@bookstore.com');
GO

INSERT INTO Books (Title, Description, PublishedDate, Price, Amount) VALUES 
('HTML', 'HTML for beginners', '1925-02-02', 39.99, 30),
('REACT', 'React for beginners', '2020-02-02', 29.99, 20);
GO

INSERT INTO Orders (BookId, UserId, Amount, Status, IsValid) VALUES 
(1, 1, 3, NULL, 1),
(2, 1, 3, NULL, 1);
GO

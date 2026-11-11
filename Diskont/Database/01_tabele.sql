-- Vežbe 3: tabele baze DiskontDB
-- Pokrenuti nad praznom bazom DiskontDB u (localdb)\MSSQLLocalDB

CREATE TABLE Roles (
    IdRole  INT IDENTITY(1,1) PRIMARY KEY,
    Name    NVARCHAR(MAX) NOT NULL
);

CREATE TABLE Users (
    IdUser       INT IDENTITY(1,1) PRIMARY KEY,
    FirstName    NVARCHAR(MAX) NOT NULL,
    LastName     NVARCHAR(MAX) NOT NULL,
    Email        NVARCHAR(MAX) NOT NULL,
    PasswordHash NVARCHAR(MAX) NOT NULL,
    Status       BIT NOT NULL,
    CreatedDate  DATETIME NOT NULL DEFAULT GETDATE(),
    IdRole       INT NOT NULL FOREIGN KEY REFERENCES Roles(IdRole)
);

CREATE TABLE Categories (
    IdCategory INT IDENTITY(1,1) PRIMARY KEY,
    Name       NVARCHAR(MAX) NOT NULL
);

CREATE TABLE Products (
    IdProduct  INT IDENTITY(1,1) PRIMARY KEY,
    Name       NVARCHAR(MAX) NOT NULL,
    Price      DECIMAL(18,2) NOT NULL,
    Count      INT NOT NULL,
    IdCategory INT NOT NULL FOREIGN KEY REFERENCES Categories(IdCategory)
);

CREATE TABLE Orders (
    IdOrder    INT IDENTITY(1,1) PRIMARY KEY,
    IdUser     INT NOT NULL FOREIGN KEY REFERENCES Users(IdUser),
    OrderDate  DATETIME NOT NULL DEFAULT GETDATE(),
    TotalPrice DECIMAL(18,2) NOT NULL
);

CREATE TABLE OrderItems (
    IdOrderItem INT IDENTITY(1,1) PRIMARY KEY,
    IdOrder     INT NOT NULL FOREIGN KEY REFERENCES Orders(IdOrder),
    IdProduct   INT NOT NULL FOREIGN KEY REFERENCES Products(IdProduct),
    Quantity    INT NOT NULL,
    UnitPrice   DECIMAL(18,2) NOT NULL
);

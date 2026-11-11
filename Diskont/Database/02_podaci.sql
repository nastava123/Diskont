-- Vežbe 3: početni podaci

INSERT INTO Roles (Name) VALUES (N'Administrator'), (N'Kupac');

INSERT INTO Users (FirstName, LastName, Email, PasswordHash, Status, IdRole)
VALUES
    (N'Petar', N'Petrović',  'petar.p@example.com', 'hashedPassword123', 1, 2),
    (N'Ana',   N'Jovanović', 'ana.j@example.com',   'hashedPassword456', 1, 1);

INSERT INTO Categories (Name)
VALUES (N'Mlečni proizvodi'), (N'Pekara'), (N'Napici');

INSERT INTO Products (Name, Price, Count, IdCategory) VALUES
    (N'Mleko 1l', 129.99, 40, 1),
    (N'Jogurt',    89.50,  0, 1),
    (N'Hleb',      79.00, 25, 2),
    (N'Kafa 200g', 499.00, 12, 3),
    (N'Sok od jabuke', 159.00, 30, 3);

INSERT INTO Orders (IdUser, TotalPrice) VALUES (1, 758.98);

INSERT INTO OrderItems (IdOrder, IdProduct, Quantity, UnitPrice) VALUES
    (1, 1, 2, 129.99),
    (1, 4, 1, 499.00);

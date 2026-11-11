-- Vežbe 5: korpa
-- Porudžbina sa IsCompleted = 0 je korpa, a sa IsCompleted = 1 završena porudžbina.

ALTER TABLE Orders ADD IsCompleted BIT NOT NULL DEFAULT 0;
GO

-- porudžbine koje već postoje su završene
UPDATE Orders SET IsCompleted = 1;
GO

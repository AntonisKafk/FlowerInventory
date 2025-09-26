-- Seed data for Categories table
INSERT INTO FlowerCategories (Name) VALUES
('Roses'),
('Tulips'),
('Daisies');
GO

-- Seed data for Flowers table
INSERT INTO Flowers (Name, CategoryId, Price) VALUES
('Red Rose', 1, 2.50),
('White Rose', 1, 2.75),
('Yellow Tulip', 2, 1.80),
('Pink Tulip', 2, 1.90),
('White Daisy', 3, 1.20),
('Yellow Daisy', 3, 1.30);
GO
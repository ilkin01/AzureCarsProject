INSERT INTO [Users] (PhoneNumber, Fullname, PostCount)
VALUES ('1234567890', 'John Doe', 5),
       ('9876543210', 'Jane Smith', 10),
       ('5555555555', 'Alice Johnson', 3);

-- Inserting data into Finance table
INSERT INTO [Finances] (Price, CurrencyType, IsCredit, IsBarter, IsGuarantee)
VALUES (25000, 'USD', 1, 0, 1),
       (15000, 'EUR', 0, 1, 0),
       (30000, 'GBP', 1, 1, 1);

-- Inserting data into Status table
INSERT INTO [Statuses] (IsHit, IsPaint)
VALUES (1, 0),
       (0, 1),
       (1, 1);

-- Inserting data into Engine table
INSERT INTO [Engines] (EngineCapacity, EnginePower, FuelType)
VALUES (1500, 100, 'Gasoline'),
       (2000, 120, 'Diesel'),
       (1800, 110, 'Hybrid');

-- Inserting data into Main table
INSERT INTO [Mains] (Vendor, Model, SubModel, CarType, Year, Kilometer, Color)
VALUES ('Toyota', 'Camry', 'SE', 'Sedan', 2018, 30000, 'Blue'),
       ('Honda', 'Accord', 'EX', 'Sedan', 2019, 25000, 'Silver'),
       ('Ford', 'Mustang', 'GT', 'Coupe', 2020, 15000, 'Red');

-- Inserting data into Car table
INSERT INTO [Cars] (MarketPlace, VINCode, SeatCount, OwnerCount, City, About, Transmission, TractionType, MainId, EngineId, StatusId, FinanceId, OwnerId)
VALUES ('CarMarket', 'ABC123456789', 5, 2, 'New York', 'This is a great car', 'Automatic', '4WD', 1, 1, 1, 1, 1),
       ('AutoSales', 'DEF987654321', 4, 1, 'Los Angeles', 'Excellent condition', 'Manual', 'RWD', 2, 2, 2, 2, 2),
       ('CarShop', 'GHI789012345', 2, 3, 'Chicago', 'Well-maintained', 'CVT', 'FWD', 3, 3, 3, 3, 3);

INSERT INTO [Posts] ([Date], [IsBoosted], [IsVip], [IsPremium], [UserId], [ViewCount], [CarId])
VALUES (GETDATE(), 1, 0, 1, 1, 100, 1);

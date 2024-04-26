﻿CREATE TABLE Notifications(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	[Vendor] NVARCHAR(100) NOT NULL,
	[Model] NVARCHAR(100),
	[UserId] INT FOREIGN KEY REFERENCES [Users](Id) ON DELETE CASCADE NOT NULL
)
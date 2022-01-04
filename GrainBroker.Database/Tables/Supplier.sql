﻿CREATE TABLE Supplier(
Id             UNIQUEIDENTIFIER NOT NULL,
LocationId     UNIQUEIDENTIFIER	NOT NULL
CONSTRAINT PK_Supplier PRIMARY KEY (Id)
CONSTRAINT FK_Supplier_Location FOREIGN KEY (LocationId) REFERENCES Location(Id)
);
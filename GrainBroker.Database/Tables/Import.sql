CREATE TABLE Import(
Id             UNIQUEIDENTIFIER NOT NULL,
FileName       Varchar(60)      NOT NULL,
ImportDate	   DateTime			NoT NULL	
CONSTRAINT PK_Import PRIMARY KEY (Id)
);
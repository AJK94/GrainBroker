CREATE TABLE PurchaseOrder(
Id                    UNIQUEIDENTIFIER NOT NULL,
OrderDate             DATETIME         NOT NULL,
CustomerId            UNIQUEIDENTIFIER NOT NULL,
SupplierId            UNIQUEIDENTIFIER NOT NULL,
RequiredAmount        INT			   NOT NULL,
SuppliedAmount        INT			   NOT NULL,
DeliveryCost          DECIMAL(18,2)    NOT NULL,
ImportId			  UNIQUEIDENTIFIER NOT NULL
CONSTRAINT PK_PurchaseOrder PRIMARY KEY (Id)
CONSTRAINT FK_PurchaseOrder_Import FOREIGN KEY (ImportId) REFERENCES Import(Id)
);

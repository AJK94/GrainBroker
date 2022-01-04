CREATE TABLE PurchaseOrder(
Id                    UNIQUEIDENTIFIER NOT NULL,
OrderDate             DATETIME         NOT NULL,
CustomerId            UNIQUEIDENTIFIER NOT NULL,
SupplierId            UNIQUEIDENTIFIER NOT NULL,
OrderRequiredAmount   INT			   NOT NULL,
SuppliedAmount        INT			   NOT NULL,
CostOfDelivery        DECIMAL(18,2)    NOT NULL
CONSTRAINT PK_PurchaseOrder PRIMARY KEY (Id)
);

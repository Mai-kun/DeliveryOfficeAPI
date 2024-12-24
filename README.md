```mermaid
erDiagram
    Bills {
        guid Id PK
        datetimeoffset Date
        string Warehouse
        int TotalAmount
        boolean IsPaid
        guid BuyerId FK
        guid SupplierId FK
        datetimeoffset CreatedAt
        datetimeoffset ModifiedAt
        boolean IsDeleted
    }

    Products {
        guid Id PK
        string Name
        int Quantity
        string Unit
        int Price
        datetimeoffset CreatedAt
        datetimeoffset ModifiedAt
        boolean IsDeleted
    }

    BillProduct {
      guid BillsId PK
      guid ProductsId PK
    }

    Buyers {
        guid Id PK
        string Name
        datetimeoffset CreatedAt
        datetimeoffset ModifiedAt
        boolean IsDeleted
    }

    Suppliers {
        guid Id PK
        string Name
        string Address
        datetimeoffset CreatedAt
        datetimeoffset ModifiedAt
        boolean IsDeleted
    }

    Buyers ||--o{ Bills : ""
    Suppliers ||--o{ Bills : ""
    Bills ||--o{ BillProduct : ""
    Products ||--o{ BillProduct : ""
```

```SQL
INSERT INTO Suppliers (Id, Name, Address, CreatedAt, ModifiedAt, IsDeleted)
VALUES 
(NEWID(), 'Supplier A', 'Market Street, 123', GETDATE(), NULL, 0),
(NEWID(), 'Supplier B', 'Central Avenue, 456', GETDATE(), NULL, 0),
(NEWID(), 'Supplier C', 'Industrial Park, 789', GETDATE(), NULL, 0),
(NEWID(), 'Supplier D', 'Warehouse Street, 321', GETDATE(), NULL, 0),
(NEWID(), 'Supplier E', 'Outskirts Street, 654', GETDATE(), NULL, 0)
GO
INSERT INTO Buyers (Id, Name, CreatedAt, ModifiedAt, IsDeleted)
VALUES 
(NEWID(), 'Buyer 1', GETDATE(), NULL, 0),
(NEWID(), 'Buyer 2', GETDATE(), NULL, 0),
(NEWID(), 'Buyer 3', GETDATE(), NULL, 0),
(NEWID(), 'Buyer 4', GETDATE(), NULL, 0),
(NEWID(), 'Buyer 5', GETDATE(), NULL, 0);
GO
INSERT INTO Products (Id, Name, Quantity, Unit, Price, CreatedAt, ModifiedAt, IsDeleted)
VALUES 
(NEWID(), 'Product A', 100, 'pcs', 15.99, GETDATE(), NULL, 0),
(NEWID(), 'Product B', 200, 'pcs', 25.49, GETDATE(), NULL, 0),
(NEWID(), 'Product C', 300, 'pcs', 35.00, GETDATE(), NULL, 0),
(NEWID(), 'Product D', 150, 'pcs', 10.75, GETDATE(), NULL, 0),
(NEWID(), 'Product E', 250, 'pcs', 5.50, GETDATE(), NULL, 0);
GO
DECLARE @Supplier1 UNIQUEIDENTIFIER = (SELECT Id FROM Suppliers ORDER BY Id OFFSET 0 ROWS FETCH NEXT 1 ROWS ONLY);
DECLARE @Supplier2 UNIQUEIDENTIFIER = (SELECT Id FROM Suppliers ORDER BY Id OFFSET 1 ROWS FETCH NEXT 1 ROWS ONLY);
DECLARE @Supplier3 UNIQUEIDENTIFIER = (SELECT Id FROM Suppliers ORDER BY Id OFFSET 2 ROWS FETCH NEXT 1 ROWS ONLY);
DECLARE @Supplier4 UNIQUEIDENTIFIER = (SELECT Id FROM Suppliers ORDER BY Id OFFSET 3 ROWS FETCH NEXT 1 ROWS ONLY);
DECLARE @Supplier5 UNIQUEIDENTIFIER = (SELECT Id FROM Suppliers ORDER BY Id OFFSET 4 ROWS FETCH NEXT 1 ROWS ONLY);
DECLARE @Buyer1 UNIQUEIDENTIFIER = (SELECT Id FROM Buyers ORDER BY Id OFFSET 0 ROWS FETCH NEXT 1 ROWS ONLY);
DECLARE @Buyer2 UNIQUEIDENTIFIER = (SELECT Id FROM Buyers ORDER BY Id OFFSET 1 ROWS FETCH NEXT 1 ROWS ONLY);
DECLARE @Buyer3 UNIQUEIDENTIFIER = (SELECT Id FROM Buyers ORDER BY Id OFFSET 2 ROWS FETCH NEXT 1 ROWS ONLY);
DECLARE @Buyer4 UNIQUEIDENTIFIER = (SELECT Id FROM Buyers ORDER BY Id OFFSET 3 ROWS FETCH NEXT 1 ROWS ONLY);
INSERT INTO Bills (Id, Date, Warehouse, TotalAmount, IsPaid, BuyerId, SupplierId, CreatedAt, ModifiedAt, IsDeleted)
VALUES 
(NEWID(), GETDATE(), 'Warehouse A', 500.00, 1, @Buyer1, @Supplier1, GETDATE(), NULL, 0),
(NEWID(), GETDATE(), 'Warehouse B', 100.00, 0, @Buyer2, @Supplier2, GETDATE(), NULL, 0),
(NEWID(), GETDATE(), 'Warehouse C', 750.00, 1, @Buyer3, @Supplier3, GETDATE(), NULL, 0),
(NEWID(), GETDATE(), 'Warehouse D', 300.00, 0, @Buyer4, @Supplier4, GETDATE(), NULL, 0),
(NEWID(), GETDATE(), 'Warehouse E', 200.00, 1, @Buyer4, @Supplier5, GETDATE(), NULL, 0);
GO
DECLARE @Bill1 UNIQUEIDENTIFIER = (SELECT Id FROM Bills ORDER BY Id OFFSET 0 ROWS FETCH NEXT 1 ROWS ONLY);
DECLARE @Bill2 UNIQUEIDENTIFIER = (SELECT Id FROM Bills ORDER BY Id OFFSET 1 ROWS FETCH NEXT 1 ROWS ONLY);
DECLARE @Bill3 UNIQUEIDENTIFIER = (SELECT Id FROM Bills ORDER BY Id OFFSET 2 ROWS FETCH NEXT 1 ROWS ONLY);
DECLARE @Bill4 UNIQUEIDENTIFIER = (SELECT Id FROM Bills ORDER BY Id OFFSET 3 ROWS FETCH NEXT 1 ROWS ONLY);
DECLARE @Bill5 UNIQUEIDENTIFIER = (SELECT Id FROM Bills ORDER BY Id OFFSET 4 ROWS FETCH NEXT 1 ROWS ONLY);
DECLARE @Product1 UNIQUEIDENTIFIER = (SELECT Id FROM Products ORDER BY Id OFFSET 0 ROWS FETCH NEXT 1 ROWS ONLY);
DECLARE @Product2 UNIQUEIDENTIFIER = (SELECT Id FROM Products ORDER BY Id OFFSET 1 ROWS FETCH NEXT 1 ROWS ONLY);
DECLARE @Product3 UNIQUEIDENTIFIER = (SELECT Id FROM Products ORDER BY Id OFFSET 2 ROWS FETCH NEXT 1 ROWS ONLY);
DECLARE @Product4 UNIQUEIDENTIFIER = (SELECT Id FROM Products ORDER BY Id OFFSET 3 ROWS FETCH NEXT 1 ROWS ONLY);
DECLARE @Product5 UNIQUEIDENTIFIER = (SELECT Id FROM Products ORDER BY Id OFFSET 4 ROWS FETCH NEXT 1 ROWS ONLY);
INSERT INTO BillProduct (BillsId, ProductsId)
VALUES 
(@Bill1, @Product1),
(@Bill1, @Product2),
(@Bill1, @Product3),
(@Bill2, @Product4),
(@Bill2, @Product5),
(@Bill3, @Product3),
(@Bill4, @Product1),
(@Bill5, @Product5)

```

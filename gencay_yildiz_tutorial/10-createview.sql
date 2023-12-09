USE Northwind

IF OBJECT_ID('view_name', 'V') IS NOT NULL
    DROP VIEW view_name;
GO

CREATE VIEW view_name AS 
SELECT Adi, Soyadi 
FROM Personeller 
WHERE PersonelID > 0;
GO
SELECT * from view_name
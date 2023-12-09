USE Northwind

--Group By
Select * from Urunler

Select KategoriID, COUNT(*) from Urunler group by KategoriID

Select PersonelID, COUNT(*) from Satislar group by PersonelID

Select PersonelID, SUM(NakliyeUcreti) from Satislar group by PersonelID

--Group by and where

Select KategoriID, COUNT(*) from Urunler where KategoriID < 5 group by KategoriID 

Select PersonelID, COUNT(*) from Satislar where personelID < 4 group by PersonelID 

Select PersonelID, SUM(NakliyeUcreti) from Satislar  group by PersonelID


--Group by and having, used aggregate functions
Select KategoriID, COUNT(*) from Urunler  group by KategoriID having COUNT(*) > 6


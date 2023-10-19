USE Northwind

--Select and where

Select * from Personeller where Sehir = 'London'

Select * from Personeller where BagliCalistigiKisi < 5

-- and operator
Select * from Personeller where Sehir = 'London' and Ulke = 'UK'

-- or operator 
Select * from Personeller where UnvanEki = 'Mr.' or Sehir = 'Seattle'

-- <> Not equal
-- = equal
-- <= less than or equal to
-- >= greater than or equal to 

Select * from Personeller where Bolge <> 'NULL'

-- WHERE & BETWEEN

Select * from Personeller where YEAR(DogumTarihi) between 1950 and 1965

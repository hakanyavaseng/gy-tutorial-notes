USE Northwind

-- STRING FUNCTIONS

-- LEFT

Select LEFT(adi,2) from Personeller

-- RIGHT

Select RIGHT(adi,2) from Personeller

--UPPER
Select UPPER(adi) from Personeller

--LOWER
Select LOWER(adi) from Personeller

--SUBSTRING
Select SUBSTRING(SoyAdi,3,2) from Personeller

--RTRIM & LTRIM
Select 'YAVAS         '
Select RTRIM('YAVAS           ')


--REVERSE
Select REVERSE(adi) from Personeller


-- REPLACE
Select REPLACE('Hakan Yavas', 'Hakan', 'Alperen')

-- CHARINDEX
Select musteriAdi, CHARINDEX('r', MusteriAdi) from Musteriler


-- EX1
select MusteriAdi from Musteriler
Select SUBSTRING(MusteriAdi,0, CHARINDEX(' ', MusteriAdi)) from Musteriler --gets customers' names

-- EX2
Select SUBSTRING(MusteriAdi, CHARINDEX(' ', MusteriAdi), LEN(MusteriAdi) - (CHARINDEX(' ', MusteriAdi) - 1)) from Musteriler 



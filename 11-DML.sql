--DML
-- SELECT INSERT UPDATE DELETE

--SELECT 
Select * from Personeller

--INSERT
--Insert TABLE_NAME (COLUMNS) VALUES (DEGERLER)
Insert Personeller(Adi,SoyAdi) Values ('Hakan','Yavas')
Insert Personeller Values('Yavas', 'Hakan', 'Developer', 'Mr.', '04.01.2002', GETDATE(), 'Eskisehir', 'Eskisehir', 'Ic Anadolu', '26000', 'TR', '5430000000', NULL,NULL,NULL,NULL,NULL)

--Can be written with INTO command
--Insert Into Personeller Values('Yavas', 'Hakan', 'Developer', 'Mr.', '04.01.2002', GETDATE(), 'Eskisehir', 'Eskisehir', 'Ic Anadolu', '26000', 'TR', '5430000000', NULL,NULL,NULL,NULL,NULL)

--Not Null and lengths must be considered.

Insert Personeller(Unvan,UnvanEki) Values('a','b') -- -> Cannot insert the value NULL into column 'SoyAdi', table 'Northwind.dbo.Personeller'; column does not allow nulls. INSERT fails.

--Identity columns can not be assigned.

--Belirtilen tablolara deger gonderilmedilir.
Insert Personeller(Adi,Soyadi) values ('Yavas') -- -> There are more columns in the INSERT statement than values specified in the VALUES clause. The number of values in the VALUES clause must match the number of columns specified in the INSERT statement.
Insert Personeller values ('Yavas','Hakan') -- -> Column name or number of supplied values does not match table definition.

--Trick point
Insert Musteriler(MusteriID,MusteriAdi,Adres) Values ('MHMYZ','Mehmet', 'Yozgat'),('NCCNKR','Necati', 'Cankiri')

--Saving the values to new table from another table
Create TABLE OrnekPersoneller 
(
adi varchar(50),
soyadi varchar(50)
)
Insert OrnekPersoneller Select Adi, Soyadi from Personeller

--Ex
Select Adi,Soyadi, Ulke into OrnekPersoneller2 from Personeller

--Deleting additional tables 
DROP Table OrnekPersoneller, OrnekPersoneller2
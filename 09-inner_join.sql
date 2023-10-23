USE Northwind

--Inner Join

--Hangi personeller hangi satisi yapmistir
Select * from Personeller p Inner Join Satislar s on p.PersonelID = s.PersonelID

--Which prodcut, which cat.
Select u.UrunAdi, k.KategoriAdi from Urunler u Inner Join Kategoriler k on u.KategoriID = k.KategoriID

--Where command & inner join

Select u.UrunAdi from Urunler u Inner Join Kategoriler k on u.KategoriID = k.KategoriID where k.KategoriAdi = 'Beverages'

Select COUNT(u.UrunAdi) from Urunler u inner join Kategoriler k on k.KategoriID = u.KategoriID where k.KategoriAdi = 'Beverages'

--Which employee-which sale

Select s.SatisID, p.Adi + ' ' + p.SoyAdi from Satislar s inner join Personeller p on p.PersonelID = s.PersonelID

--
Select u.UrunAdi from Urunler u inner join Tedarikciler t on u.TedarikciID = t.TedarikciID where t.Faks <> 'NULL'

Select u.UrunAdi from Urunler u inner join Tedarikciler t on u.TedarikciID = t.TedarikciID where t.Faks is not null

----------------------------------------------------------------


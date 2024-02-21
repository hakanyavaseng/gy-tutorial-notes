USE Northwind

--Inner Join

--Hangi personeller hangi satisi yapmistir
Select * from Personeller p Inner Join Satislar s on p.PersonelID = s.PersonelID

--Which prodcut, which category.
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

-- To union more than two tables with relational


-- Nancy's sales after 1997 
Select * from Personeller p inner join Satislar s on p.PersonelID = s.PersonelID inner join Musteriler m on s.MusteriID = m.MusteriID
where p.Adi = 'Nancy' and YEAR(s.satisTarihi) > 1997

--Limited tedarikci, seafood and total amount of sales

Select SUM(u.hedefStokDuzeyi * u.BirimFiyati) from Urunler u Inner Join Tedarikciler t on 
u.TedarikciID = t.TedarikciID Inner Join Kategoriler k on u.KategoriID = k.KategoriID 
where t.SirketAdi like '%Ltd.%' and k.KategoriAdi = 'Seafood'


-- To union same table 
Select p1.Adi as kisi, p2.Adi as bagliOlduguKisi from Personeller p1 inner join personeller p2 on p1.BagliCalistigiKisi = p2.PersonelID


-- Inner Join  & Group By

Select KategoriID, COUNT(*) from Urunler group by KategoriID


-- Ex 1
Select p.Adi + ' ' + p.Soyadi, COUNT(s.satisID) from Personeller p inner join Satislar s on p.PersonelID = s.PersonelID 
where p.Adi like 'm%' group by p.Adi + ' ' + p.SoyAdi having COUNT(s.satisID) > 100

-- Ex 2
Select k.kategoriAdi, COUNT(u.urunAdi) as adet from Urunler u inner join Kategoriler k on u.KategoriID = k.KategoriID 
where k.KategoriAdi = 'Seafood' group by KategoriAdi

-- Ex 3 
Select  p.Adi, COUNT(s.satisID) from Personeller p inner join Satislar s on p.PersonelID = s.PersonelID group by p.Adi

-- Ex 4
Select p.Adi, COUNT(s.satisID) from Personeller p inner join satislar s on s.PersonelID = p.PersonelID group by p.Adi order by COUNT(s.satisID) desc

-- Ex 5
Select SUM(sd.BirimFiyati * sd.miktar), s.satisTarihi from Personeller p inner join satislar s on p.PersonelID = s.PersonelID inner join 
[Satis Detaylari] sd on s.SatisID = sd.SatisID where adi like '%a%' and s.SatisID > 10500 group by s.SatisTarihi
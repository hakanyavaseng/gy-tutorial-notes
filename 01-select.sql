USE Northwind

--Select Command

Select 3 

Select 'Hakan'

Select 3,5,7

Select 'Hakan', 'Yavas', 21

Select * from Personeller

Select Adi, Soyadi from Personeller

-- Alias

Select 3 as Deger

Select 3 Deger

Select 'Hakan' Adý, 'Yavas' Soyadý

Select Adi Names, SoyAdi Surnames from Personeller

-- Alias with space char

Select 2002 [My Birthday]

-- Table name with space char

Select * from [Satis Detaylari]

--Union columns

Select Adi, Soyadi from Personeller
Select Adi + ' ' + SoyAdi [Employee Infos] from Personeller

-- Unions columns with different types

Select Adi + ' ' + IseBaslamaTarihi from Personeller

Select Adi + ' ' + CONVERT(nvarchar, IseBaslamaTarihi) [Union] from Personeller






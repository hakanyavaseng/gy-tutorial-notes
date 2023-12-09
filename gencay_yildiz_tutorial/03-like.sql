USE Northwind

--Like 

-- % operator 

Select Adi,Soyadi from Personeller where Adi like 'j%'

Select Adi,Soyadi from Personeller where Adi like '%y'

Select * from Personeller where adi like '%ert'

Select * from Personeller where adi like 'r%t'

-- unnecessary usage
Select * from Personeller where adi like 'r%' and adi like '%t'

Select * from Personeller where Adi like '%an%'

Select * from Personeller where adi like 'n%an%'

-- _ operator

Select * from Personeller where adi like 'a_n%' 

Select * from Personeller where adi like 'm___a%'

-- [] (ya da) operator

Select * from Personeller where adi like '[nmr]%' -- Brings employees which their first name's first char n or m or r 

Select * from Personeller where adi like '%[ai]%' 

-- [*-*] operator (alphabet)

Select adi from Personeller where adi like '[a-k]%'

-- [^*] operator (degil operatoru)

Select * from Personeller where adi like '[^a]%'



Select * from Personeller where adi like '[^an]%'


-- Like & Escape Characters
-- The operators which we use in like queries, if data contains %, _, [] etc. operators we can get error.
-- Then we need escape characters.

-- [] operator
-- Escape command

Select * from Personeller where adi like '[_]%' 

Select * from Personeller where adi like '\_%' Escape '\'
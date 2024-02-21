--FUNCTIONS IN TSQL--

--STRING FUNCTIONS

	--ASCII
	SELECT ASCII('A') --65

	--CHAR
	SELECT CHAR(65) --A

	--CONCAT
	SELECT CONCAT('Hakan', ' ', 'Yavas') --SELECT 'Hakan' + ' ' + 'Yavas'

	--LEFT & RIGHT
	SELECT LEFT('Hakan Yavas', 5) -- Hakan
	SELECT RIGHT('Hakan Yavas', 5) -- Yavas

	--TRIM
	SELECT LTRIM('          Hakan Yavas')
	SELECT RTRIM('Hakan Yavas        ')
	SELECT TRIM('       Hakan Yavas         ')
	SELECT TRIM(LEADING 'Hakan' FROM 'Hakan Yavas') -- Yavas
	SELECT TRIM(TRAILING 'Yavas' FROM 'Hakan Yavas') -- Hakan 
	SELECT TRIM(BOTH '#' FROM '#Hakan Yavas#') -- Hakan Yavas

	--LOWER,UPPER,REVERSE,REPLICATE
	SELECT LOWER('HAKAN YAVAS') -- hakan yavas
	SELECT UPPER('hakan yavas') -- HAKAN YAVAS
	SELECT REVERSE('Hakan Yavas') -- savaY nakaH  //Generally used for performance optimization in search operations.
	SELECT REPLICATE('Hakan',2) -- HakanHakan
	
	--REPLACE
	SELECT REPLACE('T-SQL is good!', 'T-SQL', 'Transact SQL') -- Transact SQL is good!

	--SUBSTRING
	SELECT SUBSTRING('SQL Server 2022 is good!',1,15) -- SQL Server 2022

	--CHARINDEX
	SELECT CHARINDEX('2022', 'SQL Server 2022') --12

	--!STRING_SPLIT! --Returns a table.
	SELECT * FROM string_split('Hakan Yavas', ' ')
	--value
	--Hakan
	--Yavas
	
	SELECT * FROM string_split('�stanbul,Ankara,�zmir', ',')
	
	SELECT * FROM string_split('Hakan Yavas Eskisehir Osmangazi University', ' ', 1)
	--Hakan	1
	--Yavas	2
	--Eskisehir 3
	--Osmangazi	4
	--University 5


--DATETIME FUNCTIONS

	--CURRENT DATE TIME
	SELECT GETDATE() GETDATE_, SYSDATETIME() SYSDATETIME_, 
	SYSDATETIMEOFFSET() SYSDTOFFSET, SYSUTCDATETIME() SYSUTCDT,
	CURRENT_TIMESTAMP CURRENTTIMESTAMP, GETUTCDATE() GETUTCDATE

	--DATEPART
	--2023-12-29 09:55:56.433
	SELECT
	DATEPART(YEAR, '2023-12-29 09:55:56.433') YEAR_,
	DATEPART(DAY, '2023-12-29 09:55:56.433') DAY_,
	DATEPART(MONTH, '2023-12-29 09:55:56.433') MONTH_,
	DATEPART(HOUR, '2023-12-29 09:55:56.433') HOUR_,
	DATEPART(MINUTE, '2023-12-29 09:55:56.433') MINUTE_,
	DATEPART(SECOND, '2023-12-29 09:55:56.433') SECOND_,
	DATEPART(WEEK,'2023-12-29 09:55:56.433') WEEK_

	--DATENAME
	SELECT
	DATENAME(YEAR, '2023-12-29 09:55:56.433') YEAR_, --2023
	DATENAME(MONTH, '2023-12-29 09:55:56.433') MONTH_, --December
	DATENAME(DW, '2023-12-29 09:55:56.433') DAYOFWEEK_ --Friday

	--DATE/TIME FROMPARTS
	SELECT DATEFROMPARTS(2023,12,29) -- 2023-12-29
	SELECT DATETIMEFROMPARTS(2023,12,29,10,07,56,433) -- 2023-12-29 10:07:56.433
	SELECT TIMEFROMPARTS(10,08,56,25,5) -- 10:08:56.00025

	--DAY/MONTH/YEAR
	SELECT YEAR(GETDATE()),MONTH(GETDATE()),DAY(GETDATE())
	
	--DATEDIFF
	SELECT DATEDIFF(YEAR, '2001-10-05', GETDATE()) -- 22 , 2023-12-29 10:14:09.800 - 2001-10-05

	--DATEADD
	SELECT DATEADD(YEAR, 10, '1997-09-17') --2007-09-17 00:00:00.000
	SELECT DATEADD(MONTH, 1, '1997-09-17') --1997-10-17 00:00:00.000
	SELECT DATEADD(DAY, 11, '1997-09-17') --1997-09-28 00:00:00.000

	--ISDATE
	SELECT ISDATE('2023-12-29') -- 1
	SELECT ISDATE('2023-29-12') -- 0
	SELECT ISDATE('2023-13-29') -- 0




	
	









--MATH FUNCTIONS

--TYPE CONVERSION FUNCTIONS

--SYSTEM FUNCTIONS
--�ART BLOKLARI

	-- IF 
	--CANNOT BE USED IN SELECT STATEMENTS
	
	DECLARE @EXAMRESULT AS INT = 70
	DECLARE @STATUS_ AS VARCHAR(20)

	IF @EXAMRESULT > 60
		SET @STATUS_ = 'BA�ARILI'
	ELSE 
		SET @STATUS_ =  'BA�ARISIZ'

	SELECT @EXAMRESULT, @STATUS_
	--

	DECLARE @STATUS VARCHAR(20)
	DECLARE @POINT AS INT = 2
	
	IF @POINT = 1
		SET @STATUS = '�OK K�T�'
	IF @POINT = 2
		SET @STATUS = 'K�T�'
	IF @POINT = 3
		SET @STATUS = 'NE �Y� NE K�T�'
	IF @POINT = 4
		SET @STATUS = '�Y�'
	IF @POINT = 5
		SET @STATUS = '�OK �Y�'

	SELECT @POINT, @STATUS
	

	--IIF
	--CAN BE USED IN SELECT STATEMENTS, WORKS ONLY FOR 2 ASSESMENT

	
	DECLARE @CUSTOMERGENDER AS VARCHAR = 'K'
	SELECT IIF(@CUSTOMERGENDER ='K', 'Kad�n', 'Erkek')
	

	--CASE WHEN

	DECLARE @AGE AS INT=70

	SELECT 
	CASE 
		WHEN @AGE < 35 THEN 'GEN�'
		WHEN @AGE BETWEEN 35 AND 60 THEN 'ORTA'
		WHEN @AGE > 60 THEN 'YA�LI'
	END AS AGEGROUP







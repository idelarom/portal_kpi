USE [OPTracker]
GO
/****** Object:  StoredProcedure [dbo].[Sps_CalculoMargenBruto]    Script Date: 31/07/2017 06:31:20 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--exec [Sps_CalculoMargenBruto] @pFechaIni = '2014-01-01', @pFechaFin = '2014-12-31',@pAreas = '', @pCommando=2, @pLstEmpleados='ADELANGE'
--exec [Sps_CalculoMargenBruto_dummy] @pFechaIni = '2014-01-01', @pFechaFin = '2014-12-31',@pAreas = '', @pCommando=2, @pLstEmpleados='ADELANGE'

ALTER PROCEDURE [dbo].[Sps_CalculoMargenBruto]
		 @pCveOport		INT = NULL
		,@pAreas		VARCHAR(100) = ''
		,@pLstEmpleados VARCHAR(max) = ''
		,@pCommando		INT = NULL
		,@pFechaIni		DATETIME = NULL
		,@pFechaFin		DATETIME = NULL
AS
-- -------------------------------------------------------------------------------- 
-- Proyecto					: OpTracker.
-- Base de Datos			: OpTracker   
-- Tablas Involucradas		: Oportunidad,Usuarios,usuarios d,FabricaxOp
-- Stores Involucradas		: N/A
-- Funciones Involucradas	: N/A    
-- Módulo					: Calculo de Margen Bruto x Ingeniero
-- Objetivo					: Generar un reporte donde muestra el 10% del MargenBruto por Ingeniero
--    
-- Observaciones			: 
-- Query Prueba				: exec [Sps_CalculoMargenBruto_dummy] @pFechaIni = '2017-03-20', @pFechaFin = '2017-03-24',@pAreas = '', @pCommando=2, @pLstEmpleados='ADELANGE'
							  --exec [Sps_CalculoMargenBruto] @pFechaIni = '2017-03-20', @pFechaFin = '2017-03-24',@pAreas = '', @pCommando=1, @pLstEmpleados=''
--     
-- CONS	AUTOR							RFECHA		CC		MODIFICACION    
-- -------------------------------------------------------------------------------- 
-- 0001 Israel Ortiz Escobedo			20/Mar/2013			Creacion
-- -------------------------------------------------------------------------------- 
SET NOCOUNT ON;
-- -------------------------------------------------------------------------------- 
-- DECLARACION DE VARIABLES
-- -------------------------------------------------------------------------------- 
DECLARE @cveOport			INTEGER
DECLARE @FolioOp			VARCHAR(10)
DECLARE @NombreClie			VARCHAR(100)
DECLARE @usuRegAct			VARCHAR(50)
DECLARE @Nombre				VARCHAR(100)
DECLARE @TotalHorasOport	DECIMAL(18,4)
DECLARE @Horas				DECIMAL(18,4)
DECLARE @MargenBruto		DECIMAL(18,2)
DECLARE @Margen10			DECIMAL(18,2)
DECLARE @PorcentajeMargen	REAL
declare @FechaAct			datetime
-- -------------------------------------------------------------------------------- 
-- DECLARACION DE TABLAS TEMPORALES
-- -------------------------------------------------------------------------------- 
CREATE TABLE #tmpOportHoras(
	 cveOport		INTEGER
	,FolioOp		VARCHAR(10)
	,NombreClie		VARCHAR(100)
	,usuRegAct		VARCHAR(50)
	,Nombre			VARCHAR(100)
	,Horas			DECIMAL(18,4)
	,PorcHoras		DECIMAL(18,4)
	,PorcHorasNew	DECIMAL(18,4)
	,MargenBruto	DECIMAL(18,2)
	,Margen10		DECIMAL(18,2)
	,MontoIng		DECIMAL(18,2)
	,MontoIngNew	DECIMAL(18,2)
	,Color			VARCHAR(12)
	,Fecha			datetime
)

DECLARE @t1 TABLE 
    ( 
        t1 int 
    ) 
    
DECLARE @t2 TABLE 
    ( 
        t2 VARCHAR(100) 
    )   
    
-- -------------------------------------------------------------------------------- 
-- PROCESO
-- -------------------------------------------------------------------------------- 
BEGIN

	IF @pFechaFin IS NOT NULL 
		SET @pFechaFin = DATEADD(DAY,1,@pFechaFin) 
		
	IF @pAreas <> '' and @pLstEmpleados = ''
	BEGIN 
		WHILE CHARINDEX(',',@pAreas) > 0 
		BEGIN 
			INSERT INTO @t1 SELECT substring(@pAreas,1,(charindex(',',@pAreas)-1)) 
			SET @pAreas = substring(@pAreas,charindex(',',@pAreas)+1,len(@pAreas)) 
		END 
		INSERT INTO @t1 
		SELECT @pAreas 
    	END
	ELSE
	BEGIN 
		INSERT INTO @t1 
		SELECT 
			 Area
		FROM TblAreas 
		WHERE Activo = 1 
		ORDER BY NombreArea
	END
	
			IF @pLstEmpleados <> ''
	BEGIN 
		WHILE CHARINDEX(',',@pLstEmpleados) > 0 
		BEGIN 
			INSERT INTO @t2 SELECT substring(@pLstEmpleados,1,(charindex(',',@pLstEmpleados)-1)) 
			SET @pLstEmpleados = substring(@pLstEmpleados,charindex(',',@pLstEmpleados)+1,len(@pLstEmpleados)) 
		END 
		INSERT INTO @t2 
		SELECT @pLstEmpleados 
    	END
	ELSE
	BEGIN 
		INSERT INTO @t2 
		SELECT 
			 Area
		FROM TblAreas 
		WHERE Activo = 1 
		ORDER BY NombreArea

		--select * from @t2 
	END


	SELECT @PorcentajeMargen = PorcentajeMargen FROM dbo.tbl_Configuraciones WHERE Id_CNSC_Config = 8 

		SELECT 
			 oportunidad.cveoport
			,oportunidad.Folio_Op
			,oportunidad.NombCli
			,MargenBruto = (oportunidad.montofinal*(oportunidad.margenfinal/100))
			,Margen10 = ((oportunidad.montofinal*(oportunidad.margenfinal/100))*(@PorcentajeMargen/100)) --CAST(ROUND(((oportunidad.montofinal*(oportunidad.margenfinal/100))*.1),2) AS DECIMAL(18,2)) -- 10%
			,Oportunidad.FechaAct
		INTO #tmpOportMargen
		FROM dbo.Oportunidad Oportunidad
		RIGHT JOIN dbo.Usuarios Usuarios
		ON oportunidad.resp_m = usuarios.cveusuario
		RIGHT JOIN dbo.FabricaxOp FabricaxOp
		ON oportunidad.cveoport = FabricaxOp.cveoportunidad 
		WHERE Oportunidad.Cerrada = 'si' 
			AND FabricaxOp.LNPrincipal = 1
			AND Usuarios.Area IN (SELECT * FROM @t1)
			AND oportunidad.estatus = 1 
			AND oportunidad.CveOport = ISNULL(@pCveOport,oportunidad.CveOport)
			AND Oportunidad.FechaAct >= @pFechaIni
			AND oportunidad.fechaact < @pFechaFin
		ORDER BY Cveoport
			
	--HORAS REALES
	SELECT a.CveOport, a.Folio_Op, a.NombCli, b.usuRegAct, C.Nombre, SUM(ISNULL(b.Tiempo,0)) AS Horas , a.MargenBruto, a.Margen10,a.FechaAct
	INTO #tmpHorasReales
	FROM #tmpOportMargen a
	JOIN dbo.ActivxOp  b
	ON a.CveOport = b.CveOport
	JOIN dbo.Usuarios C
	ON C.Login = B.usuRegAct
	GROUP BY a.CveOport,a.Folio_Op,a.NombCli,b.usuRegAct,C.Nombre,a.MargenBruto, a.Margen10,a.FechaAct
	HAVING SUM(ISNULL(b.Tiempo,0))>=0.50
	ORDER BY a.CveOport ASC 
	

	
	-- DECLARACION DE CURSOR
	DECLARE Mi_Cursor CURSOR FOR 
	SELECT  CveOport
			,Folio_Op
			,NombCli
			,usuRegAct
			,Nombre
			,Horas 
			,MargenBruto
			,Margen10
			,FechaAct
	FROM #tmpHorasReales
	ORDER BY Horas ASC 


	DECLARE @Porcentaje100  AS DECIMAL(18,4)
	DECLARE @CveOportAnt	AS INTEGER
	SET @Porcentaje100 = 0
	OPEN Mi_Cursor 
	FETCH NEXT FROM Mi_Cursor INTO @cveOport,@FolioOp,@NombreClie,@usuRegAct,@Nombre,@Horas,@MargenBruto,@Margen10,@FechaAct
	WHILE @@FETCH_STATUS = 0 
	BEGIN 
		DECLARE @PorcHorasIng	DECIMAL(18,4)
		DECLARE @Origen			VARCHAR(20)
		
		-- SE RESETEA EL NUMERO PARA LA SUMA DEL PORCENTAJE
		
		IF ISNULL(@CveOportAnt,0) <> @cveOport
		BEGIN
			SET @Porcentaje100 = 0
			SET @CveOportAnt = @cveOport
		END
		
		SELECT @TotalHorasOport = SUM(ISNULL(Horas,0))
		FROM #tmpHorasReales a
		WHERE a.CveOport = @cveOport
		GROUP BY a.CveOport
		ORDER BY a.CveOport ASC 
	
		
		IF EXISTS (SELECT * FROM tbl_OportModMargenBruto WHERE CveOport = @cveOport AND usuRegAct = @usuRegAct AND BitActivo = 1)
		BEGIN
			SET @PorcHorasIng = (SELECT TOP 1 PorcHorasNew FROM tbl_OportModMargenBruto WHERE CveOport = @cveOport AND usuRegAct = @usuRegAct AND BitActivo = 1)
			SET @Origen = '255,255,157'
			
			-- SI EL PORCENTAJE SE PASA DEL 100 SE LE QUITA UNA DIEZMILECIMA --
			SET @Porcentaje100 = @Porcentaje100+@PorcHorasIng
			IF @Porcentaje100>1
				SET @PorcHorasIng = @PorcHorasIng - 0.0001
			-- ------------------------------------------------------------- --
			
			INSERT INTO #tmpOportHoras ( cveOport,FolioOp,NombreClie,usuRegAct,Nombre,Horas,PorcHoras,PorcHorasNew,MargenBruto,Margen10,MontoIng,MontoIngNew,Color ,Fecha)
			SELECT 
				 @cveOport
				,@FolioOp
				,@NombreClie
				,@usuRegAct
				,@Nombre
				,@Horas
				,CASE WHEN @TotalHorasOport <> 0 THEN (@Horas/@TotalHorasOport) ELSE 0 END 
				,@PorcHorasIng
				,@MargenBruto
				,@Margen10
				,CASE WHEN @TotalHorasOport <> 0 THEN ((@Horas/@TotalHorasOport)* @Margen10) ELSE 0 END 
				,CASE WHEN @TotalHorasOport <> 0 THEN ((@PorcHorasIng)* @Margen10) ELSE 0 END 
				,@Origen
				,@FechaAct

		END
		ELSE
		BEGIN
			SET @PorcHorasIng = CASE WHEN @TotalHorasOport <> 0 THEN (@Horas/@TotalHorasOport) ELSE 0 END
			SET @Origen = 'Original'
			
			-- SI EL PORCENTAJE SE PASA DEL 100 SE LE QUITA UNA DIEZMILECIMA --
			SET @Porcentaje100 = @Porcentaje100+@PorcHorasIng
			IF @Porcentaje100>1
			BEGIN
				SET @PorcHorasIng = @PorcHorasIng - 0.0001
				PRINT @Porcentaje100
			END
			-- ------------------------------------------------------------- --
			

			INSERT INTO #tmpOportHoras ( cveOport,FolioOp,NombreClie,usuRegAct,Nombre,Horas,PorcHoras,MargenBruto,Margen10,MontoIng,Color, Fecha )
			SELECT 
				 @cveOport
				,@FolioOp
				,@NombreClie
				,@usuRegAct
				,@Nombre
				,@Horas
				,@PorcHorasIng 
				,@MargenBruto
				,@Margen10
				,CASE WHEN @TotalHorasOport <> 0 THEN ((@PorcHorasIng)* @Margen10) ELSE 0 END 
				,@Origen
				,@FechaAct

		END


	FETCH NEXT FROM Mi_Cursor INTO @cveOport,@FolioOp,@NombreClie,@usuRegAct,@Nombre,@Horas,@MargenBruto,@Margen10,@FechaAct
	END 
	CLOSE Mi_Cursor 
	DEALLOCATE Mi_Cursor 

	IF @pCommando = 1
	BEGIN
		
		SELECT 
			 cveOport
			,FolioOp
			,NombreClie
			,usuRegAct
			,Nombre
			,Horas
			,PorcHoras = CASE WHEN PorcHorasNew IS NULL THEN CAST((ISNULL(PorcHoras,0)*100) AS DECIMAL(18,2)) ELSE CAST((PorcHorasNew*100) AS DECIMAL(18,2)) END
			,MargenBruto = MargenBruto
			,Margen10 = Margen10
			,MontoIng = MontoIng
			,MontoIngNew = CASE WHEN MontoIngNew IS NULL THEN 
						CASE WHEN ISNULL(MontoIng,0) = 0 THEN 0 ELSE ISNULL(MontoIng,0) END
								ELSE ISNULL(MontoIngNew,0) END 
			,Color
		FROM #tmpOportHoras
		ORDER BY cveOport, Horas ASC  	
	END
	ELSE 
	
	IF @pCommando = 2
	BEGIN
		SELECT 
			 cveOport
			,FolioOp
			,NombreClie
			,usuRegAct
			,Nombre
			,Horas
			,PorcHoras = CASE WHEN PorcHorasNew IS NULL THEN CAST((PorcHoras*100) AS DECIMAL(18,2)) ELSE CAST((PorcHorasNew*100) AS DECIMAL(18,2)) END
			,MargenBruto = MargenBruto
			,Margen10 = Margen10
			,MontoIng = MontoIng
			,MontoIngNew = CASE WHEN MontoIngNew IS NULL THEN 
						CASE WHEN ISNULL(MontoIng,0) = 0 THEN 0 ELSE ISNULL(MontoIng,0) END
								ELSE ISNULL(MontoIngNew,0) END 
			,Color
			,CONVERT(varchar(7), Fecha,120) as Fecha
		FROM #tmpOportHoras
		WHERE usuRegAct IN (SELECT * FROM @t2)
		ORDER BY cveOport, Horas ASC  			
	END
	ELSE
		UPDATE #tmpOportHoras SET MontoIngNew = CASE WHEN MontoIngNew IS NULL THEN CASE WHEN MontoIng = 0 THEN '' ELSE MontoIng END ELSE MontoIngNew END 
		
	IF @pCommando = 3
	BEGIN
		--SELECT * FROM @t2 tabla2
		--WHERE tabla2.t2 = 'nfloresg'
		
		--SELECT * FROM #tmpOportHoras tmp	
		--WHERE tmp.usuRegAct = 'schausee'
		
		--SELECT 
		--	 Usu.Login
		--	,Usu.Nombre
		--	,tmp.MontoIngNew
		----	,SUM(tmp.MontoIngNew)
		--FROM #tmpOportHoras tmp
		--RIGHT JOIN @t2 tabla2
		--ON UPPER(tabla2.t2) = UPPER(tmp.usuRegAct)
		--JOIN dbo.Usuarios Usu
		--ON UPPER(Usu.Login) = UPPER(tabla2.t2)
		----WHERE UPPER(usuRegAct) IN (SELECT * FROM @t2)
		----GROUP BY Usu.Login,Usu.Nombre
		--ORDER BY Usu.Login ASC  
		
		SELECT 
			 Usu.Login
			,Usu.Nombre
			,SUM(ISNULL(tmp.MontoIngNew,0)) as MontoIngNew
		FROM #tmpOportHoras tmp
		RIGHT JOIN @t2 tabla2
		ON UPPER(tabla2.t2) = UPPER(tmp.usuRegAct)
		JOIN dbo.Usuarios Usu
		ON UPPER(Usu.Login) = UPPER(tabla2.t2)		
		--WHERE UPPER(usuRegAct) IN (SELECT * FROM @t2)
		GROUP BY Usu.Login,Usu.Nombre
		ORDER BY Usu.Login ASC  								
	END
	
	IF @pCommando = 4
	BEGIN
		-- SE OBTIENEN TODAS LAS OPORTUNIDADES DONDE ESTA INVOLUCRADO EL INGENIERO
		SELECT DISTINCT 
			 CveOport 
		INTO #tmpOports
		FROM #tmpOportHoras
		WHERE usuRegAct = @pLstEmpleados
		
		SELECT 
			 a.cveOport
			,FolioOp
			,NombreClie
			,usuRegAct
			,Nombre
			,Horas
			,PorcHoras = CASE WHEN PorcHorasNew IS NULL THEN CAST((PorcHoras*100) AS DECIMAL(18,2)) ELSE CAST((PorcHorasNew*100) AS DECIMAL(18,2)) END
			,MargenBruto = MargenBruto
			,Margen10 = Margen10
			,MontoIng = MontoIng
			,MontoIngNew = CASE WHEN MontoIngNew IS NULL THEN 
						CASE WHEN ISNULL(MontoIng,0) = 0 THEN 0 ELSE ISNULL(MontoIng,0) END
								ELSE ISNULL(MontoIngNew,0) END 
			,Color
		FROM #tmpOportHoras a
		JOIN #tmpOports b
		ON a.CveOport = b.CveOport
		--WHERE usuRegAct = @pLstEmpleados
		ORDER BY a.cveOport, a.Horas ASC  
		
	END
	

	
END



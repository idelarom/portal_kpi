USE [OPTracker]
GO
/****** Object:  StoredProcedure [dbo].[Sps_CalculoKPI_test]    Script Date: 28/07/2017 08:51:46 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--exec [Sps_CalculoKPI] @pFechaInicial='2014-05-01',@pFechaFinal='2014-05-28',@pLstEmpleados='PMARTINB'

ALTER PROCEDURE [dbo].[Sps_CalculoKPI_test]
@pLstEmpleados		VARCHAR(max) = ''
,@pFechaInicial		DATETIME = NULL
,@pFechaFinal		DATETIME = NULL
,@pUsr				varchar(1000)
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
-- Query Prueba				: exec [Sps_CalculoMargenBruto] @pFechaIni = N'2013/03/01', @pFechaFin = N'2013/03/22'  --11558
--     
-- CONS	AUTOR							RFECHA		CC		MODIFICACION    
-- -------------------------------------------------------------------------------- 
-- 0001 Israel Ortiz Escobedo			20/Mar/2013			Creacion
-- -------------------------------------------------------------------------------- 
SET NOCOUNT ON;
-- -------------------------------------------------------------------------------- 
-- DECLARACION DE VARIABLES
-- -------------------------------------------------------------------------------- 

-- -------------------------------------------------------------------------------- 
-- DECLARACION DE TABLAS TEMPORALES
-- -------------------------------------------------------------------------------- 
BEGIN	
	--Declara y crea tablas Temporales
	----------------------------------------------------------------
	declare @v varchar(4000)
	
	DECLARE @tempLogin2 TABLE 
    ( 
        pLogin VARCHAR(100) 
    ) 
    
    CREATE TABLE #tblEmpleados(
	 usuRegAct		VARCHAR(50)
	,Nombre			VARCHAR(100)

	)
	
	CREATE TABLE #tmpPrev(
	 usuRegAct		VARCHAR(50)
	,Nombre			VARCHAR(100)
	,MontoIngPrev	DECIMAL(18,4)
	)
	
	CREATE TABLE #tmpImple(
	 usuRegAct		VARCHAR(50)
	,Nombre			VARCHAR(100)
	,MontoIngImpl	DECIMAL(18,4)
	)
	
	CREATE TABLE #tmpCostoIng(
	 usuRegAct		VARCHAR(50)
	,Nombre			VARCHAR(100)
	,CostoIng		DECIMAL(18,4)
	)
	
	CREATE TABLE #tmpSop(
	 usuRegAct		VARCHAR(50)
	,Nombre			VARCHAR(100)
	,MontoIngSop	DECIMAL(18,4)
	)
	
	CREATE TABLE #tmpProy(
	 usuRegAct		VARCHAR(50)
	,Nombre			VARCHAR(100)
	,MontoIngProy	DECIMAL(18,4)
	)	
	
	CREATE TABLE #tmpValidarCCing(
	 usuRegAct		VARCHAR(50)
	,Nombre			VARCHAR(100)
	,Validacion		INT
	)
	--------------------------------------------------------------
	SET @pLstEmpleados = UPPER(@pLstEmpleados)
	
	--Consulta en Navision aquellos Empleados que pertenecen a un CC de Ingenieria
	set @v='
	insert into #tmpValidarCCing (usuRegAct,Nombre,Validacion)
	SELECT *
	FROM OPENQUERY(MGSNAV01, ''exec NAVINFO.dbo.spq_ValidarCCIngenieria @pLstEmpleados= '''' ' + @pLstEmpleados + ' '''' '')'
	exec (@v)	
	
	INSERT INTO #tmpPrev
	EXEC [Sps_CalculoMargenBruto] @pFechaIni = @pFechaInicial, @pFechaFin = @pFechaFinal , @pCommando = 3, @pLstEmpleados = @pLstEmpleados
			
	INSERT INTO #tmpImple
	EXEC [Sps_Implementacion_test] @pFechaInicial = @pFechaInicial, @pFechaFinal = @pFechaFinal , @Comando = 2, @pLstEmpleados = @pLstEmpleados	
		
	set @v='
	insert into #tmpCostoIng (usuRegAct,Nombre,CostoIng)
	SELECT *
	FROM OPENQUERY(MGSNAV01, ''exec NAVINFO.dbo.spq_GetEmployeeCost_test @pFechaInicial= '''' ' + convert(nvarchar(100),@pFechaInicial) + ' '''' ,@pFechaFinal= '''' ' + convert(nvarchar(100),@pFechaFinal) + ' '''' ,@pLstEmpleados= '''' ' + @pLstEmpleados + ' ''''
	'')'
	exec (@v)
	print @pLstEmpleados
	IF @pLstEmpleados <> ''
		BEGIN 
			INSERT INTO @tempLogin2( pLogin)
			SELECT *
			FROM DBO.fn_cadena_to_tabla(@pLstEmpleados)
		END	
					
	INSERT INTO #tblEmpleados (usuRegAct,Nombre)
	SELECT distinct RTRIM(LTRIM(UPPER([Usuario Red]))) as Login , RTRIM(LTRIM(UPPER([First Name]))) + ' ' + RTRIM(LTRIM(UPPER([Middle Name]))) + ' ' + RTRIM(LTRIM(UPPER([Last Name]))) as Nombre
	from [MGSNAV01].Navision.dbo.Employee
	INNER JOIN @tempLogin2 ON  ltrim(rtrim(upper([Usuario Red] ))) =  ltrim(rtrim(upper(pLogin)))  COLLATE DATABASE_DEFAULT 

	declare @tbl_sailaine table (suma  money, usuario  varchar(50))

	insert into @tbl_sailaine (suma,usuario)
	 SELECT 
	 ISNULL(SUM(horas_tarea ),0) AS SUMA,
	 rep as usuario
	 FROM [mts-db03\sistemas].HELPDESK.dbo.diary
	 WHERE @pFechaInicial <= entry_dt AND @pFechaFinal >= entry_dt
	 GROUP BY rep

	--Obtiene el numero de horas de SaiiLine y lo multiplica por 35 en caso de que el 
	--empleado pertenesca a un CC de Ingenieria de lo contrario es 0	
	INSERT INTO #tmpSop (usuRegAct,Nombre,MontoIngSop)
	SELECT emp.usuRegAct AS 'Login'
		  ,emp.Nombre		  
		  ,CASE ValidarCCIng.Validacion 
				WHEN 1 
				THEN  ISNULL(SAI.SUMA,0) * 35 
				ELSE 0 end
		  FROM #tblEmpleados emp 
		  LEFT JOIN #tmpValidarCCing ValidarCCIng ON emp.usuRegAct = ValidarCCIng.usuRegAct		
		  LEFT OUTER JOIN
		  @tbl_sailaine AS SAI ON LTRIM(RTRIM(UPPER(SAI.usuario))) = LTRIM(RTRIM(UPPER(emp.Nombre)))

	select * from [PMTracker].[dbo].tblCalculoKPITmp
	SELECT DISTINCT  emp.usuRegAct AS 'Login'
		  ,emp.Nombre
		  ,ROUND(ISNULL(MontoIngPrev,0),8) AS MontoIngPrev
		  ,ROUND(ISNULL(MontoIngImpl,0),8) AS MontoImpl
		  --,dbo.FN_ValidaTipoCambioImplementacion(emp.usuRegAct,@pFechaInicial,@pFechaFinal) as ValidaImp
		  ,ROUND(ISNULL(MontoIngSop,0),8) AS MontoSop
		  -- ,0 AS MontoSop
		  ,0 AS MontoProy
		  --,[dbo].[ValidaPrimerTrimestre](@pFechaInicial,emp.usuRegAct) as validacion
		  ,ROUND(ISNULL(MontoIngPrev,0) + ISNULL(MontoIngImpl,0) + ISNULL(MontoIngSop,0),8) AS Total
		  ,case [dbo].[ValidaPrimerTrimestre](@pFechaInicial,emp.usuRegAct) when 1 then ISNULL(CONVERT(NVARCHAR(50),(CostIng.CostoIng)),'Sin Costo Ingeniero') else ROUND(ISNULL(MontoIngPrev,0) + ISNULL(MontoIngImpl,0) + ISNULL(MontoIngSop,0),2) end AS TotalMod
		  ,ISNULL(CONVERT(NVARCHAR(50),(CostIng.CostoIng)),'0') AS 'Costo'
		  --,'KPI' = CASE ROUND(isnull(CostIng.CostoIng,0),2) WHEN 0 then 0 ELSE  ROUND((( ISNULL(MontoIngPrev,0) + ISNULL(MontoIngImpl,0) ) / (CostIng.CostoIng)),2) end		  
		  ,'KPI' = CASE ROUND(isnull(CostIng.CostoIng,0),2) WHEN 0 then 0 ELSE  ROUND((((ISNULL(MontoIngPrev,0) + ISNULL(MontoIngImpl,0) + ISNULL(MontoIngSop,0)) / ISNULL(CostIng.CostoIng,1) )* 100) ,2) end		  
		  ,'iteraciones' = [dbo].[ObtenerIteracion](@pFechaInicial,@pFechaFinal,emp.usuRegAct)
		  ,'CC' = [dbo].Fn_TraeCC_KPI(emp.usuRegAct,@pFechaFinal)
		  ,@pUsr
		  FROM #tblEmpleados emp
		  LEFT JOIN #tmpPrev prev ON emp.usuRegAct = prev.usuRegAct
		  LEFT JOIN #tmpImple imp ON emp.usuRegAct = imp.usuRegAct 
		  LEFT JOIN #tmpSop  Sop ON emp.usuRegAct = Sop.usuRegAct 
		  LEFT JOIN #tmpCostoIng CostIng ON emp.usuRegAct = CostIng.usuRegAct
		  ORDER BY 1

END	

--select [dbo].Fn_TraeCC_KPI('PMARTINB','2014-10-01')
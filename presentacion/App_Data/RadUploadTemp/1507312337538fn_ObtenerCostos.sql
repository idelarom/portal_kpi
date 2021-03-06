USE [NAVINFO]
GO
/****** Object:  UserDefinedFunction [dbo].[fn_ObtenerCostos]    Script Date: 01/08/2017 10:54:16 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- HUMBERTO DE LA ROSA
-- OBTIENE UN LISTADO DE COSTO POR CADENA D EUSUARIOS
ALTER FUNCTION [dbo].[fn_ObtenerCostos] 
(@pFechaInicial DATETIME,
@pFechaFinal DATETIME,
@pLstEmpleados VARCHAR(max))

RETURNS @tab TABLE (
     USUARIO		VARCHAR(50)
	,idEmployee		integer
	, ACUMULADO MONEY 
	,Esquema varchar(250) null
	,NOMINA BIT
	,ASIMILADOS BIT
	,HONORARIOS BIT
	,MORAL BIT

)

AS
BEGIN	
	DECLARE @Tmp_tblCostoIngeniero TABLE 
		( 
		NumEmpleado INTEGER NULL,
		Costo money NULL,		
		PeriodoInicial datetime NULL,
		PeriodoFinal datetime NULL,
		Esquema		nvarchar(50) null,
		[Termination Date] datetime null
		) 
		
		--Crea Tabla Filtrada
		DECLARE @Tmp_tblCostoIngenieroFiltrado TABLE 
		( 
		NumEmpleado INTEGER NULL,
		Costo money NULL,		
		PeriodoInicial datetime NULL,
		PeriodoFinal datetime NULL,
		Esquema		VARCHAR(50) null
		) 


		DECLARE @tempLogin TABLE 
		( 
		pLogin VARCHAR(100) 
		) 
    
		IF @pLstEmpleados <> ''
		BEGIN 
			WHILE CHARINDEX(',',@pLstEmpleados) > 0 
				BEGIN 
					INSERT INTO @tempLogin SELECT substring(@pLstEmpleados,1,(charindex(',',@pLstEmpleados)-1)) 
					SET @pLstEmpleados = substring(@pLstEmpleados,charindex(',',@pLstEmpleados)+1,len(@pLstEmpleados)) 
				END 
				
				INSERT INTO @tempLogin 
				SELECT @pLstEmpleados 
		END					
		DECLARE @tblEmpleados TABLE
		(
			USUARIO		VARCHAR(50)
		,idEmployee		integer
		,[Termination Date] datetime
		, ACUMULADO MONEY NULL
		)

		insert into @tblEmpleados (USUARIO,idEmployee,[Termination Date])
		SELECT
		LTRIM(RTRIM(UPPER([Usuario Red])))  COLLATE DATABASE_DEFAULT ,
		CONVERT(INT, LTRIM(RTRIM([No_]))),	
		[Termination Date] 
		FROM NAVISION.dbo.Employee INNER JOIN
		@tempLogin AS tu on LTRIM(RTRIM(UPPER(tu.pLogin))) = LTRIM(RTRIM(UPPER([Usuario Red])))  COLLATE DATABASE_DEFAULT 
	


		--Captura los costos de un ingeniero 
		insert into @Tmp_tblCostoIngeniero (NumEmpleado,PeriodoInicial,PeriodoFinal,Costo,[Termination Date], Esquema)
		select 
		Tabla1.Empleado
		,[Fecha Inicio] =  case Tabla1.[Fecha Inicio] when '1753-01-01 00:00:00.000' then  null else Tabla1.[Fecha Inicio]end
		,[Fecha Final] = case Tabla1.[Fecha Final] when '1753-01-01 00:00:00.000' then null else 
				(Select top 1 (DATEADD(day, -1, isnull([Fecha Inicio],0))) 
				from NAVISION.dbo.[Esquema Empleado] 
				Where [Empleado] =Tabla1.Empleado and Tabla1.Cancelado = 0 and Tabla1.EsquemaPago = 'NOMS'
				and [Fecha Inicio] > Tabla1.[Fecha Inicio] order by [Fecha Inicio] ) end 
		,Sum(Tabla1.Monto) as SueldoTotal ,
		ISNULL(te.[Termination Date],'1753-01-01 00:00:00.000'),
		LTRIM(RTRIM(UPPER(Tabla1.EsquemaPago)))
		from 
		NAVISION.dbo.[Esquema Empleado] Tabla1 
		INNER JOIN @tblEmpleados as te  on te.idEmployee = Tabla1.Empleado
		INNER JOIN NAVISION.dbo.[Esquema Empleado] Tabla2 on Tabla1.timestamp = Tabla2.timestamp	 
		WHERE  Tabla1.Cancelado = 0 and 
		LTRIM(RTRIM(UPPER(Tabla1.EsquemaPago))) = LTRIM(RTRIM(UPPER('NOMS')))
		group by
		te.idEmployee, te.[Termination Date],
		Tabla1.[Fecha Inicio],Tabla1.Empleado,Tabla1.[Fecha Final],Tabla1.Cancelado,Tabla1.EsquemaPago
		order by Tabla1.[Fecha Inicio]
					
			
	
		--Captura los costos de un ingeniero 
		insert into @Tmp_tblCostoIngeniero (NumEmpleado,PeriodoInicial,PeriodoFinal,Costo,[Termination Date], Esquema)
		select 
		Tabla1.Empleado
		,[Fecha Inicio] =  case Tabla1.[Fecha Inicio] when '1753-01-01 00:00:00.000' then  null else Tabla1.[Fecha Inicio]end
		,[Fecha Final] =
		(case Tabla1.[Fecha Final] when '1753-01-01 00:00:00.000' then 
					(Select top 1 (DATEADD(day, -1, isnull(Tabla3.[Fecha Inicio],0))) 
					from NAVISION.dbo.[Esquema Empleado] Tabla3 
					Where Tabla3.[Empleado] = Tabla1.Empleado and Tabla3.Cancelado = 0 
					and Tabla3.EsquemaPago IN ('HONSP','MORAL','F&A ASIM') 								
					and Tabla3.[Fecha Inicio] > Tabla1.[Fecha Inicio] 
					order by [Fecha Inicio] ) 
			else (Select top 1 (DATEADD(day, -1, isnull(Tabla4.[Fecha Inicio],0))) 
					from NAVISION.dbo.[Esquema Empleado] Tabla4 
					Where Tabla4.[Empleado] = Tabla1.Empleado and Tabla4.Cancelado = 0 
					and Tabla4.EsquemaPago IN ('HONSP','MORAL','F&A ASIM') 
					and Tabla4.[Fecha Inicio] > Tabla1.[Fecha Inicio]
					order by Tabla4.[Fecha Inicio] )end)
		,Sum(Tabla1.Monto) as SueldoTotal ,
		ISNULL(te.[Termination Date],'1753-01-01 00:00:00.000'),
		LTRIM(RTRIM(UPPER(Tabla1.EsquemaPago)))
		from 
		NAVISION.dbo.[Esquema Empleado] Tabla1 
		INNER JOIN @tblEmpleados as te  on te.idEmployee = Tabla1.Empleado
		INNER JOIN NAVISION.dbo.[Esquema Empleado] Tabla2 on Tabla1.timestamp = Tabla2.timestamp	 
		WHERE  Tabla1.Cancelado = 0 
		and LTRIM(RTRIM(UPPER(Tabla1.EsquemaPago))) IN ('HONSP','MORAL','F&A ASIM')
		group by
		te.idEmployee, te.[Termination Date],
		Tabla1.[Fecha Inicio],Tabla1.Empleado,Tabla1.[Fecha Final],Tabla1.Cancelado,Tabla1.EsquemaPago
		order by Tabla1.[Fecha Inicio]
				
		update @Tmp_tblCostoIngeniero set PeriodoFinal = null
		where PeriodoFinal ='1753-01-01 00:00:00.000'

		update @Tmp_tblCostoIngeniero set PeriodoFinal = [Termination Date]
		where PeriodoFinal is null
		and [Termination Date] <> '1753-01-01 00:00:00.000'
	
		--Filtra los costos de un ingeniero de acuerdo a la fecha 
		insert into @Tmp_tblCostoIngenieroFiltrado (NumEmpleado,PeriodoInicial,PeriodoFinal,Costo, Esquema)
		select NumEmpleado,PeriodoInicial,PeriodoFinal,Costo, Esquema
		from @Tmp_tblCostoIngeniero inner join
		@tblEmpleados as te on te.idEmployee = NumEmpleado					
		where
			((( @pFechaInicial <= PeriodoInicial) and ( @pFechaFinal >= PeriodoInicial)) 
		or (( @pFechaInicial <= PeriodoFinal) and ( @pFechaFinal >= PeriodoFinal))
		or (( @pFechaInicial <= PeriodoInicial) and ( @pFechaFinal >= PeriodoFinal))
		or (( @pFechaInicial >= PeriodoInicial) and ( @pFechaFinal <= PeriodoFinal))
		or (( @pFechaInicial >= PeriodoInicial) and ( PeriodoFinal is null)) )    

	
		DELETE @Tmp_tblCostoIngenieroFiltrado WHERE ISNULL(Esquema,'') = ''
		--select * from #Tmp_tblCostoIngenieroFiltrado

		--Modifica la fecha Inicial 
		update @Tmp_tblCostoIngenieroFiltrado
		set PeriodoInicial = @pFechaInicial
		where PeriodoInicial < @pFechaInicial
	 
		--Modifica la fecha Final
		update @Tmp_tblCostoIngenieroFiltrado
		set PeriodoFinal = @pFechaFinal
		where (PeriodoFinal > @pFechaFinal) or  PeriodoFinal is null
	 
	
		declare @tab_final table (num_empleado int, acumulador money)

		UPDATE @tblEmpleados SET 
		ACUMULADO = ISNULL(([dbo].[ConvertirCostoDiario_DLLs]( Costo/(365/12), B.PeriodoInicial,B.PeriodoFinal)),0)
		FROM @tblEmpleados AS A
		INNER JOIN @Tmp_tblCostoIngenieroFiltrado AS B
		ON B.NumEmpleado = A.idEmployee

		insert into @tab (idEmployee,USUARIO,ACUMULADO,Esquema, NOMINA, ASIMILADOS, MORAL, HONORARIOS)
		SELECT 
		idEmployee, 
		USUARIO,
		SUM(ISNULL(([dbo].[ConvertirCostoDiario_DLLs]( Costo/(365/12), B.PeriodoInicial,B.PeriodoFinal)),0)) ,
		ISNULL(Esquema,'') AS ESQUEMA,
		CASE ISNULL(Esquema,'') WHEN 'NOMS'THEN 1 ELSE 0 END,
		CASE ISNULL(Esquema,'') WHEN 'F&A ASIM'THEN 1 ELSE 0 END,
		CASE ISNULL(Esquema,'') WHEN 'MORAL'THEN 1 ELSE 0 END,
		CASE ISNULL(Esquema,'') WHEN 'HONSP'THEN 1 ELSE 0 END
		FROM @tblEmpleados AS A
		inner JOIN @Tmp_tblCostoIngenieroFiltrado AS B
		ON B.NumEmpleado = A.idEmployee
		where ISNULL(Esquema,'') <> 'NOM2'
		GROUP BY ISNULL(Esquema,''),IDEMPLOYEE, USUARIO
		ORDER BY 2

      RETURN

END -- End Function

USE [OPTracker]
GO
/****** Object:  StoredProcedure [dbo].[Sps_Implementacion_test]    Script Date: 02/08/2017 01:03:56 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--exec [Sps_Implementacion] @pFechaInicial = 'Oct  1 2013 12:00:00:000AM', @pFechaFinal = 'Oct 31 2013 12:00:00:000AM', @pLstEmpleados = 'AORTIZH,amorang,CGALVEZR,DVIDALER,DROSASN,ELOPEZG,EELIZONE,egalanm,ffernand,FOLIVARR,HPEREZRI,IMARTIND,JESCAMIH,MRAMIREM,NRAMOSF,nfloresg,OESPINOR,PAYALAS,PMARTINB,RPARRAT,sgaytans,MGARCIAR,RVILLAVB,RGONZALS,CBONILLR,RRAMIREO,DRAMIREL,DMONTEMG,DORDAZA,EECHAZAC,FVELAZQN,GROBLEDA,GSOTOG,IGONZALC,ARAMOSC,JPEREZR,JELIZONR,JHERNANG,KFRANCOV,kcamachl,RMATAV,RGAYTANA,RPOMPAZ,RSANCHES,rrosass,STORRESM,VMORENOF',@Comando = 1
--exec [Sps_Implementacion] @pFechaInicial = '2014-01-01', @pFechaFinal = '2014-12-31', @pLstEmpleados = 'FOLIVARR',@Comando = 1
ALTER  PROCEDURE [dbo].[Sps_Implementacion_test]
		@pLstEmpleados VARCHAR(max) = ''
		,@pFechaInicial		DATETIME = NULL
		,@pFechaFinal		DATETIME = NULL
		,@Comando		INT
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
-- Query Prueba				: exec [Sps_Implementacion] @pLstEmpleados = 'egalanm,rparrat,amorang,rvillavb,jescamih,mramirem',@pFechaInicial = '2013-01-01',@pFechaFinal = '2013-12-31',@Comando = 1
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
	
	--Declarar Tabla temporar para empleados
	DECLARE @t2 TABLE 
    ( 
        t2 VARCHAR(100) 
    ) 		
	
	--LLenar tabla temporaral con los empleados recibidos por parametro
	IF @pLstEmpleados <> ''
	BEGIN 
		
				INSERT INTO @t2( t2)
				SELECT *
				FROM DBO.fn_cadena_to_tabla(@pLstEmpleados)
    END	
	
		IF @Comando= 1
		BEGIN
			SELECT usr.clave_usuario as CveIngeniero
				  ,RTRIM(LTRIM(usr.nombre_usuario)) + ' ' + RTRIM(LTRIM(aPaterno_usuario)) + ' ' + RTRIM(LTRIM(aMaterno_usuario)) as NombreIngeniero
				  ,pry.Folio
				  ,pry.nombre_proyecto
				  ,ent.idEntregable
				  ,ent.NombreEntregable
				  ,det.Horas_ActReal
				  ,ent.CostoTotal
				  --,case when (isnull(Pmov.idTipoMoneda,0) <> 0 AND Pmov.TazaCambio >= 1  AND Pmov.TazaCambio IS NOT NULL) then (ISNULL(det.Monto_actppto/isnull((select top 1 TipoCambio from [MGSNAV01].[NAVINFO].[dbo].[Info_CPED_MIcro] where Pedido COLLATE DATABASE_DEFAULT = det.CPED COLLATE DATABASE_DEFAULT),Pmov.TazaCambio),0)) else ISNULL(det.Monto_actppto,0) END AS Monto_actppto
				  ,case when (isnull(Pmov.idTipoMoneda,0) <> 0 AND Pmov.TazaCambio >= 1  AND Pmov.TazaCambio IS NOT NULL) then (ISNULL(det.Monto_actppto/isnull((select top 1 TipoCambio from [MGSNAV01].[NAVINFO].[dbo].[Info_CPED_MIcro] where Pedido COLLATE DATABASE_DEFAULT = det.CPED COLLATE DATABASE_DEFAULT),isnull(Pmov.TazaCambio,1)),0)) else ISNULL(det.Monto_actppto,0) END AS Monto_actppto
				  --,case when (isnull(Pmov.idTipoMoneda,0) <> 0 AND Pmov.TazaCambio >= 1  AND Pmov.TazaCambio IS NOT NULL) then (ISNULL(det.Monto_actppto/Pmov.TazaCambio,0)) else ISNULL(det.Monto_actppto,0) END AS Monto_actppto2
				  --,det.Monto_actppto				  
				  --,(select top 1 TipoCambio from [MGSNAV01].[NAVINFO].[dbo].[Info_CPED_MIcro] where Pedido COLLATE DATABASE_DEFAULT = det.CPED COLLATE DATABASE_DEFAULT) as TazaCambio
				  --,Pmov.TazaCambio as TazaCambio2
				  ,det.CPED
				  ,det.Monto_ActReal
				  ,usr.Login_password
				  ,CONVERT(varchar(7), Ent.FechaRealFin,120) as Fecha
				  FROM @t2 AS ing	  
				 LEFT JOIN [PMTracker].dbo.usuario Usr ON LTRIM(RTRIM(UPPER(ing.t2))) = LTRIM(RTRIM(UPPER(Usr.Login_password)))
				 JOIN [PMTracker].dbo.PptoDetalle det ON Usr.clave_usuario = det.idUsuario
				 JOIN [PMTracker].dbo.tblEntregables Ent on det.idEntregable = Ent.idEntregable
				 JOIN [PMTracker].dbo.proyecto Pry ON Pry.clave_proyecto = Ent.idProyecto
				 JOIN [PMTracker].dbo.PptoMovimiento Pmov on Pry.clave_proyecto = Pmov.clave_proyecto and pry.ppto_version_actual_proyecto = Pmov.version
				 WHERE Ent.FechaRealFin IS NOT NULL 
				   AND Ent.FechaRealFin >= @pFechaInicial + ' 00:00:00' AND FechaRealFin <= @pFechaFinal + ' 23:59:59'
				   AND det.Version = Pry.ppto_version_actual_proyecto
			--ORDER BY ent.idEntregable ASC
			ORDER BY FechaRealFin,pry.nombre_proyecto ASC, ent.idEntregable ASC, RTRIM(LTRIM(usr.nombre_usuario)) + ' ' + RTRIM(LTRIM(aPaterno_usuario)) + ' ' + RTRIM(LTRIM(aMaterno_usuario)) ASC
		END
		
		IF @Comando = 2
		BEGIN
				  
			SELECT  
			UPPER(usr.Login_password) as login_password
			,RTRIM(LTRIM(usr.nombre_usuario)) + ' ' + RTRIM(LTRIM(aPaterno_usuario)) + ' ' + RTRIM(LTRIM(aMaterno_usuario))	as ingeniero
			,convert(money,null) as total
				,isnull(Pmov.idTipoMoneda,0) as idTipoMoneda
				,Pmov.TazaCambio as tazacambio
				,det.Monto_actppto
				,DET.CPED
				,CONVERT(decimal(18,0),LTRIM(RTRIM(REPLACE(ISNULL(NULLIF(DET.CPED,''),'0') COLLATE DATABASE_DEFAULT,'CPED-',''))))  as cped_pedido
			into #tab_tempfinal2			
			FROM @t2 AS ing	  
			INNER JOIN [PMTracker].dbo.usuario Usr ON LTRIM(RTRIM(UPPER(ing.t2))) = LTRIM(RTRIM(UPPER(Usr.Login_password)))
			INNER JOIN [PMTracker].dbo.PptoDetalle det ON Usr.clave_usuario = det.idUsuario 
			--INNER JOIN [MGSNAV01].[NAVINFO].[dbo].[Info_CPED_MIcro] as icpm on icpm.pedido COLLATE DATABASE_DEFAULT = det.CPED COLLATE DATABASE_DEFAULT
			INNER JOIN [PMTracker].dbo.tblEntregables Ent on det.idEntregable = Ent.idEntregable			
			INNER JOIN [PMTracker].dbo.proyecto Pry ON Pry.clave_proyecto = Ent.idProyecto
			INNER JOIN [PMTracker].dbo.PptoMovimiento Pmov on Pry.clave_proyecto = Pmov.clave_proyecto and pry.ppto_version_actual_proyecto = Pmov.version
			WHERE
			Ent.FechaRealFin IS NOT NULL
			AND Ent.FechaRealFin >= @pFechaInicial + ' 00:00:00' AND FechaRealFin <= @pFechaFinal + ' 23:59:59'
			AND det.Version = Pry.ppto_version_actual_proyecto

			
			update #tab_tempfinal2 set	total = Monto_actppto / isnull( (icpm.TipoCambio),isnull(TazaCambio,1))			
			FROM #tab_tempfinal2
			LEFT OUTER JOIN
			[MGSNAV01].[NAVINFO].[dbo].[Info_CPED_MIcro] as icpm on 
			CONVERT(decimal(18,0),LTRIM(RTRIM(REPLACE(pedido  COLLATE DATABASE_DEFAULT,'CPED-',''))))  = cped_pedido			
			where tazacambio >= 1 and idTipoMoneda <> 0

			--update #tab_tempfinal2 set	total = Monto_actppto / 1
			--FROM #tab_tempfinal2
			--where 

			UPDATE #tab_tempfinal2 SET total = Monto_actppto / 1
		    WHERE total IS NULL
		

			SELECT 
			login_password,ingeniero,SUM(total) AS TOTAL 
			FROM #tab_tempfinal2
			group by login_password,ingeniero
			drop table #tab_tempfinal2
		END				
end
						

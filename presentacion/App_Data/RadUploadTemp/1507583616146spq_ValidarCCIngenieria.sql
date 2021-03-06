USE [NAVINFO]
GO
/****** Object:  StoredProcedure [dbo].[spq_ValidarCCIngenieria]    Script Date: 31/07/2017 06:30:24 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--EXEC [spq_ValidarCCIngenieria] 'egalanm'
--EXEC sp_CPED_Info_Micro 'CPED-019064'
--EXEC sp_CPED_Info_Micro 'CPED-017046'
--EXEC sp_CPED_Info_Micro 'CPED-019508'
--EXEC spq_GetEmployeeCost 'egalanm','2013-08-01 00:00:00.000','2013-08-31 00:00:00.000'
--exec [spq_GetEmployeeCost] @pFechaInicial = 'Oct  1 2013 12:00:00:000AM', @pFechaFinal = 'Oct 31 2013 12:00:00:000AM', @pLstEmpleados = 'AORTIZH,amorang,CGALVEZR,DVIDALER,DROSASN,ELOPEZG,EELIZONE,egalanm,ffernand,FOLIVARR,HPEREZRI,IMARTIND,JESCAMIH,MRAMIREM,NRAMOSF,nfloresg,OESPINOR,PAYALAS,PMARTINB,RPARRAT,sgaytans,MGARCIAR,RVILLAVB,RGONZALS,CBONILLR,RRAMIREO,DRAMIREL,DMONTEMG,DORDAZA,EECHAZAC,FVELAZQN,GROBLEDA,GSOTOG,IGONZALC,ARAMOSC,JPEREZR,JELIZONR,JHERNANG,KFRANCOV,kcamachl,RMATAV,RGAYTANA,RPOMPAZ,RSANCHES,rrosass,STORRESM,VMORENOF'
ALTER PROCEDURE [dbo].[spq_ValidarCCIngenieria]
	 @pLstEmpleados		VARCHAR(max) = ''
AS
BEGIN
	--select @pLstEmpleados,@pFechaInicial,@pFechaFinal
SET FMTONLY OFF;
SET NOCOUNT ON;
	
		DECLARE @tempLogin TABLE 
		( 
			pLogin VARCHAR(100) 
		) 
    
		DECLARE @tblEmpleados TABLE
		(
		 usuRegAct		VARCHAR(50)
		,Nombre			VARCHAR(100)
		,idEmpleado		integer
		,idEmployee		integer
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
		
		INSERT INTO @tblEmpleados (usuRegAct,Nombre,idEmployee)
			SELECT   RTRIM(LTRIM(UPPER([Usuario Red])))
					,RTRIM(LTRIM(UPPER(([First Name] + ' ' + [Middle Name] + ' ' + [Last Name]))))
					,[No_] 
			FROM @tempLogin 
			INNER JOIN NAVISION.dbo.Employee ON RTRIM(LTRIM(pLogin)) = RTRIM(LTRIM([Usuario Red]))			
		
		select  DISTINCT usuRegAct
				,Nombre 
				,dbo.fnValidarCCIngenieria(idEmployee) AS validacion
		from @tblEmpleados
	
END



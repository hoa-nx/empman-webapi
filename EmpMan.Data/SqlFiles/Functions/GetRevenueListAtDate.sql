USE [EmpManAPI]
GO

/****** Object:  UserDefinedFunction [dbo].[GetRevenueListAtDate]    Script Date: 2017/09/19 9:32:52 ******/
DROP FUNCTION [dbo].[GetRevenueListAtDate]
GO

/****** Object:  UserDefinedFunction [dbo].[GetRevenueListAtDate]    Script Date: 2017/09/19 9:32:52 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE FUNCTION [dbo].[GetRevenueListAtDate] (
@CompanyID INT =NULL ,
@DeptID INT =NULL,
@TeamID INT =NULL,
@StartDate DATE =NULL, 
@EndDate DATE =NULL )

RETURNS TABLE 
AS 
RETURN 
--LAY DOANH SO TRONG KHOANG THOI GIAN
SELECT dates.ymd, VEM.*  , 1000 AS WorkEmpType
FROM (
SELECT 
	OBJ.NUMBER AS MTH
,	DATENAME(MONTH, DATEADD(MONTH, OBJ.NUMBER , DateAdd(day,1,EOMONTH(case when @StartDate IS NULL THEN CONVERT(DATE, GETDATE()) ELSE @StartDate  END,-1))))  AS MONTHNAME
,	DATEADD(MONTH, OBJ.NUMBER , DateAdd(day,1,EOMONTH(case when @StartDate IS NULL THEN CONVERT(DATE, GETDATE()) ELSE @StartDate  END,-1))) AS YMD 
FROM 
	MASTER.dbo.SPT_VALUES OBJ
WHERE 
	OBJ.TYPE= 'P' 
AND OBJ.NUMBER <= DATEDIFF(MONTH,CONVERT(DATE,case when @StartDate IS NULL THEN CONVERT(DATE, GETDATE()) ELSE @StartDate  END,120) , CONVERT(DATE,case when @EndDate IS NULL THEN CONVERT(DATE, GETDATE()) ELSE @EndDate  END,120)) 

) dates   
LEFT OUTER JOIN ( 
	SELECT 
		REV.ReportYearMonth
		,SUM(COALESCE(REV.InMonthDevMM,0))	InMonthDevMM
		,SUM(COALESCE(REV.InMonthTransMM,0))	InMonthTransMM	
		,SUM(COALESCE(REV.InMonthManagementMM,0))	InMonthManagementMM
		,SUM(COALESCE(REV.InMonthOnsiteMM,0))	InMonthOnsiteMM
		,SUM(COALESCE(REV.InMonthSumMM,0))	InMonthSumMM
	FROM 
		Revenues REV 
	WHERE 
	   (( @CompanyID IS NOT NULL AND REV.CompanyID=@CompanyID ) OR ( @CompanyID IS NULL))
	AND (( @DeptID IS NOT NULL AND REV.DeptID=@DeptID ) OR ( @DeptID IS NULL))
	AND (( @TeamID IS NOT NULL AND REV.TeamID=@TeamID ) OR ( @TeamID IS NULL))
    AND (REV.ReportYearMonth  BETWEEN  DateAdd(day,1,EOMONTH(case when @StartDate IS NULL THEN CONVERT(DATE, GETDATE()) ELSE @StartDate  END,-1)) AND EOMONTH( DateAdd(day,1,EOMONTH(case when @EndDate IS NULL THEN CONVERT(DATE, GETDATE()) ELSE @EndDate  END,-1)) ) )
	GROUP BY 
		REV.ReportYearMonth
	) VEM 
ON ((VEM.ReportYearMonth IS NOT NULL AND convert(varchar(6),dates.YMD,112) = convert(varchar(6),VEM.ReportYearMonth,112) ) )
GO


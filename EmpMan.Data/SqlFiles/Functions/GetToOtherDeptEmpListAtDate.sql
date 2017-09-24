USE [EmpManAPI]
GO

/****** Object:  UserDefinedFunction [dbo].[GetToOtherDeptEmpListAtDate]    Script Date: 2017/09/19 9:34:36 ******/
DROP FUNCTION [dbo].[GetToOtherDeptEmpListAtDate]
GO

/****** Object:  UserDefinedFunction [dbo].[GetToOtherDeptEmpListAtDate]    Script Date: 2017/09/19 9:34:36 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE FUNCTION [dbo].[GetToOtherDeptEmpListAtDate] (
@CompanyID INT =NULL ,
@DeptID INT =NULL,
@TeamID INT =NULL,
@StartDate DATE =NULL, 
@EndDate DATE =NULL )

RETURNS TABLE 
AS 
RETURN 
SELECT dates.ymd, VEM.*  , 2 AS WorkEmpType
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
INNER JOIN ( 
	SELECT 
		* 
	FROM 
		EmpDetailWorks WOK
	WHERE 
		WOK.WorkEmpTypeMasterID = 31 
	AND WOK.WorkEmpTypeMasterDetailID =2  --loai nhan su sang dept khac ho tro
	AND WOK.DeptID <> 1 
) WOK
ON (
	(WOK.EndDate IS NOT NULL AND CONVERT(DATE, DATEADD(DAY, 20, dates.ymd)) between WOK.StartDate AND WOK.EndDate) 
	OR 
	(WOK.EndDate IS NULL AND WOK.StartDate < CONVERT(DATE, DATEADD(DAY, 20, dates.ymd)))
	)
LEFT OUTER JOIN ViewEmp VEM ON WOK.EmpID = VEM.ID 

 

GO


USE [EmpManAPI]
GO

/****** Object:  UserDefinedFunction [dbo].[GetWorkingEmpListAtDate]    Script Date: 2017/09/19 9:34:52 ******/
DROP FUNCTION [dbo].[GetWorkingEmpListAtDate]
GO

/****** Object:  UserDefinedFunction [dbo].[GetWorkingEmpListAtDate]    Script Date: 2017/09/19 9:34:52 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE FUNCTION [dbo].[GetWorkingEmpListAtDate] (
@CompanyID INT =NULL ,
@DeptID INT =NULL,
@TeamID INT =NULL,
@StartDate DATE =NULL, 
@EndDate DATE =NULL )

RETURNS TABLE 
AS 
RETURN 

SELECT dates.ymd, VEM.*  , 0 AS WorkEmpType
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
		* 
	FROM 
		ViewEmp 
	WHERE 
		ContractDate IS NOT NULL 
	AND (( @CompanyID IS NOT NULL AND CurrentCompanyID=@CompanyID ) OR ( @CompanyID IS NULL))
	AND (( @DeptID IS NOT NULL AND CurrentDeptID=@DeptID ) OR ( @DeptID IS NULL))
	AND (( @TeamID IS NOT NULL AND CurrentTeamID=@TeamID ) OR ( @TeamID IS NULL))
	) VEM 
ON ((VEM.JobLeaveDate IS NOT NULL AND DATEADD(DAY,20, dates.ymd) < VEM.JobLeaveDate AND VEM.ContractDate < dates.ymd ) OR (VEM.ContractDate < dates.ymd AND VEM.JobLeaveDate IS NULL))

UNION ALL 
SELECT dates.ymd, VEM.*  , 1 AS WorkEmpType
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
	AND WOK.WorkEmpTypeMasterDetailID =1  --loai nhan su tu dept khac chuyen sang
	AND WOK.DeptID=@DeptID
) WOK
ON (
	(WOK.EndDate IS NOT NULL AND dates.ymd between WOK.StartDate AND WOK.EndDate) 
	OR 
	(WOK.EndDate IS NULL AND WOK.StartDate < dates.ymd)
	)
LEFT OUTER JOIN ViewEmp VEM ON WOK.EmpID = VEM.ID 

EXCEPT

SELECT dates.ymd, VEM.*  , 4 AS WorkEmpType
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
	AND WOK.WorkEmpTypeMasterDetailID =4  --loai nhan su nghi tam thoi ( nghi thai san...)
	AND WOK.DeptID = @DeptID 
) WOK
ON (
	(WOK.EndDate IS NOT NULL AND CONVERT(DATE, DATEADD(DAY, 5, dates.ymd)) between WOK.StartDate AND WOK.EndDate) 
	OR 
	(WOK.EndDate IS NULL AND WOK.StartDate < CONVERT(DATE, DATEADD(DAY, 5, dates.ymd)))
	)
	
LEFT OUTER JOIN ViewEmp VEM ON WOK.EmpID = VEM.ID 
EXCEPT 
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
	AND WOK.DeptID <> @DeptID
) WOK
ON (
	(WOK.EndDate IS NOT NULL AND CONVERT(DATE, DATEADD(DAY, 20, dates.ymd)) between WOK.StartDate AND WOK.EndDate) 
	OR 
	(WOK.EndDate IS NULL AND WOK.StartDate < CONVERT(DATE, DATEADD(DAY, 20, dates.ymd)))
	)
LEFT OUTER JOIN ViewEmp VEM ON WOK.EmpID = VEM.ID 

 

GO


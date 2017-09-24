USE [EmpManAPI]
GO

/****** Object:  UserDefinedFunction [dbo].[GetWorkingEmpCountAtDate]    Script Date: 2017/09/19 9:35:17 ******/
DROP FUNCTION [dbo].[GetWorkingEmpCountAtDate]
GO

/****** Object:  UserDefinedFunction [dbo].[GetWorkingEmpCountAtDate]    Script Date: 2017/09/19 9:35:17 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE FUNCTION [dbo].[GetWorkingEmpCountAtDate] (
@CompanyID INT =NULL ,
@DeptID INT =NULL,
@TeamID INT =NULL,
@StartDate DATE =NULL, 
@EndDate DATE =NULL )

RETURNS TABLE 
AS 
RETURN 

SELECT dates.ymd, COALESCE(COUNT(*),0) CNT , 0 AS WorkEmpType
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
		VEM.* 
	FROM 
		ViewEmp VEM--nhan su dang lam viec trong bo phan ( loai tru di phan onsite / phan tam nghi / phan sang dept khac ho tro )
		LEFT OUTER JOIN [dbo].[GetOnisteEmpListAtDate](@CompanyID,@DeptID,@TeamID,@StartDate,@EndDate) ONS  ON  VEM.ID = ONS.ID --ONSITE
		LEFT OUTER JOIN [dbo].[GetStopWorkingEmpListAtDate](@CompanyID,@DeptID,@TeamID,@StartDate,@EndDate) STO  ON  VEM.ID = STO.ID --NGHI GIUA CHUNG
		LEFT OUTER JOIN [dbo].[GetToOtherDeptEmpListAtDate](@CompanyID,@DeptID,@TeamID,@StartDate,@EndDate) TOE  ON  VEM.ID = TOE.ID --HO TRO DEPT KHAC
		LEFT OUTER JOIN [dbo].[GetContractedJobLeavedEmpListAtDate](@CompanyID,@DeptID,@TeamID,@StartDate,@EndDate) JOE  ON  VEM.ID = JOE.ID --NGHI VIEC
	WHERE 
		 VEM.ContractDate IS NOT NULL 
	AND (( @CompanyID IS NOT NULL AND  VEM.CurrentCompanyID=@CompanyID ) OR ( @CompanyID IS NULL))
	AND (( @DeptID IS NOT NULL AND  VEM.CurrentDeptID=@DeptID ) OR ( @DeptID IS NULL))
	AND (( @TeamID IS NOT NULL AND  VEM.CurrentTeamID=@TeamID ) OR ( @TeamID IS NULL))
	AND ( ONS.ID IS NULL  AND STO.ID IS NULL  AND TOE.ID IS NULL  AND JOE.ID IS NULL)

	) VEM 
ON ((VEM.JobLeaveDate IS NOT NULL AND DATEADD(DAY,20, dates.ymd) < VEM.JobLeaveDate AND VEM.ContractDate < dates.ymd ) OR (VEM.ContractDate < dates.ymd AND VEM.JobLeaveDate IS NULL))
GROUP BY dates.ymd

UNION ALL 
SELECT dates.ymd, COALESCE(COUNT(*),0) CNT  , 1 AS WorkEmpType
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
GROUP BY dates.ymd

UNION ALL

SELECT dates.ymd, COALESCE(COUNT(*),0) CNT  , 4 AS WorkEmpType
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
GROUP BY dates.ymd

UNION ALL 

SELECT dates.ymd, COALESCE(COUNT(*),0) CNT   , 2 AS WorkEmpType
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
GROUP BY dates.ymd
 
UNION ALL 

SELECT dates.ymd, COALESCE(COUNT(*),0) CNT   , 3 AS WorkEmpType
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
	AND WOK.WorkEmpTypeMasterDetailID =3  --loai nhan su onsite
	AND WOK.DeptID = @DeptID 
) WOK
ON (
	(WOK.EndDate IS NOT NULL AND CONVERT(DATE, DATEADD(DAY, 20, dates.ymd)) between WOK.StartDate AND WOK.EndDate) 
	OR 
	(WOK.EndDate IS NULL AND WOK.StartDate < CONVERT(DATE, DATEADD(DAY, 20, dates.ymd)))
	)
LEFT OUTER JOIN ViewEmp VEM ON WOK.EmpID = VEM.ID 
GROUP BY dates.ymd

--NGHI VIEC THEO TUNG THANG 
UNION ALL 
SELECT ymd , SUM(CASE WHEN id IS NOT NULL THEN 1 ELSE 0 END) CNT   , 999 AS WorkEmpType FROM [dbo].[GetContractedJobLeavedEmpListAtDate] (@CompanyID,@DeptID,@TeamID,@StartDate,@EndDate)
GROUP BY ymd

--DOANH SO THEO TUNG THANG
UNION ALL 
SELECT ymd , SUM(COALESCE(InMonthDevMM,0) + COALESCE(InMonthManagementMM,0) + COALESCE(InMonthOnsiteMM,0)) CNT   , 1000 AS WorkEmpType FROM [dbo].[GetRevenueListAtDate] (@CompanyID,@DeptID,@TeamID,@StartDate,@EndDate)
GROUP BY ymd
GO


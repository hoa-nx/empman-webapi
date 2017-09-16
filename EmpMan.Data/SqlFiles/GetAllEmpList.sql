USE [EmpManAPI]
GO

/****** Object:  StoredProcedure [dbo].[GetAllEmpList]    Script Date: 2017/09/16 9:40:18 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetAllEmpList] (
@CompanyID INT =NULL ,
@DeptID INT =NULL,
@TeamID INT =NULL,
@StartDate DATE =NULL, 
@EndDate DATE =NULL )
AS 
BEGIN
IF(@StartDate IS NULL)
	SET @StartDate = CONVERT(DATE, GETDATE())

IF(@EndDate IS NULL)
	SET @EndDate = CONVERT(DATE, GETDATE())

;WITH dates AS(
SELECT 
	OBJ.NUMBER AS MTH
,	DATENAME(MONTH, DATEADD(MONTH, OBJ.NUMBER , DateAdd(day,1,EOMONTH(@StartDate,-1))))  AS MONTHNAME
,	DATEADD(MONTH, OBJ.NUMBER , DateAdd(day,1,EOMONTH(@StartDate,-1))) AS YMD 
FROM 
	MASTER.dbo.SPT_VALUES OBJ
WHERE 
	OBJ.TYPE= 'P' 
AND OBJ.NUMBER <= DATEDIFF(MONTH,CONVERT(DATE,@StartDate,120) , CONVERT(DATE,@EndDate,120)) 
) 
--NHAN VIEN DANG LAM VIEC
SELECT dates.ymd, VEM.* , 0 AS WorkEmpType
FROM dates   
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
--NHAN VIEN TU BP KHAC CHUYEN TOI
UNION ALL 
SELECT dates.ymd, VEM.* , 1 AS WorkEmpType
FROM dates   
INNER JOIN ( 
	SELECT 
		* 
	FROM 
		EmpDetailWorks WOK
	WHERE 
		WOK.WorkEmpTypeMasterID = 31 
	AND WOK.WorkEmpTypeMasterDetailID =1  --loai nhan su tu dept khac chuyen sang
	AND WOK.DeptID=1 
) WOK
ON (
	(WOK.EndDate IS NOT NULL AND dates.ymd between WOK.StartDate AND WOK.EndDate) 
	OR 
	(WOK.EndDate IS NULL AND WOK.StartDate < dates.ymd)
	)
LEFT OUTER JOIN ViewEmp VEM ON WOK.EmpID = VEM.ID 
--LOAI BO NHAN SU TAM NGHI GIUA CHUNG
UNION ALL

SELECT dates.ymd, VEM.* , 4 AS WorkEmpType
FROM dates   
INNER JOIN ( 
	SELECT 
		* 
	FROM 
		EmpDetailWorks WOK
	WHERE 
		WOK.WorkEmpTypeMasterID = 31 
	AND WOK.WorkEmpTypeMasterDetailID =4  --loai nhan su nghi tam thoi ( nghi thai san...)
	AND WOK.DeptID = 1 
) WOK
ON (
	(WOK.EndDate IS NOT NULL AND CONVERT(DATE, DATEADD(DAY, 5, dates.ymd)) between WOK.StartDate AND WOK.EndDate) 
	OR 
	(WOK.EndDate IS NULL AND WOK.StartDate < CONVERT(DATE, DATEADD(DAY, 5, dates.ymd)))
	)
	
LEFT OUTER JOIN ViewEmp VEM ON WOK.EmpID = VEM.ID 
--LOAI BO NHAN SU SANG DEPT KHAC LAM
UNION ALL 
SELECT dates.ymd, VEM.* , 3 AS WorkEmpType
FROM dates   
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

END 
GO


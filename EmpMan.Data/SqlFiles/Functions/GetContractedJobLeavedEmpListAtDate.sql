USE [EmpManAPI]
GO

/****** Object:  UserDefinedFunction [dbo].[GetContractedJobLeavedEmpListAtDate]    Script Date: 2017/09/16 22:04:10 ******/
DROP FUNCTION [dbo].[GetContractedJobLeavedEmpListAtDate]
GO

/****** Object:  UserDefinedFunction [dbo].[GetContractedJobLeavedEmpListAtDate]    Script Date: 2017/09/16 22:04:10 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE FUNCTION [dbo].[GetContractedJobLeavedEmpListAtDate] (
@CompanyID INT =NULL ,
@DeptID INT =NULL,
@TeamID INT =NULL,
@StartDate DATE =NULL, 
@EndDate DATE =NULL )

RETURNS TABLE 
AS 
RETURN 
--LAY DANH SACH NHAN VIEN CHINH THUC NGHI VIEC TRONG KHOANG THOI GIAN
SELECT dates.ymd, VEM.*  , 999 AS WorkEmpType
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
	AND EmpTypeMasterDetailID NOT IN(2)--khong lay PD 
    AND ContractDate IS NOT NULL 
    AND (JobLeaveDate  BETWEEN  DateAdd(day,1,EOMONTH(case when @StartDate IS NULL THEN CONVERT(DATE, GETDATE()) ELSE @StartDate  END,-1)) AND EOMONTH( DateAdd(day,1,EOMONTH(case when @EndDate IS NULL THEN CONVERT(DATE, GETDATE()) ELSE @EndDate  END,-1)) ) )
	) VEM 
ON ((VEM.JobLeaveDate IS NOT NULL AND convert(varchar(6),dates.YMD,112) = convert(varchar(6),VEM.JobLeaveDate,112) ) )
WHERE VEM.ID IS NOT NULL
GO


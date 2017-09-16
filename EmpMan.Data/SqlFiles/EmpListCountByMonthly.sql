﻿/** SQL LAY DANH SACH NHAN VIEN DANG LAM VIEC TRONG DEPT TAI MOI THANG .... START **/
;WITH DATES AS(
SELECT 
	OBJ.NUMBER AS MTH
,	DATENAME(MONTH, DATEADD(MONTH, OBJ.NUMBER , DATEADD(DAY,1,EOMONTH('2017-07-01',-1))))  AS MONTHNAME
,	DATEADD(MONTH, OBJ.NUMBER , DATEADD(DAY,1,EOMONTH('2017-07-01',-1))) AS YMD 
FROM 
	MASTER.DBO.SPT_VALUES OBJ
WHERE 
	OBJ.TYPE= 'P' 
AND OBJ.NUMBER <= DATEDIFF(MONTH,CONVERT(DATE,'2017-07-01',120) , CONVERT(DATE,'2017-07-20',120)) 
) 
SELECT DATES.YMD, VEM.* 
FROM DATES   
LEFT OUTER JOIN ( 
	SELECT 
		* 
	FROM 
		VIEWEMP 
	WHERE 
		CONTRACTDATE IS NOT NULL 
	AND CURRENTDEPTID=1) VEM 
ON ((VEM.JOBLEAVEDATE IS NOT NULL AND DATES.YMD < VEM.JOBLEAVEDATE AND VEM.CONTRACTDATE < DATES.YMD ) OR (VEM.CONTRACTDATE < DATES.YMD AND VEM.JOBLEAVEDATE IS NULL))


/** SQL LAY DANH SACH NHAN VIEN DANG TAM NGHI GIUA CHUNG CHANG HAN NHU NGHI THAI SAN .... START **/

;WITH DATES AS(
SELECT 
	OBJ.NUMBER AS MTH
,	DATENAME(MONTH, DATEADD(MONTH, OBJ.NUMBER , DATEADD(DAY,1,EOMONTH('2017-07-01',-1))))  AS MONTHNAME
,	DATEADD(MONTH, OBJ.NUMBER , DATEADD(DAY,1,EOMONTH('2017-07-01',-1))) AS YMD 
FROM 
	MASTER.DBO.SPT_VALUES OBJ
WHERE 
	OBJ.TYPE= 'P' 
AND OBJ.NUMBER <= DATEDIFF(MONTH,CONVERT(DATE,'2017-07-01',120) , CONVERT(DATE,'2018-11-20',120)) 
) 
SELECT DATES.YMD, VEM.* 
FROM DATES   
INNER JOIN ( 
	SELECT 
		* 
	FROM 
		EMPDETAILWORKS WOK
	WHERE 
		WOK.WORKEMPTYPEMASTERID = 31 
	AND WOK.WORKEMPTYPEMASTERDETAILID =4  --LOAI NHAN SU NGHI TAM THOI ( NGHI THAI SAN...)
	AND WOK.DEPTID = 1 
) WOK
ON (
	(WOK.ENDDATE IS NOT NULL AND CONVERT(DATE, DATEADD(DAY, 5, DATES.YMD)) BETWEEN WOK.STARTDATE AND WOK.ENDDATE) 
	OR 
	(WOK.ENDDATE IS NULL AND WOK.STARTDATE < CONVERT(DATE, DATEADD(DAY, 5, DATES.YMD)))
	)
LEFT OUTER JOIN VIEWEMP VEM ON WOK.EMPID = VEM.ID 


/** SQL LAY DANH SACH NHAN VIEN SANG DEPT KHAC HO TRO  .... START **/
;WITH DATES AS(
SELECT 
	OBJ.NUMBER AS MTH
,	DATENAME(MONTH, DATEADD(MONTH, OBJ.NUMBER , DATEADD(DAY,1,EOMONTH('2017-07-01',-1))))  AS MONTHNAME
,	DATEADD(MONTH, OBJ.NUMBER , DATEADD(DAY,1,EOMONTH('2017-07-01',-1))) AS YMD 
FROM 
	MASTER.DBO.SPT_VALUES OBJ
WHERE 
	OBJ.TYPE= 'P' 
AND OBJ.NUMBER <= DATEDIFF(MONTH,CONVERT(DATE,'2017-07-01',120) , CONVERT(DATE,'2018-11-20',120)) 
)
SELECT DATES.YMD, VEM.* 
FROM DATES   
INNER JOIN ( 
	SELECT 
		* 
	FROM 
		EMPDETAILWORKS WOK
	WHERE 
		WOK.WORKEMPTYPEMASTERID = 31 
	AND WOK.WORKEMPTYPEMASTERDETAILID =2  --LOAI NHAN SU SANG DEPT KHAC HO TRO
	AND WOK.DEPTID <> 1 
) WOK
ON (
	(WOK.ENDDATE IS NOT NULL AND CONVERT(DATE, DATEADD(DAY, 20, DATES.YMD)) BETWEEN WOK.STARTDATE AND WOK.ENDDATE) 
	OR 
	(WOK.ENDDATE IS NULL AND WOK.STARTDATE < CONVERT(DATE, DATEADD(DAY, 20, DATES.YMD)))
	)
LEFT OUTER JOIN VIEWEMP VEM ON WOK.EMPID = VEM.ID 


/** SQL LAY DANH SACH NHAN VIEN TU DEPT KHAC CHUYEN SANG DEPT MINH .... START **/
;WITH DATES AS(
SELECT 
	OBJ.NUMBER AS MTH
,	DATENAME(MONTH, DATEADD(MONTH, OBJ.NUMBER , DATEADD(DAY,1,EOMONTH('2017-07-01',-1))))  AS MONTHNAME
,	DATEADD(MONTH, OBJ.NUMBER , DATEADD(DAY,1,EOMONTH('2017-07-01',-1))) AS YMD 
FROM 
	MASTER.DBO.SPT_VALUES OBJ
WHERE 
	OBJ.TYPE= 'P' 
AND OBJ.NUMBER <= DATEDIFF(MONTH,CONVERT(DATE,'2017-07-01',120) , CONVERT(DATE,'2018-11-20',120)) 
)
SELECT DATES.YMD, VEM.* 
FROM DATES   
INNER JOIN ( 
	SELECT 
		* 
	FROM 
		EMPDETAILWORKS WOK
	WHERE 
		WOK.WORKEMPTYPEMASTERID = 31 
	AND WOK.WORKEMPTYPEMASTERDETAILID =1  --LOAI NHAN SU TU DEPT KHAC CHUYEN SANG
	AND WOK.DEPTID=1 
) WOK
ON (
	(WOK.ENDDATE IS NOT NULL AND DATES.YMD BETWEEN WOK.STARTDATE AND WOK.ENDDATE) 
	OR 
	(WOK.ENDDATE IS NULL AND WOK.STARTDATE < DATES.YMD)
	)
LEFT OUTER JOIN VIEWEMP VEM ON WOK.EMPID = VEM.ID 

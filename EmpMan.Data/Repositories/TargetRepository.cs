using EmpMan.Common;
using EmpMan.Common.ViewModels;
using EmpMan.Data.Infrastructure;
using EmpMan.Model.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpMan.Data.Repositories
{

    public interface ITargetRepository : IRepository<Target>
    {
        IEnumerable<StatisticViewModel> GetCurrentTotalMMAndTargetMMStatistic();
        IEnumerable<StatisticViewModel> GetMMByTypeAndYearMonthStatistic(int?[] years ,bool isUnpivotColumnToRows);
        IEnumerable<ReportStatisticViewModel> GetMMByTypeAndYearMonthStatisticReport(int year);
        IEnumerable<StatisticViewModel> GetCompareMMByTypeAndYearMonthStatistic(int prevYear, int curYear);
        IEnumerable<StatisticViewModel> GetCompareCutommerMMByTypeAndYearMonthStatistic(int curYear);
        IEnumerable<StatisticViewModel> GetCompareCutommerMMByTypeAndYearMonthStatistic(int prevYear, int curYear);
        IEnumerable<WorkEmpTypeStatisticViewModel> GetEmpCountMonthlyStatistic(int? companyID, int? deptID, int? teamID, DateTime startDate, DateTime endDate);


    }

    public class TargetRepository : RepositoryBase<Target>, ITargetRepository
    {
        public TargetRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }

        public IEnumerable<StatisticViewModel> GetCurrentTotalMMAndTargetMMStatistic()
        {
            
            StringBuilder sql = new StringBuilder();

            sql.AppendLine(" SELECT ");
            sql.AppendLine("    TAR.COMPANYID ");
            sql.AppendLine(" ,  TAR.DEPTID  ");
            sql.AppendLine(" ,  YEAR(TAR.YEARMONTH) YMD ");
            sql.AppendLine(" ,  SUM(COALESCE (TAR.DEVMM,0))  DevMMTarget");
            sql.AppendLine(" ,  SUM(COALESCE (TAR.OnsiteMM,0))  OnsiteMMTarget");
            sql.AppendLine(" ,  SUM(COALESCE (TAR.ManMM,0))  ManMMTarget");
            sql.AppendLine(" ,  SUM(COALESCE (TOTALMM,0) - COALESCE(ONSITEMM,0)) TOTALMMTARGETEXCLUDEONSITE ");
            sql.AppendLine(" ,  SUM(COALESCE (TOTALMM,0)) TOTALMMTARGET  ");
            sql.AppendLine(" ,  SUM(REV.ACTUALMM) TOTALMMTOCURRENTACTUAL ");
            sql.AppendLine(" ,  SUM(REV.ACTUALMMEXCLUDEONSITE) TOTALMMTOCURRENTACTUALEXCLUDEONSITE ");

            sql.AppendLine(" FROM TARGETS TAR ");
            sql.AppendLine(" LEFT OUTER JOIN ");
            sql.AppendLine(" (SELECT DT.COMPANYID, DT.DEPTID , YEAR(DT.REPORTYEARMONTH) YM, SUM(COALESCE (DT.INMONTHDEVMM,0) + COALESCE (DT.INMONTHMANAGEMENTMM,0) + COALESCE (DT.INMONTHONSITEMM,0)  ) ACTUALMM , SUM(COALESCE (DT.INMONTHDEVMM,0) + COALESCE (DT.INMONTHMANAGEMENTMM,0)  ) ACTUALMMEXCLUDEONSITE ");
            sql.AppendLine(" FROM REVENUES  DT ");
            sql.AppendLine(" GROUP BY DT.COMPANYID, DT.DEPTID ,YEAR(DT.REPORTYEARMONTH)) REV ");

            sql.AppendLine(" ON  TAR.COMPANYID = REV.COMPANYID ");
            sql.AppendLine(" AND TAR.DEPTID = REV.DEPTID ");
            sql.AppendLine(" AND  YEAR(TAR.YEARMONTH) = REV.YM ");
            sql.AppendLine(" GROUP BY TAR.COMPANYID, TAR.DEPTID , YEAR(TAR.YEARMONTH) ");

            return DbContext.Database.SqlQuery<StatisticViewModel>(sql.ToString());
            
        }
        
        /// <summary>
        /// Lay doanh so theo thang nam
        /// </summary>
        /// <param name="year"></param>
        /// <param name="isUnpivotColumnToRows"></param>
        /// <returns></returns>
        public IEnumerable<StatisticViewModel> GetMMByTypeAndYearMonthStatistic(int?[] years, bool isUnpivotColumnToRows)
        {
            StringBuilder sql = new StringBuilder();

            sql = this.getMMByTypeAndYearMonthStatisticSqlUnpivot( years, isUnpivotColumnToRows);
            
            return DbContext.Database.SqlQuery<StatisticViewModel>(sql.ToString());

        }

        /// <summary>
        /// Lay doanh so theo thang nam
        /// </summary>
        /// <param name="year"></param>
        /// <param name="isUnpivotColumnToRows"></param>
        /// <returns></returns>
        public IEnumerable<StatisticViewModel> GetCompareMMByTypeAndYearMonthStatistic(int prevYear , int curYear)
        {
            StringBuilder sql = new StringBuilder();
            string fullSql = "";

            sql = this.getMMByTypeAndYearMonthStatisticSqlUnpivot(new int?[] { prevYear, curYear }, false);

            fullSql += @" SELECT MONTHTONAME =
                                  CASE WHEN REV.MONTHTONAME ='Jan' THEN N'Tháng 01' 
		                                WHEN REV.MONTHTONAME ='Feb' THEN N'Tháng 02' 
		                                WHEN REV.MONTHTONAME ='Mar' THEN N'Tháng 03' 
		                                WHEN REV.MONTHTONAME ='Apr' THEN N'Tháng 04' 
		                                WHEN REV.MONTHTONAME ='May' THEN N'Tháng 05' 
		                                WHEN REV.MONTHTONAME ='Jun' THEN N'Tháng 06' 
		                                WHEN REV.MONTHTONAME ='Jul' THEN N'Tháng 07' 
		                                WHEN REV.MONTHTONAME ='Aug' THEN N'Tháng 08' 
		                                WHEN REV.MONTHTONAME ='Sep' THEN N'Tháng 09' 
		                                WHEN REV.MONTHTONAME ='Oct' THEN N'Tháng 10' 
		                                WHEN REV.MONTHTONAME ='Nov' THEN N'Tháng 11' 
		                                WHEN REV.MONTHTONAME ='Dec' THEN N'Tháng 12' END 

                                 , REV.COMPANYID
                                 , REV.DEPTID
                                 , SUM(case when YEAR(REPORTYEARMONTH )= @CURRENT_YEAR@ THEN REV.INMONTHDEVMM ELSE 0 END ) INMONTHDEVMM
                                 , SUM(case when YEAR(REPORTYEARMONTH )= @PREV_YEAR@ THEN REV.INMONTHDEVMM ELSE 0 END ) INMONTHDEVMM_PREV

                                 , SUM(case when YEAR(REPORTYEARMONTH )= @CURRENT_YEAR@ THEN REV.INMONTHTRANSMM ELSE 0 END ) INMONTHTRANSMM
                                 , SUM(case when YEAR(REPORTYEARMONTH )= @PREV_YEAR@ THEN REV.INMONTHTRANSMM ELSE 0 END ) INMONTHTRANSMM_PREV

                                 , SUM(case when YEAR(REPORTYEARMONTH )= @CURRENT_YEAR@ THEN REV.INMONTHMANAGEMENTMM ELSE 0 END ) INMONTHMANAGEMENTMM
                                 , SUM(case when YEAR(REPORTYEARMONTH )= @PREV_YEAR@ THEN REV.INMONTHMANAGEMENTMM ELSE 0 END ) INMONTHMANAGEMENTMM_PREV

                                 , SUM(case when YEAR(REPORTYEARMONTH )= @CURRENT_YEAR@ THEN REV.INMONTHONSITEMM ELSE 0 END ) INMONTHONSITEMM
                                 , SUM(case when YEAR(REPORTYEARMONTH )= @PREV_YEAR@ THEN REV.INMONTHONSITEMM ELSE 0 END ) INMONTHONSITEMM_PREV

                                 , SUM(case when YEAR(REPORTYEARMONTH )= @CURRENT_YEAR@ THEN REV.InMonthSumMM ELSE 0 END ) InMonthSumMM
                                 , SUM(case when YEAR(REPORTYEARMONTH )= @PREV_YEAR@ THEN REV.InMonthSumMM ELSE 0 END ) InMonthSumMM_PREV

                                 , SUM(case when YEAR(REPORTYEARMONTH )= @CURRENT_YEAR@ THEN REV.InMonthSumIncludeOnsiteMM ELSE 0 END ) InMonthSumIncludeOnsiteMM
                                 , SUM(case when YEAR(REPORTYEARMONTH )= @PREV_YEAR@ THEN REV.InMonthSumIncludeOnsiteMM ELSE 0 END ) InMonthSumIncludeOnsiteMM_PREV

                                 , SUM(case when YEAR(REPORTYEARMONTH )= @CURRENT_YEAR@ THEN REV.InMonthDevSumExcludeTransOnsiteMM ELSE 0 END ) InMonthDevSumExcludeTransOnsiteMM
                                 , SUM(case when YEAR(REPORTYEARMONTH )= @PREV_YEAR@ THEN REV.InMonthDevSumExcludeTransOnsiteMM ELSE 0 END ) InMonthDevSumExcludeTransOnsiteMM_PREV

                                from (";

            fullSql += sql;

            fullSql += @" ) REV

                            GROUP BY REV.COMPANYID, REV.DEPTID , REV.MONTHTONAME

                            ORDER BY MONTHTONAME";

            fullSql = fullSql.Replace("@CURRENT_YEAR@", curYear.ToString()).Replace("@PREV_YEAR@", prevYear.ToString()).Replace("ORDER BY COMPANYID , DEPTID , REPORTYEARMONTH" ,"");
            //add them sql dung de gom nhom du lieu 

            return DbContext.Database.SqlQuery<StatisticViewModel>(fullSql);

        }

        /// <summary>
        /// Lay doanh so theo tung khach hang cua nam nay
        /// </summary>
        /// <param name="curYear"></param>
        /// <returns></returns>
        public IEnumerable<StatisticViewModel> GetCompareCutommerMMByTypeAndYearMonthStatistic(int curYear)
        {
            StringBuilder sql = new StringBuilder();

            sql = this.getCustomerMMByTypeAndYearMonthStatisticSql(new int?[] { curYear });

            return DbContext.Database.SqlQuery<StatisticViewModel>(sql.ToString());

        }

        /// <summary>
        /// Lay doanh so theo tung khach hang cua nam nay va nam truoc
        /// </summary>
        /// <param name="prevYear"></param>
        /// <param name="curYear"></param>
        /// <returns></returns>
        public IEnumerable<StatisticViewModel> GetCompareCutommerMMByTypeAndYearMonthStatistic(int prevYear, int curYear)
        {
            StringBuilder sql = new StringBuilder();

            sql = this.getCustomerMMByTypeAndYearMonthStatisticSql(new int?[] { prevYear,curYear });

            return DbContext.Database.SqlQuery<StatisticViewModel>(sql.ToString());

        }

        /// <summary>
        /// Lấy sql để get doanh số của các năm
        /// </summary>
        /// <param name="years"></param>
        /// <returns></returns>
        private StringBuilder getMMByTypeAndYearMonthStatisticSqlUnpivot(int?[] years, bool isUnpivotColumnToRows)
        {
            StringBuilder sql = new StringBuilder();
            int countYear = 0;
            if (isUnpivotColumnToRows)
            {
                for(int i=0; i< years.Length; i++)
                {
                    int startYearMonth = int.Parse((years[i] + "01"));
                    if(countYear>0)
                    {
                        sql.AppendLine(" UNION ALL ");
                    }
                    sql.AppendLine(" SELECT ");
                    sql.AppendLine("     TAB.COMPANYID");
                    sql.AppendLine("     , MAX(COMPANYS.NAME) COMPANYNAME");
                    sql.AppendLine("     , TAB.DEPTID");
                    sql.AppendLine("     , MAX(DEPTS.NAME) DEPTNAME");
                    sql.AppendLine("     , TAB.NAME");
                    sql.AppendLine("     , SUM(CASE WHEN FORMAT(TAB.REPORTYEARMONTH, 'yyyyMM') =  " + startYearMonth + " THEN VALUE END)  MONTH1");
                    sql.AppendLine("     , SUM(CASE WHEN FORMAT(TAB.REPORTYEARMONTH, 'yyyyMM') =  " + startYearMonth + 1 + "  THEN VALUE END)  MONTH2");
                    sql.AppendLine("     , SUM(CASE WHEN FORMAT(TAB.REPORTYEARMONTH, 'yyyyMM') =  " + startYearMonth + 2 + "  THEN VALUE END)  MONTH3");
                    sql.AppendLine("     , SUM(CASE WHEN FORMAT(TAB.REPORTYEARMONTH, 'yyyyMM') =  " + startYearMonth + 3 + "  THEN VALUE END)  MONTH4");
                    sql.AppendLine("     , SUM(CASE WHEN FORMAT(TAB.REPORTYEARMONTH, 'yyyyMM') =  " + startYearMonth + 4 + "  THEN VALUE END)  MONTH5");
                    sql.AppendLine("     , SUM(CASE WHEN FORMAT(TAB.REPORTYEARMONTH, 'yyyyMM') =  " + startYearMonth + 5 + "  THEN VALUE END)  MONTH6");
                    sql.AppendLine("     , SUM(CASE WHEN FORMAT(TAB.REPORTYEARMONTH, 'yyyyMM') =  " + startYearMonth + 6 + "  THEN VALUE END)  MONTH7");
                    sql.AppendLine("     , SUM(CASE WHEN FORMAT(TAB.REPORTYEARMONTH, 'yyyyMM') =  " + startYearMonth + 7 + "  THEN VALUE END)  MONTH8");
                    sql.AppendLine("     , SUM(CASE WHEN FORMAT(TAB.REPORTYEARMONTH, 'yyyyMM') =  " + startYearMonth + 8 + "  THEN VALUE END)  MONTH9");
                    sql.AppendLine("     , SUM(CASE WHEN FORMAT(TAB.REPORTYEARMONTH, 'yyyyMM') =  " + startYearMonth + 9 + "  THEN VALUE END)  MONTH10");
                    sql.AppendLine("     , SUM(CASE WHEN FORMAT(TAB.REPORTYEARMONTH, 'yyyyMM') =  " + startYearMonth + 10 + "  THEN VALUE END)  MONTH11");
                    sql.AppendLine("     , SUM(CASE WHEN FORMAT(TAB.REPORTYEARMONTH, 'yyyyMM') =  " + startYearMonth + 11 + "  THEN VALUE END)  MONTH12");
                    sql.AppendLine("     FROM");
                    sql.AppendLine("     (");
                    sql.AppendLine("         SELECT REPORTYEARMONTH, COMPANYID, DEPTID,");
                    sql.AppendLine("                  CASE UNP.NAME");
                    sql.AppendLine("                     WHEN 'INMONTHDEVMM' THEN '1.LẬP TRÌNH'");
                    sql.AppendLine("                     WHEN 'INMONTHTRANSMM' THEN '2.PHIÊN DỊCH'");
                    sql.AppendLine("                     WHEN 'INMONTHMANAGEMENTMM' THEN '3.QUẢN LÝ'");
                    sql.AppendLine("                     WHEN 'INMONTHONSITEMM' THEN '4.ONSITE DÀI HẠN'");
                    sql.AppendLine("                     WHEN 'INMONTHSUMMMEXCLUDETRANS' THEN '5.TỔNG MM KHÔNG TÍNH PD'");
                    sql.AppendLine("                     WHEN 'INMONTHSUMMMEXCLUDETRANSONSITE' THEN '6.TỔNG MM KHÔNG TÍNH PD VÀ ONSITE'");
                    sql.AppendLine("                     WHEN 'INMONTHSUMMM' THEN '7.TỔNG CỘNG'");
                    sql.AppendLine("                     ELSE  UNP.NAME END 'NAME'");
                    sql.AppendLine("                  , UNP.VALUE");
                    sql.AppendLine("                  FROM(SELECT");
                    sql.AppendLine("                     REPORTYEARMONTH, COMPANYID, DEPTID");
                    sql.AppendLine("                  , SUM(COALESCE(INMONTHDEVMM, 0)) INMONTHDEVMM");
                    sql.AppendLine("                  , SUM(COALESCE(INMONTHTRANSMM, 0)) INMONTHTRANSMM");
                    sql.AppendLine("                  , SUM(COALESCE(INMONTHMANAGEMENTMM, 0)) INMONTHMANAGEMENTMM");
                    sql.AppendLine("                  , SUM(COALESCE(INMONTHONSITEMM, 0)) INMONTHONSITEMM");
                    sql.AppendLine("                  , SUM(COALESCE(INMONTHSUMMM, 0)) + SUM(COALESCE(INMONTHONSITEMM, 0)) INMONTHSUMMM");
                    sql.AppendLine("                  , SUM(COALESCE(INMONTHSUMMM, 0)) - SUM(COALESCE(INMONTHTRANSMM, 0)) - SUM(COALESCE(INMONTHONSITEMM, 0)) INMONTHSUMMMEXCLUDETRANSONSITE");
                    sql.AppendLine("                  , SUM(COALESCE(INMONTHSUMMM, 0)) - SUM(COALESCE(INMONTHTRANSMM, 0)) INMONTHSUMMMEXCLUDETRANS");
                    sql.AppendLine("                  FROM REVENUES");
                    if (years[i] != 0)
                    {
                        sql.AppendLine(" WHERE YEAR(REPORTYEARMONTH) =  " + years[i]);
                    }
                    sql.AppendLine("                  GROUP BY");
                    sql.AppendLine("                  REPORTYEARMONTH, COMPANYID, DEPTID");
                    sql.AppendLine("                  ) REV");
                    sql.AppendLine("                  UNPIVOT(");
                    sql.AppendLine("                  VALUE");

                    sql.AppendLine("                      FOR NAME IN(INMONTHDEVMM, INMONTHTRANSMM, INMONTHMANAGEMENTMM, INMONTHONSITEMM, INMONTHSUMMMEXCLUDETRANS, INMONTHSUMMMEXCLUDETRANSONSITE, INMONTHSUMMM)");
                    sql.AppendLine("                  ) UNP");
                    sql.AppendLine(" ) TAB");
                    sql.AppendLine(" LEFT OUTER JOIN COMPANYS ON TAB.COMPANYID = COMPANYS.ID");
                    sql.AppendLine(" LEFT OUTER JOIN DEPTS ON TAB.DEPTID = DEPTS.ID");
                    sql.AppendLine(" GROUP BY");
                    sql.AppendLine("     TAB.COMPANYID");
                    sql.AppendLine("     , TAB.DEPTID");
                    sql.AppendLine("     , TAB.NAME");

                    countYear++;
                }
                

            }
            else
            {
                sql.AppendLine(" SELECT ");
                sql.AppendLine("   REPORTYEARMONTH ");
                sql.AppendLine(" , CONVERT(CHAR(3), REPORTYEARMONTH, 0) MONTHTONAME ");
                sql.AppendLine(" , COMPANYID ");
                sql.AppendLine(" , DEPTID ");
                sql.AppendLine(" , SUM(COALESCE(INMONTHDEVMM, 0)) INMONTHDEVMM ");
                sql.AppendLine(" , SUM(COALESCE(INMONTHTRANSMM, 0)) INMONTHTRANSMM ");
                sql.AppendLine(" , SUM(COALESCE(INMONTHMANAGEMENTMM, 0)) INMONTHMANAGEMENTMM ");
                sql.AppendLine(" , SUM(COALESCE(INMONTHONSITEMM, 0)) INMONTHONSITEMM ");
                sql.AppendLine(" , SUM(COALESCE(INMONTHSUMMM, 0)) + SUM(COALESCE(INMONTHONSITEMM, 0))  InMonthSumMM ");   //tong tat ca MM
                sql.AppendLine(" , SUM(COALESCE(INMONTHSUMMM, 0)) + SUM(COALESCE(INMONTHONSITEMM, 0)) - SUM(COALESCE(INMONTHTRANSMM, 0))  InMonthSumIncludeOnsiteMM ");   //tong tat ca MM - Phien dich
                sql.AppendLine(" , SUM(COALESCE(INMONTHSUMMM, 0)) - SUM(COALESCE(INMONTHTRANSMM, 0)) - SUM(COALESCE(INMONTHONSITEMM, 0))  InMonthDevSumExcludeTransOnsiteMM ");   //tong tat ca MM - Phien dich - Onsite
                                                                                                                                                                                  //sql.AppendLine(" , SUM(COALESCE(INMONTHDEVMM, 0)) OVER(PARTITION BY YEAR(REPORTYEARMONTH) ORDER BY COMPANYID, DEPTID, REPORTYEARMONTH )  InMonthDevSumOverYearMM ");   //tong MM lap trinh trong nam

                sql.AppendLine(" FROM REVENUES ");
                if (years != null)
                {
                    sql.AppendLine(" WHERE YEAR(REPORTYEARMONTH) IN ( " + StringHelper.GetSqlStringFromArrayInt(years) + " )");

                }
                sql.AppendLine(" GROUP BY ");
                sql.AppendLine(" COMPANYID, DEPTID, REPORTYEARMONTH");


                sql.AppendLine(" ORDER BY COMPANYID , DEPTID , REPORTYEARMONTH");
            }
            

            return sql;

        }

        /// <summary>
        /// Lấy sql để get doanh số của các năm theo tung khach hang
        /// </summary>
        /// <param name="years"></param>
        /// <returns></returns>
        private StringBuilder getCustomerMMByTypeAndYearMonthStatisticSql(int?[] years)
        {
            StringBuilder sql = new StringBuilder();

            sql.AppendLine(" SELECT ");
            sql.AppendLine(" 	YEAR(REV.REPORTYEARMONTH)  YEAR ");
            sql.AppendLine("  , REV.COMPANYID  ");
            sql.AppendLine("  , REV.DEPTID  ");
            sql.AppendLine("  , REV.CustomerID ");
            sql.AppendLine("  , MAX(CUS.ShortName) CustomerName ");
            sql.AppendLine("  , SUM(COALESCE(REV.INMONTHDEVMM, 0)) INMONTHDEVMM  ");
            sql.AppendLine("  , SUM(COALESCE(REV.INMONTHTRANSMM, 0)) INMONTHTRANSMM  ");
            sql.AppendLine("  , SUM(COALESCE(REV.INMONTHMANAGEMENTMM, 0)) INMONTHMANAGEMENTMM  ");
            sql.AppendLine("  , SUM(COALESCE(REV.INMONTHONSITEMM, 0)) INMONTHONSITEMM  ");
            sql.AppendLine("  , SUM(COALESCE(REV.INMONTHSUMMM, 0)) + SUM(COALESCE(REV.INMONTHONSITEMM, 0))  InMonthSumMM  ");
            sql.AppendLine("  , SUM(COALESCE(REV.INMONTHSUMMM, 0)) + SUM(COALESCE(REV.INMONTHONSITEMM, 0)) - SUM(COALESCE(REV.INMONTHTRANSMM, 0))  InMonthSumIncludeOnsiteMM  ");
            sql.AppendLine("  , SUM(COALESCE(REV.INMONTHSUMMM, 0)) - SUM(COALESCE(REV.INMONTHTRANSMM, 0)) - SUM(COALESCE(REV.INMONTHONSITEMM, 0))  InMonthDevSumExcludeTransOnsiteMM  ");
            sql.AppendLine("  FROM REVENUES REV LEFT OUTER JOIN Customers CUS ON REV.CustomerID = CUS.ID ");
            if (years != null)
            {
                sql.AppendLine(" WHERE YEAR(REV.REPORTYEARMONTH) IN ( " + StringHelper.GetSqlStringFromArrayInt(years) + " )");

            }
            sql.AppendLine("  GROUP BY  ");
            sql.AppendLine("  YEAR(REV.REPORTYEARMONTH),REV.COMPANYID, REV.DEPTID, REV.CustomerID ");
            sql.AppendLine("  ORDER BY YEAR(REV.REPORTYEARMONTH),REV.COMPANYID , REV.DEPTID , REV.CustomerID ");

            return sql;

        }

        public IEnumerable<ReportStatisticViewModel> GetMMByTypeAndYearMonthStatisticReport(int year)
        {
            StringBuilder sql = new StringBuilder();
            int startYearMonth = int.Parse((year + "01"));

            sql.AppendLine(" SELECT ");
            sql.AppendLine("     TAB.COMPANYID");
            sql.AppendLine("     , MAX(COMPANYS.NAME) COMPANYNAME");
            sql.AppendLine("     , TAB.DEPTID");
            sql.AppendLine("     , MAX(DEPTS.NAME) DEPTNAME");
            sql.AppendLine("     , TAB.NAME CONTENT");
            sql.AppendLine("     , SUM(COALESCE(CASE WHEN FORMAT(TAB.REPORTYEARMONTH, 'yyyyMM') =  " + startYearMonth + " THEN VALUE END,0))  MONTH1");
            sql.AppendLine("     , SUM(COALESCE(CASE WHEN FORMAT(TAB.REPORTYEARMONTH, 'yyyyMM') =  " + (startYearMonth + 1) + "  THEN VALUE END,0))  MONTH2");
            sql.AppendLine("     , SUM(COALESCE(CASE WHEN FORMAT(TAB.REPORTYEARMONTH, 'yyyyMM') =  " + (startYearMonth + 2) + "  THEN VALUE END,0))  MONTH3");
            sql.AppendLine("     , SUM(COALESCE(CASE WHEN FORMAT(TAB.REPORTYEARMONTH, 'yyyyMM') =  " + (startYearMonth + 3) + "  THEN VALUE END,0))  MONTH4");
            sql.AppendLine("     , SUM(COALESCE(CASE WHEN FORMAT(TAB.REPORTYEARMONTH, 'yyyyMM') =  " + (startYearMonth + 4) + "  THEN VALUE END,0))  MONTH5");
            sql.AppendLine("     , SUM(COALESCE(CASE WHEN FORMAT(TAB.REPORTYEARMONTH, 'yyyyMM') =  " + (startYearMonth + 5) + "  THEN VALUE END,0))  MONTH6");
            sql.AppendLine("     , SUM(COALESCE(CASE WHEN FORMAT(TAB.REPORTYEARMONTH, 'yyyyMM') =  " + (startYearMonth + 6) + "  THEN VALUE END,0))  MONTH7");
            sql.AppendLine("     , SUM(COALESCE(CASE WHEN FORMAT(TAB.REPORTYEARMONTH, 'yyyyMM') =  " + (startYearMonth + 7) + "  THEN VALUE END,0))  MONTH8");
            sql.AppendLine("     , SUM(COALESCE(CASE WHEN FORMAT(TAB.REPORTYEARMONTH, 'yyyyMM') =  " + (startYearMonth + 8) + "  THEN VALUE END,0))  MONTH9");
            sql.AppendLine("     , SUM(COALESCE(CASE WHEN FORMAT(TAB.REPORTYEARMONTH, 'yyyyMM') =  " + (startYearMonth + 9) + "  THEN VALUE END,0))  MONTH10");
            sql.AppendLine("     , SUM(COALESCE(CASE WHEN FORMAT(TAB.REPORTYEARMONTH, 'yyyyMM') =  " + (startYearMonth + 10) + "  THEN VALUE END,0))  MONTH11");
            sql.AppendLine("     , SUM(COALESCE(CASE WHEN FORMAT(TAB.REPORTYEARMONTH, 'yyyyMM') =  " + (startYearMonth + 11) + "  THEN VALUE END,0))  MONTH12");

            sql.AppendLine("     FROM");
            sql.AppendLine("     (");
            sql.AppendLine("         SELECT REPORTYEARMONTH, COMPANYID, DEPTID,");
            sql.AppendLine("                  CASE UNP.NAME");
            sql.AppendLine("                     WHEN 'INMONTHDEVMM' THEN N'1.Lập trình'");
            sql.AppendLine("                     WHEN 'INMONTHTRANSMM' THEN N'2.Phiên dịch'");
            sql.AppendLine("                     WHEN 'INMONTHMANAGEMENTMM' THEN N'3.Quản lý'");
            sql.AppendLine("                     WHEN 'INMONTHONSITEMM' THEN N'4.Onsite dài hạn'");
            sql.AppendLine("                     WHEN 'INMONTHSUMMMEXCLUDETRANS' THEN N'5.Tổng MM(không tính PD)'");
            sql.AppendLine("                     WHEN 'INMONTHSUMMMEXCLUDETRANSONSITE' THEN N'6.Tổng MM(không tính PD và Onsite)'");
            sql.AppendLine("                     WHEN 'INMONTHSUMMM' THEN N'7.Tổng MM'");
            sql.AppendLine("                     ELSE  UNP.NAME END 'NAME'");
            sql.AppendLine("                  , UNP.VALUE");
            sql.AppendLine("                  FROM(SELECT");
            sql.AppendLine("                     REPORTYEARMONTH, COMPANYID, DEPTID");
            sql.AppendLine("                  , SUM(COALESCE(INMONTHDEVMM, 0)) INMONTHDEVMM");
            sql.AppendLine("                  , SUM(COALESCE(INMONTHTRANSMM, 0)) INMONTHTRANSMM");
            sql.AppendLine("                  , SUM(COALESCE(INMONTHMANAGEMENTMM, 0)) INMONTHMANAGEMENTMM");
            sql.AppendLine("                  , SUM(COALESCE(INMONTHONSITEMM, 0)) INMONTHONSITEMM");
            sql.AppendLine("                  , SUM(COALESCE(INMONTHSUMMM, 0)) + SUM(COALESCE(INMONTHONSITEMM, 0)) INMONTHSUMMM");
            sql.AppendLine("                  , SUM(COALESCE(INMONTHSUMMM, 0)) - SUM(COALESCE(INMONTHTRANSMM, 0)) - SUM(COALESCE(INMONTHONSITEMM, 0)) INMONTHSUMMMEXCLUDETRANSONSITE");
            sql.AppendLine("                  , SUM(COALESCE(INMONTHSUMMM, 0)) - SUM(COALESCE(INMONTHTRANSMM, 0)) INMONTHSUMMMEXCLUDETRANS");
            sql.AppendLine("                  FROM REVENUES");
            if (year != 0)
            {
                sql.AppendLine(" WHERE YEAR(REPORTYEARMONTH) =  " + year);
            }
            sql.AppendLine("                  GROUP BY");
            sql.AppendLine("                  REPORTYEARMONTH, COMPANYID, DEPTID");
            sql.AppendLine("                  ) REV");
            sql.AppendLine("                  UNPIVOT(");
            sql.AppendLine("                  VALUE");

            sql.AppendLine("                      FOR NAME IN(INMONTHDEVMM, INMONTHTRANSMM, INMONTHMANAGEMENTMM, INMONTHONSITEMM, INMONTHSUMMMEXCLUDETRANS, INMONTHSUMMMEXCLUDETRANSONSITE, INMONTHSUMMM)");
            sql.AppendLine("                  ) UNP");
            sql.AppendLine(" ) TAB");
            sql.AppendLine(" LEFT OUTER JOIN COMPANYS ON TAB.COMPANYID = COMPANYS.ID");
            sql.AppendLine(" LEFT OUTER JOIN DEPTS ON TAB.DEPTID = DEPTS.ID");
            sql.AppendLine(" GROUP BY");
            sql.AppendLine("     TAB.COMPANYID");
            sql.AppendLine("     , TAB.DEPTID");
            sql.AppendLine("     , TAB.NAME");
            sql.AppendLine(" ORDER BY");
            sql.AppendLine("     TAB.COMPANYID");
            sql.AppendLine("     , TAB.DEPTID");
            sql.AppendLine("     , TAB.NAME");

            return DbContext.Database.SqlQuery<ReportStatisticViewModel>(sql.ToString());

        }

        public IEnumerable<WorkEmpTypeStatisticViewModel> GetEmpCountMonthlyStatistic(int? companyID, int? deptID, int? teamID, DateTime startDate, DateTime endDate)
        {
            var companyIDParam = new SqlParameter
            {
                ParameterName = "CompanyID",
                Value = companyID
            };
            var deptIDParam = new SqlParameter
            {
                ParameterName = "DeptID",
                Value = deptID
            };
            var teamIDParam = new SqlParameter
            {
                ParameterName = "TeamID",
                Value = null
            };
            var startDateParam = new SqlParameter
            {
                ParameterName = "StartDate",
                Value = startDate
            };
            var endDateParam = new SqlParameter
            {
                ParameterName = "EndDate",
                Value = endDate
            };

            string sql = @" SELECT 
                                YMD 
                                , cast(convert(varchar(6),YMD,112) as nvarchar)  YM
                                , [0] WorkingEmpCount
                                , [1] FromOtherDeptEmpCount
                                , [2] ToOtherDeptEmpCount
                                , [3] OnsiteEmpCount
                                , [4] StopWorkingEmpCount
                                , [999] ContractedJobLeavedEmpCount
                                , [1000] RevenueCount
                                , COALESCE(PIV.[0],0) + COALESCE(PIV.[1],0)  + COALESCE(PIV.[3],0) TotalEmpCount 
                            FROM 
                                 (
                                    SELECT * FROM [dbo].[GetWorkingEmpCountAtDate] (@CompanyID,@DeptID,NULL,@startDate,@endDate)
                                 ) SRC
                                 PIVOT 
                                 (
	                                MAX(CNT)
	                                FOR WorkEmpType IN ( [0],[1],[2],[3],[4],[999],[1000])
                                  ) PIV
                            ";

            sql = sql.Replace("@CompanyID", companyID.ToString() );
            sql = sql.Replace("@DeptID", deptID.ToString());
            sql = sql.Replace("@TeamID", "NULL");
            sql = sql.Replace("@startDate","'" + startDate.ToString("yyyy/MM/dd") + "'");
            sql = sql.Replace("@endDate", "'" + endDate.ToString("yyyy/MM/dd") +"'");

            return DbContext.Database.SqlQuery<WorkEmpTypeStatisticViewModel>(sql.ToString()) ;

        }

    }
}

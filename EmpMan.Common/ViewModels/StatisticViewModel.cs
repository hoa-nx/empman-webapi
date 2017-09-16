using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpMan.Common.ViewModels
{
    public class StatisticViewModel
    {
        public string name { set; get; }
        public decimal value { set; get; }
        public int? CompanyID { set; get; }
        public string CompnayName { set; get; }

        public int? DeptID { set; get; }
        public string DeptName{ set; get; }

        public int? TeamID { set; get; }
        public string TeamName{ set; get; }

        public int? CustomerID { set; get; }
        public string CustomerName { set; get; }

        public decimal? Month1{ set; get; }
        public decimal? Month2 { set; get; }
        public decimal? Month3 { set; get; }
        public decimal? Month4 { set; get; }
        public decimal? Month5 { set; get; }
        public decimal? Month6 { set; get; }
        public decimal? Month7 { set; get; }
        public decimal? Month8 { set; get; }
        public decimal? Month9 { set; get; }
        public decimal? Month10 { set; get; }
        public decimal? Month11 { set; get; }
        public decimal? Month12 { set; get; }

        public DateTime? ReportYearMonth { set; get; }

        public string MonthToName { set; get; }

        public int? Year { set; get; }
        public int? YMD { set; get; }

        public decimal? DevMMTarget { set; get; }

        public decimal? OnsiteMMTarget { set; get; }

        public decimal? ManMMTarget { set; get; }

        /// <summary>
        /// Số MM tổng lập trình + quản lý + onsite (mục tiêu)
        /// </summary>
        public decimal? totalMMTarget { set; get; }
        /// <summary>
        /// Số MM tổng lập trình + quản lý - onsite (Mục tiêu)
        /// </summary>
        public decimal? totalMMTargetExcludeOnsite { set; get; }
        /// <summary>
        /// Số MM tổng lập trình + quản lý + onsite
        /// </summary>
        public decimal? totalMMToCurrentActual { set; get; }
        /// <summary>
        /// Số MM tổng lập trình + quản lý - onsite
        /// </summary>
        public decimal? totalMMToCurrentActualExcludeOnsite { set; get; }
        /// <summary>
        /// Đã thực hiện  trong tháng (tạm tính)--Số công LTV(MM)
        /// </summary>
        public decimal? InMonthDevMM { set; get; }

        /// <summary>
        /// Đã thực hiện  trong tháng (tạm tính)--Số công LTV(MM)
        /// </summary>
        public decimal? InMonthDevMM_Prev { set; get; }

        /// <summary>
        /// Đã thực hiện  trong tháng (tạm tính)--Số công phiên dịch(MM)
        /// </summary>
        public decimal? InMonthTransMM { set; get; }

        /// <summary>
        /// Đã thực hiện  trong tháng (tạm tính)--Số công phiên dịch(MM)
        /// </summary>
        public decimal? InMonthTransMM_Prev { set; get; }

        /// <summary>
        /// Đã thực hiện  trong tháng (tạm tính)--Số công QL (MM)
        /// </summary>
        public decimal? InMonthManagementMM { set; get; }

        /// <summary>
        /// Đã thực hiện  trong tháng (tạm tính)--Số công QL (MM)
        /// </summary>
        public decimal? InMonthManagementMM_Prev { set; get; }

        /// <summary>
        /// Đã thực hiện  trong tháng (tạm tính)--Số công onsite (MM)
        /// </summary>
        public decimal? InMonthOnsiteMM { set; get; }

        /// <summary>
        /// Đã thực hiện  trong tháng (tạm tính)--Số công onsite (MM)
        /// </summary>
        public decimal? InMonthOnsiteMM_Prev { set; get; }

        /// <summary>
        /// Đã thực hiện trong tháng (tạm tính ) bao gồm cả onsite và phiên dịch
        /// </summary>
        public decimal? InMonthSumMM { set; get; }

        /// <summary>
        /// Đã thực hiện trong tháng (tạm tính ) bao gồm cả onsite và phiên dịch
        /// </summary>
        public decimal? InMonthSumMM_Prev { set; get; }

        /// <summary>
        /// Đã thực hiện  trong tháng (tạm tính) +  Tong (MM) bao gồm onsite MM - Phiên dịch
        /// </summary>
        public decimal? InMonthSumIncludeOnsiteMM { set; get; }

        /// <summary>
        /// Đã thực hiện  trong tháng (tạm tính) +  Tong (MM) bao gồm onsite MM - Phiên dịch
        /// </summary>
        public decimal? InMonthSumIncludeOnsiteMM_Prev { set; get; }
        
        /// <summary>
        /// Đã thực hiện  trong tháng (tạm tính)-- Tổng số công lập trình quản lý onsite (không tính PD va onsite )
        /// </summary>
        public decimal? InMonthDevSumExcludeTransOnsiteMM { set; get; }

        /// <summary>
        /// Đã thực hiện  trong tháng (tạm tính)-- Tổng số công lập trình quản lý onsite (không tính PD va onsite )
        /// </summary>
        public decimal? InMonthDevSumExcludeTransOnsiteMM_Prev { set; get; }
    }

    public class ReportStatisticViewModel
    {
        public Nullable<int> CompanyID { set; get; }
        public string CompanyName { set; get; }

        
        public Nullable<int> DeptID { set; get; }
        public string DeptName { set; get; }

        public string Content { set; get; }

        public Nullable<decimal> Month1 { set; get; }
        public Nullable<decimal> Month2 { set; get; }
        public Nullable<decimal> Month3 { set; get; }
        public Nullable<decimal> Month4 { set; get; }
        public Nullable<decimal> Month5 { set; get; }
        public Nullable<decimal> Month6 { set; get; }
        public Nullable<decimal> Month7 { set; get; }
        public Nullable<decimal> Month8 { set; get; }
        public Nullable<decimal> Month9 { set; get; }
        public Nullable<decimal> Month10 { set; get; }
        public Nullable<decimal> Month11 { set; get; }
        public Nullable<decimal> Month12 { set; get; }
        
    }

    public class WorkEmpTypeStatisticViewModel
    {
        public string YM { set; get; }
        public DateTime YMD { set; get; }
        public decimal CNT { set; get; }
        public decimal? WorkingEmpCount { set; get; }
        public decimal? FromOtherDeptEmpCount { set; get; }
        public decimal? ToOtherDeptEmpCount { set; get; }
        public decimal? OnsiteEmpCount { set; get; }
        public decimal? StopWorkingEmpCount { set; get; }
        public decimal? ContractedJobLeavedEmpCount { set; get; }
        public decimal? TotalEmpCount { set; get; }
        public decimal WorkEmpType { set; get; }
        public decimal? RevenueCount { set; get; }
        public string WorkEmpTypeName { set; get; }

    }
}

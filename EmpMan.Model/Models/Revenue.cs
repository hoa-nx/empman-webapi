using EmpMan.Model.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Thông tin doanh thu 
/// </summary>
namespace EmpMan.Model.Models
{
    /// <summary>
    /// Quản lý doanh thu theo từng tháng từng khách hàng
    /// </summary>
    [Table("Revenues")]
    public class Revenue : Auditable
    {
        /// <summary>
        /// Khóa tự sinh
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { set; get; }

        /// <summary>
        /// Công ty
        /// </summary>
        public int? CompanyID { set; get; }

        /// <summary>
        /// Bộ phận
        /// </summary>
        public int? DeptID { set; get; }

        /// <summary>
        /// Tổ
        /// </summary>
        public int? TeamID { set; get; }

        /// <summary>
        /// Id Báo cáo 
        /// </summary>
        public int? ReporterID { set; get; }

        /// <summary>
        /// Ngày báo cáo
        /// </summary>
        public DateTime? ReportDate { set; get; }

        /// <summary>
        /// Tháng năm báo cáo
        /// </summary>
        public DateTime? ReportYearMonth { set; get; }

        /// <summary>
        /// Order No
        /// </summary>
        [MaxLength(256)]
        public string OrderNo { set; get; }

        /// <summary>
        /// No bao gia
        /// </summary>
        [MaxLength(256)]
        public string EstimateNo { set; get; }

        /// <summary>
        /// Title của báo cáo
        /// </summary>
        public string ReportTitle { set; get; }

        /// <summary>
        /// Code dự án
        /// </summary>
        [ForeignKey("ProjectDetail")]
        public int? ProjectID{ set; get; }

        /// <summary>
        /// Code chi tiết dự án
        /// </summary>
        [ForeignKey("ProjectDetail")]
        public int? ProjectDetailID { set; get; }

        /// <summary>
        /// Tên dự án
        /// </summary>
        public string ProjectName { set; get; }
        
        /// <summary>
        /// Nội dung của dự án 
        /// </summary>
        public string ProjectContent { set; get; }

        /// <summary>
        /// Kiểu estimate ( UL , UQ ,....)
        /// <para/> CommonData.Code =20
        /// </summary>
        [ForeignKey("EstimateType")]
        public int? EstimateTypeMasterID { set; get; }

        /// <summary>
        /// Kiểu estimate ( UL , UQ ,....)
        /// <para/> CommonData.Code =20
        /// </summary>
        [ForeignKey("EstimateType")]
        public int? EstimateTypeMasterDetailID { set; get; }

        /// <summary>
        /// Mã khách hàng
        /// </summary>
        public int? CustomerID { set; get; }

        /// <summary>
        /// Tên khách hàng
        /// </summary>
        public string CustomerName { set; get; }

        /// <summary>
        /// Ngày đặt hàng start
        /// </summary>
        public DateTime? OrderStartDate { set; get; }

        /// <summary>
        /// Ngày làm end
        /// </summary>
        public DateTime? OrderEndDate { set; get; }

        /// <summary>
        /// Theo số liệu quotation/order-Tổng số MM(gồm cả quản lý & phiên dịch)
        /// </summary>
        public decimal? OrderProjectSumMM { set; get; }
        /// <summary>
        /// Theo số liệu quotation/order-ĐVT đơn giá (MasterDetail.ID = 25)
        /// </summary>
        [ForeignKey("OrderUnit")]
        public int? OrderUnitMasterID { set; get; }

        /// <summary>
        /// Theo số liệu quotation/order-ĐVT đơn giá (MasterDetail.ID = 25)
        /// </summary>
        [ForeignKey("OrderUnit")]
        public int? OrderUnitMasterDetailID { set; get; }

        /// <summary>
        /// Master chuyển đổi đơn giá 
        /// </summary>
        public int? ExchangeRateID { set; get; }

        /// <summary>
        /// Đon giá áp dụng ( get từ master đơn giá)
        /// </summary>
        public int? CustomerUnitPriceID { set; get; }

        /// <summary>
        /// Theo số liệu quotation/order - Đơn giá 
        /// </summary>
        public decimal? OrderPrice { set; get; }

        /// <summary>
        /// Theo số liệu quotation/order - Số tiền(Qui đổi USD)
        /// </summary>
        public decimal? OrderPriceToUsd { set; get; }

        /// <summary>
        /// Lũy kế số đã thực hiện các tháng trước --Tổng số MM
        /// </summary>
        public decimal? AccPreMonthSumMM { set; get; }

        /// <summary>
        /// Lũy kế số đã thực hiện các tháng trước --Số tiền(Qui đổi USD)
        /// </summary>
        public decimal? AccPreMonthSumToUsd { set; get; }

        /// <summary>
        /// Đã thực hiện  trong tháng (tạm tính)--Số công LTV(MM)
        /// </summary>
        public decimal? InMonthDevMM { set; get; }

        /// <summary>
        /// Đã thực hiện  trong tháng (tạm tính)--Số công phiên dịch(MM)
        /// </summary>
        public decimal? InMonthTransMM { set; get; }

        /// <summary>
        /// Đã thực hiện  trong tháng (tạm tính)--Số công QL (MM)
        /// </summary>
        public decimal? InMonthManagementMM { set; get; }

        /// <summary>
        /// Đã thực hiện  trong tháng (tạm tính)--Số công onsite (MM)
        /// </summary>
        public decimal? InMonthOnsiteMM { set; get; }

        /// <summary>
        /// Nhân viên onsite trong tháng
        /// </summary>
        public string InMonthOnsiteEmp { set; get; }

        /// <summary>
        /// Đã thực hiện  trong tháng (tạm tính)-- Tong (MM).
        /// <para/> Không bao gồm số công onsite
        /// </summary>
        public decimal? InMonthSumMM { set; get; }
        
        /// <summary>
        /// Đã thực hiện  trong tháng (tạm tính)-- Tong (MM) bao gồm onsite MM
        /// </summary>
        public decimal? InMonthSumIncludeOnsiteMM { set; get; }

        /// <summary>
        /// Đã thực hiện  trong tháng (tạm tính)-- Tổng số công lập trình quản lý onsite (không tính PD)
        /// </summary>
        public decimal? InMonthDevSumExcludeTransMM { set; get; }

        /// <summary>
        /// Đã thực hiện  trong tháng (tạm tính)-- Số tiền(Qui đổi USD)
        /// </summary>
        public decimal? InMonthToUsd { set; get; }

        /// <summary>
        /// Đã thực hiện  trong tháng (tạm tính)-- Số tiền(Qui đổi USD)
        /// </summary>
        public decimal? InMonthToVnd { set; get; }

        /// <summary>
        /// Số còn phải thực hiện các kỳ sau -- Tháng tiếp theo
        /// </summary>
        public decimal? NextMonth { set; get; }

        /// <summary>
        /// Số còn phải thực hiện các kỳ sau -- Số MM
        /// </summary>
        public decimal? NextMonthMM { set; get; }

        /// <summary>
        /// Số còn phải thực hiện các kỳ sau -- Số tiền(Qui đổi USD)
        /// </summary>
        public decimal? NextMonthToUsd { set; get; }

        
        /// <summary>
        /// PM
        /// </summary>
        public int? PMID { set; get; }

        /// <summary>
        /// PL
        /// </summary>
        public int? PLID { set; get; }

        [ForeignKey("CompanyID")]
        public virtual Company Company { set; get; }

        [ForeignKey("DeptID")]
        public virtual Dept Dept { set; get; }

        [ForeignKey("TeamID")]
        public virtual Team Team { set; get; }

        [ForeignKey("CustomerUnitPriceID")]
        public virtual CustomerUnitPrice CustomerUnitPrice { set; get; }

        public virtual ProjectDetail ProjectDetail { set; get; }
                
        public virtual MasterDetail EstimateType { set; get; }

        [ForeignKey("CustomerID")]
        public virtual Customer Customer { set; get; }

        public virtual MasterDetail OrderUnit { set; get; }

        [ForeignKey("ExchangeRateID")]
        public virtual ExchangeRate ExchangeRate { set; get; }

        [ForeignKey("PMID")]
        public virtual Emp PM { set; get; }

        [ForeignKey("PLID")]
        public virtual Emp PL { set; get; }

    }
}

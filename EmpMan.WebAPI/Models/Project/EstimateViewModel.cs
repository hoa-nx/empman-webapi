using EmpMan.Web.Models.Common;
using EmpMan.Web.Models.Emp;
using EmpMan.Web.Models.File;
using EmpMan.Web.Models.Master;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EmpMan.Web.Models.Project
{
    public class EstimateViewModel : AuditableViewModel
    {
        /// <summary>
        /// Khóa chính (tự sinh)
        /// </summary>

        public int No { set; get; }

        /// <summary>
        /// Mã báo giá
        /// </summary>
        public string ID { set; get; }

        /// <summary>
        /// Tên báo giá
        /// </summary>
        public string Name { set; get; }

        /// <summary>
        /// Tên tăt
        /// </summary>
        public string ShortName { set; get; }

        /// <summary>
        /// Tên trên report
        /// </summary>
        public string NameUseInReport { set; get; }

        /// <summary>
        /// Khách hàng
        /// </summary>
        public int? CustomerID { set; get; }

        /// <summary>
        /// Dự toán khách hàng
        /// </summary>
        public decimal? CustomerYosanMM { set; get; }

        /// <summary>
        /// Ngày khách hàng yêu cầu báo giá
        /// </summary>
        public DateTime? CustomerRequestDate { set; get; }

        /// <summary>
        /// Ngày KH mong muốn gửi lại báo giá
        /// </summary>
        public DateTime? CustomerKiboSendDate { set; get; }


        /// <summary>
        /// Ngày dự định start
        /// </summary>
        public DateTime? SchedulePojectStartDate { set; get; }

        /// <summary>
        /// Ngày dự định kết thúc
        /// </summary>
        public DateTime? SchedulePojectEndDate { set; get; }

        /// <summary>
        /// Ngày giao hàng cuối cùng
        /// </summary>
        public DateTime? CustomerKiboLastDeliveryDate { set; get; }

        /// <summary>
        /// Ngày FJS dự định gửi báo giá
        /// </summary>
        public DateTime? ScheduleSendDate { set; get; }

        /// <summary>
        /// Ngày start báo giá
        /// </summary>
        public DateTime? StartEstimateDate { set; get; }

        /// <summary>
        /// Ngày FJN gửi báo giá lần đầu tiên
        /// </summary>
        public DateTime? ActualSendFirstDate { set; get; }

        /// <summary>
        /// Ngày FJN gửi báo giá cuối cùng
        /// </summary>
        public DateTime? ActualSendLastDate { set; get; }

        /// <summary>
        /// Số lần gửi báo giá qua lại
        /// </summary>
        public int? SendEstimateCount { set; get; }

        /// <summary>
        /// Người chịu trách nhiệm báo giá
        /// </summary>
        public int? EstimateEmpID { set; get; }

        /// <summary>
        /// List người tham gia báo giá
        /// </summary>
        public string EstimateEmpList { set; get; }

        /// <summary>
        /// Ngày leader gửi báo giá
        /// </summary>
        public DateTime? EstimateEmpReportDate { set; get; }

        /// <summary>
        /// Người phụ trách giao dịch KH
        /// </summary>
        public int? TransEmpID { set; get; }

        /// <summary>
        /// Nội dung giao dịch báo giá
        /// </summary>
        public string EstimateContent { set; get; }

        /// <summary>
        /// Bse
        /// </summary>
        public int? BseID { set; get; }

        /// <summary>
        /// Số công lấy yêu cầu
        /// </summary>
        public decimal? EstimateRequireMM { set; get; }

        /// <summary>
        /// Số công thiết kế cơ bản
        /// </summary>
        public decimal? EstimateBasicMM { set; get; }

        /// <summary>
        /// Số công thiết kế chi tiết
        /// </summary>
        public decimal? EstimateDetailMM { set; get; }

        /// <summary>
        /// Số công lập trình
        /// </summary>
        public decimal? EstimateDevMM { set; get; }

        /// <summary>
        /// Số công dịch
        /// </summary>
        public decimal? EstimateTransMM { set; get; }

        /// <summary>
        /// Số công quản lý
        /// </summary>
        public decimal? EstimateManMM { set; get; }

        /// <summary>
        /// Số công unit test
        /// </summary>
        public decimal? EstimateUtMM { set; get; }

        /// <summary>
        /// Số công test kết hợp
        /// </summary>
        public decimal? EstimateCombineTestMM { set; get; }

        /// <summary>
        /// Số công system test
        /// </summary>
        public decimal? EstimateSystemTestMM { set; get; }

        /// <summary>
        /// Số công user test
        /// </summary>
        public decimal? EstimateUserTestMM { set; get; }

        /// <summary>
        /// Tong so cong bao gia
        /// </summary>
        public decimal? TotalMM { set; get; }
        /// <summary>
        /// Kết quả báo giá ( trạng thái ) : ok , fail, chờ 
        /// <para/> Master.Code = 29
        /// </summary>
        public int? EstimateResultMasterID { set; get; }

        /// <summary>
        /// Kết quả báo giá ( trạng thái ) : ok , fail, chờ 
        /// <para/> Master.Code = 29 
        /// </summary>
        public int? EstimateResultMasterDetailID { set; get; }

        /// <summary>
        /// Comment kết quả báo giá
        /// </summary>
        public string ResultNote { set; get; }
        /// <summary>
        /// Hệ điều hành
        /// </summary>
        public string OS { set; get; }

        /// <summary>
        /// Ngôn ngữ lập trình
        /// </summary>
        public string Language { set; get; }

        /// <summary>
        /// Các tool khác
        /// </summary>
        public string OtherSofts { set; get; }

        /// <summary>
        /// Số tháng bảo hành sản phẩm
        /// </summary>
        public int? WarrantyMonths { set; get; }

        /// <summary>
        /// Ngày bắt đầu tính bảo hành
        /// </summary>
        public DateTime? WarrantyStartDate { set; get; }
        /// <summary>
        /// Kiểu estimate ( UL , UQ ,....)
        /// <para/> CommonData.Code =20
        /// </summary>
        public int? EstimateTypeMasterID { set; get; }

        /// <summary>
        /// Kiểu estimate ( UL , UQ ,....)
        /// <para/> CommonData.Code =20
        /// </summary>
        public int? EstimateTypeMasterDetailID { set; get; }

        /// <summary>
        /// Số No đặt hàng
        /// </summary>
        public string OrderReceivedID { set; get; }

        /// <summary>
        /// File
        /// </summary>
        public int? FileID { set; get; }


        public virtual CustomerViewModel Customer { set; get; }


        public virtual EmpViewModel EstimateEmp { set; get; }


        //public virtual EmpViewModel TransEmp { set; get; }


        //public virtual EmpViewModel Bse { set; get; }

        public virtual MasterDetailViewModel EstimateResult { set; get; }

        public virtual MasterDetailViewModel EstimateType { set; get; }
        //public virtual FileStorageViewModel File { set; get; }

        /// <summary>
        /// Hệ điều hành
        /// </summary>
        public List<string> OSLists { set; get; }

        /// <summary>
        /// Ngôn ngữ lập trình
        /// </summary>
        public List<string> LanguageLists { set; get; }

        /// <summary>
        /// Các tool khác
        /// </summary>
        public List<string> OtherSoftLists { set; get; }
    }
}
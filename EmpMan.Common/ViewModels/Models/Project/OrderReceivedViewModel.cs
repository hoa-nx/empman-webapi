using EmpMan.Common.ViewModels.Models.Common;
using EmpMan.Common.ViewModels.Models.Emp;
using EmpMan.Common.ViewModels.Models.File;
using EmpMan.Common.ViewModels.Models.Master;
using EmpMan.Web.Common.ViewModels.Project;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EmpMan.Common.ViewModels.Models.Project
{
    public class OrderReceivedViewModel : AuditableViewModel
    {
        /// <summary>
        /// Khóa chính (tự sinh)
        /// </summary>
        public int No { set; get; }

        /// <summary>
        /// Mã order
        /// </summary>
        [Key]
        public string ID { set; get; }

        /// <summary>
        /// Tên order
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
        /// Báo giá 
        /// </summary>
        public string EstimateID { set; get; }

        /// <summary>
        /// Số MM đặt hàng
        /// </summary>
        public decimal? TotalOrderMM { set; get; }

        /// <summary>
        /// Ngày đặt hàng
        /// </summary>
        public DateTime? OrderDate { set; get; }

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
        /// Người chịu trách nhiệm đối ứng dự án
        /// </summary>
        public int? PMEmpID { set; get; }

        /// <summary>
        /// Người phụ trách giao dịch KH
        /// </summary>
        public int? TransEmpID { set; get; }

        /// <summary>
        /// Nội dung giao dịch báo giá
        /// </summary>
        public string OrderContent { set; get; }

        /// <summary>
        /// Bse
        /// </summary>
        public int? BseID { set; get; }

        /// <summary>
        /// Số công lấy yêu cầu
        /// </summary>
        public decimal? OrderRequireMM { set; get; }

        /// <summary>
        /// Số công thiết kế cơ bản
        /// </summary>
        public decimal? OrderBasicMM { set; get; }

        /// <summary>
        /// Số công thiết kế chi tiết
        /// </summary>
        public decimal? OrderDetailMM { set; get; }

        /// <summary>
        /// Số công lập trình
        /// </summary>
        public decimal? OrderDevMM { set; get; }

        /// <summary>
        /// Số công dịch
        /// </summary>
        public decimal? OrderTransMM { set; get; }

        /// <summary>
        /// Số công quản lý
        /// </summary>
        public decimal? OrderManMM { set; get; }

        /// <summary>
        /// Số công unit test
        /// </summary>
        public decimal? OrderUtMM { set; get; }

        /// <summary>
        /// Số công test kết hợp
        /// </summary>
        public decimal? OrderCombineTestMM { set; get; }

        /// <summary>
        /// Số công system test
        /// </summary>
        public decimal? OrderSystemTestMM { set; get; }

        /// <summary>
        /// Số công user test
        /// </summary>
        public decimal? OrderUserTestMM { set; get; }

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
        /// Kỳ hạn nghiệm thu khách hàng
        /// </summary>
        public DateTime? CustomerConfirmDate { set; get; }


        /// <summary>
        /// File
        /// </summary>
        public int? FileID { set; get; }


        public virtual EstimateViewModel Estimate { set; get; }


        //public virtual EmpViewModel PMEmp { set; get; }


        //public virtual EmpViewModel TransEmp { set; get; }


        //public virtual EmpViewModel Bse { set; get; }


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

        public string OrderStatusName { set; get;}

        /// <summary>
        /// List cac file dinh kem
        /// </summary>
        public IEnumerable<FileResultViewModel> AttachmentFileLists { set; get; }
    }
}
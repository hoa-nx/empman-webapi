using EmpMan.Common.ViewModels.Models.Common;
using EmpMan.Common.ViewModels.Models.Project;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EmpMan.Common.ViewModels.Models.Master
{
    public class MasterDetailViewModel : AuditableViewModel
    {

        /// <summary>
        /// Khóa chính tự sinh
        /// </summary>
        public int ID { set; get; }

        /// <summary>
        /// Loại dự liệu ( khóa ngoại của bảng CommonData.ID)
        /// <para/> Ví dụ : Chứng chỉ tiếng Nhật thì sẽ có MeiKbn = 10
        /// </summary>
        ///[Column(Order = 0), Key]
        [Required(ErrorMessage = "Mã phân loại danh mục bắt buộc nhập")]
        public int MasterID { set; get; }

        /// <summary>
        /// Mã danh mục con
        /// </summary>
        [Required(ErrorMessage = "Mã danh mục bắt buộc nhập")]
        public int MasterDetailCode { set; get; }

        /// <summary>
        /// Tên
        /// <para/> Ví dụ :
        /// <para/> Ứng với MeiKbn =10 . Code =1 thì có tên là "Chứng chỉ N1"
        /// </summary>
        [Required(ErrorMessage = "Tên danh mục bắt buộc nhập")]
        [MaxLength(256)]
        public string Name { set; get; }

        /// <summary>
        /// Tên gọi tắt
        /// </summary>
        [MaxLength(256)]
        public string ShortName { set; get; }

        /// <summary>
        /// Tên hiển thị trên báo cáo
        /// </summary>
        [MaxLength(512)]
        public string DisplayReportName { set; get; }

        /// <summary>
        /// Thuộc loại dữ liệu phụ cấp?
        /// </summary>
        public bool IsAllowanceType { set; get; }

        /// <summary>
        /// Ngày bắt đầu hữu hiệu
        /// </summary>
        public DateTime? StartDate { set; get; }

        /// <summary>
        /// Ngày kết thúc
        /// </summary>
        public DateTime? EndDate { set; get; }

        /// <summary>
        /// Mức phụ cấp (nếu có)
        /// </summary>
        public decimal? Allowance { set; get; }

        /// <summary>
        /// Title của item 
        /// </summary>
        public string Value1Title { set; get; }

        /// <summary>
        /// Giá trị của item
        /// </summary>
        public string Value1Data { set; get; }

        /// <summary>
        /// Title của item 
        /// </summary>
        public string Value2Title { set; get; }

        /// <summary>
        /// Giá trị của item
        /// </summary>
        public string Value2Data { set; get; }

        /// <summary>
        /// Title của item 
        /// </summary>
        public string Value3Title { set; get; }

        /// <summary>
        /// Giá trị của item
        /// </summary>
        public string Value3Data { set; get; }

        /// <summary>
        /// Title của item 
        /// </summary>
        public string Value4Title { set; get; }

        /// <summary>
        /// Giá trị của item
        /// </summary>
        public string Value4Data { set; get; }

        /// <summary>
        /// Title của item 
        /// </summary>
        public string Value5Title { set; get; }

        /// <summary>
        /// Giá trị của item
        /// </summary>
        public string Value5Data { set; get; }

        /// <summary>
        /// Title của item 
        /// </summary>
        public string Value6Title { set; get; }

        /// <summary>
        /// Giá trị của item
        /// </summary>
        public string Value6Data { set; get; }

        /// <summary>
        /// Title của item 
        /// </summary>
        public string Value7Title { set; get; }

        /// <summary>
        /// Giá trị của item
        /// </summary>
        public string Value7Data { set; get; }

        /// <summary>
        /// Title của item 
        /// </summary>
        public string Value8Title { set; get; }

        /// <summary>
        /// Giá trị của item
        /// </summary>
        public string Value8Data { set; get; }

        /// <summary>
        /// Title của item 
        /// </summary>
        public string Value9Title { set; get; }

        /// <summary>
        /// Giá trị của item
        /// </summary>
        public string Value9Data { set; get; }

        /// <summary>
        /// Title của item 
        /// </summary>
        public string Value10Title { set; get; }

        /// <summary>
        /// Giá trị của item
        /// </summary>
        public string Value10Data { set; get; }
        
        public virtual MasterViewModel EstimateType { get; set; }

        public virtual ProjectDetailViewModel ProjectDetail { set; get; }

    }
}
using EmpMan.Common.ViewModels.Models.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EmpMan.Common.ViewModels.Models.Master
{
    public class MasterViewModel : AuditableViewModel
    {
        /// <summary>
        /// Số tự sinh
        /// </summary>
        public int No { set; get; }

        /// <summary>
        /// Khóa chính
        /// </summary>
        [Required(ErrorMessage = "Mã phân loại danh mục bắt buộc nhập")]
        public int ID { set; get; }

        /// <summary>
        /// Tên của loại dữ liệu 
        /// <para/> ví dụ : Loại hợp đồng lao động
        /// </summary>
        [Required(ErrorMessage = "Tên phân loại danh mục bắt buộc nhập")]
        [MaxLength(256)]
        public string Name { set; get; }

        /// <summary>
        /// Tên viết tắt
        /// </summary>
        public string ShortName { set; get; }

        /// <summary>
        /// Tên hiển thị trên các báo cáo
        /// </summary>
        public string DisplayReportName { set; get; }
        /// <summary>
        /// Thuộc loại dữ liệu phụ cấp?
        /// </summary>
        public bool IsAllowanceType { set; get; }
        /// <summary>
        /// Giá trị bất kì 
        /// </summary>
        public string Value1 { set; get; }

        /// <summary>
        /// Giá trị bất kì 
        /// </summary>
        public string Value2 { set; get; }

        /// <summary>
        /// Giá trị bất kì 
        /// </summary>
        public string Value3 { set; get; }

        /// <summary>
        /// Giá trị bất kì 
        /// </summary>
        public string Value4 { set; get; }

        /// <summary>
        /// Giá trị bất kì 
        /// </summary>
        public string Value5 { set; get; }

        /// <summary>
        /// Giá trị bất kì 
        /// </summary>
        public string Value6 { set; get; }

        /// <summary>
        /// Giá trị bất kì 
        /// </summary>
        public string Value7 { set; get; }

        /// <summary>
        /// Giá trị bất kì 
        /// </summary>
        public string Value8 { set; get; }

        /// <summary>
        /// Giá trị bất kì 
        /// </summary>
        public string Value9 { set; get; }

        /// <summary>
        /// Giá trị bất kì 
        /// </summary>
        public string Value10 { set; get; }

        /// <summary>
        /// Giá trị bất kì 
        /// </summary>
        public string Value11 { set; get; }

        /// <summary>
        /// Giá trị bất kì 
        /// </summary>
        public string Value12 { set; get; }

        /// <summary>
        /// Giá trị bất kì 
        /// </summary>
        public string Value13 { set; get; }

        /// <summary>
        /// Giá trị bất kì 
        /// </summary>
        public string Value14 { set; get; }

        /// <summary>
        /// Giá trị bất kì 
        /// </summary>
        public string Value15 { set; get; }

        /// <summary>
        /// Giá trị bất kì 
        /// </summary>
        public string Value16 { set; get; }

        /// <summary>
        /// Giá trị bất kì 
        /// </summary>
        public string Value17 { set; get; }

        /// <summary>
        /// Giá trị bất kì 
        /// </summary>
        public string Value18 { set; get; }

        /// <summary>
        /// Giá trị bất kì 
        /// </summary>
        public string Value19 { set; get; }

        /// <summary>
        /// Giá trị bất kì 
        /// </summary>
        public string Value20 { set; get; }

        public virtual IEnumerable<MasterDetailViewModel> MasterDetails { get; set; }
    }
}
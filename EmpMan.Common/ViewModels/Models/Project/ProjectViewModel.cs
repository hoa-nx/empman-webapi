using EmpMan.Common.ViewModels.Models.Common;
using EmpMan.Common.ViewModels.Models.Master;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EmpMan.Common.ViewModels.Models.Project
{
    public class ProjectViewModel : AuditableViewModel
    {
        /// <summary>
        /// Khóa tự sinh
        /// </summary>
        public int No { set; get; }

        /// <summary>
        /// Mã dự án
        /// </summary>
        [Required(ErrorMessage = "Mã dự án bắt buộc nhập")]
        public int ID { set; get; }

        /// <summary>
        /// Mã khách hàng
        /// </summary>
        public int? CustomerID { set; get; }

        /// <summary>
        /// No đặt hàng
        /// </summary>
        public string OrderReceivedID { set; get; }

        /// <summary>
        /// Tên dự án
        /// </summary>
        [Required]
        [MaxLength(256)]
        public string Name { set; get; }

        /// <summary>
        /// Tên tiếng Nhật
        /// </summary>
        [MaxLength(256)]
        public string NameJp { set; get; }

        /// <summary>
        /// Tên viết tắt
        /// </summary>
        [MaxLength(256)]
        public string ShortName { set; get; }

        /// <summary>
        /// Mã dự án trong công ty ( WMS)
        /// </summary>
        public string CompanyProjectID { set; get; }

        /// <summary>
        /// Số MM báo giá
        /// </summary>
        public decimal? EstimateManMonth { set; get; }

        /// <summary>
        /// Số MM thực tế
        /// </summary>
        public decimal? ActualManMonth { set; get; }

        public virtual CustomerViewModel ProjectCustomer { set; get; }

        public virtual IEnumerable<ProjectDetailViewModel> ProjectDetails { set; get; }

        public virtual IEnumerable<ProjectDetailResourceViewModel> ProjectDetailResources { set; get; }

    }
}
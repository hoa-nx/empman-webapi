using EmpMan.Common.ViewModels.Models.Common;
using EmpMan.Common.ViewModels.Models.Emp;
using EmpMan.Common.ViewModels.Models.Master;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EmpMan.Common.ViewModels.Models.Master
{
    public class CompanyViewModel : AuditableViewModel
    {

        public int No{ set; get; }

        [Required(ErrorMessage = "Mã công ty bắt buộc nhập")]
        public int ID { set; get; }

        [Required(ErrorMessage = "Tên công bắt buộc nhập")]
        [MaxLength(256)]
        public string Name { set; get; }

        [Required(ErrorMessage = "Tên tắt công ty bắt buộc nhập")]
        [MaxLength(256)]
        public string ShortName { set; get; }

        /// <summary>
        /// CEO ID ( Liên kết với table EMP)
        /// </summary>
        public int? CeoID { set; get; }

        /// <summary>
        /// Giám đốc( Liên kết với table EMP)
        /// </summary>

        public int? DirectorID { set; get; }

        /// <summary>
        /// Phó giám đốc ( Liên kết với table EMP)
        /// </summary>

        public int? DeputyDirectorID { set; get; }

        /// <summary>
        /// Địa chỉ
        /// </summary>
        [MaxLength(1024)]
        public string Address1 { set; get; }

        /// <summary>
        /// Địa chỉ
        /// </summary>
        [MaxLength(1024)]
        public string Address2 { set; get; }

        /// <summary>
        /// Điện thoại
        /// </summary>
        [MaxLength(64)]
        public string Phone1 { set; get; }

        /// <summary>
        /// Điện thoại
        /// </summary>
        [MaxLength(64)]
        public string Phone2 { set; get; }

        /// <summary>
        /// Fax
        /// </summary>
        [MaxLength(64)]
        public string Fax { set; get; }

        /// <summary>
        /// Địa chỉ mail liên lạc
        /// </summary>
        [MaxLength(256)]
        public string ContactEmail { set; get; }

        /// <summary>
        /// Trang chủ
        /// </summary>
        [MaxLength(256)]
        public string WebSiteUrl { set; get; }

        /// <summary>
        /// Mã số thuế
        /// </summary>
        [MaxLength(256)]
        public string TaxCode { set; get; }

        /// <summary>
        /// Địa chỉ nhận mã số thuế
        /// </summary>
        [MaxLength(1024)]
        public string TaxAddress { set; get; }

        /// <summary>
        /// Ngày thành lập
        /// </summary>
        public DateTime? CreateDate { set; get; }

        /// <summary>
        /// Vốn điều lệ
        /// </summary>
        public decimal? Captital { set; get; }

        /// <summary>
        /// Domain công ty
        /// </summary>
        [MaxLength(256)]
        public string DomainEmail { set; get; }

        /// <summary>
        /// Địa chỉ IP Global
        /// </summary>
        [MaxLength(1024)]
        public string GlobalIpList { set; get; }

        public virtual EmpViewModel CeoModel { set; get; }

        public virtual EmpViewModel DirectorModel { set; get; }

        public virtual EmpViewModel DeputyDirectorModel { set; get; }


        /// <summary>
        /// Thông tin các rule (thông báo) của công ty
        /// </summary>
        public virtual IEnumerable<CompanyRuleViewModel> CompanyRules { get; set; }
    }
}
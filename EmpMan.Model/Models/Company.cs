using EmpMan.Model.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpMan.Model.Models
{
    /// <summary>
    /// Table thông tin công ty
    /// </summary>
    [Table("Companys")]
    public class Company : Auditable
    {
        /// <summary>
        /// Khóa chính (tự sinh)
        /// </summary>

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int No { set; get; }

        /// <summary>
        /// Mã công ty
        /// </summary>
        [Key]
        public int ID { set; get; }

        /// <summary>
        /// Tên công ty
        /// </summary>
        [Required]
        [MaxLength(256)]
        public string Name { set; get; }

        /// <summary>
        /// Tên tăt
        /// </summary>
        [Required]
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
        public string Address1{ set; get; }

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

        [ForeignKey("CeoID")]
        public virtual Emp Ceo { set; get; }

        [ForeignKey("DirectorID")]
        public virtual Emp Director { set; get; }

        [ForeignKey("DeputyDirectorID")]
        public virtual Emp DeputyDirector { set; get; }

        /// <summary>
        /// Thông tin các rule (thông báo) của công ty
        /// </summary>
        public virtual ICollection<CompanyRule> CompanyRuleList { get; set; }
    }
}

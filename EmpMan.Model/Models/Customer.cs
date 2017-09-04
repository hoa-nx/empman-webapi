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
    /// Bảng chứa thông tin khách hàng
    /// </summary>
    [Table("Customers")]
    public class Customer : Auditable
    {

        /// <summary>
        /// Khóa chính tự  sinh
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int No { set; get; }

        /// <summary>
        /// Mã khách hàng
        /// </summary>
        [Key]
        public int ID { set; get; }

        /// <summary>
        /// Tên khách hàng
        /// </summary>
        [Required]
        [MaxLength(256)]
        public string Name { set; get; }

        /// <summary>
        /// Tên tắt
        /// </summary>
        [Required]
        [MaxLength(256)]
        public string ShortName { set; get; }

        /// <summary>
        /// Tên dùng khi  báo cáo
        /// </summary>
        [MaxLength(1024)]
        public string NameUseInReport { set; get; }

        /// <summary>
        /// Ngày ký HD liên kết
        /// </summary>
        public DateTime? ContractDate { set; get; }

        /// <summary>
        /// Ngày kết sổ mỗi tháng (ngày 20, cuối tháng ...)
        /// </summary>
        public int? Sime { set; get; }

        /// <summary>
        /// Số điện thoại
        /// </summary>
        [MaxLength(64)]
        public string Phone1 { set; get; }

        /// <summary>
        /// Số điện thoại
        /// </summary>
        [MaxLength(64)]
        public string Phone2 { set; get; }

        /// <summary>
        /// Fax
        /// </summary>
        [MaxLength(64)]
        public string Fax { set; get; }

        /// <summary>
        /// Email domain
        /// </summary>
        [MaxLength(256)]
        public string EmailDomain { set; get; }

        /// <summary>
        /// Trang chủ
        /// </summary>
        [MaxLength(256)]
        public string WebSiteUrl { set; get; }

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
        /// IP Address của khách hàng
        /// </summary>
        [MaxLength(1024)]
        public string GlobalIpList { set; get; }

        /// <summary>
        /// Mã số thuế (có không ?)
        /// </summary>
        [MaxLength(256)]
        public string TaxCode { set; get; }

        /// <summary>
        /// Địa chỉ gửi hóa đơn
        /// </summary>
        [MaxLength(1024)]
        public string TaxAddress { set; get; }

        /// <summary>
        /// % số công quản lý
        /// </summary>
        public decimal? MangRate { set; get; }

        /// <summary>
        /// % số công phiên dịch
        /// </summary>
        public decimal? TransRate { set; get; }

        /// <summary>
        /// Thuoc group nao ( vi du nhu Uchida yoko)
        /// </summary>
        [MaxLength(256)]
        public string GroupName { set; get; }
        /// <summary>
        /// Theo số liệu quotation/order-ĐVT đơn giá (MasterDetail.ID = 25)
        /// </summary>
        [ForeignKey("DefaultOrderUnit")]
        public int? DefaultOrderUnitMasterID { set; get; }

        /// <summary>
        /// Theo số liệu quotation/order-ĐVT đơn giá (MasterDetail.ID = 25)
        /// </summary>
        [ForeignKey("DefaultOrderUnit")]
        public int? DefaultOrderUnitMasterDetailID { set; get; }

        public virtual MasterDetail DefaultOrderUnit { set; get; }

        public virtual ICollection<CustomerUnitPrice> CustomerUnitPrices { set; get; }
    }
}

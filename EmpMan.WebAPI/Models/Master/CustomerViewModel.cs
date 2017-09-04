using EmpMan.Web.Models.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EmpMan.Web.Models.Master
{
    public class CustomerViewModel : AuditableViewModel
    {
        /// <summary>
        /// số tự  sinh
        /// </summary>
        public int No { set; get; }

        /// <summary>
        /// Mã khách hàng
        /// </summary>
        [Required(ErrorMessage = "Mã khách hàng bắt buộc nhập")]
        public int ID { set; get; }

        /// <summary>
        /// Tên khách hàng
        /// </summary>
        [Required(ErrorMessage = "Tên khách hàng bắt buộc nhập")]
        [MaxLength(256)]
        public string Name { set; get; }

        /// <summary>
        /// Tên tắt
        /// </summary>
        [MaxLength(256)]
        [Required(ErrorMessage = "Tên tắt khách hàng bắt buộc nhập")]
        public string ShortName { set; get; }

        /// <summary>
        /// Tên dùng khi  báo cáo
        /// </summary>
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
        public string Phone1 { set; get; }

        /// <summary>
        /// Số điện thoại
        /// </summary>
        public string Phone2 { set; get; }

        /// <summary>
        /// Fax
        /// </summary>
        public string Fax { set; get; }

        /// <summary>
        /// Email domain
        /// </summary>
        public string EmailDomain { set; get; }

        /// <summary>
        /// Trang chủ
        /// </summary>
        public string WebSiteUrl { set; get; }

        /// <summary>
        /// Địa chỉ
        /// </summary>
        public string Address1 { set; get; }

        /// <summary>
        /// Địa chỉ
        /// </summary>
        public string Address2 { set; get; }

        /// <summary>
        /// IP Address của khách hàng
        /// </summary>
        public string GlobalIpList { set; get; }

        /// <summary>
        /// Mã số thuế (có không ?)
        /// </summary>
        public string TaxCode { set; get; }

        /// <summary>
        /// Địa chỉ gửi hóa đơn
        /// </summary>
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
        public int? DefaultOrderUnitMasterID { set; get; }

        /// <summary>
        /// Theo số liệu quotation/order-ĐVT đơn giá (MasterDetail.ID = 25)
        /// </summary>
        public int? DefaultOrderUnitMasterDetailID { set; get; }


        public virtual MasterDetailViewModel DefaultOrderUnit { set; get; }

        public virtual IEnumerable<CustomerUnitPriceViewModel> CustomerUnitPrices { set; get; }
    }
}
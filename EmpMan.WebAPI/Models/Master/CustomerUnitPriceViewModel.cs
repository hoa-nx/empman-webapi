using EmpMan.Web.Models.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EmpMan.Web.Models.Master
{ 
    public class CustomerUnitPriceViewModel : AuditableViewModel
    {
        /// <summary>
        /// Khóa chính tự  sinh
        /// </summary>
        public int ID { set; get; }

        /// <summary>
        /// Mã khách hàng
        /// </summary>
        public int CustomerID { set; get; }

        /// <summary>
        /// Tên khách hàng
        /// </summary>
        [Required]
        [MaxLength(256)]
        public string Name { set; get; }

        /// <summary>
        /// Ngày áp dụng
        /// </summary>
        public DateTime? StartDate { set; get; }

        /// <summary>
        /// Ngày hết hiệu lực
        /// </summary>
        public DateTime? EndDate { set; get; }
        
        /// <summary>
        /// Tỉ lệ số công quản lý ( 10% )
        /// </summary>
        public decimal? MangRate { set; get; }

        /// <summary>
        /// Tỉ lệ số công phiên dịch ( 12.5 % hoặc 15%)
        /// </summary>
        public decimal? TransRate { set; get; }

        /// <summary>
        /// Đơn vị tính
        /// </summary>
        public int? OrderUnitMasterID { set; get; }

        /// <summary>
        /// Đơn vị tính
        /// </summary>
        public int? OrderUnitMasterDetailID { set; get; }


        /// <summary>
        /// Đơn giá
        /// </summary>
        public decimal? OrderPrice { set; get; }

        /// <summary>
        /// giàm giá
        /// </summary>
        public decimal? Discount { set; get; }

        /// <summary>
        /// Phương pháp chi trả
        /// </summary>
        [MaxLength(1024)]
        public string PayMethod { set; get; }
        
        public virtual CustomerViewModel Customer { set; get; }

        public virtual MasterDetailViewModel OrderUnit { set; get; }
    }
}
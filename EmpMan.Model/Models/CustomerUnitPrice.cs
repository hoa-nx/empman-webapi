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
    [Table("CustomerUnitPrices")]
    public class CustomerUnitPrice : Auditable
    {

        /// <summary>
        /// Khóa chính tự  sinh
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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
        public DateTime? StartDate{ set; get; }

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
        [ForeignKey("OrderUnit")]
        public int? OrderUnitMasterID { set; get; }

        /// <summary>
        /// Đơn vị tính
        /// </summary>
        [ForeignKey("OrderUnit")]
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

        [ForeignKey("CustomerID")]
        public virtual Customer Customer { set; get; }

        public virtual MasterDetail OrderUnit { set; get; }

    }
}

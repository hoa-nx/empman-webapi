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
    /// Table thông tin nhận đặt hàng
    /// </summary>
    [Table("OrderReceiveds")]
    public class OrderReceived : Auditable
    {
        /// <summary>
        /// Khóa chính (tự sinh)
        /// </summary>

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int No { set; get; }

        /// <summary>
        /// Mã order
        /// </summary>
        [Key]
        [MaxLength(256)]
        public string ID { set; get; }

        /// <summary>
        /// Tên order
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
        /// Tên trên report
        /// </summary>
        [MaxLength(512)]
        public string NameUseInReport { set; get; }

        /// <summary>
        /// Báo giá 
        /// </summary>
        [MaxLength(256)]
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

        [ForeignKey("EstimateID")]
        public virtual Estimate Estimate { set; get; }

        [ForeignKey("PMEmpID")]
        public virtual Emp PMEmp { set; get; }

        [ForeignKey("TransEmpID")]
        public virtual Emp TransEmp { set; get; }

        [ForeignKey("BseID")]
        public virtual Emp Bse { set; get; }

        [ForeignKey("FileID")]
        public virtual FileStorage File { set; get; }
    }
}

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
    /// Thông tin nhân viên --quản lý support
    /// </summary>
    [Table("EmpSupports")]
    public class EmpSupport : Auditable

    {
        /// <summary>
        /// Khóa chính tự sinh
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { set; get; }

        /// <summary>
        /// Mã nhân viên
        /// </summary>
        [Required]
        public int EmpID { set; get; }

        /// <summary>
        /// Ngày start 
        /// </summary>
        public DateTime? StartDate { set; get; }

        /// <summary>
        /// Ngày end
        /// </summary>
        public DateTime? EndDate { set; get; }

        /// <summary> 
        /// Loại support  (thực tập / thử việc ...)
        /// <para/> CommonData.Code =26
        /// </summary>
        [ForeignKey("SupportType")]
        public int? SupportTypeMasterID { set; get; }

        /// <summary> 
        /// Loại support  (thực tập / thử việc ...)
        /// <para/> CommonData.Code =26
        /// </summary>
        [ForeignKey("SupportType")]
        public int? SupportTypeMasterDetailID { set; get; }

        /// <summary>
        /// Ngày nhận tiền support
        /// </summary>
        public DateTime? ReceivedSupportFeeDate1 { set; get; }

        /// <summary>
        /// Ngày nhận tiền support
        /// </summary>
        public DateTime? ReceivedSupportFeeDate2 { set; get; }

        /// <summary>
        /// Ngày nhận tiền support
        /// </summary>
        public DateTime? ReceivedSupportFeeDate3 { set; get; }

        /// <summary>
        /// Tổng tiền nhận được lần 1 
        /// </summary>
        public decimal? ReceivedSupportFee1 { set; get; }

        /// <summary>
        /// Tổng tiền nhận được lần 2
        /// </summary>
        public decimal? ReceivedSupportFee2 { set; get; }


        /// <summary>
        /// Tổng tiền nhận được lần 3 
        /// </summary>
        public decimal? ReceivedSupportFee3 { set; get; }

        /// <summary>
        /// Người được support
        /// </summary>
        public int? TraineeID { set; get; }

        /// <summary>
        /// Kết quả đánh giá 
        /// </summary>
        public string Result { set; get; }

        /// <summary>
        /// Hanh dong
        /// </summary>
        public string Action { set; get; }


        [ForeignKey("EmpID")]
        public virtual Emp Emp { set; get; }

        public virtual MasterDetail SupportType { set; get; }

        [ForeignKey("TraineeID")]
        public virtual Emp Trainee { set; get; }


    }

}

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
    [Table("EmpEstimates")]
    public class EmpEstimate : Auditable

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
        /// Tháng năm đối tượng
        /// </summary>
        public DateTime? YearMonth { set; get; }

        /// <summary>
        /// Điểm đánh giá
        /// </summary>
        public decimal? EstimatePoint { set; get; }

        /// <summary>
        /// MM effort trong tháng
        /// </summary>
        public decimal? EffortMM { set; get; }

        /// <summary>
        /// Tiền thưởng trong tháng
        /// </summary>
        public decimal? BonusUsd { set; get; }

        [ForeignKey("EmpID")]
        public virtual Emp Emp { set; get; }

    }

}

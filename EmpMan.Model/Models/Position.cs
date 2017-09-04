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
    /// Thông tin chức vụ
    /// </summary>
    [Table("Positions")]
    public class Position : Auditable
    {
             

        /// <summary>
        /// Mã tự sinh
        /// </summary>
        
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int No { set; get; }

        /// <summary>
        /// Mã chức vụ
        /// </summary>
        [Key]
        public int ID { set; get; }

        /// <summary>
        /// Tên chức vụ
        /// </summary>
        [Required]
        [MaxLength(256)]
        public string Name { set; get; }

        /// <summary>
        /// tên tắt
        /// </summary>
        [Required]
        [MaxLength(256)]
        public string ShortName { set; get; }

        /// <summary>
        /// role tương ứng
        /// </summary>
        public string RoleId { set; get; }

        /// <summary>
        /// Số tháng bình quân để đạt ngạch tương ứng
        /// </summary>
        public int? MonthAvg { set; get; }

        /// <summary>
        /// phụ cấp 
        /// </summary>
        public decimal? Allowance { set; get; }

        /// <summary>
        /// chức vụ cha
        /// </summary>
        public int? ParentID { set; get; }

        /// <summary>
        /// chức vụ tiếp theo (cao lên)
        /// </summary>
        public int? NextLevelID { set; get; }


        /// <summary>
        /// Nhóm chức vụ
        /// <para/> Master.Code =28
        /// </summary>
        [ForeignKey("PositionGroup")]
        public int? PositionGroupMasterID { set; get; }

        /// <summary>
        /// Code tương ứng của nhóm chức vụ
        /// <para/> Master.Code =28
        /// </summary>
        [ForeignKey("PositionGroup")]
        public int? PositionGroupMasterDetailID { set; get; }

        /// <summary>
        /// Số MM có thể đối ứng cùa ngạch
        /// </summary>
        public decimal? MM { set; get; }

        /// <summary>
        /// Số tiền tăng lương theo ngạch (chuẩn công ty)
        /// </summary>
        public decimal? StandardMoneyIncrease { set; get; }

        [ForeignKey("RoleId")]
        public virtual AppRole AppRole { set; get; }

        public virtual MasterDetail PositionGroup { set; get; }

    }
}

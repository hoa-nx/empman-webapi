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
    /// Thông tin nhân viên --Phụ cấp
    /// </summary>
    [Table("EmpAllowances")]
    public class EmpAllowance : Auditable
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { set; get; }

        /// <summary>
        /// Code nhân viên
        /// </summary>
        [Required]
        [Index("ix_EmpAllowance_Emp_StartDate_EndData_Allowance", 1, IsUnique = true)]
        public int EmpID { set; get; }

        /// <summary>
        /// Ngày start nhận phụ cấp
        /// </summary>
        [Index("ix_EmpAllowance_Emp_StartDate_EndData_Allowance", 2, IsUnique = true)]
        public DateTime? StartDate { set; get; }

        /// <summary>
        /// Ngày kết thúc nhận phụ cấp
        /// </summary>
        [Index("ix_EmpAllowance_Emp_StartDate_EndData_Allowance", 3, IsUnique = true)]
        public DateTime? EndDate { set; get; }

        /// <summary>
        /// Loại phụ cấp ( CommonData.IsAllowance=true)
        /// <para/> 
        /// </summary>
        /// 
        [Index("ix_EmpAllowance_Emp_StartDate_EndData_Allowance", 4, IsUnique = true)]
        [ForeignKey("AllowanceType"), Column(Order = 0)]
        public int? AllowanceTypeMasterID { set; get; }
        /// <summary>
        /// Chi tiết Loại phụ cấp ( CommonData.IsAllowance=true)
        /// <para/> 
        /// </summary>
        [Index("ix_EmpAllowance_Emp_StartDate_EndData_Allowance", 5, IsUnique = true)]
        [ForeignKey("AllowanceType"), Column(Order = 1)]
        public int? AllowanceTypeMasterDetailID { set; get; }

        /// <summary>
        /// Số tiền phụ cấp
        /// </summary>
        public decimal? AllowanceMoney { set; get; }

        /// <summary>
        /// Ngày ký kết
        /// </summary>
        public DateTime? SignDate { set; get; }

        /// <summary>
        /// Kết quả
        /// </summary>
        public string Result { set; get; }

        /// <summary>
        /// Hành động tiếp theo
        /// </summary>
        public string Action { set; get; }

        /// <summary>
        /// File đính kèm
        /// </summary>
        public int? FileID { set; get; }

        /// <summary>
        /// khóa ngoại nhân viên
        /// </summary>
        [ForeignKey("EmpID")]
        public virtual Emp Emp { set; get; }

        /// <summary>
        /// khóa ngoại loại phụ cấp
        /// </summary>
        public virtual MasterDetail AllowanceType { set; get; }

        /// <summary>
        /// Khóa ngoại của bảng FileStorage
        /// </summary>
        [ForeignKey("FileID")]
        public virtual FileStorage File { set; get; }

        ///// <summary>
        ///// Danh sách master data
        ///// </summary>
        //public virtual ICollection<Master> MasterList { set; get; }

        ///// <summary>
        ///// Danh sách master data detail
        ///// </summary>
        //public virtual ICollection<MasterDetail> MasterDetailList { set; get; }
        ///// <summary>
        ///// Danh sách file
        ///// </summary>
        //public virtual ICollection<FileStorage> FileList { set; get; }

        ///// <summary>
        ///// Danh sách nhân viên
        ///// </summary>
        //public virtual ICollection<Emp> EmpList { set; get; }
    }
}

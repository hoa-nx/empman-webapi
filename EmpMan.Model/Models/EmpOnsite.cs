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
    /// Thông tin nhân viên --quản lý onsite
    /// </summary>
    [Table("EmpOnsites")]
    public class EmpOnsite : Auditable
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
        [Index("ix_EmpOnsite_Emp_StartDate_EndData_OnsiteType", 1, IsUnique = true)]
        public int EmpID { set; get; }

        /// <summary>
        /// Ngày start 
        /// </summary>
        [Index("ix_EmpOnsite_Emp_StartDate_EndData_OnsiteType", 2, IsUnique = true)]
        public DateTime? StartDate { set; get; }

        /// <summary>
        /// Ngày end
        /// </summary>
        [Index("ix_EmpOnsite_Emp_StartDate_EndData_OnsiteType", 3, IsUnique = true)]
        public DateTime? EndDate { set; get; }

        /// <summary> 
        /// Loại onsite ( intership / ngắn hạn / dài hạn )
        /// <para/> CommonData.Code =22
        /// </summary>
        [Index("ix_EmpOnsite_Emp_StartDate_EndData_OnsiteType", 4, IsUnique = true)]
        [ForeignKey("OnsiteType")]
        public int OnsiteTypeMasterID { set; get; }

        /// <summary> 
        /// Code Loại onsite ( intership / ngắn hạn / dài hạn )
        /// <para/> CommonData.Code =22
        /// </summary>
        [Index("ix_EmpOnsite_Emp_StartDate_EndData_OnsiteType", 5, IsUnique = true)]
        [ForeignKey("OnsiteType")]
        public int OnsiteTypeMasterDetailID { set; get; }

        /// <summary>
        /// Khoảng thời gian onsite
        /// </summary>
        public decimal? OnsiteKikan { set; get; }
        /// <summary>
        /// Thời gian cam kết làm cho công ty sau onsite
        /// </summary>
        public decimal? PromiseWorkKikan { set; get; }

        /// <summary>
        /// Đơn vị tính
        /// <para/> Master.Code =23
        /// </summary>
        [ForeignKey("OnsiteKikanTimeUnit")]
        public int? OnsiteKikanTimeUnitMasterID { set; get; }

        /// <summary>
        /// Mã chi tiết Đơn vị tính
        /// <para/> Master.Code =23
        /// </summary>
        [ForeignKey("OnsiteKikanTimeUnit")]
        public int? OnsiteKikanTimeUnitMasterDetailID { set; get; }

        /// <summary>
        /// Có ký cam kết không ?
        /// </summary>
        public bool? IsContractSign { set; get; }

        /// <summary>
        /// Ngày ký kết
        /// </summary>
        public DateTime? SignDate { set; get; }

        /// <summary>
        /// Team ở FJS JAPAN
        /// </summary>
        public int ? JapanTeamID { set; get; }

        /// <summary>
        /// Đi cho KH nào
        /// </summary>
        public int? CustomerID { set; get; }
       
        /// <summary>
        /// Kết quả
        /// </summary>
        public string Result { set; get; }

        /// <summary>
        /// Hành động
        /// </summary>
        public string Action { set; get; }

        /// <summary>
        /// file hợp đồng
        /// </summary>
        public int? FileID { set; get; }

        [ForeignKey("EmpID")]
        public virtual Emp Emp { set; get; }

        public virtual MasterDetail OnsiteType { set; get; }

        public virtual MasterDetail OnsiteKikanTimeUnit { set; get; }

        [ForeignKey("JapanTeamID")]
        public virtual Team EmpOnsiteJapanTeam { set; get; }

        [ForeignKey("CustomerID")]
        public virtual Customer Customer { set; get; }

        [ForeignKey("FileID")]
        public virtual FileStorage File { set; get; }


        //public virtual ICollection<Emp> EmpList { set; get; }

        //public virtual ICollection<MasterDetail> OnsiteTypeList { set; get; }

        //public virtual ICollection<MasterDetail> OnsiteKikanTimeUnitList { set; get; }

        //public virtual ICollection<Team> TeamList { set; get; }

        //public virtual ICollection<Customer> CustomerList { set; get; }

        //public virtual ICollection<FileStorage> FileList { set; get; }

    }
}

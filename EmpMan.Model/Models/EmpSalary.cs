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
    /// Thông tin nhân viên --lương nhân viên
    /// </summary>
    [Table("EmpSalarys")]
    public class EmpSalary : Auditable
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
        [Index("ix_EmpSalarys_Emp_StartDate_EndData", 1,IsUnique =true)]
        public int EmpID { set; get; }

        /// <summary>
        /// No hợp đồng (Khóa ngoại)
        /// </summary>
        [Index("ix_EmpSalarys_Emp_StartDate_EndData", 2, IsUnique = true)]
        public int? ContractID { set; get; }

        /// <summary>
        /// Ngày start 
        /// </summary>
        [Index("ix_EmpSalarys_Emp_StartDate_EndData", 3, IsUnique = true)]
        public DateTime? StartDate { set; get; }

        /// <summary>
        /// Ngày end
        /// </summary>
        [Index("ix_EmpSalarys_Emp_StartDate_EndData", 4, IsUnique = true)]
        public DateTime? EndDate { set; get; }

        /// <summary>
        /// Hệ số lương  lần trước
        /// </summary>
        public decimal? PreSalaryUnit { set; get; }

        /// <summary>
        /// Lương cơ bản lần trước 
        /// </summary>
        public decimal? PreNetSalary { set; get; }

        /// <summary>
        /// Mức tăng thực tế  lần trước( bao nhiêu $)
        /// </summary>
        public decimal? PreAdditionMoney { set; get; }

        /// <summary>
        /// Tỉ lệ tăng so với chuẩn công ty  lần trước
        /// </summary>
        public decimal? PreAdditionPercent { set; get; }

        /// <summary>
        /// Phụ cấp cố định lần trước
        /// </summary>
        public decimal? PreNetAllowance { set; get; }

        /// <summary>
        /// Tháng XL  lần trước
        /// </summary>
        public decimal? PreYMSalary { set; get; }

        /// <summary>
        /// Hệ số lương  lần này
        /// </summary>
        public decimal? KonSalaryUnit { set; get; }

        /// <summary>
        /// Lương cơ bản lần này 
        /// </summary>
        public decimal? KonNetSalary { set; get; }

        /// <summary>
        /// Mức tăng thực tế  lần này( bao nhiêu $)
        /// </summary>
        public decimal? KonAdditionMoney { set; get; }

        /// <summary>
        /// Tỉ lệ tăng so với chuẩn công ty  lần này
        /// </summary>
        public decimal? KonAdditionPercent { set; get; }

        /// <summary>
        /// Phụ cấp cố định lần này
        /// </summary>
        public decimal? KonNetAllowance { set; get; }

        /// <summary>
        /// Tháng XL  lần này
        /// </summary>
        public decimal? KonYMSalary { set; get; }

        /// <summary>
        /// Tháng XL lần tiếp theo
        /// </summary>
        public decimal? NextYMSalary { set; get; }

        /// <summary>
        /// Loại XL 6 tháng /  1 năm / …. ( Master.Code =21)
        /// </summary>
        [ForeignKey("SalaryIncreaseType"), Column(Order = 0)]
        public int? SalaryIncreaseTypeMasterID { set; get; }

        /// <summary>
        /// Loại XL 6 tháng /  1 năm / …. ( Master.Code =21)
        /// </summary>
        [ForeignKey("SalaryIncreaseType"), Column(Order = 1)]
        public int? SalaryIncreaseTypeMasterDetailID { set; get; }

        /// <summary>
        /// Ngày ký
        /// </summary>
        public DateTime? SignDate { set; get; }

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

        [ForeignKey("ContractID")]
        public virtual EmpContract Contract { set; get; }

        public virtual MasterDetail SalaryIncreaseType { set; get; }

        [ForeignKey("FileID")]
        public virtual FileStorage File { set; get; }

        //public virtual ICollection<Emp> EmpList { set; get; }

        //public virtual ICollection<EmpContract> ContractList { set; get; }

        //public virtual ICollection<MasterDetail> SalaryIncreaseTypeList { set; get; }

        //public virtual ICollection<FileStorage> FileList { set; get; }
    }
}

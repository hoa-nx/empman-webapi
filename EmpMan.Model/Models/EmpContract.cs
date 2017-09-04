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
    /// Thông tin nhân viên --hợp đồng lao động
    /// </summary>
    [Table("EmpContracts")]
    public class EmpContract : Auditable
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
        /// No hợp đồng
        /// </summary>
        public string ContractNo { set; get; }

        /// <summary>
        /// Ngày start 
        /// </summary>
        public DateTime? StartDate { set; get; }

        /// <summary>
        /// Ngày end
        /// </summary>
        public DateTime? EndDate { set; get; }

        /// <summary> 
        /// Loại HĐ ví dụ như thử việc / ngắn hạn / dài hạn
        /// <para/> CommonData.Code =17
        /// </summary>
        [ForeignKey("ContractType"), Column(Order = 0)]
        public int ContractTypeMasterID { set; get; }

        /// <summary> 
        /// Loại HĐ ví dụ như thử việc / ngắn hạn / dài hạn
        /// <para/> CommonData.Code =17
        /// </summary>
        [ForeignKey("ContractType"), Column(Order = 1)]
        public int ContractTypeMasterDetailID { set; get; }

        /// <summary>
        /// Ngày ký kết
        /// </summary>
        public DateTime? SignDate { set; get; }

        /// <summary>
        /// Hệ số lương cơ bản
        /// </summary>
        public decimal? SalaryUnit{ set; get; }

        /// <summary>
        /// Lương cơ bản ( lương cứng)
        /// </summary>
        public decimal? NetSalary { set; get; }

        /// <summary>
        /// Phụ cấp cố định
        /// </summary>
        public decimal? NetAllowance { set; get; }

        /// <summary>
        /// Phụ cấp khác 
        /// </summary>
        public decimal? OtherAllowance { set; get; }

        /// <summary>
        /// Số lần gia hạn phụ lục hợp đồng
        /// </summary>
        public int? SubContractSignCount{ set; get; }

        /// <summary>
        /// Kết quả
        /// </summary>
        public string Result { set; get; }

        /// <summary>
        /// Hành động
        /// </summary>
        public string Action { set; get; }

        /// <summary>
        /// Nội dung hợp đồng
        /// </summary>
        public string Content { set; get; }

        /// <summary>
        /// file hợp đồng
        /// </summary>
        public int? FileID { set; get; }

        [ForeignKey("EmpID")]
        public virtual Emp Emp { set; get; }

        public virtual MasterDetail ContractType { set; get; }

        [ForeignKey("FileID")]
        public virtual FileStorage File { set; get; }

        //public virtual ICollection<Emp> EmpList { set; get; }

        //public virtual ICollection<MasterDetail> ContractTypeList { set; get; }

        //public virtual ICollection<FileStorage> FileList { set; get; }
    }
}

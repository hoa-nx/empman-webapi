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
    /// Lưu thông itn bộ phận
    /// </summary>
    [Table("Depts")]
    public class Dept : Auditable
    {
        /// <summary>
        /// Khóa tự sinh
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int No{ set; get; }

        /// <summary>
        /// Mã bộ phận
        /// </summary>

        [Key]
        public int ID { set; get; }

        /// <summary>
        /// Tên bộ phận
        /// </summary>
        [Required]
        [MaxLength(256)]
        public string Name { set; get; }

        /// <summary>
        /// Tên tắt bộ phận
        /// </summary>
        [Required]
        [MaxLength(256)]
        public string ShortName { set; get; }

        /// <summary>
        /// Ngày thành lập
        /// </summary>
        public DateTime? CreateDate { set; get; }

        /// <summary>
        /// Tổng Quản lý cao nhất ( Liên kết với bảng EMP)
        /// </summary>
        public int? TopManagerID { set; get; }

        /// <summary>
        /// Quản lý cấp cao 1( Liên kết với bảng EMP)
        /// </summary>
        public int? Manager1ID { set; get; }

        /// <summary>
        /// Quản lý cấp cao 2 ( Liên kết với bảng EMP)
        /// </summary>
        public int? Manager2ID { set; get; }

        /// <summary>
        /// Phó Quản lý 1( Liên kết với bảng EMP)
        /// </summary>
        public int? ViceManager1ID { set; get; }

        /// <summary>
        /// Phó quản lý 2 ( Liên kết với bảng EMP)
        /// </summary>
        public int? ViceManager2ID { set; get; }

        /// <summary>
        /// Phó quản lý 3 ( Liên kết với bảng EMP)
        /// </summary>
        public int? ViceManager3ID { set; get; }

        /// <summary>
        /// Công ty
        /// </summary>
        public int? CompanyID{ set; get; }

        /// <summary>
        /// Mail group
        /// </summary>
        [MaxLength(256)]
        public string MailGroup { set; get; }

        [ForeignKey("TopManagerID")]
        public virtual Emp TopManager { set; get; }

        [ForeignKey("Manager1ID")]
        public virtual Emp Manager1 { set; get; }

        [ForeignKey("Manager2ID")]
        public virtual Emp Manager2 { set; get; }

        [ForeignKey("ViceManager1ID")]
        public virtual Emp ViceManager1 { set; get; }

        [ForeignKey("ViceManager2ID")]
        public virtual Emp ViceManager2 { set; get; }

        [ForeignKey("ViceManager3ID")]
        public virtual Emp ViceManager3 { set; get; }

        //public virtual ICollection<Emp> TopManagerList { get; set; }

    }
}

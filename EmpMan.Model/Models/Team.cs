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
    /// Thông tin tổ nhóm
    /// </summary>
    [Table("Teams")]
    public class Team : Auditable
    {

        /// <summary>
        /// Khóa chính tự sinh
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int No { set; get; }


        /// <summary>
        /// Mã team
        /// </summary>
        [Key]
        public int ID { set; get; }

        /// <summary>
        /// Tên nhóm
        /// </summary>
        [Required]
        [MaxLength(256)]
        public string Name { set; get; }

        /// <summary>
        /// Tên tắt
        /// </summary>
        [Required]
        [MaxLength(256)]
        public string ShortName { set; get; }

        /// <summary>
        /// Ngảy thành lập
        /// </summary>
        public DateTime? CreateDate { set; get; }

        /// <summary>
        /// Leader ( liên kết với bảng emp)
        /// </summary>
        public int? TopLeaderID { set; get; }

        /// <summary>
        /// SubLeader ( liên kết với bảng emp)
        /// </summary>
        public int? SubLeaderID { set; get; }

        /// <summary>
        /// Thuoc dep nao
        /// </summary>
        public int? DeptID { set; get; }

        /// <summary>
        /// Địa chỉ mail
        /// </summary>
        public string MailGroup { set; get; }

        [ForeignKey("TopLeaderID")]
        public virtual Emp TopLeader { set; get; }

        [ForeignKey("SubLeaderID")]
        public virtual Emp SubLeader { set; get; }

        [ForeignKey("DeptID")]
        public virtual Dept Dept { set; get; }

        //public virtual ICollection<Emp> TopLeaderList { get; set; }

        //public virtual ICollection<Emp> SubLeaderList { get; set; }

    }
}

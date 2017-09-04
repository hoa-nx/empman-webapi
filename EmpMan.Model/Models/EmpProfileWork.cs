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
    /// Thông tin nhân viên --profile kỹ thuật
    /// </summary>
    [Table("EmpProfileWorks")]
    public class EmpProfileWork : Auditable
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { set; get; }

        /// <summary>
        /// Code nhân viên
        /// </summary>
        [Required]
        public int EmpID { set; get; }

        /// <summary>
        /// ID profile
        /// </summary>
        [Required]
        public int EmpProfileID { set; get; }

        /// <summary>
        /// 作業期間 START
        /// </summary>
        public DateTime? StartDate{ set; get; }

        /// <summary>
        /// 作業期間 END
        /// </summary>
        public DateTime? EndDate { set; get; }

        /// <summary>
        /// 業務名/業務内容
        /// </summary>
        public string WorkContent { set; get; }

        /// <summary>
        /// OS
        /// </summary>
        [MaxLength(256)]
        public string Os{ set; get; }

        /// <summary>
        /// 言語/アプリケーション
        /// </summary>
        [MaxLength(256)]
        public string LangTool { set; get; }

        /// <summary>
        /// Loại công việc đối ứng ( thiết kế / phân tích / code / test ...)
        /// </summary>
        public string WorkType { set; get; }

        /// <summary>
        /// File Template
        /// </summary>
        public int? TemplateID{ set; get; }

        /// <summary>
        /// khóa ngoại nhân viên
        /// </summary>
        [ForeignKey("EmpID")]
        public virtual Emp Emp { set; get; }

        /// <summary>
        /// khóa ngoại profile nhân viên
        /// </summary>
        [ForeignKey("EmpProfileID")]
        public virtual EmpProfile EmpProfile { set; get; }
        
        
    }
}

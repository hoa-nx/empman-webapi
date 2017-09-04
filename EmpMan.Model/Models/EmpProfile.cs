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
    [Table("EmpProfiles")]
    public class EmpProfile : Auditable
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
        /// 得意な業務
        /// </summary>
        public string WorkGood { set; get; }

        /// <summary>
        /// 業務知識/資格
        /// </summary>
        public string Keikaku { set; get; }

        /// <summary>
        /// 英語その他
        /// </summary>
        public string EnglishLevel { set; get; }

        /// <summary>
        /// 最終学歴
        /// </summary>
        public string Collect { set; get; }

        /// <summary>
        /// File ID đính kèm
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
        [ForeignKey("FileID")]
        public virtual FileStorage File { set; get; }

        /// <summary>
        /// Danh sách nhân viên
        /// </summary>
        //public virtual ICollection<Emp> EmpList { set; get; }

        public virtual ICollection<EmpProfileTech> EmpProfileTechList { set; get; }

        public virtual ICollection<EmpProfileWork> EmpProfileWorkList { set; get; }
    }
}

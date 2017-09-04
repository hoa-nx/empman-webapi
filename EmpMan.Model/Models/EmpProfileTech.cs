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
    [Table("EmpProfileTechs")]
    public class EmpProfileTech : Auditable
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
        /// Ngôn ngữ
        /// </summary>
        public string Lang { set; get; }

        /// <summary>
        /// Thời gian sử dụng
        /// </summary>
        public string Kikan { set; get; }

        /// <summary>
        /// Đơn vị tính thời gian là tháng
        /// </summary>
        public bool? IsUnitMonth { set; get; }

        /// <summary>
        /// Đơn vị tính thời gian là năm
        /// </summary>
        public bool? IsUnitYear { set; get; }

        /// <summary>
        /// Mức độ (能力レベル: 1.上級 2.中級 3.初級)
        /// </summary>
        public string Level { set; get; }


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

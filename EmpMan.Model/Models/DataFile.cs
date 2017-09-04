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
    [Table("DataFiles")]
    public class DataFile : Auditable
    {
        /// <summary>
        /// Khóa tự sinh
        /// </summary>
        /// 
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { set; get; }

        /// <summary>
        /// File ID
        /// </summary>
        public int FileID { set; get; }

        /// <summary>
        /// Ten bang
        /// </summary>
        [Required]
        [MaxLength(256)]
        public string TableName { set; get; }

        /// <summary>
        /// Khoa ngoai cua bang TableName
        /// </summary>
        public int DataID { set; get; }
        
        /// <summary>
        /// Tên 
        /// </summary>
        [MaxLength(256)]
        public string Name { set; get; }

        /// <summary>
        /// Khoa ngoai cua bang TableName
        /// </summary>
        public int? EmpID { set; get; }

        [ForeignKey("FileID")]

        public virtual FileStorage File { set; get; }

        [ForeignKey("EmpID")]
        public virtual Emp Emp { set; get; }

    }
}

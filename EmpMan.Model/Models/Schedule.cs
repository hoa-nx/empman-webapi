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
    /// Schedule công việc
    /// </summary>
    public class Schedule : Auditable
    {
        /// <summary>
        /// Khóa chính (tự sinh)
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { set; get; }

        /// <summary>
        /// Người tạo
        /// </summary>
        public int? CreatorID { set; get; }

        /// <summary>
        /// Tựa đề
        /// </summary>
        [MaxLength(512)]
        public string Title { set; get; }

        /// <summary>
        /// Mô tả
        /// </summary>
        public string Description { set; get; }

        /// <summary>
        /// Vị trí
        /// </summary>
        public string Location { set; get; }
        /// <summary>
        /// Loại
        /// </summary>
        public int? Type { set; get; }

        /// <summary>
        /// Thời điểm bắt đầu
        /// </summary>
        public DateTime? TimeStart { set; get; }

        /// <summary>
        /// Thời điểm kết thúc
        /// </summary>
        public DateTime? TimeEnd { set; get; }

    }
}

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
    /// Quản lý record  course seminar
    /// </summary>
    [Table("SeminarRecords")]
    public class SeminarRecord : Auditable
    {
        /// <summary>
        /// Khóa tự sinh
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { set; get; }

        /// <summary>
        /// mã khóa học
        /// </summary>
        public int? SeminarCourseID { set; get; }

        /// <summary>
        /// mã nhân viên tham gia
        /// </summary>
        public int? EmpID { set; get; }

        /// <summary>
        /// có đăng ký tham gia
        /// </summary>
        public bool? IsParticipation { set; get; }

        /// <summary>
        /// Có mặt không ? Nếu có trị là 1
        /// </summary>
        public bool? IsPresent { set; get; }

        /// <summary>
        /// Ngày thực tế tổ chức
        /// </summary>
        public DateTime? ActualSeminarDate { set; get; }

        /// <summary>
        /// Kết quả test
        /// </summary>
        public bool? IsPassedTest { set; get; }



        [ForeignKey("SeminarCourseID")]
        public virtual SeminarCourse SeminarCourse { set; get; }

        [ForeignKey("EmpID")]
        public virtual Emp Emp { set; get; }
                
    }
}

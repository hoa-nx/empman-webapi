using EmpMan.Web.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmpMan.Web.Models.Emp
{
    public class SeminarRecordViewModel : AuditableViewModel
    {
        /// <summary>
        /// Khóa tự sinh
        /// </summary>
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


        public virtual SeminarCourseViewModel SeminarCourse { set; get; }

        public virtual EmpViewModel Emp { set; get; }

    }
}
using EmpMan.Web.Models.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EmpMan.Web.Models.Emp
{
    public class EmpProfileWorkViewModel : AuditableViewModel
    {
        [Key]
        public int ID { set; get; }

        /// <summary>
        /// Code nhân viên
        /// </summary>
        [Required(ErrorMessage = "Mã đủ nhân viên bắt buộc nhập")]
        public int EmpID { set; get; }

        /// <summary>
        /// ID profile
        /// </summary>
        [Required(ErrorMessage = "Mã profile nhân viên bắt buộc nhập")]
        public int EmpProfileID { set; get; }

        /// <summary>
        /// 作業期間 START
        /// </summary>
        public DateTime? StartDate { set; get; }

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
        public string Os { set; get; }

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
        public int? TemplateID { set; get; }

        /// <summary>
        /// khóa ngoại nhân viên
        /// </summary>
        public virtual EmpViewModel Emp { set; get; }

        /// <summary>
        /// khóa ngoại profile nhân viên
        /// </summary>
        public virtual EmpProfileViewModel EmpProfile { set; get; }
    }
}
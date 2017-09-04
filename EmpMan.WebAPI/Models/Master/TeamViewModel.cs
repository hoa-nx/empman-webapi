using EmpMan.Web.Models.Common;
using EmpMan.Web.Models.Emp;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EmpMan.Web.Models.Master
{
    public class TeamViewModel : AuditableViewModel
    {
        /// <summary>
        /// Khóa chính tự sinh
        /// </summary>
        public int No { set; get; }

        /// <summary>
        /// Mã team
        /// </summary>
        [Required(ErrorMessage = "Mã nhóm(tổ) bắt buộc nhập")]
        public int ID{ set; get; }

        /// <summary>
        /// Tên nhóm
        /// </summary>
        [Required(ErrorMessage = "Tên nhóm(tổ) bắt buộc nhập")]
        [MaxLength(256)]
        public string Name { set; get; }

        /// <summary>
        /// Tên tắt
        /// </summary>
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

        public virtual EmpViewModel TopLeader { set; get; }

        public virtual EmpViewModel SubLeader { set; get; }

        public virtual DeptViewModel Dept { set; get; }

    }
}
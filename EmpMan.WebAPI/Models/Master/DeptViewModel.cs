using EmpMan.Web.Models.Common;
using EmpMan.Web.Models.Emp;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EmpMan.Web.Models.Master
{
    public class DeptViewModel : AuditableViewModel
    {
        public int No { set; get; }

        [Required(ErrorMessage = "Mã bộ phận bắt buộc nhập")]
        public int ID { set; get; }

        [Required(ErrorMessage = "Tên bộ phận bắt buộc nhập")]
        public string Name { set; get; }

        [Required(ErrorMessage = "Tên tắt bộ phận bắt buộc nhập")]
        public string ShortName { set; get; }

        public DateTime? CreateDate { set; get; }

        /// <summary>
        /// Tổng Quản lý cao nhất ( Liên kết với bảng EMP)
        /// </summary>
        public int? TopManagerID { set; get; }

        /// <summary>
        /// Quản lý cấp cao 1( Liên kết với bảng EMP)
        /// </summary>
        public int? Manager1ID { set; get; }

        /// <summary>
        /// Quản lý cấp cao 2 ( Liên kết với bảng EMP)
        /// </summary>
        public int? Manager2ID { set; get; }

        /// <summary>
        /// Phó Quản lý 1( Liên kết với bảng EMP)
        /// </summary>
        public int? ViceManager1ID { set; get; }

        /// <summary>
        /// Phó quản lý 2 ( Liên kết với bảng EMP)
        /// </summary>
        public int? ViceManager2ID { set; get; }

        /// <summary>
        /// Phó quản lý 3 ( Liên kết với bảng EMP)
        /// </summary>
        public int? ViceManager3ID { set; get; }

        /// <summary>
        /// Công ty
        /// </summary>
        public int? CompanyID { set; get; }

        /// <summary>
        /// Mail group
        /// </summary>
        [MaxLength(256)]
        public string MailGroup { set; get; }

        public virtual EmpViewModel TopManager { set; get; }

        public virtual EmpViewModel Manager1 { set; get; }

        public virtual EmpViewModel Manager2 { set; get; }

        public virtual EmpViewModel ViceManager1 { set; get; }

        public virtual EmpViewModel ViceManager2 { set; get; }

        public virtual EmpViewModel ViceManager3 { set; get; }

        public virtual CompanyViewModel Compnay { set; get; }
    }
}
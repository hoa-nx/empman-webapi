using EmpMan.Common.ViewModels.Models.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EmpMan.Common.ViewModels.Models.Emp
{
    public class EmpProfileTechViewModel : AuditableViewModel
    {
        [Key]
        public int ID { set; get; }

        /// <summary>
        /// Code nhân viên
        /// </summary>
        [Required(ErrorMessage = "Mã nhân viên bắt buộc nhập")]
        public int EmpID { set; get; }

        /// <summary>
        /// ID profile
        /// </summary>
        [Required(ErrorMessage = "Mã profile nhân viên bắt buộc nhập")]
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
        public virtual EmpViewModel  Emp { set; get; }

        /// <summary>
        /// khóa ngoại profile nhân viên
        /// </summary>
        public virtual EmpProfileViewModel EmpProfile { set; get; }
    }
}
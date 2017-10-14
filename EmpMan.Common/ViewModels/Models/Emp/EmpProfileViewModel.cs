using EmpMan.Common.ViewModels.Models.Common;
using EmpMan.Common.ViewModels.Models.File;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EmpMan.Common.ViewModels.Models.Emp
{
    public class EmpProfileViewModel : AuditableViewModel
    {
        [Key]
        public int ID { set; get; }

        /// <summary>
        /// Code nhân viên
        /// </summary>
        [Required(ErrorMessage = "mã nhân viên bắt buộc nhập")]
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
        public virtual EmpViewModel Emp { set; get; }

        /// <summary>
        /// khóa ngoại loại phụ cấp
        /// </summary>
        public virtual FileStorageViewModel File { set; get; }

        /// <summary>
        /// Danh sách file
        /// </summary>
        public virtual IEnumerable<FileStorageViewModel> FileList { set; get; }

        /// <summary>
        /// Danh sách nhân viên
        /// </summary>
        public virtual IEnumerable<EmpViewModel> EmpList { set; get; }

        public virtual IEnumerable<EmpProfileTechViewModel> EmpProfileTechList { set; get; }

        public virtual IEnumerable<EmpProfileWorkViewModel> EmpProfileWorkList { set; get; }
    }
}
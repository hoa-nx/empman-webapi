using EmpMan.Common.ViewModels.Models.Common;
using EmpMan.Common.ViewModels.Models.Emp;
using EmpMan.Common.ViewModels.Models.File;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EmpMan.Common.ViewModels.Models.Master
{
    public class CompanyRuleViewModel : AuditableViewModel
    {

        public int No { set; get; }
        /// <summary>
        /// Khóa chính (tự sinh)
        /// </summary>

        public int ID { set; get; }

        /// <summary>
        /// Công ty
        /// </summary>
        public int? CompanyID { set; get; }
        /// <summary>
        /// Ngày thông báo qui định / ngày công bố
        /// </summary>
        [Required(ErrorMessage = "Ngày gửi thông báo bắt buộc nhập")]
        public DateTime? NoticeDate { set; get; }

        /// <summary>
        /// Người tạo rule
        /// </summary>
        public int? CreatorID { set; get; }

        /// <summary>
        /// Người gửi
        /// </summary>
        public int? SenderID { set; get; }
        /// <summary>
        /// Tên người gửi có thể chỉnh sửa
        /// </summary>
        [MaxLength(256)]
        [Required(ErrorMessage = "Tên người gửi bắt buộc nhập")]
        public string SenderName { set; get; }

        /// <summary>
        /// Tên thông báo
        /// </summary>
        [Required(ErrorMessage = "Tên thông báo bắt buộc nhập")]
        [MaxLength(256)]
        public string Name { set; get; }

        /// <summary>
        /// Nội dung
        /// </summary>
        public string Content { set; get; }
        /// <summary>
        /// Đính kèm file
        /// </summary>
        public int? FileID { set; get; }

        /// <summary>
        /// Loại thông báo ví dụ như nhân sự / qui định oniste….
        /// <para/> Master.Code = 25
        /// </summary>
        public int? RuleTypeMasterID { set; get; }

        /// <summary>
        /// Loại thông báo ví dụ như nhân sự / qui định oniste….
        /// <para/> Mã tương ứng với loại Master.Code = 25
        /// </summary>
        public int? RuleTypeMasterDetailID { set; get; }

        /// <summary>
        /// Ngày áp dụng
        /// </summary>
        public DateTime? ValidDateStart { set; get; }

        /// <summary>
        /// Ngày kết thúc áp dụng
        /// </summary>
        public DateTime? ValidDateEnd { set; get; }

        /// <summary>
        /// Nhóm đối tượng thực hiện
        /// </summary>
        public int? ActionObject { set; get; }

        
        public virtual CompanyRuleViewModel Company { set; get; }

        public virtual EmpViewModel Creator { set; get; }

        public virtual EmpViewModel Sender { set; get; }


        public virtual FileStorageViewModel File { set; get; }


        public virtual MasterDetailViewModel RuleTypeMaster { set; get; }


        public virtual MasterDetailViewModel TypeMasterDetail { set; get; }


        public virtual IEnumerable<EmpViewModel> Emps { get; set; }

        public virtual IEnumerable<FileStorageViewModel> FileStorages { get; set; }

        public virtual IEnumerable<MasterDetailViewModel> MasterDetails { get; set; }

        public virtual IEnumerable<ApplicationRoleViewModel> AppRoles { get; set; }

    }
}
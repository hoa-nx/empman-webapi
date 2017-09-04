using EmpMan.Web.Models.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EmpMan.Web.Models.Master
{
    public class PositionViewModel : AuditableViewModel
    {
        /// <summary>
        /// Mã tự sinh
        /// </summary>
        public int No { set; get; }

        /// <summary>
        /// Mã chức vụ
        /// </summary>
        [Required(ErrorMessage = "Mã chức vụ bắt buộc nhập")]
        public int ID{ set; get; }

        /// <summary>
        /// Tên chức vụ
        /// </summary>
        [Required(ErrorMessage = "Tên chức vụ bắt buộc nhập")]
        [MaxLength(256)]
        public string Name { set; get; }

        /// <summary>
        /// tên tắt
        /// </summary>
        [Required(ErrorMessage = "Tên tắt bắt buộc nhập")]
        [MaxLength(256)]
        public string ShortName { set; get; }

        /// <summary>
        /// role tương ứng
        /// </summary>
        public string RoleId { set; get; }

        /// <summary>
        /// Số tháng bình quân để đạt ngạch tương ứng
        /// </summary>
        public int? MonthAvg { set; get; }

        /// <summary>
        /// phụ cấp 
        /// </summary>
        public decimal? Allowance { set; get; }

        /// <summary>
        /// chức vụ cha
        /// </summary>
        public int? ParentID { set; get; }

        /// <summary>
        /// chức vụ tiếp theo (cao lên)
        /// </summary>
        public int? NextLevelID { set; get; }

        /// <summary>
        /// Nhóm chức vụ
        /// <para/> Master.Code =28
        /// </summary>
        public int? PositionGroupMasterID { set; get; }

        /// <summary>
        /// Code tương ứng của nhóm chức vụ
        /// <para/> Master.Code =28
        /// </summary>
        public int? PositionGroupMasterDetailID { set; get; }

        /// <summary>
        /// Số MM có thể đối ứng cùa ngạch
        /// </summary>
        public decimal? MM { set; get; }

        /// <summary>
        /// Số tiền tăng lương theo ngạch (chuẩn công ty)
        /// </summary>
        public decimal? StandardMoneyIncrease { set; get; }

        public virtual ApplicationRoleViewModel PositionRole { set; get; }

        public virtual IEnumerable<ApplicationRoleViewModel> Roless { set; get; }

        public virtual MasterDetailViewModel PositionGroup { set; get; }
    }
}
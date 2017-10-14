using EmpMan.Common.ViewModels.Models.Common;
using EmpMan.Common.ViewModels.Models.File;
using EmpMan.Common.ViewModels.Models.Master;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EmpMan.Common.ViewModels.Models.Emp
{
    public class EmpAllowanceViewModel : AuditableViewModel
    {
        public int ID { set; get; }

        /// <summary>
        /// Code nhân viên
        /// </summary>
        [Required(ErrorMessage = "Mã nhân viên bắt buộc nhập")]
        public int EmpID { set; get; }

        /// <summary>
        /// Ngày start nhận phụ cấp
        /// </summary>
        public DateTime? StartDate { set; get; }

        /// <summary>
        /// Ngày kết thúc nhận phụ cấp
        /// </summary>
        public DateTime? EndDate { set; get; }

        /// <summary>
        /// Loại phụ cấp ( CommonData.IsAllowance=true)
        /// <para/> 
        /// </summary>
        /// 
        public int? AllowanceTypeMasterID { set; get; }
        /// <summary>
        /// Chi tiết Loại phụ cấp ( CommonData.IsAllowance=true)
        /// <para/> 
        /// </summary>
        public int? AllowanceTypeMasterDetailID { set; get; }

        /// <summary>
        /// Số tiền phụ cấp
        /// </summary>
        public decimal? AllowanceMoney { set; get; }

        /// <summary>
        /// Ngày ký kết
        /// </summary>
        public DateTime? SignDate { set; get; }

        /// <summary>
        /// Kết quả
        /// </summary>
        public string Result { set; get; }

        /// <summary>
        /// Hành động tiếp theo
        /// </summary>
        public string Action { set; get; }

        /// <summary>
        /// File đính kèm
        /// </summary>
        public int? FileID { set; get; }

        /// <summary>
        /// khóa ngoại nhân viên
        /// </summary>
        public virtual EmpViewModel Emp { set; get; }

        /// <summary>
        /// khóa ngoại loại phụ cấp
        /// </summary>
        public virtual MasterDetailViewModel AllowanceType { set; get; }

        /// <summary>
        /// Khóa ngoại của bảng FileStorage
        /// </summary>
        public virtual FileStorageViewModel File { set; get; }


        /// <summary>
        /// Danh sách common data
        /// </summary>
        public virtual IEnumerable<MasterViewModel> Masters { set; get; }

        /// <summary>
        /// Danh sách common data detail
        /// </summary>
        public virtual IEnumerable<MasterDetailViewModel> MasterDetails{ set; get; }
        /// <summary>
        /// Danh sách file
        /// </summary>
        public virtual IEnumerable<FileStorageViewModel> Files { set; get; }

        /// <summary>
        /// Danh sách nhân viên
        /// </summary>
        public virtual IEnumerable<EmpViewModel> Emps { set; get; }
    }
}
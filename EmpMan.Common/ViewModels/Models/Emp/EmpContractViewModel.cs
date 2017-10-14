using EmpMan.Common.ViewModels.Models.Common;
using EmpMan.Common.ViewModels.Models.File;
using EmpMan.Common.ViewModels.Models.Master;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EmpMan.Common.ViewModels.Models.Emp
{
    public class EmpContractViewModel : AuditableViewModel
    {
        public int ID { set; get; }

        /// <summary>
        /// Mã nhân viên
        /// </summary>
        [Required(ErrorMessage = "Mã nhân viên bắt buộc nhập")]
        public int EmpID { set; get; }

        /// <summary>
        /// No hợp đồng
        /// </summary>
        public string ContractNo { set; get; }

        /// <summary>
        /// Ngày start 
        /// </summary>
        public DateTime? StartDate { set; get; }

        /// <summary>
        /// Ngày end
        /// </summary>
        public DateTime? EndDate { set; get; }

        /// <summary> 
        /// Loại HĐ ví dụ như thử việc / ngắn hạn / dài hạn
        /// <para/> CommonData.Code =17
        /// </summary>
        public int ContractTypeMasterID { set; get; }

        /// <summary> 
        /// Loại HĐ ví dụ như thử việc / ngắn hạn / dài hạn
        /// <para/> CommonData.Code =17
        /// </summary>
        public int ContractTypeMasterDetailID { set; get; }

        /// <summary>
        /// Ngày ký kết
        /// </summary>
        public DateTime? SignDate { set; get; }

        /// <summary>
        /// Hệ số lương cơ bản
        /// </summary>
        public decimal? SalaryUnit { set; get; }

        /// <summary>
        /// Lương cơ bản ( lương cứng)
        /// </summary>
        public decimal? NetSalary { set; get; }

        /// <summary>
        /// Phụ cấp cố định
        /// </summary>
        public decimal? NetAllowance { set; get; }

        /// <summary>
        /// Phụ cấp khác 
        /// </summary>
        public decimal? OtherAllowance { set; get; }

        /// <summary>
        /// Số lần gia hạn phụ lục hợp đồng
        /// </summary>
        public int? SubContractSignCount { set; get; }

        /// <summary>
        /// Kết quả
        /// </summary>
        public string Result { set; get; }

        /// <summary>
        /// Hành động
        /// </summary>
        public string Action { set; get; }

        /// <summary>
        /// Nội dung hợp đồng
        /// </summary>
        public string Content { set; get; }

        /// <summary>
        /// file hợp đồng
        /// </summary>
        public int? FileID { set; get; }

        public virtual EmpViewModel Emp { set; get; }

        public virtual MasterDetailViewModel ContractType { set; get; }

        public virtual FileStorageViewModel File { set; get; }

        public virtual IEnumerable<EmpViewModel> EmpList { set; get; }
        public virtual IEnumerable<MasterDetailViewModel> ContractTypeList { set; get; }
        public virtual IEnumerable<FileStorageViewModel> FileList { set; get; }

    }
}
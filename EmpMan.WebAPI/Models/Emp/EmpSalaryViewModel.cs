using EmpMan.Web.Models.Common;
using EmpMan.Web.Models.File;
using EmpMan.Web.Models.Master;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EmpMan.Web.Models.Emp
{
    public class EmpSalaryViewModel : AuditableViewModel
    {
        /// <summary>
        /// Khóa chính tự sinh
        /// </summary>
        public int ID { set; get; }

        /// <summary>
        /// Mã nhân viên
        /// </summary>
        [Required(ErrorMessage = "Mã nhân viên bắt buộc nhập")]
        public int EmpID { set; get; }
        /// <summary>
        /// No hợp đồng (Khóa ngoại)
        /// </summary>
        public int? ContractID { set; get; }

        /// <summary>
        /// Ngày start 
        /// </summary>
        public DateTime? StartDate { set; get; }

        /// <summary>
        /// Ngày end
        /// </summary>
        public DateTime? EndDate { set; get; }

        /// <summary>
        /// Hệ số lương  lần trước
        /// </summary>
        public decimal? PreSalaryUnit { set; get; }

        /// <summary>
        /// Lương cơ bản lần trước 
        /// </summary>
        public decimal? PreNetSalary { set; get; }

        /// <summary>
        /// Mức tăng thực tế  lần trước( bao nhiêu $)
        /// </summary>
        public decimal? PreAdditionMoney { set; get; }

        /// <summary>
        /// Tỉ lệ tăng so với chuẩn công ty  lần trước
        /// </summary>
        public decimal? PreAdditionPercent { set; get; }

        /// <summary>
        /// Phụ cấp cố định lần trước
        /// </summary>
        public decimal? PreNetAllowance { set; get; }

        /// <summary>
        /// Tháng XL  lần trước
        /// </summary>
        public decimal? PreYMSalary { set; get; }

        /// <summary>
        /// Hệ số lương  lần này
        /// </summary>
        public decimal? KonSalaryUnit { set; get; }

        /// <summary>
        /// Lương cơ bản lần này 
        /// </summary>
        public decimal? KonNetSalary { set; get; }

        /// <summary>
        /// Mức tăng thực tế  lần này( bao nhiêu $)
        /// </summary>
        public decimal? KonAdditionMoney { set; get; }

        /// <summary>
        /// Tỉ lệ tăng so với chuẩn công ty  lần này
        /// </summary>
        public decimal? KonAdditionPercent { set; get; }

        /// <summary>
        /// Phụ cấp cố định lần này
        /// </summary>
        public decimal? KonNetAllowance { set; get; }

        /// <summary>
        /// Tháng XL  lần này
        /// </summary>
        public decimal? KonYMSalary { set; get; }

        /// <summary>
        /// Tháng XL lần tiếp theo
        /// </summary>
        public decimal? NextYMSalary { set; get; }

        /// <summary>
        /// Loại XL 6 tháng /  1 năm / …. ( Master.Code =21)
        /// </summary>
        public int? SalaryIncreaseTypeMasterID { set; get; }

        /// <summary>
        /// Loại XL 6 tháng /  1 năm / …. ( Master.Code =21)
        /// </summary>
        public int? SalaryIncreaseTypeMasterDetailID { set; get; }

        /// <summary>
        /// Ngày ký
        /// </summary>
        public DateTime? SignDate { set; get; }

        /// <summary>
        /// Kết quả
        /// </summary>
        public string Result { set; get; }

        /// <summary>
        /// Hành động
        /// </summary>
        public string Action { set; get; }

        /// <summary>
        /// file hợp đồng
        /// </summary>
        public int? FileID { set; get; }

        public virtual EmpViewModel Emp { set; get; }

        public virtual EmpContractViewModel Contract { set; get; }

        public virtual MasterDetailViewModel SalaryIncreaseType { set; get; }

        public virtual FileStorageViewModel File { set; get; }

        public virtual IEnumerable<EmpViewModel> EmpList { set; get; }

        public virtual IEnumerable<EmpContractViewModel> ContractList { set; get; }

        public virtual IEnumerable<MasterDetailViewModel> SalaryIncreaseTypeList { set; get; }

        public virtual IEnumerable<FileStorageViewModel> FileList { set; get; }
    }
}
using EmpMan.Web.Models.Common;
using EmpMan.Web.Models.File;
using EmpMan.Web.Models.Master;
using EmpMan.Web.Models.Project;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EmpMan.Web.Models.Emp
{
    public class EmpOnsiteViewModel : AuditableViewModel
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
        /// Ngày start 
        /// </summary>
        [Required(ErrorMessage = "Ngày bắt đầu bắt buộc nhập")]
        public DateTime? StartDate { set; get; }

        /// <summary>
        /// Ngày end
        /// </summary>
        public DateTime? EndDate { set; get; }

        /// <summary> 
        /// Loại onsite ( intership / ngắn hạn / dài hạn )
        /// <para/> CommonData.Code =22
        /// </summary>
        public int OnsiteTypeMasterID { set; get; }

        /// <summary> 
        /// Code Loại onsite ( intership / ngắn hạn / dài hạn )
        /// <para/> CommonData.Code =22
        /// </summary>
        public int OnsiteTypeMasterDetailID { set; get; }

        /// <summary>
        /// Khoảng thời gian onsite
        /// </summary>
        public decimal? OnsiteKikan { set; get; }
        /// <summary>
        /// Thời gian cam kết làm cho công ty sau onsite
        /// </summary>
        public decimal? PromiseWorkKikan { set; get; }

        /// <summary>
        /// Đơn vị tính
        /// <para/> Master.Code =23
        /// </summary>
        public int? OnsiteKikanTimeUnitMasterID { set; get; }

        /// <summary>
        /// Mã chi tiết Đơn vị tính
        /// <para/> Master.Code =23
        /// </summary>
        public int? OnsiteKikanTimeUnitMasterDetailID { set; get; }

        /// <summary>
        /// Có ký cam kết không ?
        /// </summary>
        public bool? IsContractSign { set; get; }

        /// <summary>
        /// Ngày ký kết
        /// </summary>
        public DateTime? SignDate { set; get; }

        /// <summary>
        /// Team ở FJS JAPAN
        /// </summary>
        public int? JapanTeamID { set; get; }

        /// <summary>
        /// Đi cho KH nào
        /// </summary>
        public int? CustomerID { set; get; }

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

        public virtual MasterDetailViewModel OnsiteType { set; get; }

        public virtual MasterDetailViewModel OnsiteKikanTimeUnit { set; get; }

        public virtual TeamViewModel EmpOnsiteJapanTeam { set; get; }

        public virtual CustomerViewModel Customer { set; get; }

        public virtual FileStorageViewModel File { set; get; }


        public virtual IEnumerable<EmpViewModel> Emps { set; get; }

        public virtual IEnumerable<MasterDetailViewModel> OnsiteTypes { set; get; }

        public virtual IEnumerable<MasterDetailViewModel> OnsiteKikanTimeUnits { set; get; }

        public virtual IEnumerable<TeamViewModel> Teams { set; get; }

        public virtual IEnumerable<CustomerViewModel> Customers { set; get; }

        public virtual IEnumerable<FileStorageViewModel> Files { set; get; }
    }
}
using EmpMan.Common.ViewModels.Models.Common;
using EmpMan.Common.ViewModels.Models.Master;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EmpMan.Common.ViewModels.Models.Emp
{
    public class EmpDetailWorkViewModel : AuditableViewModel
    {
        /// <summary>
        /// Khóa chính tự sinh
        /// </summary>
        public int ID { set; get; }

        /// <summary>
        /// Mã nhân viên
        /// </summary>
        [Required(ErrorMessage = "mã nhân viên bắt buộc nhập")]
        public int EmpID { set; get; }

        /// <summary>
        /// Ngày start 
        /// </summary>
        //[Required(ErrorMessage = "Ngày bắt đầu bắt buộc nhập")]
        public DateTime? StartDate { set; get; }

        /// <summary>
        /// Ngày end
        /// </summary>
        public DateTime? EndDate { set; get; }

        /// <summary>
        /// Công ty làm việc hiện tại (Liên kết với bảng company)
        /// </summary>
        public int? CompanyID { set; get; }

        public int? IsChangeCompanyID { set; get; }

        /// <summary>
        /// Dept làm việc hiện tại (Liên kết với bảng dept)
        /// </summary>
        public int? DeptID { set; get; }

        public int? IsChangeDeptID { set; get; }

        /// <summary>
        /// Team hiện tại (Liên kết với bảng Team)
        /// </summary>
        public int? TeamID { set; get; }

        public int? IsChangeTeamID { set; get; }
        /// <summary>
        /// Chức vụ hiện tại (Liên kết với bảng Position)
        /// </summary>
        public int? PositionID { set; get; }
        public int? IsChangePositionID { set; get; }
        /// <summary>
        /// Công ty làm việc hiện tại (Liên kết với bảng company)
        /// </summary>
        public int? Company2ID { set; get; }
        public int? IsChangeCompany2ID { set; get; }
        /// <summary>
        /// Dept làm việc hiện tại (Liên kết với bảng dept)
        /// </summary>
        public int? Dept2ID { set; get; }
        public int? IsChangeDept2ID { set; get; }
        /// <summary>
        /// Team hiện tại (Liên kết với bảng Team)
        /// </summary>
        public int? Team2ID { set; get; }
        public int? IsChangeTeam2ID { set; get; }
        /// <summary>
        /// Chức vụ hiện tại (Liên kết với bảng Position)
        /// </summary>
        public int? Position2ID { set; get; }
        public int? IsChangePosition2ID { set; get; }
        /// <summary>
        /// Loại nhân viên (Nhân viên dept khác chuyển sang , NV sang dept khác hỗ trợ , nhân viên onsite)
        /// <para/> Master.Code =31
        /// </summary>

        public int? WorkEmpTypeMasterID { set; get; }

        /// <summary>
        /// Loại nhân viên (Nhân viên dept khác chuyển sang , NV sang dept khác hỗ trợ , nhân viên onsite)
        /// <para/> Master.Code =31
        /// </summary>

        public int? WorkEmpTypeMasterDetailID { set; get; }
        public int? IsChangeWorkEmpType { set; get; }

        /// <summary>
        /// Loại nhân viên (dev/PD///)
        /// <para/> Master.Code =18
        /// </summary>

        public int? EmpTypeMasterID { set; get; }

        /// <summary>
        /// Loại nhân viên (dev/PD///)
        /// <para/> Master.Code =18
        /// </summary>

        public int? EmpTypeMasterDetailID { set; get; }
        public int? IsChangeEmpType { set; get; }

        /// <summary>
        /// Chứng chỉ tiếng Nhật hiện tại
        /// <para/> Master.Code =10
        /// </summary>

        public int? JapaneseLevelMasterID { set; get; }

        /// <summary>
        /// Chứng chỉ tiếng Nhật hiện tại
        /// <para/> Master.Code =10
        /// </summary>

        public int? JapaneseLevelMasterDetailID { set; get; }
        public int? IsChangeJapaneseLevel { set; get; }
        /// <summary>
        /// Phụ cấp nghiệp vụ hiện tại
        /// <para/> Master.Code =11
        /// </summary>

        public int? BusinessAllowanceLevelMasterID { set; get; }

        /// <summary>
        /// Phụ cấp nghiệp vụ hiện tại
        /// <para/> Master.Code =11
        /// </summary>

        public int? BusinessAllowanceLevelMasterDetailID { set; get; }
        public int? IsChangeBusinessAllowanceLevel { set; get; }
        /// <summary>
        /// Phòng chuyên biệt có internet
        /// <para/> Master.Code =12
        /// </summary>

        public int? RoomWithInternetAllowanceLevelMasterID { set; get; }

        /// <summary>
        /// Phòng chuyên biệt có internet
        /// <para/> Master.Code =12
        /// </summary>

        public int? RoomWithInternetAllowanceLevelMasterDetailID { set; get; }
        public int? IsChangeRoomWithInternetAllowanceLevel { set; get; }

        /// <summary>
        /// Phòng chuyên biệt không internet
        /// <para/> Master.Code =13
        /// </summary>

        public int? RoomNoInternetAllowanceLevelMasterID { set; get; }

        /// <summary>
        /// Phòng chuyên biệt không internet
        /// <para/> Master.Code =13
        /// </summary>

        public int? RoomNoInternetAllowanceLevelMasterDetailID { set; get; }
        public int? IsChangeRoomNoInternetAllowanceLevel { set; get; }
        /// <summary>
        /// Bậc BSE 
        /// <para/> Master.Code =15
        /// </summary>

        public int? BseAllowanceLevelMasterID { set; get; }

        /// <summary>
        /// Bậc BSE 
        /// <para/> Master.Code =15
        /// </summary>

        public int? BseAllowanceLevelMasterDetailID { set; get; }
        public int? IsChangeBseAllowanceLevel { set; get; }
        /// <summary>
        /// Trường tốt nghiệp sau cùng
        /// <para/> Master.Code =17
        /// </summary>

        public int? CollectMasterID { set; get; }

        /// <summary>
        /// Code các trường tốt nghiệp sau cùng
        /// <para/> Master.Code =17
        /// </summary>

        public int? CollectMasterDetailID { set; get; }
        public int? IsChangeCollect { set; get; }
        /// <summary>
        /// Bậc tốt nghiệp ( cao đẳng / đại học / cao học )
        /// <para/> Master.Code = 19 
        /// </summary>

        public int? EducationLevelMasterID { set; get; }

        /// <summary>
        /// mã các bậc tốt nghiệp ( cao đẳng / đại học / cao học )
        /// <para/> Master.Code = 19 
        /// </summary>

        public int? EducationLevelMasterDetailID { set; get; }
        public int? IsChangeEducationLevel { set; get; }
        /// <summary>
        /// Loại hợp đồng 
        /// <para/> Master.Code =17
        /// </summary>

        public int? ContractTypeMasterID { set; get; }

        /// <summary>
        /// Code tương ứng của loại hợp đồng 
        /// <para/> Master.Code =17
        /// </summary>

        public int? ContractTypeMasterDetailID { set; get; }
        public int? IsChangeContractType { set; get; }
        public DateTime? SignDate { set; get; }

        /// <summary>
        /// Khach hang nhan onsite
        /// </summary>
        public int? OnsiteCustomerID { set; get; }
        public int? IsChangeOnsiteCustomerID { set; get; }


        /// <summary>
        /// Kết quả
        /// </summary>
        public string Result { set; get; }

        /// <summary>
        /// Hành động
        /// </summary>
        public string Action { set; get; }


        public virtual EmpViewModel Emp { set; get; }

        public virtual DeptViewModel Company { set; get; }
        
        public virtual DeptViewModel Dept { set; get; }

        public virtual TeamViewModel Team { set; get; }
        
        public virtual PositionViewModel Position { set; get; }

        public virtual MasterDetailViewModel ContractType { set; get; }

        public virtual MasterDetailViewModel EmpType { set; get; }

        public virtual MasterDetailViewModel JapaneseLevel { set; get; }

        public virtual MasterDetailViewModel BusinessAllowanceLevel { set; get; }

        public virtual MasterDetailViewModel RoomWithInternetAllowanceLevel { set; get; }

        public virtual MasterDetailViewModel RoomNoInternetAllowanceLevel { set; get; }

        public virtual MasterDetailViewModel BseAllowanceLevel { set; get; }

        public virtual MasterDetailViewModel Collect { set; get; }

        public virtual MasterDetailViewModel EducationLevel { set; get; }
        /// <summary>
        /// Danh sach nhan vien can cap nhat
        /// </summary>
        public List<int?> ListEmpID { set; get; }

        public string CompanyName { set; get; }
        public string DeptName { set; get; }
        public string TeamName { set; get; }
        public string PositionName { set; get; }
        public string EmpTypeName { set; get; }
        public string JapaneseLevelName { set; get; }
        public string BusinessAllowanceLevelName { set; get; }
        public string RoomWithInternetAllowanceLevelName { set; get; }
        public string RoomNoInternetAllowanceLevelName { set; get; }
        public string BseAllowanceLevelName { set; get; }
        public string CollectName { set; get; }
        public string EducationLevelName { set; get; }
        public string ContractTypeName { set; get; }
        public string CompanyName2 { set; get; }
        public string DeptName2 { set; get; }
        public string TeamName2 { set; get; }
        public string PositionName2 { set; get; }
        public string WorkEmpTypeName { set; get; }
        public string OnsiteCustomerName { set; get; }

        public bool? IsDetailWorkCreateData { set; get; }

    }
}
using EmpMan.Web.Models.Common;
using EmpMan.Web.Models.Master;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EmpMan.Web.Models.Emp
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
        [Required(ErrorMessage = "Ngày bắt đầu bắt buộc nhập")]
        public DateTime? StartDate { set; get; }

        /// <summary>
        /// Ngày end
        /// </summary>
        public DateTime? EndDate { set; get; }

        /// <summary>
        /// Công ty làm việc hiện tại (Liên kết với bảng company)
        /// </summary>
        public int? CompanyID { set; get; }

        /// <summary>
        /// Dept làm việc hiện tại (Liên kết với bảng dept)
        /// </summary>
        public int? DeptID { set; get; }

        /// <summary>
        /// Team hiện tại (Liên kết với bảng Team)
        /// </summary>
        public int? TeamID { set; get; }

        /// <summary>
        /// Chức vụ hiện tại (Liên kết với bảng Position)
        /// </summary>
        public int? PositionID { set; get; }
        /// <summary>
        /// Công ty làm việc hiện tại (Liên kết với bảng company)
        /// </summary>
        public int? Company2ID { set; get; }

        /// <summary>
        /// Dept làm việc hiện tại (Liên kết với bảng dept)
        /// </summary>
        public int? Dept2ID { set; get; }

        /// <summary>
        /// Team hiện tại (Liên kết với bảng Team)
        /// </summary>
        public int? Team2ID { set; get; }

        /// <summary>
        /// Chức vụ hiện tại (Liên kết với bảng Position)
        /// </summary>
        public int? Position2ID { set; get; }

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


        /// <summary>
        /// Ngày ký kết
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

        public List<int?> ListEmpID { set; get; }

    }
}
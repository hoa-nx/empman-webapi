using EmpMan.Model.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpMan.Model.Models
{
    /// <summary>
    /// Thông tin nhân viên --quá trình làm việc
    /// </summary>
    [Table("EmpDetailWorks")]
    public class EmpDetailWork : Auditable
    {
        /// <summary>
        /// Khóa chính tự sinh
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { set; get; }

        /// <summary>
        /// Mã nhân viên
        /// </summary>
        [Required]
        [Index("ix_EmpDetailWork_Emp_StartDate_EndData_WorkTypeID", 1, IsUnique = true)]
        public int EmpID { set; get; }

        /// <summary>
        /// Ngày start 
        /// </summary>
        [Index("ix_EmpDetailWork_Emp_StartDate_EndData_WorkTypeID", 2, IsUnique = true)]
        public DateTime? StartDate { set; get; }

        /// <summary>
        /// Ngày end
        /// </summary>
        [Index("ix_EmpDetailWork_Emp_StartDate_EndData_WorkTypeID", 3, IsUnique = true)]
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
        [ForeignKey("WorkEmpType")]
        public int? WorkEmpTypeMasterID { set; get; }

        /// <summary>
        /// Loại nhân viên (Nhân viên dept khác chuyển sang , NV sang dept khác hỗ trợ , nhân viên onsite)
        /// <para/> Master.Code =31
        /// </summary>
        [ForeignKey("WorkEmpType")]
        public int? WorkEmpTypeMasterDetailID { set; get; }
        public int? IsChangeWorkEmpType { set; get; }

        /// <summary>
        /// Loại nhân viên (dev/PD///)
        /// <para/> Master.Code =18
        /// </summary>
        [ForeignKey("EmpType")]
        public int? EmpTypeMasterID { set; get; }

        /// <summary>
        /// Loại nhân viên (dev/PD///)
        /// <para/> Master.Code =18
        /// </summary>
        [ForeignKey("EmpType")]
        public int? EmpTypeMasterDetailID { set; get; }
        public int? IsChangeEmpType { set; get; }

        /// <summary>
        /// Chứng chỉ tiếng Nhật hiện tại
        /// <para/> Master.Code =10
        /// </summary>
        [ForeignKey("JapaneseLevel")]
        public int? JapaneseLevelMasterID { set; get; }

        /// <summary>
        /// Chứng chỉ tiếng Nhật hiện tại
        /// <para/> Master.Code =10
        /// </summary>
        [ForeignKey("JapaneseLevel")]
        public int? JapaneseLevelMasterDetailID { set; get; }
        public int? IsChangeJapaneseLevel { set; get; }
        /// <summary>
        /// Phụ cấp nghiệp vụ hiện tại
        /// <para/> Master.Code =11
        /// </summary>
        [ForeignKey("BusinessAllowanceLevel")]
        public int? BusinessAllowanceLevelMasterID { set; get; }

        /// <summary>
        /// Phụ cấp nghiệp vụ hiện tại
        /// <para/> Master.Code =11
        /// </summary>
        [ForeignKey("BusinessAllowanceLevel")]
        public int? BusinessAllowanceLevelMasterDetailID { set; get; }
        public int? IsChangeBusinessAllowanceLevel { set; get; }
        /// <summary>
        /// Phòng chuyên biệt có internet
        /// <para/> Master.Code =12
        /// </summary>
        [ForeignKey("RoomWithInternetAllowanceLevel")]
        public int? RoomWithInternetAllowanceLevelMasterID { set; get; }
        
        /// <summary>
        /// Phòng chuyên biệt có internet
        /// <para/> Master.Code =12
        /// </summary>
        [ForeignKey("RoomWithInternetAllowanceLevel")]
        public int? RoomWithInternetAllowanceLevelMasterDetailID { set; get; }
        public int? IsChangeRoomWithInternetAllowanceLevel{ set; get; }

        /// <summary>
        /// Phòng chuyên biệt không internet
        /// <para/> Master.Code =13
        /// </summary>
        [ForeignKey("RoomNoInternetAllowanceLevel")]
        public int? RoomNoInternetAllowanceLevelMasterID { set; get; }

        /// <summary>
        /// Phòng chuyên biệt không internet
        /// <para/> Master.Code =13
        /// </summary>
        [ForeignKey("RoomNoInternetAllowanceLevel")]
        public int? RoomNoInternetAllowanceLevelMasterDetailID { set; get; }
        public int? IsChangeRoomNoInternetAllowanceLevel{ set; get; }
        /// <summary>
        /// Bậc BSE 
        /// <para/> Master.Code =15
        /// </summary>
        [ForeignKey("BseAllowanceLevel")]
        public int? BseAllowanceLevelMasterID { set; get; }

        /// <summary>
        /// Bậc BSE 
        /// <para/> Master.Code =15
        /// </summary>
        [ForeignKey("BseAllowanceLevel")]
        public int? BseAllowanceLevelMasterDetailID { set; get; }
        public int? IsChangeBseAllowanceLevel { set; get; }
        /// <summary>
        /// Trường tốt nghiệp sau cùng
        /// <para/> Master.Code =17
        /// </summary>
        [ForeignKey("Collect")]
        public int? CollectMasterID { set; get; }

        /// <summary>
        /// Code các trường tốt nghiệp sau cùng
        /// <para/> Master.Code =17
        /// </summary>
        [ForeignKey("Collect")]
        public int? CollectMasterDetailID { set; get; }
        public int? IsChangeCollect { set; get; }
        /// <summary>
        /// Bậc tốt nghiệp ( cao đẳng / đại học / cao học )
        /// <para/> Master.Code = 19 
        /// </summary>
        [ForeignKey("EducationLevel")]
        public int? EducationLevelMasterID { set; get; }

        /// <summary>
        /// mã các bậc tốt nghiệp ( cao đẳng / đại học / cao học )
        /// <para/> Master.Code = 19 
        /// </summary>
        [ForeignKey("EducationLevel")]
        public int? EducationLevelMasterDetailID { set; get; }
        public int? IsChangeEducationLevel { set; get; }
        /// <summary>
        /// Loại hợp đồng 
        /// <para/> Master.Code =17
        /// </summary>
        [ForeignKey("ContractType")]
        public int? ContractTypeMasterID { set; get; }

        /// <summary>
        /// Code tương ứng của loại hợp đồng 
        /// <para/> Master.Code =17
        /// </summary>
        [ForeignKey("ContractType")]
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

        [ForeignKey("EmpID")]
        public virtual Emp Emp { set; get; }

        [ForeignKey("CompanyID")]
        public virtual Dept Company { set; get; }

        [ForeignKey("DeptID")]
        public virtual Dept Dept { set; get; }

        [ForeignKey("TeamID")]
        public virtual Team Team { set; get; }

        [ForeignKey("PositionID")]
        public virtual Position Position { set; get; }

        public virtual MasterDetail ContractType { set; get; }

        public virtual MasterDetail WorkEmpType { set; get; }

        public virtual MasterDetail EmpType { set; get; }

        public virtual MasterDetail JapaneseLevel { set; get; }

        public virtual MasterDetail BusinessAllowanceLevel { set; get; }

        public virtual MasterDetail RoomWithInternetAllowanceLevel { set; get; }

        public virtual MasterDetail RoomNoInternetAllowanceLevel { set; get; }

        public virtual MasterDetail BseAllowanceLevel { set; get; }

        public virtual MasterDetail Collect { set; get; }

        public virtual MasterDetail EducationLevel { set; get; }

    }
}

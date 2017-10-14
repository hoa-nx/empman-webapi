using EmpMan.Common.ViewModels.Models.Common;
using EmpMan.Common.ViewModels.Models.File;
using EmpMan.Common.ViewModels.Models.Master;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EmpMan.Common.ViewModels.Models.Emp
{
    public class EmpViewModel : AuditableViewModel
    {

        public int No { set; get; }
        /// <summary>
        /// Khóa chính tự sinh
        /// </summary>
        public int ID { set; get; }

        /// <summary>
        /// Full name 
        /// </summary>
        [Required(ErrorMessage = "Tên đầy đủ nhân viên bắt buộc nhập")]
        [MaxLength(256)]
        public string FullName { set; get; }

        /// <summary>
        /// Tên nhân viên
        /// </summary>
        [MaxLength(256)]
        public string Name { set; get; }

        /// <summary>
        /// Phiên âm tiếng Nhật
        /// </summary>
        [MaxLength(256)]
        public string Furigana { set; get; }

        /// <summary>
        /// Giới tính
        /// </summary>
        public bool? Gender { get; set; }

        /// <summary>
        /// Số CMND / Passport No
        /// </summary>
        [MaxLength(256)]
        public string IdentNo { set; get; }

        /// <summary>
        /// Ngày cấp CM
        /// </summary>
        public DateTime? IdentIssueDate { set; get; }

        /// <summary>
        /// Nơi cấp CMND
        /// </summary>
        public string IdentIssuePlace { set; get; }

        /// <summary>
        /// MST ca nhan
        /// </summary>
        [MaxLength(256)]
        public string TaxCode { set; get; }

        /// <summary>
        /// Ngày cấp MST
        /// </summary>
        public DateTime? TaxCodeIssueDate { set; get; }

        /// <summary>
        /// Số quản lý trong công ty ( liên kết system ngoại)
        /// </summary>
        [MaxLength(256)]
        public string ExtLinkNo { set; get; }

        /// <summary>
        /// Số hồ sơ của phòng training trong công ty ( liên kết system ngoại)
        /// </summary>
        [MaxLength(256)]
        public string TrainingProfileNo { set; get; }

        /// <summary>
        /// Quên quán
        /// </summary>
        [MaxLength(256)]
        public string BornPlace { set; get; }

        /// <summary>
        /// Hình ảnh nhân viên (Path)
        /// </summary>
        [MaxLength(512)]
        public string Avatar { set; get; }

        /// <summary>
        /// Sử dụng cho mục đích control hiển thị hình ảnh ở client sử dụng ng2-file-drop
        /// </summary>
        public bool ShowAvatar { set; get; }

        /// <summary>
        /// Địa chỉ mail công ty 
        /// </summary>
        [MaxLength(256)]
        public string WorkingEmail { set; get; }

        /// <summary>
        /// Địa chỉ mail cá nhân
        /// </summary>
        [MaxLength(256)]
        public string PersonalEmail { set; get; }

        /// <summary>
        /// Ngày sinh
        /// </summary>
        public DateTime? BirthDay { set; get; }

        /// <summary>
        /// Account trong công ty
        /// </summary>
        [MaxLength(64)]
        public string AccountName { set; get; }

        /// <summary>
        /// Điện thoại
        /// </summary>
        [MaxLength(64)]
        public string PhoneNumber1 { set; get; }

        /// <summary>
        /// Điện thoại
        /// </summary>
        [MaxLength(64)]
        public string PhoneNumber2 { set; get; }

        /// <summary>
        /// Điện thoại
        /// </summary>
        [MaxLength(64)]
        public string PhoneNumber3 { set; get; }

        /// <summary>
        /// Địa chỉ
        /// </summary>
        [MaxLength(1024)]
        public string Address1 { set; get; }

        /// <summary>
        /// Địa chỉ
        /// </summary>
        [MaxLength(1024)]
        public string Address2 { set; get; }

        /// <summary>
        /// Công ty làm việc hiện tại (Liên kết với bảng company)
        /// </summary>
        public int? CurrentCompanyID { set; get; }

        /// <summary>
        /// Dept làm việc hiện tại (Liên kết với bảng dept)
        /// </summary>
        public int? CurrentDeptID { set; get; }

        /// <summary>
        /// Team hiện tại (Liên kết với bảng Team)
        /// </summary>
        public int? CurrentTeamID { set; get; }


        /// <summary>
        /// Chức vụ hiện tại (Liên kết với bảng Position)
        /// </summary>
        public int? CurrentPositionID { set; get; }

        /// <summary>
        /// Ngày phỏng vấn
        /// </summary>
        public DateTime? InterviewDate { set; get; }

        /// <summary>
        /// Người phỏng vấn
        /// </summary>
        public string InterviewEmp { set; get; }

        /// <summary>
        /// Ngày nói chuyện DKLV
        /// </summary>
        public DateTime? WorkingConditionTalkDate { set; get; }

        /// <summary>
        /// Ngày bắt đầu thực tập
        /// </summary>
        public DateTime? StartIntershipDate { set; get; }

        /// <summary>
        /// Ngày kết thúc thực tập
        /// </summary>
        public DateTime? EndIntershipDate { set; get; }

        /// <summary>
        /// Ngày vào công ty
        /// </summary>
        public DateTime? StartWorkingDate { set; get; }

        /// <summary>
        /// Ngày bắt đầu học việc
        /// </summary>
        public DateTime? StartLearningDate { set; get; }

        /// <summary>
        /// Ngày kết thúc học việc
        /// </summary>
        public DateTime? EndLearningDate { set; get; }

        /// <summary>
        /// Ngày bắt đầu thử việc
        /// </summary>
        public DateTime? StartTrialDate { set; get; }

        /// <summary>
        /// Ngày kết thúc thử việc
        /// </summary>
        public DateTime? EndTrialDate { set; get; }

        /// <summary>
        /// Kết quả thử việc
        /// </summary>
        public string TrialResult { set; get; }

        /// <summary>
        /// Ngày ký hợp đồng lao động chính thức lần đầu
        /// </summary>
        public DateTime? ContractDate { set; get; }

        /// <summary>
        /// Ngày bắt đầu nghỉ thai sản
        /// </summary>
        public DateTime? BabyBornStartDate { set; get; }

        /// <summary>
        /// Ngày kết thúc nghỉ thai sản (dự định)
        /// </summary>
        public DateTime? BabyBornScheduleEndDate { set; get; }

        /// <summary>
        /// Ngày kết thúc nghỉ thai sản thực tế
        /// </summary>
        public DateTime? BabyBornActualEndDate { set; get; }

        /// <summary>
        /// Ngày bắt đầu nghỉ thai sản
        /// </summary>
        public DateTime? BabyBornStartDate2 { set; get; }

        /// <summary>
        /// Ngày kết thúc nghỉ thai sản (dự định)
        /// </summary>
        public DateTime? BabyBornScheduleEndDate2 { set; get; }

        /// <summary>
        /// Ngày kết thúc nghỉ thai sản thực tế
        /// </summary>
        public DateTime? BabyBornActualEndDate2 { set; get; }

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
        /// Ngày gửi gửi đơn xin nghỉ (hoặc thông báo miệng)
        /// </summary>
        public DateTime? JobLeaveRequestDate { set; get; }

        /// <summary>
        /// Ngày nghỉ việc (Ngày làm sau cùng)
        /// </summary>
        public DateTime? JobLeaveDate { set; get; }

        /// <summary>
        /// Đã nghỉ việc
        /// </summary>
        public bool? IsJobLeave { set; get; }

        /// <summary>
        /// Lý do nghỉ việc 
        /// </summary>
        public string JobLeaveReason { set; get; }

        /// <summary>
        /// Google Id
        /// </summary>
        [MaxLength(256)]
        public string GoogleId { set; get; }

        /// <summary>
        /// Ngày lập gia đình
        /// </summary>
        public DateTime? MarriedDate { set; get; }

        /// <summary>
        /// Đã kết hôn
        /// </summary>
        public bool? IsMarried { set; get; }

        /// <summary>
        /// Nội dung công việc trước khi vào công ty
        /// </summary>
        public string ExperienceBeforeContent { set; get; }

        /// <summary>
        /// Kinh nghiệm trước khi vào công ty
        /// </summary>
        public decimal? ExperienceBeforeConvert { set; get; }

        /// <summary>
        /// Qui đổi kinh nghiệm
        /// </summary>
        public decimal? ExperienceConvert { set; get; }

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
        /// Là BSE
        /// </summary>
        public bool? IsBSE { set; get; }

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
        /// Sơ lược tính tình nhân viên
        /// </summary>

        public string Temperament { set; get; }

        /// <summary>
        /// Người giới thiệu
        /// </summary>
        [MaxLength(256)]
        public string Introductor { set; get; }

        /// <summary>
        /// Nhóm máu
        /// </summary>
        [MaxLength(10)]
        public string BloodGroup { set; get; }

        /// <summary>
        /// Sở thích
        /// </summary>
        [MaxLength(1024)]
        public string Hobby { set; get; }

        /// <summary>
        /// Mục tiêu nghề nghiệp
        /// </summary>
        public string Objective { set; get; }

        /// <summary>
        /// Hình nhân viên ( Liên kết với FileStorage)
        /// </summary>
        public int? FileID { set; get; }

        /// <summary>
        /// Các file khác  ( Liên kết với FileStorage)
        /// </summary>
        public int? ProfileAttachmentID { set; get; }

        public virtual CompanyViewModel Company { set; get; }

        public virtual DeptViewModel Dept { set; get; }
        
        public virtual TeamViewModel Team { set; get; }
        
        public virtual PositionViewModel Position { set; get; }
        
        //public virtual MasterDetailViewModel ContractType { set; get; }

        //public virtual MasterDetailViewModel EmpType { set; get; }

        //public virtual MasterDetailViewModel JapaneseLevel { set; get; }

        //public virtual MasterDetailViewModel BusinessAllowanceLevel { set; get; }

        //public virtual MasterDetailViewModel RoomWithInternetAllowanceLevel { set; get; }

        //public virtual MasterDetailViewModel RoomNoInternetAllowanceLevel { set; get; }

        //public virtual MasterDetailViewModel BseAllowanceLevel { set; get; }

        //public virtual MasterDetailViewModel Collect { set; get; }

        //public virtual MasterDetailViewModel EducationLevel { set; get; }

        //public virtual FileStorageViewModel File { set; get; }

        //public virtual FileStorageViewModel ProfileAttachment { set; get; }

        //public virtual IEnumerable<EmpProfileViewModel> EmpProfiles { get; set; }
        //public virtual IEnumerable<EmpProfileTechViewModel> EmpProfileTechs{ get; set; }
        //public virtual IEnumerable<EmpProfileWorkViewModel> EmpProfileWorks { get; set; }
        //public virtual IEnumerable<EmpContractViewModel> EmpContracts { get; set; }
        //public virtual IEnumerable<EmpSalaryViewModel> EmpSalary { get; set; }
        //public virtual IEnumerable<EmpAllowanceViewModel> EmpAllowances { get; set; }
        //public virtual IEnumerable<EmpDetailWorkViewModel> EmpDetailWorks { get; set; }
        //public virtual IEnumerable<EmpOnsiteViewModel> EmpOnsites { get; set; }
        //public virtual IEnumerable<EmpSupportViewModel> EmpSupport { get; set; }

        /* List các item không tồn tại trong Model */
        public string CompanyName { set; get; }
        public string DeptName { set; get; }
        public string TeamName { set; get; }
        public string PositionName { set; get; }
        public string JapaneseLevelName { set; get; }
        public string BusinessAllowanceName { set; get; }
        public string BseAllowanceLevelName { set; get; }
        public string RoomNoInternetAllowanceLevelName { set; get; }
        public string RoomWithInternetAllowanceLevelName { set; get; }

        public string ContracTypeName { set; get; }
        public string EmpTypeName { set; get; }
        public string CollectName { set; get; }
        public string EducationLevelName { set; get; }
        public int? KeikenFromStartWorkingMonths { set; get; }
        public int? KeikenFromContractMonths { set; get; }

        public int? Age { set; get; }
        public float? AgeFFull{ set; get; }
        public int? IsBirthDay { set; get; }
        public int? ContractedCount { set; get; }
        public int? TrialCount { set; get; }
        public int? ContractedLTNMonthCount { set; get; }
        public int? OtherCount { set; get; }
        public int? TransCount { set; get; }
        public int? OnsiteCount { set; get; }
        public int? ContractedJobLeavedCount { set; get; }
        public int? TrialJobLeavedCount { set; get; }
        public int? TrialInProcessingYearCount { set; get; }
        public int? ExpMonth{ set; get; }
        public int? TrialJobLeavedInProcessingYearCount { set; get; }
        public int? ContractedJobLeavedInProcessingYearCount { set; get; }
        public int? ProcessingYear { set; get; }
        public string KeikenFromContractYearBunrui { set; get; }
        public int? TotalRecords { set; get; }
        
        public string NewPositionName { set; get; }

        //public int? WorkEmpType { set; get; }
        //public DateTime? YMD { set; get; }
    }
}
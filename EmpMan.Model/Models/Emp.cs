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
    /// Lưu thông tin nhân viên (thông tin cơ bản)
    /// </summary>
    [Table("Emps")]
    public class Emp : Auditable
    {

        /// <summary>
        /// Số tự sinh
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int No { set; get; }
    
        /// <summary>
        /// Khóa chính bảng nhân viên
        /// </summary>
        [Key]
        public int ID { set; get; }

        /// <summary>
        /// Full name 
        /// </summary>
        [Required]
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
        [ForeignKey("ContractType")]
        public int? ContractTypeMasterID { set; get; }

        /// <summary>
        /// Code tương ứng của loại hợp đồng 
        /// <para/> Master.Code =17
        /// </summary>
        [ForeignKey("ContractType")]
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
        [ForeignKey("EmpType")]
        public int? EmpTypeMasterID { set; get; }

        /// <summary>
        /// Loại nhân viên (dev/PD///)
        /// <para/> Master.Code =18
        /// </summary>
        [ForeignKey("EmpType")]
        public int? EmpTypeMasterDetailID { set; get; }

        /// <summary>
        /// Là BSE
        /// </summary>
        public bool? IsBSE { set; get; }

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

        [ForeignKey("CurrentCompanyID")]
        public virtual Company Company { set; get; }

        [ForeignKey("CurrentDeptID")]
        public virtual Dept Dept { set; get; }

        [ForeignKey("CurrentTeamID")]
        public virtual Team Team { set; get; }

        [ForeignKey("CurrentPositionID")]
        public virtual Position Position { set; get; }
                
        public virtual MasterDetail ContractType { set; get; }

        public virtual MasterDetail EmpType { set; get; }

        public virtual MasterDetail JapaneseLevel { set; get; }

        public virtual MasterDetail BusinessAllowanceLevel { set; get; }

        public virtual MasterDetail RoomWithInternetAllowanceLevel { set; get; }

        public virtual MasterDetail RoomNoInternetAllowanceLevel { set; get; }

        public virtual MasterDetail BseAllowanceLevel { set; get; }

        public virtual MasterDetail Collect { set; get; }

        public virtual MasterDetail EducationLevel { set; get; }

        [ForeignKey("FileID")]
        public virtual FileStorage File { set; get; }

        [ForeignKey("ProfileAttachmentID")]
        public virtual FileStorage ProfileAttachment { set; get; }


        public virtual ICollection<EmpAllowance> EmpAllowances { get; set; }

        public virtual ICollection<EmpContract> EmpContracts { get; set; }

        public virtual ICollection<EmpDetailWork> EmpDetailWorks { get; set; }

        public virtual ICollection<EmpOnsite> EmpOnsites { get; set; }

        public virtual ICollection<EmpProfile> EmpProfiles { get; set; }

        public virtual ICollection<EmpProfileTech> EmpProfileTechs { get; set; }

        public virtual ICollection<EmpProfileWork> EmpProfileWorks { get; set; }

        public virtual ICollection<EmpSalary> EmpSalarys { get; set; }

        public virtual ICollection<EmpSupport> EmpSupports { get; set; }

        /* List các item không tồn tại trong Model */
        [NotMapped]
        public string CompanyName { set; get; }
        [NotMapped]
        public string DeptName { set; get; }
        [NotMapped]
        public string TeamName { set; get; }
        [NotMapped]
        public string PositionName { set; get; }
        [NotMapped]
        public string JapaneseLevellName { set; get; }
        [NotMapped]
        public string BusinessAllowanceName { set; get; }
        [NotMapped]
        public string BseAllowanceLevelName { set; get; }
        [NotMapped]
        public string RoomNoInternetAllowanceLevelName { set; get; }
        [NotMapped]
        public string RoomWithInternetAllowanceLevelName { set; get; }
        [NotMapped]
        public string ContracTypeName { set; get; }
        [NotMapped]
        public string EmpTypeName { set; get; }
        [NotMapped]
        public string CollectName { set; get; }
        [NotMapped]
        public string EducationLevelName { set; get; }
        [NotMapped]
        public int? KeikenFromStartWorkingMonths { set; get; }
        [NotMapped]
        public int? KeikenFromContractMonths { set; get; }
        [NotMapped]
        public int? Age { set; get; }
        [NotMapped]
        public float? AgeFFull { set; get; }
        [NotMapped]
        public int? IsBirthDay { set; get; }
        [NotMapped]
        public int? ContractedCount { set; get; }
        [NotMapped]
        public int? TrialCount { set; get; }
        [NotMapped]
        public int? ContractedLTNMonthCount { set; get; }
        [NotMapped]
        public int? OtherCount { set; get; }
        [NotMapped]
        public int? TransCount { set; get; }
        [NotMapped]
        public int? OnsiteCount { set; get; }
        [NotMapped]
        public int? ContractedJobLeavedCount { set; get; }
        [NotMapped]
        public int? TrialJobLeavedCount { set; get; }
        [NotMapped]
        public int? ExpMonth { set; get; }

    }
}

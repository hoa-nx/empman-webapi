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
    /// Lưu thông tuyển dụng
    /// </summary>
    [Table("RecruitmentStaffs")]
    public class RecruitmentStaff : Auditable
    {
        /// <summary>
        /// Khóa tự sinh
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int ID{ set; get; }

        /// <summary>
        /// Mã lần tuyển dụng
        /// </summary>

        [MaxLength(128)]
        public string RecruitmentID { set; get; }

        /// <summary>
        /// ID của ứng viên ( mã ứng tuyển + number từ 1 )
        /// </summary>

        [MaxLength(128)]
        public string RecruitmentStaffID { set; get; }

        /// <summary>
        /// Tên lần tuyển dụng
        /// </summary>
        [Required]
        [MaxLength(256)]
        public string Name { set; get; }

        /// <summary>
        /// Tên tắt 
        /// </summary>
        [MaxLength(256)]
        public string ShortName { set; get; }

        /// <summary>
        /// Loại tuyển dụng ( LTV chính thức , học việc, tổng vụ ..)
        /// MasterDetail(30)
        /// </summary>
        public int? RecruitmentTypeMasterID { set; get; }
        /// <summary>
        /// Loại tuyển dụng ( LTV chính thức , học việc, tổng vụ ..)
        /// MasterDetail(30)
        /// </summary>
        public int? RecruitmentTypeMasterDetailID { set; get; }

        /// <summary>
        /// Ngày Mời Vào Công Ty
        /// </summary>
        public DateTime? RequestInCompanyDate { set; get; }
        
        /// <summary>
        /// Kết Quả Phỏng Vấn
        /// </summary>
        public string InterviewResult { set; get; }

        /// <summary>
        /// Ngày Mời Phỏng Vấn
        /// </summary>
        public DateTime? RequestInterviewDate { set; get; }
        /// <summary>
        /// Thời Gian Phỏng Vấn
        /// </summary>
        public DateTime? InterViewTime { set; get; }

        /// <summary>
        /// Thời Gian Test Vòng 1
        /// </summary>
        public string ExamRound1 { set; get; }

        /// <summary>
        /// Quyết Định
        /// </summary>
        public string ExamResult { set; get; }

        /// <summary>
        /// ID công ty
        /// </summary>
        public int? CompanyCvNo { set; get; }

        /// <summary>
        /// Khóa
        /// </summary>
        public int? Pharse { set; get; }

        /// <summary>
        /// Họ và tên
        /// </summary>
        public string FullName { set; get; }

        /// <summary>
        /// Ngày sinh
        /// </summary>
        public DateTime? BirthDay { set; get; }

        /// <summary>
        /// Giới tính
        /// </summary>
        public bool? Gender { set; get; }

        /// <summary>
        /// Quốc tịch
        /// </summary>
        public string National { set; get; }

        /// <summary>
        /// CMND
        /// </summary>
        public string IdentNo { set; get; }

        /// <summary>
        /// Điện thoại
        /// </summary>
        public string PhoneNumber { set; get; }
        /// <summary>
        /// Email
        /// </summary>
        public string Email { set; get; }
        /// <summary>
        /// Mức lương mong muốn
        /// </summary>
        public decimal? KiboSalary { set; get; }

        /// <summary>
        /// Trình Độ 1
        /// </summary>
        public string EducationLevel { set; get; }

        /// <summary>
        /// Tên Trường 1
        /// </summary>
        public string CollectName { set; get; }


        /// <summary>
        /// Chuyên Ngành 1
        /// </summary>
        public string ProfessionalKbn { set; get; }


        /// <summary>
        /// Hệ đào tạo 1
        /// </summary>
        public string EducationType { set; get; }

        /// <summary>
        /// Xếp loại 1
        /// </summary>
        public string Grade { set; get; }

        /// <summary>
        /// Bằng cấp 1
        /// </summary>
        public bool? IsCertificated { set; get; }

        /// <summary>
        /// Số môn học còn nợ
        /// </summary>
        public int? DebtSubjectCount { set; get; }

        /// <summary>
        /// Lý do nợ bằng
        /// </summary>
        public string DebtSubjectReason { set; get; }

        /// <summary>
        /// Thời gian dự định có bằng
        /// </summary>
        public string CertificatedDateTime { set; get; }

        /// <summary>
        /// Chứng chỉ khác
        /// </summary>
        public string OtherCertificated { set; get; }

        /// <summary>
        /// Tiếng Nhật
        /// </summary>
        public string JapaneseLevel { set; get; }
        /// <summary>
        /// Tiếng Anh
        /// </summary>
        public string EnglishLevel { set; get; }
        /// <summary>
        /// Kỹ năng khác
        /// </summary>
        public string OtherSkill { set; get; }
        /// <summary>
        /// Hôn nhân
        /// </summary>
        public string MarriedStatus { set; get; }
        /// <summary>
        /// Mục tiêu nghề nghiệp
        /// </summary>
        public string Objective { set; get; }
        /// <summary>
        /// Ghi chú
        /// </summary>
        public string CvNote { set; get; }
        /// <summary>
        /// Nhận xét
        /// </summary>
        public string Comment1 { set; get; }
        /// <summary>
        /// Đánh giá
        /// </summary>
        public string Comment2 { set; get; }
        /// <summary>
        /// Ngày tạo
        /// </summary>
        public DateTime? CvCreateDate { set; get; }
        
        /// <summary>
        /// Ngày chỉnh sửa
        /// </summary>
        public DateTime? CvUpdateDate { set; get; }

        /// <summary>
        /// Số Hồ Sơ Đã Nộp
        /// </summary>
        public int? CvSendCount { set; get; }

        /// <summary>
        /// Danh Sách Đợt Đã Nộp
        /// </summary>
        public string CvSendList { set; get; }

        /// <summary>
        /// Ngày bắt đầu đi làm
        /// </summary>
        public DateTime? StartWorkingDate { set; get; }

        /// <summary>
        /// Nơi ở hiện nay
        /// </summary>
        public string AdddressPlace { set; get; }

        /// <summary>
        /// Nguyên Quán
        /// </summary>
        public string BornPlace { set; get; }
        /// <summary>
        /// Sở thích
        /// </summary>
        public string Hobby { set; get; }

        /// <summary>
        /// Miễn thi vòng 1
        /// </summary>
        public string IsTestRound1ByPass { set; get; }

        /// <summary>
        /// Điểm chung vòng 1
        /// </summary>
        public decimal? GradeTestRound1 { set; get; }
        /// <summary>
        /// Điểm Anh Văn vòng 1
        /// </summary>
        public decimal? EngGradeTestRound1 { set; get; }
        /// <summary>
        /// Điểm chuyên môn vòng 1
        /// </summary>
        public decimal? ProfessionalKbnGradeTestRound1 { set; get; }
        /// <summary>
        /// Điểm vòng 2
        /// </summary>
        public decimal? GradeTestRound2 { set; get; }

        /// <summary>
        /// Trạng thái
        /// </summary>
        public string CvStatus { set; get; }
        /// <summary>
        /// Loại Nhân Viên
        /// </summary>
        public string EmpType { set; get; }

        /// <summary>
        /// Ngày Mời Phổ Biến Huấn Luyện
        /// </summary>
        public DateTime? TrainingClassConditionTalkDate { set; get; }
        /// <summary>
        /// Ngày Mời Nói Chuyện Điều Kiện Làm Việc
        /// </summary>
        public DateTime? WorkingConditionTalkDate { set; get; }

        /// <summary>
        /// Hình ứng viên
        /// </summary>
        public string Avatar { set; get; }
        /// <summary>
        /// Đã gửi tin nhắn cho ứng viên
        /// </summary>
        public bool? IsSendSMS { set; get; }
        /// <summary>
        /// Số lần gửi
        /// </summary>
        public int? SMSCount { set; get; }

        /// <summary>
        /// Nội dung gửi
        /// </summary>
        public string SMSContent { set; get; }

        /// <summary>
        /// Giới thiệu qua huấn luyện
        /// </summary>
        public bool? IsTrainingIntroduction { set; get; }

        /// <summary>
        /// Bộ phận nhận 
        /// </summary>
        public int? DeptReceived { set; get; }
        /// <summary>
        /// Team nhận
        /// </summary>
        public int? TeamReceived { set; get; }

        /// <summary>
        /// Ngày dự định vào công ty làm
        /// </summary>
        public DateTime? TrialStartDate { set; get; }
        /// <summary>
        /// Người support
        /// </summary>
        public int? SupportEmpID { set; get; }
        /// <summary>
        /// Bộ ghost nào
        /// </summary>
        [MaxLength(256)]
        public string GhostPC { set; get; }

        /// <summary>
        /// Ngày gửi mail thông báo cho bộ IT về việc nhân người
        /// </summary>
        public DateTime? ItMailNotificationDate { set; get; }
        /// <summary>
        /// Ngày gửi mail cho bộ phận nhân sự  về việc nhận người mới
        /// </summary>
        public DateTime? ResourceDeptMailNotificationDate { set; get; }

        /// <summary>
        /// Mã nhân viên chính thức trong hệ thống
        /// </summary>
        public int? SystemEmpID { set; get; }

        /// <summary>
        /// File đối tượng
        /// </summary>
        public int? FileID { set; get; }

        /// <summary>
        /// Phòng để phỏng vấn
        /// </summary>
        public string InterviewRoom { set; get; }

        /// <summary>
        /// Ngày giờ phỏng vấn
        /// </summary>
        public DateTime? InterviewDate { set; get; }
        /// <summary>
        /// Đánh giá
        /// </summary>
        public string InterviewComment { set; get; }

        /// <summary>
        /// Đăng ký pv
        /// </summary>
        public bool? IsRegister { set; get; }

        /// <summary>
        /// Kết thúc phỏng vấn
        /// </summary>
        public bool? IsFinished { set; get; }



        /// <summary>
        /// Số người đăng ký pv staff
        /// </summary>
        [NotMapped]
        public int? Cnt { set; get; }
        /// <summary>
        /// Trang thai approved cdoi voi nguoi dang ky
        /// </summary>
        [NotMapped]
        public int? ApprovedStatusInterview { set; get; }

        [ForeignKey("SystemEmpID")]
        public virtual Emp SystemEmp { set; get; }

        [ForeignKey("DeptReceived")]
        public virtual Dept Dept { set; get; }

        [ForeignKey("TeamReceived")]
        public virtual Team Team { set; get; }

        [ForeignKey("SupportEmpID")]
        public virtual Emp SupportEmp { set; get; }

        [ForeignKey("FileID")]
        public virtual FileStorage File { set; get; }

    }
}

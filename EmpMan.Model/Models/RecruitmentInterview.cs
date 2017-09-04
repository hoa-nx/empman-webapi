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
    /// Lưu Thông tin phỏng vấn
    /// </summary>
    [Table("RecruitmentInterviews")]
    public class RecruitmentInterview : Auditable
    {
        /// <summary>
        /// Khóa tự sinh
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int ID { set; get; }

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
        [MaxLength(256)]
        public string Name { set; get; }

        /// <summary>
        /// Tên tắt 
        /// </summary>
        [MaxLength(256)]
        public string ShortName { set; get; }

        /// <summary>
        /// Người đăng ký phỏng vấn
        /// </summary>
        public int? RegInterviewEmpID { set; get; }

        /// <summary>
        /// Có tham gia phỏng vấn
        /// </summary>
        public bool? IsInterviewed { set; get; }
        /// <summary>
        /// Hủy phỏng vấn do ứng viên không tới hoặc lý do khác
        /// </summary>
        public bool? IsStaffCancel { set; get; }

        /// <summary>
        /// Ngày giờ phỏng vấn
        /// </summary>
        public DateTime? ScheduleInterviewDate { set; get; }

        /// <summary>
        /// Ngày giờ phỏng vấn thực tế
        /// </summary>
        public DateTime? ActualInterviewDate { set; get; }

        /// <summary>
        /// Phòng dự định phỏng vấn
        /// </summary>
        public string ScheduleInterviewRoom { set; get; }
        /// <summary>
        /// Phòng thực tế phỏng vấn
        /// </summary>
        public string ActualInterviewRoom { set; get; }

        /// <summary>
        /// Nội dung phỏng vấn ( khóa ngoại tới table khác ??)
        /// </summary>
        public string InterviewContent { set; get; }
        /// <summary>
        /// Ghi chú về kết quả phỏng vấn ( giải thích thêm)
        /// </summary>
        public string InterviewComment { set; get; }
        /// <summary>
        /// Kết quả của người phỏng vấn
        /// </summary>
        public string InterviewResult { set; get; }

        /// <summary>
        /// kết thúc , không phỏng vấn ứng viên này nữa
        /// </summary>
        public bool? IsFinished { set; get; }

        /// <summary>
        /// Ngày báo cáo kết quả phỏng vấn
        /// </summary>
        public DateTime? ReportDate { set; get; }

        /// <summary>
        /// Giới thiệu qua huấn luyện
        /// </summary>
        public bool? IsTrainingIntroduction { set; get; }

        /// <summary>
        /// Đã gửi SMS thông báo cho người đăng ký phỏng vấn ( trường hợp cần reset thì sẽ reset lại trị )
        /// </summary>
        public bool? IsSendSMS { set; get; }

        /// <summary>
        /// Số lần gửi SMS
        /// </summary>
        public int? SMSCount { set; get; }

        /// <summary>
        /// Nội dung gửi tại các lần gửi
        /// </summary>
        public string SMSContent { set; get; }

        /// <summary>
        /// File đối tượng
        /// </summary>
        public int? FileID { set; get; }

        /// <summary>
        /// File đối tượng
        /// </summary>
        public int? RecruitmentStaffNo { set; get; }

        [ForeignKey("RegInterviewEmpID")]
        public virtual Emp RegInterviewEmp { set; get; }

        [ForeignKey("RecruitmentStaffNo")]
        public virtual RecruitmentStaff RecruitmentStaff{ set; get; }

        [ForeignKey("FileID")]
        public virtual FileStorage File{ set; get; }
    }
}

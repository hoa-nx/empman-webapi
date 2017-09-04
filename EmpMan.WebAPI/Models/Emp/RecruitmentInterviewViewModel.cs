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
    public class RecruitmentInterviewViewModel : AuditableViewModel
    {
        /// <summary>
        /// Khóa tự sinh
        /// </summary>
        public int ID { set; get; }

        /// <summary>
        /// Mã lần tuyển dụng
        /// </summary>
        public string RecruitmentID { set; get; }

        /// <summary>
        /// ID của ứng viên ( mã ứng tuyển + number từ 1 )
        /// </summary>
        
        public string RecruitmentStaffID { set; get; }

        /// <summary>
        /// Tên lần tuyển dụng
        /// </summary>

        public string Name { set; get; }

        /// <summary>
        /// Tên tắt 
        /// </summary>
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

        public virtual EmpViewModel RegInterviewEmp { set; get; }

        public virtual RecruitmentStaffViewModel  RecruitmentStaff { set; get; }

        public virtual FileStorageViewModel File { set; get; }

        /// <summary>
        /// Tên người phỏng vấn ( cho phù hợp với model của client)
        /// </summary>
        public string Label { set; get; }

        /// <summary>
        /// ID người phỏng vấn ( cho phù hợp với model của client)
        /// </summary>
        public int? Value{ set; get; }

        /// <summary>
        /// email người phỏng vấn
        /// </summary>
        public string WorkingEmail { set; get; }

        /// <summary>
        /// phone người phỏng vấn
        /// </summary>
        public string PhoneNumber1 { set; get; }
    }
}
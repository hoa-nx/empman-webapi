using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpMan.Common.Enums
{
    public enum RecruitmentStaffImportColumnEnum
    {
        /// <summary>
        /// Ngày Mời Vào Công Ty
        /// </summary>
        RequestInCompanyDate = 1,

        /// <summary>
        /// Kết Quả Phỏng Vấn
        /// </summary>
        InterviewResult = 2,

        /// <summary>
        /// Ngày Mời Phỏng Vấn
        /// </summary>
        RequestInterviewDate = 3,
        /// <summary>
        /// Thời Gian Phỏng Vấn
        /// </summary>
        InterViewTime = 4,

        /// <summary>
        /// Thời Gian Test Vòng 1
        /// </summary>
        ExamRound1 = 5,

        /// <summary>
        /// Quyết Định
        /// </summary>
        ExamResult = 6,

        /// <summary>
        /// ID công ty
        /// </summary>
        CompanyCvNo = 7,

        /// <summary>
        /// Khóa
        /// </summary>
        Pharse = 8,

        /// <summary>
        /// Họ và tên
        /// </summary>
        FullName = 9,

        /// <summary>
        /// Ngày sinh
        /// </summary>
        BirthDay = 10,

        /// <summary>
        /// Giới tính
        /// </summary>
        Gender = 11,

        /// <summary>
        /// Quốc tịch
        /// </summary>
        National = 12,

        /// <summary>
        /// CMND
        /// </summary>
        IdentNo = 13,

        /// <summary>
        /// Điện thoại
        /// </summary>
        PhoneNumber = 14,
        /// <summary>
        /// Email
        /// </summary>
        Email = 15,
        /// <summary>
        /// Mức lương mong muốn
        /// </summary>
        KiboSalary = 16,

        /// <summary>
        /// Trình Độ 1
        /// </summary>
        EducationLevel = 17,

        /// <summary>
        /// Tên Trường 1
        /// </summary>
        CollectName = 18,


        /// <summary>
        /// Chuyên Ngành 1
        /// </summary>
        ProfessionalKbn = 19,


        /// <summary>
        /// Hệ đào tạo 1
        /// </summary>
        EducationType = 21,

        /// <summary>
        /// Xếp loại 1
        /// </summary>
        Grade = 22,

        /// <summary>
        /// Bằng cấp 1
        /// </summary>
        IsCertificated = 23,

        /// <summary>
        /// Số môn học còn nợ
        /// </summary>
        DebtSubjectCount = 24 ,

        /// <summary>
        /// Lý do nợ bằng
        /// </summary>
        DebtSubjectReason = 25,

        /// <summary>
        /// Thời gian dự định có bằng
        /// </summary>
        CertificatedDateTime = 26,
        /// <summary>
        /// Chứng chỉ khác
        /// </summary>
        OtherCertificated = 27,
        /// <summary>
        /// Tiếng Nhật
        /// </summary>
        JapaneseLevel = 28,
        /// <summary>
        /// Tiếng Anh
        /// </summary>
        EnglishLevel = 29,
        /// <summary>
        /// Kỹ năng khác
        /// </summary>
        OtherSkill = 30,
        /// <summary>
        /// Hôn nhân
        /// </summary>
        MarriedStatus = 31,
        /// <summary>
        /// Mục tiêu nghề nghiệp
        /// </summary>
        Objective = 32,
        /// <summary>
        /// Ghi chú
        /// </summary>
        CvNote = 33,
        /// <summary>
        /// Nhận xét
        /// </summary>
        Comment1 = 34,
        /// <summary>
        /// Đánh giá
        /// </summary>
        Comment2 = 35,
        /// <summary>
        /// Ngày tạo
        /// </summary>
        CvCreateDate = 36,

        /// <summary>
        /// Ngày chỉnh sửa
        /// </summary>
        CvUpdateDate = 37,

        /// <summary>
        /// Số Hồ Sơ Đã Nộp
        /// </summary>
        CvSendCount = 38,

        /// <summary>
        /// Danh Sách Đợt Đã Nộp
        /// </summary>
        CvSendList = 39,

        /// <summary>
        /// Ngày bắt đầu đi làm
        /// </summary>
        StartWorkingDate = 40,

        /// <summary>
        /// Nơi ở hiện nay
        /// </summary>
        AdddressPlace = 41,

        /// <summary>
        /// Nguyên Quán
        /// </summary>
        BornPlace = 42,
        /// <summary>
        /// Sở thích
        /// </summary>
        Hobby = 43,

        /// <summary>
        /// Miễn thi vòng 1
        /// </summary>
        IsTestRound1ByPass = 44,

        /// <summary>
        /// Điểm chung vòng 1
        /// </summary>
        GradeTestRound1 = 45,
        /// <summary>
        /// Điểm Anh Văn vòng 1
        /// </summary>
        EngGradeTestRound1 = 46,
        /// <summary>
        /// Điểm chuyên môn vòng 1
        /// </summary>
        ProfessionalKbnGradeTestRound1 = 47,
        /// <summary>
        /// Điểm vòng 2
        /// </summary>
        GradeTestRound2 = 48,

        /// <summary>
        /// Trạng thái
        /// </summary>
        CvStatus = 49,
        /// <summary>
        /// Loại Nhân Viên
        /// </summary>
        EmpType = 50,

        /// <summary>
        /// Ngày Mời Phổ Biến Huấn Luyện
        /// </summary>
        TrainingClassConditionTalkDate = 51,
        /// <summary>
        /// Ngày Mời Nói Chuyện Điều Kiện Làm Việc
        /// </summary>
        WorkingConditionTalkDate = 52
    }

    public enum RecruitmentStaffTrainerImportColumnEnum
    {
        /// <summary>
        /// Số thứ tự
        /// </summary>
        No = 1,

        /// <summary>
        /// Họ và tên
        /// </summary>
        FullName = 2,

        /// <summary>
        /// Ngày sinh
        /// </summary>
        BirthDay = 4,

        /// <summary>
        /// Giới tính
        /// </summary>
        Gender = 5,

        /// <summary>
        /// Ngon ngu lap trinh
        /// </summary>
        ProgrammingLanguage = 6,

        /// <summary>
        /// Tên Trường 1
        /// </summary>
        CollectName = 7,

        /// <summary>
        /// Trình Độ 1
        /// </summary>
        EducationLevel = 8,

        /// <summary>
        /// Hệ đào tạo 1
        /// </summary>
        EducationType = 9,

        /// <summary>
        /// Xếp loại 1
        /// </summary>
        Grade = 10,

        /// <summary>
        /// Nhận Xét Project 1
        /// </summary>
        Comment1 = 11,

        /// <summary>
        /// Điểm Project
        /// </summary>
        GradeTestRound1 = 12,

        /// <summary>
        /// Nguyện vọng
        /// </summary>
        Comment2 = 18
        
    }

}

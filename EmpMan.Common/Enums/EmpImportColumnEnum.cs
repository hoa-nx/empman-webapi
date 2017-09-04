using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpMan.Common.Enums
{
    public enum EmpImportColumnEnum
    {
        No =1,
        /// <summary>
        /// Khóa chính tự sinh
        /// </summary>
        ID  =2 ,

        /// <summary>
        /// Full name 
        /// </summary>
        FullName = 3 ,

        /// <summary>
        /// Tên nhân viên
        /// </summary>

        Name = 4,

        /// <summary>
        /// Phiên âm tiếng Nhật
        /// </summary>

        Furigana= 5 ,

        /// <summary>
        /// Giới tính
        /// </summary>
        Gender = 6 ,

        /// <summary>
        /// Số CMND / Passport No

        IdentNo= 7 ,

        /// <summary>
        /// Ngày cấp CM
        /// </summary>
        IdentIssueDate= 8 ,

        /// <summary>
        /// Nơi cấp CMND
        /// </summary>
        IdentIssuePlace= 9 ,

        /// <summary>
        /// MST ca nhan
        /// </summary>

        TaxCode= 10 ,

        /// <summary>
        /// Ngày cấp MST
        /// </summary>
        TaxCodeIssueDate= 11 ,

        /// <summary>
        /// Số quản lý trong công ty ( liên kết system ngoại)
        /// </summary>

        ExtLinkNo= 12 ,

        /// <summary>
        /// Số hồ sơ của phòng training trong công ty ( liên kết system ngoại)
        /// </summary>

        TrainingProfileNo= 13 ,

        /// <summary>
        /// Quên quán
        /// </summary>

        BornPlace= 14 ,

        /// <summary>
        /// Hình ảnh nhân viên (Path)
        /// </summary>

        Avatar= 15,

        /// <summary>
        /// Sử dụng cho mục đích control hiển thị hình ảnh ở client sử dụng ng2-file-drop
        /// </summary>
        ShowAvatar= 16 ,

        /// <summary>
        /// Địa chỉ mail công ty 
        /// </summary>

        WorkingEmail= 17 ,

        /// <summary>
        /// Địa chỉ mail cá nhân
        /// </summary>

        PersonalEmail= 18 ,

        /// <summary>
        /// Ngày sinh
        /// </summary>
        BirthDay= 19 ,

        /// <summary>
        /// Account trong công ty
        /// </summary>
        AccountName= 20 ,

        /// <summary>
        /// Điện thoại
        /// </summary>

        PhoneNumber1= 21 ,

        /// <summary>
        /// Điện thoại
        /// </summary>

        PhoneNumber2= 22 ,

        /// <summary>
        /// Điện thoại
        /// </summary>

        PhoneNumber3= 23 ,

        /// <summary>
        /// Địa chỉ
        /// </summary>

        Address1= 24 ,

        /// <summary>
        /// Địa chỉ
        /// </summary>

        Address2= 25 ,

        /// <summary>
        /// Công ty làm việc hiện tại (Liên kết với bảng company)
        /// </summary>
        CurrentCompanyID= 26 ,

        /// <summary>
        /// Dept làm việc hiện tại (Liên kết với bảng dept)
        /// </summary>
        CurrentDeptID= 27 ,

        /// <summary>
        /// Team hiện tại (Liên kết với bảng Team)
        /// </summary>
        CurrentTeamID= 28 ,


        /// <summary>
        /// Chức vụ hiện tại (Liên kết với bảng Position)
        /// </summary>
        CurrentPositionID= 29 ,

        /// <summary>
        /// Ngày bắt đầu thực tập
        /// </summary>
        StartIntershipDate= 30 ,

        /// <summary>
        /// Ngày kết thúc thực tập
        /// </summary>
        EndIntershipDate= 31 ,

        /// <summary>
        /// Ngày vào công ty
        /// </summary>
        StartWorkingDate= 32 ,

        /// <summary>
        /// Ngày bắt đầu học việc
        /// </summary>
        StartLearningDate= 33 ,

        /// <summary>
        /// Ngày kết thúc học việc
        /// </summary>
        EndLearningDate= 34 ,

        /// <summary>
        /// Ngày bắt đầu thử việc
        /// </summary>
        StartTrialDate= 35 ,

        /// <summary>
        /// Ngày kết thúc thử việc
        /// </summary>
        EndTrialDate= 36 ,

        /// <summary>
        /// Ngày ký hợp đồng lao động chính thức lần đầu
        /// </summary>
        ContractDate= 37 ,

        /// <summary>
        /// Loại hợp đồng 
        /// <para/> Master.Code =17
        /// </summary>
        ContractTypeMasterID= 38 ,

        /// <summary>
        /// Code tương ứng của loại hợp đồng 
        /// <para/> Master.Code =17
        /// </summary>
        ContractTypeMasterDetailID= 39 ,

        /// <summary>
        /// Ngày gửi gửi đơn xin nghỉ (hoặc thông báo miệng)
        /// </summary>
        JobLeaveRequestDate= 40 ,

        /// <summary>
        /// Ngày nghỉ việc (Ngày làm sau cùng)
        /// </summary>
        JobLeaveDate= 41 ,

        /// <summary>
        /// Đã nghỉ việc
        /// </summary>
        IsJobLeave= 42 ,

        /// <summary>
        /// Lý do nghỉ việc 
        /// </summary>
        JobLeaveReason= 43 ,

        /// <summary>
        /// Google Id
        /// </summary>

        GoogleId= 44 ,

        /// <summary>
        /// Ngày lập gia đình
        /// </summary>
        MarriedDate= 45 ,

        /// <summary>
        /// Nội dung công việc trước khi vào công ty
        /// </summary>
        ExperienceBeforeContent= 46 ,

        /// <summary>
        /// Kinh nghiệm trước khi vào công ty
        /// </summary>
        ExperienceBeforeConvert= 47 ,

        /// <summary>
        /// Qui đổi kinh nghiệm
        /// </summary>
        ExperienceConvert= 48 ,

        /// <summary>
        /// Loại nhân viên (dev/PD///)
        /// <para/> Master.Code =18
        /// </summary>
        EmpTypeMasterID= 49 ,

        /// <summary>
        /// Loại nhân viên (dev/PD///)
        /// <para/> Master.Code =18
        /// </summary>
        EmpTypeMasterDetailID= 50 ,

        /// <summary>
        /// Là BSE
        /// </summary>
        IsBSE= 51 ,

        /// <summary>
        /// Chứng chỉ tiếng Nhật hiện tại
        /// <para/> Master.Code =10
        /// </summary>
        JapaneseLevelMasterID= 52 ,

        /// <summary>
        /// Chứng chỉ tiếng Nhật hiện tại
        /// <para/> Master.Code =10
        /// </summary>

        JapaneseLevelMasterDetailID= 53 ,

        /// <summary>
        /// Phụ cấp nghiệp vụ hiện tại
        /// <para/> Master.Code =11
        /// </summary>

        BusinessAllowanceLevelMasterID= 54 ,

        /// <summary>
        /// Phụ cấp nghiệp vụ hiện tại
        /// <para/> Master.Code =11
        /// </summary>

        BusinessAllowanceLevelMasterDetailID= 55 ,

        /// <summary>
        /// Phòng chuyên biệt có internet
        /// <para/> Master.Code =12
        /// </summary>

        RoomWithInternetAllowanceLevelMasterID= 56 ,

        /// <summary>
        /// Phòng chuyên biệt có internet
        /// <para/> Master.Code =12
        /// </summary>

        RoomWithInternetAllowanceLevelMasterDetailID= 57 ,


        /// <summary>
        /// Phòng chuyên biệt không internet
        /// <para/> Master.Code =13
        /// </summary>

        RoomNoInternetAllowanceLevelMasterID= 58 ,

        /// <summary>
        /// Phòng chuyên biệt không internet
        /// <para/> Master.Code =13
        /// </summary>

        RoomNoInternetAllowanceLevelMasterDetailID= 59 ,

        /// <summary>
        /// Bậc BSE 
        /// <para/> Master.Code =15
        /// </summary>

        BseAllowanceLevelMasterID= 60 ,

        /// <summary>
        /// Bậc BSE 
        /// <para/> Master.Code =15
        /// </summary>

        BseAllowanceLevelMasterDetailID= 61 ,

        /// <summary>
        /// Trường tốt nghiệp sau cùng
        /// <para/> Master.Code =17
        /// </summary>
        CollectMasterID= 62 ,

        /// <summary>
        /// Code các trường tốt nghiệp sau cùng
        /// <para/> Master.Code =17
        /// </summary>
        CollectMasterDetailID= 63 ,

        /// <summary>
        /// Bậc tốt nghiệp ( cao đẳng / đại học / cao học )
        /// <para/> Master.Code = 19 
        /// </summary>
        EducationLevelMasterID= 64 ,

        /// <summary>
        /// mã các bậc tốt nghiệp ( cao đẳng / đại học / cao học )
        /// <para/> Master.Code = 19 
        /// </summary>
        EducationLevelMasterDetailID= 65 ,

        /// <summary>
        /// Sơ lược tính tình nhân viên
        /// </summary>

        Temperament= 66 ,

        /// <summary>
        /// Người giới thiệu
        /// </summary>
        Introductor= 67 ,

        /// <summary>
        /// Nhóm máu
        /// </summary>
        BloodGroup= 68 ,

        /// <summary>
        /// Sở thích
        /// </summary>
        Hobby= 69 ,

        /// <summary>
        /// Mục tiêu nghề nghiệp
        /// </summary>
        Objective= 70,

        DisplayOrder = 71 ,

        AccountData = 72 , 

        Note = 73 ,

        DeleteFlag =74 ,

        DataStatus =75

    }
}

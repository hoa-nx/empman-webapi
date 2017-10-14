using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpMan.Common
{
    public class CommonConstants
    {
        public const string BASE_API = "http://localhost:5000";
        public const string BASE_WEB = "http://localhost:4200";

        public const string ProductTag = "product";
        public const string PostTag = "post";
        public const string DefaultFooterId = "default";
        public static string SecKeyString = "**_)(&847tdgdbldadhakjdkaSecKeyString$&#&$#**";
        public static string HaltString = "(^_^)";
        public const string SessionCart = "SessionCart";

        public const string HomeTitle = "HomeTitle";
        public const string HomeMetaKeyword = "HomeMetaKeyword";
        public const string HomeMetaDescription = "HomeMetaDescription";

        public const string Administrator = "Administrator";

        public const string AdminUser = "admin";
        public const string DirectoryBoardUser = "DirectoryBoard";
        public const string GeneralManagerUser = "GeneralManager";
        public const string ManagerUser = "Manager";
        public const string ViceManagerUser = "ViceManager";
        public const string LeaderUser = "Leader";
        public const string SubLeaderUser = "SubLeader";
        public const string MemberUser = "Member";
        //SignalR
        public const string AdminChannel = "AdminChannel";
        public const string AnnouncementChannel = "AnnouncementChannel";
        //Ma hoa
        /// <summary>
        /// Chuỗi ký tự dùng để mã hóa mật khẩu
        /// </summary>
        public const string CIPHER_PASS_HALT_STRING = "!$#@(*&^%$|xHA124+Oggy:-)";
        //Ten cac hang muc sort
        public const string SORT_NAME_ID = "Mã nhân viên";
        public const string SORT_NAME_DEPT = "Phòng ban";
        public const string SORT_NAME_TEAM = "Team-nhóm";
        public const string SORT_NAME_POSITION = "Ngạch bậc";
        public const string SORT_NAME_FULLNAME = "Họ và tên";
        public const string SORT_NAME_NAME = "Tên";
        public const string SORT_NAME_ACCOUNT = "Tài khoản";
        public const string SORT_NAME_BIRTHDAY = "Ngày sinh";
        public const string SORT_NAME_START_WORKING_DATE = "Ngày vào công ty";
        public const string SORT_NAME_TRANING_DATE = "Ngày thử việc";
        public const string SORT_NAME_CONTRACT_DATE = "Ngày ký HĐLĐ";
        public const string SORT_NAME_GENDER = "Giới tính";
        public const string SORT_NAME_EMPTYPE = "Loại công việc";
        public const string SORT_NAME_ESTIMATE = "Hệ số đánh giá";
        public const string SORT_NAME_JAPANESE_LEVEL = "Tiếng Nhật";
        //Ten cac cot dung de search
        public const string SEARCH_COL_NAME_ID = "ID";
        public const string SEARCH_COL_NAME_DEPTID = "CurrentDeptID";
        public const string SEARCH_COL_NAME_TEAMID = "CurrentTeamID";
        public const string SEARCH_COL_NAME_POSITIONID = "CurrentPositionID";
        public const string SEARCH_COL_NAME_FULLNAME = "FullName";
        public const string SEARCH_COL_NAME_NAME = "Name";
        public const string SEARCH_COL_NAME_ACCOUNT_NAME = "AccountName";
        public const string SEARCH_COL_NAME_BIRTHDAY = "BirthDay";
        public const string SEARCH_COL_NAME_START_WORKING_DATE = "StartWorkingDate";
        public const string SEARCH_COL_NAME_START_TRIAL_DATE = "StartTrialDate";
        public const string SEARCH_COL_NAME_END_TRIAL_DATE = "EndTrialDate";
        public const string SEARCH_COL_NAME_START_INTERSHIP_DATE = "StartIntershipDate";
        public const string SEARCH_COL_NAME_END_INTERSHIP_DATE = "EndIntershipDate";
        public const string SEARCH_COL_NAME_START_LEARNING_DATE = "StartLearningDate";
        public const string SEARCH_COL_NAME_END_LEARNING_DATE = "EndLearningDate";
        public const string SEARCH_COL_NAME_CONTRACT_DATE = "ContractDate";
        public const string SEARCH_COL_NAME_JOB_LEAVE_DATE = "JobLeaveDate";
        public const string SEARCH_COL_NAME_CONTRACT_TYPE = "ContractTypeMasterDetailID";
        public const string SEARCH_COL_NAME_EMP_TYPE = "EmpTypeMasterDetailID";

        public const string SEARCH_COL_NAME_JAPANESE_LEVEL = "JapaneseLevelMasterDetailID";
        public const string SEARCH_COL_NAME_BUSSINESS_ALLOWANCE_LEVEL = "BusinessAllowanceLevelMasterDetailID";
        public const string SEARCH_COL_NAME_BSE_LEVEL = "BseAllowanceLevelMasterDetailID";

        public const string SEARCH_COL_NAME_ROOM_WITH_INTERNET_LEVEL = "RoomWithInternetAllowanceLevelMasterDetailID";
        public const string SEARCH_COL_NAME_ROOM_NO_INTERNET_LEVEL = "RoomNoInternetAllowanceLevelMasterDetailID";

        public const string SEARCH_COL_NAME_GENDER = "Gender";
        public const string SEARCH_COL_NAME_EXPERIENCE_CONVERT = "ExperienceConvert";

        //thong tin expandable group
        public const string EXP_GROUP_DEPT = "dept";
        public const string EXP_GROUP_TEAM = "team";
        public const string EXP_GROUP_POSITION = "position";
        public const string EXP_GROUP_JAPANESE_LEVEL = "japanese";
        public const string EXP_GROUP_BUSSINESS_ALLOWANCE = "bussinessallowance";
        public const string EXP_GROUP_BSE = "bse";
        public const string EXP_GROUP_KEIKEN = "keiken";
        public const string EXP_GROUP_GENDER = "gender";
        public const string EXP_GROUP_EMP_TYPE = "emptype";
        public const string EXP_GROUP_COLLECT_NAME = "collect";
        public const string EXP_GROUP_JOB_LEAVE_BY_YEARMONTH = "jobleavebyyearmonth";
        public const string EXP_GROUP_CONTRACTED_COUNT = "contractedcount";
        public const string EXP_GROUP_CONTRACTED_JOB_LEAVED_COUNT = "contractedjobleavedcount";
        public const string EXP_GROUP_CONTRACTED_LT_NM_MONTH_COUNT = "contractedltnmonthcount";
        /// <summary>
        /// So nhan vien chinh thuc ngoai tru lap trinh va phien dich
        /// </summary>
        public const string EXP_GROUP_OTHER_COUNT = "othercount";
        /// <summary>
        /// So PD dang lam viec
        /// </summary>
        public const string EXP_GROUP_TRANS_COUNT = "transcount";
        /// <summary>
        /// Tong so nhan vien thu viec nhung khong vao chinh thuc trong nam da setting ( khong tinh PD)
        /// </summary>
        public const string EXP_GROUP_TRIAL_JOB_LEAVED_IN_PROCESSING_YEAR_COUNT = "trialjobleavedinprocessingyearcount";
        /// <summary>
        /// Tong so nhan vien thu viec nhung khong vao chinh thuc ( khong tinh PD)
        /// </summary>
        public const string EXP_GROUP_TRIAL_JOB_LEAVED_COUNT = "trialjobleavedcount";
        public const string EXP_GROUP_CONTRACTED_JOB_LEAVED_IN_PROCESSING_YEAR_COUNT = "contractedjobleavedinprocessingyearcount";
        public const string EXP_GROUP_TRIAL_COUNT = "trialcount";
        public const string EXP_GROUP_TRIAL_IN_PROCESSING_YEAR_COUNT = "trialtntrocessingyearcount";
        /// <summary>
        /// Danh sach nhan vien co ngach khong phu hop voi tham nien
        /// </summary>
        public const string EXP_GROUP_EMP_NOT_SATISFIED_KEIKEN = "empnotsatisfiedkeiken";

    }
}

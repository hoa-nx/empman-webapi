using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpMan.Common.Enums
{
    /// <summary>
    /// Loại job 
    /// </summary>
    public enum JobTypeEnum
    {
        /// <summary>
        /// Thông báo lịch phỏng vấn nhân viên thử việc
        /// </summary>
        DevInterviewDateNotify=1,

        /// <summary>
        /// Thông báo nhân sự chuẩn bị vào thử việc
        /// </summary>
        TrialStaffStartTrialDateNotify=2,

        /// <summary>
        /// Thông báo nhân viên sắp hết hạn thử việc
        /// </summary>
        TrialStaffEndTrialDateNotify=3,

        /// <summary>
        /// Thông báo nhân viên được nhận chính thức
        /// </summary>
        TrialStaffToDevContractDateNotify=4,

        /// <summary>
        /// Thông báo nhân viên sắp nghỉ việc
        /// </summary>
        DevJobLeavedDateNotify = 5,

        /// <summary>
        /// Cam on cong hien cua nhân viên nghỉ việc
        /// </summary>
        ThankDevJobLeavedDateNotify = 6


    }
    /// <summary>
    /// Cach gui thong bao
    /// </summary>
    public enum NotifyMethod
    {
        /// <summary>
        /// Gui tin nhan
        /// </summary>
        SMS = 1,

        /// <summary>
        /// Gui SMS
        /// </summary>
        EMAIL = 2,

        /// <summary>
        /// Gui sms va email
        /// </summary>
        SMSAndEMAIL = 3


    }

}

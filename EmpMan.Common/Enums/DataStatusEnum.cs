using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpMan.Common.Enums
{
    /// <summary>
    /// Trạng thái của data
    /// 
    /// </summary>
    public enum DataStatusEnum
    {
        /** Dữ liệu trạng thái liên quan đến data phỏng vấn **/
        /// <summary>
        /// Chờ đăng ký phỏng vấn
        /// </summary>
        REC_INTERVIEW_RIGISTER_WAITING = 0,

        /// <summary>
        /// Không đăng ký phỏng vấn
        /// </summary>
        REC_INTERVIEW_UNRIGISTER =  10 ,

        /// <summary>
        /// Đăng ký phỏng vấn nhưng chưa có lịch phỏng vấn
        /// </summary>
        REC_INTERVIEW_RIGISTER = 20 ,

        /// <summary>
        /// Chờ phỏng vấn
        /// </summary>
        REC_INTERVIEW_WAITING = 30,
        /// <summary>
        /// Chờ kết quả phỏng vấn
        /// </summary>
        REC_INTERVIEW_RESULT_WAITING  = 40,

        /// <summary>
        /// Chờ nói chuyện DKLC
        /// </summary>
        REC_INTERVIEW_CONDITION_WORKING_TALK_WAITING  = 50,
        /// <summary>
        /// Chờ phản hồi 
        /// </summary>
        REC_INTERVIEW_CONDITION_WORKING_TALK_FEEDBACK = 60,
        /// <summary>
        /// Chờ vào thử việc
        /// </summary>
        REC_INTERVIEW_TRIAL_WAITING = 70,
        /// <summary>
        /// Vào thử việc nhưng chưa tạo EMP
        /// </summary>
        REC_INTERVIEW_TRIAL = 80,
        /// <summary>
        /// Đang thử việc ( đã tạo EMP )
        /// </summary>
        REC_INTERVIEW_TRIAL_EMPID_CREATED = 90,

        /** Dữ liệu trạng thái liên quan đến data XXXX **/


        /** Dữ liệu trạng thái liên quan đến data XXXX **/

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpMan.Common.Enums
{
    /// <summary>
    /// <para/>10 : Chứng chỉ tiếng Nhật
    /// <para/>11 : Các loại phụ cấp nghiệp vụ
    /// <para/>12 : Các loại phụ cấp phòng chuyên biệt--có kết nối internet
    /// <para/>13 : Các loại phụ cấp phòng chuyên biệt--không có kết nối internet
    /// <para/>14 : Danh sách các trường cao đẳng / đại học
    /// <para/>15 : Các loại phụ cấp BSE
    /// <para/>16 : Các loại phụ cấp qui trình
    /// <para/>17 : Loại hợp đồng lao động
    /// <para/>18 : Loại nhân viên công ty ( ví dụ như lập trình viên, phiên dịch , nhân viên qui trình , tổng vụ ...
    /// <para/>19 : Hệ tốt nghiệp cao nhất ( Cđ/ đại học / cao học)
    /// <para/>20 : Loại báo giá dùng trong báo cáo doanh số 
    /// <para/>21 : Loại xét lương : 6 tháng 1 lần / 1 năm 1 lần...
    /// <para/>22 : Loại onsite : intership / 3 tháng / 6 tháng / 1 năm/ 2 năm...
    /// <para/>23 : Đơn vị tính thời gian onsite( ngày/ tuần / tháng / năm)
    /// <para/>24 : Phân loại dự án ( báo giá / KeepLabor....)
    /// </summary>
    public enum CommonDataClassEnum
    {

        /// <summary>
        /// 10 : Chứng chỉ tiếng Nhật
        /// </summary>
        JapaneseLevel = 10,
        /// <summary>
        /// 11 : Các loại phụ cấp nghiệp vụ
        /// </summary>
        BusinessAllowanceLevel = 11,
        /// <summary>
        /// 12 : Các loại phụ cấp phòng chuyên biệt--có kết nối internet
        /// </summary>
        RoomWithInternetAllowanceLevel = 12,
        /// <summary>
        /// 13 : Các loại phụ cấp phòng chuyên biệt--không có kết nối internet
        /// </summary>
        RoomNoInternetAllowanceLevel = 13,
        /// <summary>
        /// 14 : Danh sách các trường cao đẳng / đại học
        /// </summary>
        CollectNameList = 14,
        /// <summary>
        /// 15 : Các loại phụ cấp BSE
        /// </summary>
        BseAllowanceLevel = 15,
        /// <summary>
        /// 16 : Các loại phụ cấp qui trình
        /// </summary>
        QAAllowanceLevel = 16,
        /// <summary>
        /// 17 : Loại hợp đồng lao động
        /// </summary>
        ContractType = 17,
        /// <summary>
        /// 18 : Loại nhân viên công ty ( ví dụ như lập trình viên, 
        /// phiên dịch , nhân viên qui trình , tổng vụ ...
        /// </summary>
        EmpType = 18,
        /// <summary>
        /// 19 : Hệ tốt nghiệp cao nhất ( Cđ/ đại học / cao học)
        /// </summary>
        EducationLevel = 19,
        /// <summary>
        /// 20 : Loại báo giá dùng trong báo cáo doanh số 
        /// </summary>
        EstimateType = 20,
        /// <summary>
        /// 21 : Loại xét lương : 6 tháng 1 lần / 1 năm 1 lần...
        /// </summary>
        SalaryIncreaseType = 21 ,

        /// <summary>
        /// 22 : Loại onsite : intership / 3 tháng / 6 tháng / 1 năm/ 2 năm...
        /// </summary>
        OnsiteType = 22 ,

        /// <summary>
        /// 23 : Đơn vị tính thời gian onsite( ngày/ tuần / tháng / năm)
        /// </summary>
        TimeUnit = 23,

        /// <summary>
        /// Phân loại công việc dự án là báo giá hay là keep labor
        /// </summary>
        ProjectType = 24

    }
}

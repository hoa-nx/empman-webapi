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
    /// Table chứa các thông tin chung của hệ thống.
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
    /// <para/>24 : Phân loại dự án
    /// <para/>25 : Đơn vị tiền tệ tính đơn giá hợp đồng với khách hàng OrderUnit
    /// <para/>26 : Loại support  (thực tập / thử việc ...)
    /// <para/>
    /// </summary>
    [Table("Masters")]
    public class Master : Auditable
    {
        /// <summary>
        /// Khóa chính tự sinh
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int No { set; get; }

        /// <summary>
        /// Mã dữ liệu
        /// </summary>
        [Key]
        public int ID { set; get; }

        /// <summary>
        /// Tên của loại dữ liệu 
        /// <para/> ví dụ : Loại hợp đồng lao động
        /// </summary>
        [Required]
        [MaxLength(256)]
        public string Name { set; get; }

        /// <summary>
        /// Tên viết tắt
        /// </summary>
        public string ShortName { set; get; }

        /// <summary>
        /// Tên hiển thị trên các báo cáo
        /// </summary>
        public string DisplayReportName { set; get; }

        /// <summary>
        /// Thuộc loại dữ liệu phụ cấp?
        /// </summary>
        public bool IsAllowanceType { set; get; }
        /// <summary>
        /// Giá trị bất kì 
        /// </summary>
        public string Value1 { set; get; }

        /// <summary>
        /// Giá trị bất kì 
        /// </summary>
        public string Value2 { set; get; }

        /// <summary>
        /// Giá trị bất kì 
        /// </summary>
        public string Value3 { set; get; }

        /// <summary>
        /// Giá trị bất kì 
        /// </summary>
        public string Value4 { set; get; }

        /// <summary>
        /// Giá trị bất kì 
        /// </summary>
        public string Value5 { set; get; }

        /// <summary>
        /// Giá trị bất kì 
        /// </summary>
        public string Value6 { set; get; }

        /// <summary>
        /// Giá trị bất kì 
        /// </summary>
        public string Value7 { set; get; }

        /// <summary>
        /// Giá trị bất kì 
        /// </summary>
        public string Value8 { set; get; }

        /// <summary>
        /// Giá trị bất kì 
        /// </summary>
        public string Value9 { set; get; }

        /// <summary>
        /// Giá trị bất kì 
        /// </summary>
        public string Value10 { set; get; }

        /// <summary>
        /// Giá trị bất kì 
        /// </summary>
        public string Value11 { set; get; }

        /// <summary>
        /// Giá trị bất kì 
        /// </summary>
        public string Value12 { set; get; }

        /// <summary>
        /// Giá trị bất kì 
        /// </summary>
        public string Value13 { set; get; }

        /// <summary>
        /// Giá trị bất kì 
        /// </summary>
        public string Value14 { set; get; }

        /// <summary>
        /// Giá trị bất kì 
        /// </summary>
        public string Value15 { set; get; }

        /// <summary>
        /// Giá trị bất kì 
        /// </summary>
        public string Value16 { set; get; }

        /// <summary>
        /// Giá trị bất kì 
        /// </summary>
        public string Value17 { set; get; }

        /// <summary>
        /// Giá trị bất kì 
        /// </summary>
        public string Value18 { set; get; }

        /// <summary>
        /// Giá trị bất kì 
        /// </summary>
        public string Value19 { set; get; }

        /// <summary>
        /// Giá trị bất kì 
        /// </summary>
        public string Value20 { set; get; }


        public virtual ICollection<MasterDetail> MasterDetails { set; get; }
    }
}

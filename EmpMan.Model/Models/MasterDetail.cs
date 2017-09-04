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
    /// Table chi tiết chứa các thông tin chung của hệ thống.
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
    /// </summary>
    [Table("MasterDetails")]
    public class MasterDetail : Auditable
    {

        /// <summary>
        /// Khóa chính tự sinh
        /// </summary>
        //[Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { set; get; }
        /// <summary>
        /// Loại dự liệu ( khóa ngoại của bảng CommonData.ID)
        /// <para/> Ví dụ : Chứng chỉ tiếng Nhật thì sẽ có MeiKbn = 10
        /// </summary>
        [Column(Order = 0), Key]
        public int MasterID { set; get; }

        /// <summary>
        /// Mã danh mục con
        /// </summary>
        [Column(Order = 1), Key]
        public int MasterDetailCode { set; get; }

        /// <summary>
        /// Tên
        /// <para/> Ví dụ :
        /// <para/> Ứng với MeiKbn =10 . Code =1 thì có tên là "Chứng chỉ N1"
        /// </summary>
        [Required]
        [MaxLength(256)]
        public string Name { set; get; }

        /// <summary>
        /// Tên gọi tắt
        /// </summary>
        [MaxLength(256)]
        public string ShortName { set; get; }

        /// <summary>
        /// Tên hiển thị trên báo cáo
        /// </summary>
        [MaxLength(512)]
        public string DisplayReportName { set; get; }

        /// <summary>
        /// Thuộc loại dữ liệu phụ cấp?
        /// </summary>
        public bool IsAllowanceType { set; get; }

        /// <summary>
        /// Ngày bắt đầu hữu hiệu
        /// </summary>
        public DateTime? StartDate { set; get; }

        /// <summary>
        /// Ngày kết thúc
        /// </summary>
        public DateTime? EndDate { set; get; }

        /// <summary>
        /// Mức phụ cấp (nếu có)
        /// </summary>
        public decimal? Allowance { set; get; }

        /// <summary>
        /// Title của item 
        /// </summary>
        public string Value1Title { set; get; }

        /// <summary>
        /// Giá trị của item
        /// </summary>
        public string Value1Data { set; get; }

        /// <summary>
        /// Title của item 
        /// </summary>
        public string Value2Title { set; get; }

        /// <summary>
        /// Giá trị của item
        /// </summary>
        public string Value2Data { set; get; }

        /// <summary>
        /// Title của item 
        /// </summary>
        public string Value3Title { set; get; }

        /// <summary>
        /// Giá trị của item
        /// </summary>
        public string Value3Data { set; get; }

        /// <summary>
        /// Title của item 
        /// </summary>
        public string Value4Title { set; get; }

        /// <summary>
        /// Giá trị của item
        /// </summary>
        public string Value4Data { set; get; }

        /// <summary>
        /// Title của item 
        /// </summary>
        public string Value5Title { set; get; }

        /// <summary>
        /// Giá trị của item
        /// </summary>
        public string Value5Data { set; get; }

        /// <summary>
        /// Title của item 
        /// </summary>
        public string Value6Title { set; get; }

        /// <summary>
        /// Giá trị của item
        /// </summary>
        public string Value6Data { set; get; }

        /// <summary>
        /// Title của item 
        /// </summary>
        public string Value7Title { set; get; }

        /// <summary>
        /// Giá trị của item
        /// </summary>
        public string Value7Data { set; get; }

        /// <summary>
        /// Title của item 
        /// </summary>
        public string Value8Title { set; get; }

        /// <summary>
        /// Giá trị của item
        /// </summary>
        public string Value8Data { set; get; }

        /// <summary>
        /// Title của item 
        /// </summary>
        public string Value9Title { set; get; }

        /// <summary>
        /// Giá trị của item
        /// </summary>
        public string Value9Data { set; get; }

        /// <summary>
        /// Title của item 
        /// </summary>
        public string Value10Title { set; get; }

        /// <summary>
        /// Giá trị của item
        /// </summary>
        public string Value10Data { set; get; }

        [ForeignKey("MasterID")]
        public virtual Master Master { set; get; }

    }
}

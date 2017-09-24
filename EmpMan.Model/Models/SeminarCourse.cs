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
    /// Quản lý course seminar
    /// </summary>
    [Table("SeminarCourses")]
    public class SeminarCourse : Auditable
    {
        /// <summary>
        /// Khóa tự sinh
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int No { set; get; }

        [Key]
        public int ID { set; get; }
        /// <summary>
        /// Mã phân loại khóa học
        /// </summary>
        public int? SeminarMasterID { set; get; }

        /// <summary>
        /// Mã chi tiết phân loại khóa học
        /// </summary>
        public int? SeminarMasterDetailID { set; get; }

        /// <summary>
        /// Tên khóa học
        /// </summary>
        [Required]
        [MaxLength(256)]
        public string Name { set; get; }

        /// <summary>
        /// Tên tắt
        /// </summary>
        [Required]
        [MaxLength(256)]
        public string ShortName { set; get; }

        /// <summary>
        /// Ngày tổ chức
        /// </summary>
        public DateTime? SeminarDate { set; get; }

        /// <summary>
        /// Mã nhân viên thực hiện seminar
        /// </summary>
        public int? SeminarStaffID { set; get; }


        /// <summary>
        /// Chủ đề
        /// </summary>
        public string SeminarTopic { set; get; }

        /// <summary>
        /// Địa điểm tổ chức
        /// </summary>
        public string Location { set; get; }

        /// <summary>
        /// Nội dung seminar
        /// </summary>
        public string Content { set; get; }

        /// <summary>
        /// Điều kiện tham gia
        /// </summary>
        public string CondRequired { set; get; }

        /// <summary>
        /// Đối tượng tham gia seminar ( List theo ngạch bậc)(1,2,3)
        /// </summary>
        public string PositionIDList { set; get; }

        /// <summary>
        /// Chi phí tham gia
        /// </summary>
        public decimal? Cost { set; get; }

        /// <summary>
        /// co yeu cau kiem tra sau khi xong seminar
        /// </summary>
        public bool? TestRequired { set; get; }

        /// <summary>
        /// Ngày giờ hết hạn phải gửi lại danh sách
        /// </summary>
        public DateTime? AnsSeminarDeptDeadlineDate { set; get; }

        /// <summary>
        /// Thời gian hết hạn trong nội bộ
        /// </summary>
        public DateTime? AnsLocalDeadlineDate { set; get; }

        /// <summary>
        /// Đã gửi thông báo?
        /// </summary>
        public bool? IsNotification { set; get; }

        /// <summary>
        /// Ngày giờ hết hạn đăng ký
        /// </summary>
        public DateTime? ExpireDate { set; get; }

        /// <summary>
        /// đã hoàn thành chưa 
        /// </summary>
        public bool? IsFinished { set; get; }
        
        /// <summary>
        /// File đính kèm
        /// </summary>
        public int? FileID { set; get; }

        /// <summary>
        /// Tham gia có quà tặng
        /// </summary>
        public bool? HaveGift { set; get; }

        /// <summary>
        /// Khóa học nội bộ hay ở ngoài 
        /// </summary>
        public bool? IsInternalCourse { set; get; }

        [ForeignKey("SeminarMasterID")]
        public virtual MasterDetail SeminarMaster { set; get; }

        [ForeignKey("SeminarMasterDetailID")]
        public virtual MasterDetail SeminarMasterDetail { set; get; }

        [ForeignKey("FileID")]
        public virtual FileStorage File { set; get; }

         public virtual ICollection<SeminarRecord> SeminarRecordList { get; set; }
        
    }
}

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
    /// Lưu thông tuyển dụng
    /// </summary>
    [Table("Recruitments")]
    public class Recruitment : Auditable
    {
        /// <summary>
        /// Khóa tự sinh
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int No{ set; get; }

        /// <summary>
        /// Mã lần tuyển dụng
        /// </summary>

        [Key]
        [MaxLength(128)]
        public string ID { set; get; }

        /// <summary>
        /// Tên lần tuyển dụng
        /// </summary>
        [Required]
        [MaxLength(256)]
        public string Name { set; get; }

        /// <summary>
        /// Tên tắt 
        /// </summary>
        [MaxLength(256)]
        public string ShortName { set; get; }
        /// <summary>
        /// Loại tuyển dụng ( LTV chính thức , học việc, tổng vụ ..)
        /// MasterDetail(30)
        /// </summary>
        public int? RecruitmentTypeMasterID { set; get; }
        /// <summary>
        /// Loại tuyển dụng ( LTV chính thức , học việc, tổng vụ ..)
        /// MasterDetail(30)
        /// </summary>
        public int? RecruitmentTypeMasterDetailID { set; get; }

        /// <summary>
        /// Đường dẫn các hồ sơ do phòng tuyển dụng gửi
        /// </summary>
        public string CvCompanyFolderPath { set; get; }

        /// <summary>
        /// Đường dẫn các hồ sơ do phòng  dept thông báo
        /// </summary>
        public string CvDeptFolderPath { set; get; }

        /// <summary>
        /// Số hồ sơ phỏng vấn
        /// </summary>
        public int? CvCount { set; get; }

        /// <summary>
        /// Người gửi thông báo 
        /// </summary>
        public int? SendMailFromEmpID { set; get; }

        /// <summary>
        /// Mail trả lời phỏng vấn cho ai
        /// </summary>
        public int? SendMailToEmpID { set; get; }

        /// <summary>
        /// Ngày giờ hết hạn phải gửi lại danh sách phỏng vấn
        /// </summary>
        public DateTime? AnsRecruitDeptDeadlineDate { set; get; }

        /// <summary>
        /// Thời gian hết hạn trong nội bộ
        /// </summary>
        public DateTime? AnsLocalDeadlineDate { set; get; }

        /// <summary>
        /// Đã gửi thông báo cho các leaders chưa
        /// </summary>
        public bool? IsNotification { set; get; }

        /// <summary>
        /// Ngày giờ hết hạn đăng ký phỏng vấn
        /// </summary>
        public DateTime? ExpireDate { set; get; }
        
        /// <summary>
        /// Nội dung liên quan
        /// </summary>
        public string Content { set; get; }

        /// <summary>
        /// Đợt tuyển dụng đã hoàn thành chưa 
        /// </summary>
        public bool? IsFinished { set; get; }

        /// <summary>
        /// File đối tượng
        /// </summary>
        public int? FileID { set; get; }

    }
}

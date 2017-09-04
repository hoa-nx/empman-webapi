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
    /// Lưu các qui định của công ty
    /// </summary>
    [Table("CompanyRules")]
    public class CompanyRule : Auditable
    {
        /// <summary>
        /// Khóa chính (tự sinh)
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { set; get; }

        /// <summary>
        /// Công ty
        /// </summary>
        public int? CompanyID { set; get; }
        /// <summary>
        /// Ngày thông báo qui định / ngày công bố
        /// </summary>
        [Required]
        public DateTime? NoticeDate { set; get; }

        /// <summary>
        /// Người tạo rule
        /// </summary>
        public int? CreatorID { set; get; }

        /// <summary>
        /// Người gửi
        /// </summary>
        public int? SenderID { set; get; }
        /// <summary>
        /// Tên người gửi có thể chỉnh sửa
        /// </summary>
        [MaxLength(256)]
        public string SenderName { set; get; }

        /// <summary>
        /// Tên thông báo
        /// </summary>
        [Required]
        [MaxLength(256)]
        public string Name { set; get; }

        /// <summary>
        /// Nội dung
        /// </summary>
        public string Content{ set; get; }
        /// <summary>
        /// Đính kèm file
        /// </summary>
        public int? FileID { set; get; }

        /// <summary>
        /// Loại thông báo ví dụ như nhân sự / qui định oniste….
        /// <para/> Master.Code = 25
        /// </summary>
        [ForeignKey("RuleType"), Column(Order =0)]
        public int? RuleTypeMasterID { set; get; }

        /// <summary>
        /// Loại thông báo ví dụ như nhân sự / qui định oniste….
        /// <para/> Mã tương ứng với loại Master.Code = 25
        /// </summary>
        [ForeignKey("RuleType"), Column(Order = 1)]
        public int? RuleTypeMasterDetailID { set; get; }

        /// <summary>
        /// Ngày áp dụng
        /// </summary>
        public DateTime? ValidDateStart { set; get; }

        /// <summary>
        /// Ngày kết thúc áp dụng
        /// </summary>
        public DateTime? ValidDateEnd { set; get; }

        /// <summary>
        /// Nhóm đối tượng thực hiện
        /// </summary>
        public int? ActionObject{ set; get; }

        [ForeignKey("CompanyID")]
        public virtual Company Company { set; get; }

        [ForeignKey("SenderID")]
        public virtual Emp Sender { set; get; }

        [ForeignKey("CreatorID")]
        public virtual Emp Creator { set; get; }

        [ForeignKey("FileID")]
        public virtual FileStorage File { set; get; }

        public virtual MasterDetail RuleType { set; get; }
 
    }
}

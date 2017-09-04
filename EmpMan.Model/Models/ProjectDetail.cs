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
    /// Thông tin dự án -- chi tiết dự án
    /// </summary>
    [Table("ProjectDetails")]
    public class ProjectDetail: Auditable
    {
        /// <summary>
        /// Khóa tự sinh
        /// </summary>
        //[Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { set; get; }

        /// <summary>
        /// Mã chi tiết dự án 
        /// </summary>
        [Key, Column(Order =0)]
        [Required]
        public int ProjectID { set; get; }

        /// <summary>
        /// Mã chi tiết dự án
        /// </summary>
        [Key, Column(Order = 1)]
        [Required]
        public int ProjectDetailID { set; get; }

        /// <summary>
        /// No đặt hàng
        /// </summary>
        [MaxLength(256)]
        public string OrderReceivedID { set; get; }

        /// <summary>
        /// Mã khách hàng
        /// </summary>
        public int? CustomerID { set; get; } 

        /// <summary>
        /// Mã dự án trong công ty
        /// </summary>
        public int? CompanyID { set; get; }

        /// <summary>
        /// Tên dự án
        /// </summary>
        [Required]
        [MaxLength(256)]
        public string Name { set; get; }

        /// <summary>
        /// Tên tiếng Nhật
        /// </summary>
        [MaxLength(256)]
        public string NameJp { set; get; }

        /// <summary>
        /// Tên tắt
        /// </summary>
        [MaxLength(256)]
        public string ShortName { set; get; }

        /// <summary>
        /// Số MM báo giá
        /// </summary>
        public decimal? EstimateManMonth { set; get; }

        /// <summary>
        /// Số MM thực tế
        /// </summary>
        public decimal? ActualManMonth { set; get; }
                
        /// <summary>
        /// PM
        /// </summary>
        public int? PMID { set; get; }

        /// <summary>
        /// PL
        /// </summary>
        public int? PLID { set; get; }

        /// <summary>
        /// Ngày dự định start
        /// </summary>
        public DateTime? PlanStartDate { set; get; }

        /// <summary>
        /// Ngày dự định end
        /// </summary>
        public DateTime? PlanEndDate { set; get; }

        /// <summary>
        /// Ngày thực tế start
        /// </summary>
        public DateTime? ActualStartDate { set; get; }

        /// <summary>
        /// Ngày thực tế end
        /// </summary>
        public DateTime? ActualEndDate { set; get; }

        /// <summary>
        /// Tổng số QA của dự án
        /// </summary>
        public int? TotalQACount { set; get; }

        /// <summary>
        /// Toàn bộ bug sau khi giao hàng
        /// </summary>
        public int? AfterDelBugCount { set; get; }

        /// <summary>
        /// Số bug do thay đổi thiết kế
        /// </summary>
        public int? ChangeDetailDesignBugCount { set; get; }

        /// <summary>
        /// Bug của công ty mình
        /// </summary>
        public int? MyCompanyBugCount { set; get; }

        /// <summary>
        /// Không phải bug
        /// </summary>
        public int? NotBugCount { set; get; }

        /// <summary>
        /// Bug do chỉ thị sai
        /// </summary>
        public int? MissDetailDesignBugCount { set; get; }

        /// <summary>
        /// số PG
        /// </summary>
        public int? PgCount { set; get; }

        /// <summary>
        /// Phân loại dự án
        /// <para/> CommonData.Code = 24
        /// </summary>
        [ForeignKey("ProjectType")]
        public int? ProjectTypeMasterID { set; get; }

        /// <summary>
        /// Phân loại dự án
        /// <para/> CommonData.Code = 24
        /// </summary>
        [ForeignKey("ProjectType")]
        public int? ProjectTypeMasterDetailID { set; get; }

        /// <summary>
        /// Ngôn ngữ lập trình
        /// </summary>
        public string Language { set; get; }

        /// <summary>
        /// LOC báo giá
        /// </summary>
        public int? EstLocCount { set; get; }

        /// <summary>
        /// LOC thực tế
        /// </summary>
        public int? ActualLocCount { set; get; }

        /// <summary>
        /// Năng suất báo giá
        /// </summary>
        public decimal? EstPerformance { set; get; }

        /// <summary>
        /// Năng suất thực tế
        /// </summary>
        public decimal? ActualPerformance { set; get; }

        /// <summary>
        /// Du an co trong du dinh hay khong
        /// </summary>
        public bool? IsScheduleProject { set; get; }

        [ForeignKey("ProjectID")]
        public virtual Project Project { set; get; }

        [ForeignKey("OrderReceivedID")]
        public virtual OrderReceived OrderReceived { set; get; }

        [ForeignKey("CustomerID")]
        public virtual Customer Customer { set; get; }

        [ForeignKey("CompanyID")]
        public virtual Company Company { set; get; }

        [ForeignKey("PMID")]
        public virtual Emp PM { set; get; }

        [ForeignKey("PLID")]
        public virtual Emp PL { set; get; }

        public virtual MasterDetail ProjectType { set; get; }

    }
}

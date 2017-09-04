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
    /// Thông tin dự án
    /// </summary>
    [Table("Projects")]
    public class Project : Auditable
    {
        /// <summary>
        /// Khóa tự sinh
        /// </summary>
        
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int No { set; get; }

        /// <summary>
        /// Mã dự án
        /// </summary>
        [Key]
        public int ID { set; get; }

        /// <summary>
        /// Mã khách hàng
        /// </summary>
        public int? CustomerID { set; get; }

        /// <summary>
        /// No đặt hàng
        /// </summary>
        [MaxLength(256)]
        public string OrderReceivedID { set; get; }

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
        /// Tên viết tắt
        /// </summary>
        [MaxLength(256)]
        public string ShortName { set; get; }

        /// <summary>
        /// Mã dự án trong công ty ( WMS)
        /// </summary>
        public string CompanyProjectID { set; get; }

        /// <summary>
        /// Số MM báo giá
        /// </summary>
        public decimal? EstimateManMonth { set; get; }

        /// <summary>
        /// Số MM thực tế
        /// </summary>
        public decimal? ActualManMonth { set; get; }

        [ForeignKey("OrderReceivedID")]
        public virtual OrderReceived OrderReceived { set; get; }

        [ForeignKey("CustomerID")]
        public virtual Customer ProjectCustomer { set; get; }

        public virtual ICollection<ProjectDetail> ProjectDetails { set; get; }

        public virtual ICollection<ProjectDetailResource> ProjectDetailResources { set; get; }

    }
}

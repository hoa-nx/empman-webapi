using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EmpMan.Web.Models.Common
{
    public class AuditableViewModel
    {
        public AuditableViewModel()
        {
            Status = true;
        }

        /// <summary>
        /// concurrency  check 
        /// </summary>
        //[Timestamp]
        public byte[] RowVersion { get; set; }
        /// <summary>
        /// Trình tự hiển thị
        /// </summary>
        public int? DisplayOrder { set; get; }

        /// <summary>
        /// Người / tổ chức sở hữu dữ liệu
        /// </summary>
        [MaxLength(256)]
        public string AccountData { set; get; }

        /// <summary>
        /// Ghi chú
        /// </summary>
        public string Note { set; get; }

        /// <summary>
        /// Chỉ định mức độ có thể truy cập dữ liệu
        /// </summary>
        public int? AccessDataLevel { set; get; }

        /// <summary>
        /// Ngảy tạo
        /// </summary>
        public DateTime? CreatedDate { set; get; }

        /// <summary>
        /// Người tạo
        /// </summary>
        [MaxLength(256)]
        public string CreatedBy { set; get; }

        /// <summary>
        /// Ngày update
        /// </summary>
        public DateTime? UpdatedDate { set; get; }

        /// <summary>
        /// Người update
        /// </summary>
        [MaxLength(256)]
        public string UpdatedBy { set; get; }

        /// <summary>
        /// Keyword siêu dữ liệu (SEO)
        /// </summary>
        [MaxLength(256)]
        public string MetaKeyword { set; get; }

        /// <summary>
        /// Mô tả của siêu data
        /// </summary>
        [MaxLength(256)]
        public string MetaDescription { set; get; }

        /// <summary>
        /// Trạng thái
        /// </summary>
        public bool Status { set; get; }

        /// <summary>
        /// Trạng thái của dữ liệu ( 
        /// </summary>
        public int? DataStatus { set; get; }

        /// <summary>
        /// Trình duyệt của user
        /// </summary>
        public string UserAgent { set; get; }
        /// <summary>
        /// IP Host của user
        /// </summary>
        public string UserHostAddress { set; get; }

        /// <summary>
        /// Tên host của user
        /// </summary>
        public string UserHostName { set; get; }

        /// <summary>
        /// Ngày yêu cầu
        /// </summary>
        public DateTime? RequestDate { set; get; }

        /// <summary>
        /// Người yêu cầu
        /// </summary>
        public string RequestBy { set; get; }

        /// <summary>
        /// Ngày approved
        /// </summary>
        public DateTime? ApprovedDate { set; get; }

        /// <summary>
        /// Người approved
        /// </summary>
        public string ApprovedBy { set; get; }

        /// <summary>
        /// Trạng thái approved dữ liệu
        /// </summary>
        public int? ApprovedStatus { set; get; }

        //public string Yobi_Text1 { set; get; }
        //public string Yobi_Text2 { set; get; }
        //public string Yobi_Text3 { set; get; }
        //public string Yobi_Text4 { set; get; }
        //public string Yobi_Text5 { set; get; }
        //public long? Yobi_Number1 { set; get; }
        //public long? Yobi_Number2 { set; get; }
        //public long? Yobi_Number3 { set; get; }
        //public long? Yobi_Number4 { set; get; }
        //public long? Yobi_Number5 { set; get; }
        //public decimal? Yobi_Decimal1 { set; get; }
        //public decimal? Yobi_Decimal2 { set; get; }
        //public decimal? Yobi_Decimal3 { set; get; }
        //public decimal? Yobi_Decimal4 { set; get; }
        //public decimal? Yobi_Decimal5 { set; get; }
        //public DateTime? Yobi_Date1 { set; get; }
        //public DateTime? Yobi_Date2 { set; get; }
        //public DateTime? Yobi_Date3 { set; get; }
        //public DateTime? Yobi_Date4 { set; get; }
        //public DateTime? Yobi_Date5 { set; get; }
    }
}
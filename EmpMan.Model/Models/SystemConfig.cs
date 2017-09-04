using EmpMan.Model.Abstract;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmpMan.Model.Models
{
    [Table("SystemConfigs")]
    public class SystemConfig : Auditable
    {

        [Key]
        public int ID { set; get; }

        [Required]
        [Column(TypeName = "varchar")]
        [MaxLength(50)]
        public string Code { set; get; }

        /// <summary>
        /// Tên cấu hình
        /// </summary>
        [Required]
        [MaxLength(256)]
        public string Name { set; get; }

        /// <summary>
        /// Tên tăt
        /// </summary>
        //[MaxLength(256)]
        public string ShortName { set; get; }
        /// <summary>
        /// Năm xử lý dữ liệu
        /// </summary>
        public DateTime? ProcessingYear { set; get; }

        /// <summary>
        /// Số tháng thâm niên không tính vào doanh số
        /// </summary>
        public int? ExpMonth { set; get; }

        /// <summary>
        /// khoản dùng để gửi mail
        /// </summary>
        [MaxLength(50)]
        public string MailAccountName { set; get; }

        /// <summary>
        /// Mật khẩu đã mã hóa
        /// </summary>
        [MaxLength(50)]
        public string MailAccountPassword { set; get; }

        /// <summary>
        /// Chuỗi ký tự dùng cho mã hóa
        /// </summary>
        [MaxLength(256)]
        public string MailAccountHalt { set; get; }

        /// <summary>
        /// Chuỗi json dùng để chứa trình tự sort 
        /// </summary>
        public string EmpOrderBy { set; get; }

        /// <summary>
        /// Có thấy được mức lương không 
        /// </summary>
        public bool? IsShowSalaryValue { set; get; }

        /// <summary>
        /// Có thấy được các số tiền / đơn giá tại doanh số  không 
        /// </summary>
        public bool? IsShowMoneyValue { set; get; }

        /// <summary>
        /// Chuỗi json dùng để chứa các điều kiện filter dữ liệu chung cho system
        /// </summary>
        public string EmpFilterDataValue { set; get; }

        [MaxLength(50)]
        public string ValueString { set; get; }

        public int? ValueInt { set; get; }

        public string SystemValue { set; get; }
        
    }
}
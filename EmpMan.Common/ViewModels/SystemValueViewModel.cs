using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpMan.Common.ViewModels
{
    public class SystemValueViewModel
    {
        public int ID { set; get; }

        public string Code { set; get; }
        /// <summary>
        /// Tên cấu hình
        /// </summary>

        public string Name { set; get; }

        /// <summary>
        /// Tên tăt
        /// </summary>
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
        public string MailAccountName { set; get; }

        /// <summary>
        /// Mật khẩu đã mã hóa
        /// </summary>
        public string MailAccountPassword { set; get; }

        /// <summary>
        /// Chuỗi ký tự dùng cho mã hóa
        /// </summary>
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
        /// SID
        /// </summary>
        public string SidT { set; get; }
        /// <summary>
        /// TOKEN
        /// </summary>
        public string TokT { set; get; }

    }
}

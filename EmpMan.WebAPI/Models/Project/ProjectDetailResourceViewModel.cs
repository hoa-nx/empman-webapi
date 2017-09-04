using EmpMan.Web.Models.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EmpMan.Web.Models.Project
{
    public class ProjectDetailResourceViewModel : AuditableViewModel
    {

        public int ID { set; get; }

        /// <summary>
        /// Mã dự án
        /// </summary>
        [Required(ErrorMessage = "Mã dự án bắt buộc nhập")]
        public int ProjectID { set; get; }

        /// <summary>
        /// Mã chi tiết dự án
        /// </summary>
        [Required(ErrorMessage = "Mã chi tiết dự án bắt buộc nhập")]
        public int ProjectDetailID { set; get; }

        /// <summary>
        /// Account FTP dùng cho khách hàng
        /// </summary>
        public string FtpAccout { set; get; }

        /// <summary>
        /// Mật khẩu của FTP
        /// </summary>
        public string FtpPassword { set; get; }

        /// <summary>
        /// Port FTP
        /// </summary>
        public string FtpPort { set; get; }

        /// <summary>
        /// Đường dẫn local của FTP
        /// </summary>
        public string FtpLocalPath { set; get; }

        /// <summary>
        /// Email dùng cho phiên dịch
        /// </summary>
        public string TransMailAccount { set; get; }

        /// <summary>
        /// Email dùng cho phiên dịch
        /// </summary>
        public string TransMailPassword { set; get; }

        /// <summary>
        /// Mail group dùng cho quản lý
        /// </summary>
        public string EmailManagementGroup { set; get; }

        /// <summary>
        /// Mail group dùng cho nội bộ LTV
        /// </summary>
        public string EmailDevGroup { set; get; }

        /// <summary>
        /// Thông tin QAMS dùng cho KH
        /// </summary>
        public string QAMSAccount { set; get; }

        /// <summary>
        /// Thông tin mật khẩu QAMS dùng cho KH
        /// </summary>
        public string QAMSPassword { set; get; }

        /// <summary>
        /// Địa chỉ IP khách hàng sử dụng để filter. Sử dụng ; để ngăn cách
        /// </summary>
        public string CustomerGlobalIpList { set; get; }

        /// <summary>
        /// Danh sách IP của công ty mình
        /// </summary>
        public string MyCompanyGlobalIpList { set; get; }

        /// <summary>
        /// Danh sách các máy ảo có sử dụng
        /// </summary>
        public string VirtualPc1 { set; get; }

        /// <summary>
        /// Kỳ hạn sử dụng máy ảo
        /// </summary>
        public DateTime? VirtualPc1EndDate { set; get; }

        /// <summary>
        /// Máy ảo sử dụng
        /// </summary>
        public string VirtualPc2 { set; get; }

        /// <summary>
        /// Kỳ hạn sử dụng máy ảo
        /// </summary>
        public DateTime? VirtualPc2EndDate { set; get; }

        public virtual ProjectViewModel Project { set; get; }

        public virtual ProjectDetailViewModel ProjectDetail { set; get; }

    }
}
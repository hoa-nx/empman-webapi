using EmpMan.Web.Models.Common;
using EmpMan.Web.Models.Master;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EmpMan.Web.Models.Emp
{
    public class RecruitmentViewModel : AuditableViewModel
    {
        /// <summary>
        /// Khóa tự sinh
        /// </summary>
        public int No { set; get; }

        /// <summary>
        /// Mã lần tuyển dụng
        /// </summary>

        public string ID { set; get; }

        /// <summary>
        /// Tên lần tuyển dụng
        /// </summary>

        public string Name { set; get; }

        /// <summary>
        /// Tên tắt 
        /// </summary>
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
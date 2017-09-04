using EmpMan.Web.Models.Common;
using EmpMan.Web.Models.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmpMan.Web.Models.Emp
{
    public class EmpSupportViewModel : AuditableViewModel
    {
        public int ID { set; get; }

        /// <summary>
        /// Mã nhân viên
        /// </summary>
        public int EmpID { set; get; }

        /// <summary>
        /// Ngày start 
        /// </summary>
        public DateTime? StartDate { set; get; }

        /// <summary>
        /// Ngày end
        /// </summary>
        public DateTime? EndDate { set; get; }

        /// <summary> 
        /// Loại support  (thực tập / thử việc ...)
        /// <para/> CommonData.Code =26
        /// </summary>
        public int SupportTypeMasterID { set; get; }

        /// <summary> 
        /// Loại support  (thực tập / thử việc ...)
        /// <para/> CommonData.Code =26
        /// </summary>
        public int SupportTypeMasterDetailID { set; get; }

        /// <summary>
        /// Ngày nhận tiền support
        /// </summary>
        public DateTime? ReceivedSupportFeeDate1 { set; get; }

        /// <summary>
        /// Ngày nhận tiền support
        /// </summary>
        public DateTime? ReceivedSupportFeeDate2 { set; get; }

        /// <summary>
        /// Ngày nhận tiền support
        /// </summary>
        public DateTime? ReceivedSupportFeeDate3 { set; get; }

        /// <summary>
        /// Tổng tiền nhận được lần 1 
        /// </summary>
        public decimal? ReceivedSupportFee1 { set; get; }

        /// <summary>
        /// Tổng tiền nhận được lần 2
        /// </summary>
        public decimal? ReceivedSupportFee2 { set; get; }


        /// <summary>
        /// Tổng tiền nhận được lần 3 
        /// </summary>
        public decimal? ReceivedSupportFee3 { set; get; }

        /// <summary>
        /// Người được support
        /// </summary>
        public int? TraineeID { set; get; }

        /// <summary>
        /// Kết quả đánh giá 
        /// </summary>
        public string Result { set; get; }

        /// <summary>
        /// Hanh dong
        /// </summary>
        public string Action { set; get; }

        public virtual EmpViewModel Emp { set; get; }

        public virtual MasterDetailViewModel SupportType { set; get; }

        public virtual EmpViewModel Trainee { set; get; }

    }
}
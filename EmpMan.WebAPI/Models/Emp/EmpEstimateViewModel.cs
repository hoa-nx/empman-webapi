using EmpMan.Web.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmpMan.Web.Models.Emp
{
    public class EmpEstimateViewModel : AuditableViewModel
    {
        /// <summary>
        /// Khóa chính tự sinh
        /// </summary>
        public int ID { set; get; }

        /// <summary>
        /// Mã nhân viên
        /// </summary>
        public int EmpID { set; get; }

        /// <summary>
        /// Tháng năm đối tượng
        /// </summary>
        public DateTime? YearMonth { set; get; }

        /// <summary>
        /// Điểm đánh giá
        /// </summary>
        public decimal? EstimatePoint { set; get; }

        /// <summary>
        /// MM effort trong tháng
        /// </summary>
        public decimal? EffortMM { set; get; }

        /// <summary>
        /// Tiền thưởng trong tháng
        /// </summary>
        public decimal? BonusUsd { set; get; }

        public virtual EmpViewModel Emp { set; get; }
    }
}
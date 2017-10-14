using EmpMan.Common.ViewModels.Models.Common;
using EmpMan.Common.ViewModels.Models.Emp;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EmpMan.Common.ViewModels.Models.Master
{
    public class ExchangeRateViewModel : AuditableViewModel
    {
        public int No { set; get; }

        /// <summary>
        /// Mã chuyển đổi đơn giá
        /// </summary>
        /// 
        public int ID { set; get; }

        /// <summary>
        /// Tên 
        /// </summary>
        public string Name { set; get; }

        /// <summary>
        /// Có hiệu lực từ
        /// </summary>
        public DateTime? StartDate { set; get; }

        /// <summary>
        /// TCó hiệu lực đến
        /// </summary>
        public DateTime? EndDate { set; get; }

        /// <summary>
        /// 1 USD bằng bao nhiêu VND
        /// </summary>
        public decimal? UsdToVnd { set; get; }

        /// <summary>
        /// 1 Yên bằng bao nhiêu VND
        /// </summary>
        public decimal? YenToVnd { set; get; }

        /// <summary>
        /// Usd / Yen
        /// </summary>
        public decimal? UsdToYen { set; get; }

    }
}
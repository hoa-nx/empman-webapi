using EmpMan.Model.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Thông tin doanh thu 
/// </summary>
namespace EmpMan.Model.Models
{
    /// <summary>
    /// Tỉ giá
    /// </summary>
    [Table("ExchangeRates")]
    public class ExchangeRate : Auditable
    {
        /// <summary>
        /// tự sinh
        /// </summary>
        
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int No { set; get; }

        /// <summary>
        /// Mã chuyển đổi đơn giá
        /// </summary>
        /// 
        [Key]
        public int ID{ set; get; }

       /// <summary>
        /// Tên 
        /// </summary>
        public string Name{ set; get; }

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

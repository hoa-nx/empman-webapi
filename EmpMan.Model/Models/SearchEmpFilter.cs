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
    /// Bảng chứa thông tin search trong hệ thống theo từng user (account logon)
    /// </summary>
    [Table("SearchEmpFilters")]
    public class SearchEmpFilter : Auditable
    {

        /// <summary>
        /// Khóa chính tự  sinh
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int No { set; get; }

        /// <summary>
        /// Mã search
        /// </summary>
        [Key , Column(Order =1)]
        public int CompanyID { set; get; }

        /// <summary>
        /// Mã nhân viên đối tượng
        /// </summary>
        [Key, Column(Order = 2)]
        public int? EmpID { set; get; }

        /// <summary>
        /// Account logon
        /// </summary>
        public string AccountName { set; get; }


    }
}

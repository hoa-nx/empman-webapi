using EmpMan.Common.ViewModels.Models.Common;
using System;

namespace EmpMan.Common.ViewModels.Models
{
    public class SearchEmpFilterViewModel : AuditableViewModel
    {

        /// <summary>
        /// Khóa chính tự  sinh
        /// </summary>
        public int No { set; get; }

        /// <summary>
        /// Mã search
        /// </summary>
        public int CompanyID { set; get; }

        /// <summary>
        /// Mã nhân viên đối tượng
        /// </summary>
        public int? EmpID { set; get; }

        /// <summary>
        /// Account logon
        /// </summary>
        public string AccountName { set; get; }
    }
}
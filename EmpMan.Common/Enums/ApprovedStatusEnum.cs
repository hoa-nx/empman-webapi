using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpMan.Common.Enums
{
    /// <summary>
    /// Trạng thái của approved 
    /// </summary>
    public enum ApprovedStatusEnum
    {
        /// <summary>
        /// Dữ liệu thông thường
        /// </summary>
        Normal = 0,
        /// <summary>
        /// Dữ liệu đang yêu cầu approved
        /// </summary>
        Request = 1,
        /// <summary>
        /// Từ chối approved
        /// </summary>
        RequestNg = 2,

        /// <summary>
        /// Trưởng nhóm approved
        /// </summary>
        TeamApproved = 10,

        /// <summary>
        /// Dept manager approved
        /// </summary>
        DeptApproved = 15,

        /// <summary>
        /// General magager approved
        /// </summary>
        GeneralDeptApproved = 20,

        /// <summary>
        /// Công ty Approved data ( mức cao nhất )
        /// </summary>
        Approved = 100

    }
}

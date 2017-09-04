using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpMan.Common.Enums
{
    /// <summary>
    /// Phân loại nhóm người dùng trong hệ thống
    /// </summary>
    public enum RoleEnum
    {
        /// <summary>
        /// Quyền hệ thống (không giới hạn )
        /// </summary>
        Admin,
        /// <summary>
        /// Thành viên ban giám đốc
        /// </summary>
        DirectoryBoard,

        /// <summary>
        /// Manager cấp cao quản lý chung
        /// </summary>
        GeneralManager,

        /// <summary>
        /// Manager
        /// </summary>
        Manager,

        /// <summary>
        /// Phó ban
        /// </summary>
        ViceManager,

        /// <summary>
        /// Trưởng nhóm
        /// </summary>
        Leader,

        /// <summary>
        /// Phó nhóm
        /// </summary>
        SubLeader,

        /// <summary>
        /// Nhân viên
        /// </summary>
        Member
    }
}

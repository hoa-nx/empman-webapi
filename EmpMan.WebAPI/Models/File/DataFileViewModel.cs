using EmpMan.Web.Models.Emp;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EmpMan.Web.Models.File
{
    public class DataFileViewModel
    {
        /// <summary>
        /// Khóa tự sinh
        /// </summary>
        /// 
        public int ID { set; get; }

        /// <summary>
        /// File ID
        /// </summary>
        public int FileID { set; get; }

        /// <summary>
        /// Ten bang
        /// </summary>
        [Required]
        [MaxLength(256)]
        public string TableName { set; get; }

        /// <summary>
        /// Khoa ngoai cua bang TableName
        /// </summary>
        public int DataID { set; get; }

        /// <summary>
        /// Tên 
        /// </summary>
        [MaxLength(256)]
        public string Name { set; get; }

        /// <summary>
        /// Khoa ngoai cua bang TableName
        /// </summary>
        public int? EmpID { set; get; }

        public virtual FileStorageViewModel File { set; get; }

        public virtual EmpViewModel Emp { set; get; }
    }
}
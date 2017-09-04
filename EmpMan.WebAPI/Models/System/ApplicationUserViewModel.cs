using System;
using System.Collections.Generic;
using System.Web;

namespace EmpMan.Web.Models
{
    public class AppUserViewModel 
    {
        public string Id { set; get; }
        public string FullName { set; get; }
        public string BirthDay { set; get; }
        public string Email { set; get; }
        public string Password { set; get; }
        public string UserName { set; get; }
        public string Address { get; set; }
        public string PhoneNumber { set; get; }
        public string Avatar { get; set; }
        public bool Status { get; set; }

        public string Gender { get; set; }
        public string AccountCompany { get; set; }

        /// <summary>
        /// Code team trực thuộc của user
        /// </summary>
        public int? TeamID { get; set; }

        /// <summary>
        /// Code phòng ban trực thuộc của user
        /// </summary>
        public int? DeptID { get; set; }

        /// <summary>
        /// Code công ty trực thuộc của user
        /// </summary>
        public int? CompanyID { get; set; }

        /// <summary>
        /// Code nhân viên
        /// </summary>
        public int? EmpID { get; set; }

        /// <summary>
        /// Năm xử lý
        /// </summary>
        public int? ProcessingYear { get; set; }

        public ICollection<string> Roles { get; set; }
    }
}
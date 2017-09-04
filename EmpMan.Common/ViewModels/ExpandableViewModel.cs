using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpMan.Common.ViewModels
{
    public class EmpExpandableViewModel
    {
        public int ID { set; get; }

        public string FullName { set; get; }

        public string Name { set; get; }

        public string Telephone { set; get; }

        public string WorkingEmail { set; get; }

        public string Avatar { set; get; }

        public int? CompanyID { set; get; }

        public string CompanyName { set; get; }
        
        public int? DeptID { set; get; }
        public string DeptName { set; get; }

        public int? TeamID { set; get; }
        public string TeamName { set; get; }

        public int? PositionID { set; get; }
        public string PositionName { set; get; }

        public int? KeikenMonth { set; get; }
        public decimal? KeikenYear { set; get; }
        public string JapaneseLevel { set; get; }
        
    }
}

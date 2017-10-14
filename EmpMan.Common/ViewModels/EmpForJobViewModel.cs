using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpMan.Common.ViewModels
{
    public class EmpForJobViewModel
    {
        public int? ID { set; get; }

        public string FullName { set; get; }

        public string ShortName { set; get; }

        public string WorkingEmail { set; get; }

        public string PhoneNumber { set; get; }

    }
}

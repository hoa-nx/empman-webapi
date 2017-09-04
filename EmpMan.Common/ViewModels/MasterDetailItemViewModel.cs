using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpMan.Common.ViewModels
{
    public class MasterDetailItemViewModel
    {
        public MasterDetailItemViewModel()
        {
            MasterID = 0;
            DetailID = 0;
        }

        public  int? MasterID { set; get; }
        public  int? DetailID { set; get; }

    }
}

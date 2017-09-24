using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpMan.Common.ViewModels
{
    public class TimelineViewModel
    {
        public int? ID { set; get; }
        public string Name { set; get; }
        public DateTime? StartDate{ set; get; }
        public DateTime? EndDate { set; get; }
        public string Title { set; get; }
        public string Description { set; get; }
        public string CssName { set; get; }
        public string Icon { set; get; }
        public string Avatar { set; get; }
        public string CollectName { set; get; }
        public int? Age { set; get; }
        public int? KeikenFromContractMonths { set; get; }
    }
}

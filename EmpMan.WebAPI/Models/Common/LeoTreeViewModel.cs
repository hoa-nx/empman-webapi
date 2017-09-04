using System.Collections.Generic;

namespace EmpMan.Web.Models
{
    public class LeoTreeViewModel
    {
        public string text { set; get; }

        public string value { set; get; }
        public bool? collapsed { set; get; }

        public ICollection<LeoTreeViewModel> children { set; get; }

    }
}
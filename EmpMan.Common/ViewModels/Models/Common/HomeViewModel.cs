using EmpMan.Common.ViewModels.Models.Common;
using EmpMan.Common.ViewModels.Models.Product;
using System.Collections.Generic;

namespace EmpMan.Common.ViewModels.Models.Common
{
    public class HomeViewModel
    {
        public IEnumerable<SlideViewModel> Slides { set; get; }
        public IEnumerable<ProductViewModel> LastestProducts { set; get; }
        public IEnumerable<ProductViewModel> TopSaleProducts { set; get; }

        public string Title { set; get; }
        public string MetaKeyword { set; get; }
        public string MetaDescription { set; get; }
    }
}
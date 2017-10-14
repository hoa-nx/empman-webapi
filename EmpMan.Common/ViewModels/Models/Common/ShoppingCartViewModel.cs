using EmpMan.Common.ViewModels.Models.Product;
using System;

namespace EmpMan.Common.ViewModels.Models.Common
{
    [Serializable]
    public class ShoppingCartViewModel
    {
        public int ProductId { set; get; }
        public ProductViewModel Product { set; get; }
        public int Quantity { set; get; }
    }
}
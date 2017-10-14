using EmpMan.Common.ViewModels.Models.Common;

namespace EmpMan.Common.ViewModels.Models.Product
{
    public class ProductTagViewModel
    {
        public int ProductID { set; get; }

        public string TagID { set; get; }

        public virtual ProductViewModel Post { set; get; }

        public virtual TagViewModel Tag { set; get; }
    }
}
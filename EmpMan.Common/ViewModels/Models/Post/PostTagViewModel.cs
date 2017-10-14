using EmpMan.Common.ViewModels.Models.Common;

namespace EmpMan.Common.ViewModels.Models.Post
{
    public class PostTagViewModel
    {
        public int PostID { set; get; }

        public string TagID { set; get; }

        public virtual PostViewModel Post { set; get; }

        public virtual TagViewModel Tag { set; get; }
    }
}
using System.ComponentModel.DataAnnotations;

namespace EmpMan.Common.ViewModels.Models
{
    public class ApplicationRoleViewModel
    {
        public string Id { set; get; }

        [Required(ErrorMessage = "Bạn phải nhập tên")]
        public string Name { set; get; }

        public string Description { set; get; }

    }
}
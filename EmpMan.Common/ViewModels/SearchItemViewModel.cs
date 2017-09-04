using EmpMan.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpMan.Common.ViewModels
{
    /// <summary>
    /// Dùng để lưu trữ các hạng mục search gửi từ client lên để trích xuất data đối tượng
    /// 
    /// </summary>
    public class SearchItemViewModel
    {
        public SearchItemViewModel()
        {

        }
        public int? ID { set; get; }
        public int? Page { set; get; }
        public int? PageSize { set; get; }
        public string Keyword { set; get;}
        public List<int?> NumberItems { set; get; }
        public List<string> StringItems { set; get; }
        public List<DateTime?> DateTimeItems { set; get; }
        public List<bool?> BoolItems { set; get; }
        public List<MasterDetailItemViewModel> MasterDetailItems { set; get; }
        public bool? IsDev { set; get; }
        public bool? IsTrans { set; get; }
        public bool? IsLeaveJob { set; get; }
        public bool? IsBSE { set; get; }
        public bool? IsApproved { set; get; }
        public ApprovedStatusEnum ApprovedStatus { set; get; }

        public List<string> TableItems { set; get; }
        public List<string> KeyItems { set; get; }

    }
}

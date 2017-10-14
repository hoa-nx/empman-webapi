using EmpMan.Common.ViewModels.Models.Common;
using System;
using System.ComponentModel.DataAnnotations;

namespace EmpMan.Web.Common.ViewModels.Schedule
{
    public class ScheduleViewModel : AuditableViewModel
    {
        /// <summary>
        /// Khóa chính (tự sinh)
        /// </summary>
        public int ID { set; get; }

        /// <summary>
        /// Người tạo
        /// </summary>
        public int CreatorID { set; get; }

        /// <summary>
        /// Tựa đề
        /// </summary>
        [MaxLength(512)]
        public string Title { set; get; }

        /// <summary>
        /// Mô tả
        /// </summary>
        public string Description { set; get; }

        /// <summary>
        /// Vị trí
        /// </summary>
        public string Location { set; get; }
        /// <summary>
        /// Loại
        /// </summary>
        public int Type { set; get; }

        /// <summary>
        /// Thời điểm bắt đầu
        /// </summary>
        public DateTime? TimeStart { set; get; }

        /// <summary>
        /// Thời điểm kết thúc
        /// </summary>
        public DateTime? TimeEnd { set; get; }

    }
}
using EmpMan.Common.ViewModels.Models.Common;

namespace EmpMan.Common.ViewModels.Models.Common
{
    public class FileResultViewModel : AuditableViewModel
    {
        /// <summary>
        /// ID của file sau khi upload
        /// </summary>
        public int? ID { set; get; }
        /// <summary>
        /// Tên file
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// Tên file
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Mở rộng của file
        /// </summary>
        public string FileExt { get; set; }

        /// <summary>
        /// content của file như text / zip
        /// </summary>
        public string ContentType { get; set; }

        /// <summary>
        /// Đường dẫn trên server ( chẳng hạn  : /UploadFiles/xxfolder/name.zip)
        /// 
        /// </summary>
        public string PathOnHost { get; set; }

        /// <summary>
        /// Tên table có chứa dữ liệu upload
        /// </summary>
        public string RelatedTable { get; set; }

        /// <summary>
        /// Key để liên kết với table có chứa dữ liệu upload
        /// </summary>
        public string RelatedKey { get; set; }

        /// <summary>
        /// mật khẩu đã mã hóa
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// chuỗi dùng để mã hóa
        /// </summary>
        public string HaltString { get; set; }

        /// <summary>
        /// Đường dẫn vật lý trỏ đến file ( bao gồm cả tên file)
        /// </summary>
        public string PhysicalFilePathOnHost { set; get; }
    }
}
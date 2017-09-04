using System;
using System.ComponentModel.DataAnnotations;

namespace EmpMan.Model.Abstract
{
    /// <summary>
    /// Interface các item dùng chung tại các table
    /// </summary>
    public interface IAuditable
    {
        /// <summary>
        /// concurrency  check 
        /// </summary>
        //[Timestamp]
        byte[] RowVersion { get; set; }
        /// <summary>
        /// Trình tự hiển thị
        /// </summary>
        int? DisplayOrder { set; get; }

        /// <summary>
        /// Người / tổ chức sở hữu dữ liệu
        /// </summary>
        string AccountData { set; get; }

        /// <summary>
        /// Ghi chú
        /// </summary>
        string Note { set; get; }

        /// <summary>
        /// Đối tượng có thể truy truy cập được dữ liệu.
        /// </summary>
        int? AccessDataLevel { set; get; }

        /// <summary>
        /// Người tạo
        /// </summary>
        string CreatedBy { set; get; }

        /// <summary>
        /// Ngày cập nhật
        /// </summary>
        DateTime? UpdatedDate { set; get; }

        /// <summary>
        /// Người cập nhật
        /// </summary>
        string UpdatedBy { set; get; }

        /// <summary>
        /// Khóa siêu dữ liệu
        /// </summary>
        string MetaKeyword { set; get; }

        /// <summary>
        /// Mô tả siêu dữ liệu
        /// </summary>
        string MetaDescription { set; get; }

        /// <summary>
        /// Trạng thái
        /// </summary>
        bool Status { set; get; }

        /// <summary>
        /// Trạng thái của dữ liệu ( 
        /// </summary>
        int? DataStatus { set; get; }

        /// <summary>
        /// Trình duyệt của user
        /// </summary>
        string UserAgent { set; get; }
        /// <summary>
        /// IP Host của user
        /// </summary>
        string UserHostAddress { set; get; }

        /// <summary>
        /// Tên host của user
        /// </summary>
        string UserHostName { set; get; }

        /// <summary>
        /// Ngày yêu cầu
        /// </summary>
        DateTime? RequestDate { set; get; }

        /// <summary>
        /// Người yêu cầu
        /// </summary>
        string RequestBy { set; get; }

        /// <summary>
        /// Ngày approved
        /// </summary>
        DateTime? ApprovedDate { set; get; }

        /// <summary>
        /// Người approved
        /// </summary>
        string ApprovedBy { set; get; }

        /// <summary>
        /// Trạng thái approved dữ liệu
        /// </summary>
        int? ApprovedStatus { set; get; }

        ///// <summary>
        ///// Item dự bị
        ///// </summary>
        //string Yobi_Text1 { set; get; }

        ///// <summary>
        ///// Item dự bị
        ///// </summary>
        //string Yobi_Text2 { set; get; }

        ///// <summary>
        ///// Item dự bị
        ///// </summary>
        //string Yobi_Text3 { set; get; }

        ///// <summary>
        ///// Item dự bị
        ///// </summary>
        //string Yobi_Text4 { set; get; }

        ///// <summary>
        ///// Item dự bị
        ///// </summary>
        //string Yobi_Text5 { set; get; }

        ///// <summary>
        ///// Item dự bị
        ///// </summary>
        //long? Yobi_Number1 { set; get; }

        ///// <summary>
        ///// Item dự bị
        ///// </summary>
        //long? Yobi_Number2 { set; get; }

        ///// <summary>
        ///// Item dự bị
        ///// </summary>
        //long? Yobi_Number3 { set; get; }

        ///// <summary>
        ///// Item dự bị
        ///// </summary>
        //long? Yobi_Number4 { set; get; }

        ///// <summary>
        ///// Item dự bị
        ///// </summary>
        //long? Yobi_Number5 { set; get; }

        ///// <summary>
        ///// Item dự bị
        ///// </summary>
        //decimal? Yobi_Decimal1 { set; get; }

        ///// <summary>
        ///// Item dự bị
        ///// </summary>
        //decimal? Yobi_Decimal2 { set; get; }

        ///// <summary>
        ///// Item dự bị
        ///// </summary>
        //decimal? Yobi_Decimal3 { set; get; }

        ///// <summary>
        ///// Item dự bị
        ///// </summary>
        //decimal? Yobi_Decimal4 { set; get; }

        ///// <summary>
        ///// Item dự bị
        ///// </summary>
        //decimal? Yobi_Decimal5 { set; get; }

        ///// <summary>
        ///// Item dự bị
        ///// </summary>
        //DateTime? Yobi_Date1 { set; get; }

        ///// <summary>
        ///// Item dự bị
        ///// </summary>
        //DateTime? Yobi_Date2 { set; get; }

        ///// <summary>
        ///// Item dự bị
        ///// </summary>
        //DateTime? Yobi_Date3 { set; get; }

        ///// <summary>
        ///// Item dự bị
        ///// </summary>
        //DateTime? Yobi_Date4 { set; get; }

        ///// <summary>
        ///// Item dự bị
        ///// </summary>
        //DateTime? Yobi_Date5 { set; get; }

        ///// <summary>
        ///// Ngày tạo
        ///// </summary>
        //DateTime? CreatedDate { set; get; }

    }
}
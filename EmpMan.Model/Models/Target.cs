using EmpMan.Model.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpMan.Model.Models
{
    /// <summary>
    /// Quản lý mục tiêu
    /// </summary>
    [Table("Targets")]
    public class Target : Auditable
    {
        /// <summary>
        /// Khóa chính tự sinh
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { set; get; }

        /// <summary>
        /// Công ty
        /// </summary>
        public int? CompanyID { set; get; }

        /// <summary>
        /// Bộ phận
        /// </summary>
        public int? DeptID { set; get; }

        /// <summary>
        /// Tổ
        /// </summary>
        public int? TeamID { set; get; }

        /// <summary>
        /// Mục tiêu cho tháng hay năm
        /// Nếu là năm thì cho tháng và ngày là 1 
        /// </summary>
        [Required]
        public DateTime YearMonth { set; get; }

        /// <summary>
        /// Tên mục tiêu
        /// </summary>
        [Required]
        [MaxLength(256)]
        public string Name { set; get; }
        /// <summary>
        /// Người lập kế hoạch( user  name)
        /// </summary>
        [MaxLength(256)]
        public string CreatorBy { set; get; }
        /// <summary>
        /// Ngày tạo
        /// </summary>
        public DateTime? CreateDate { set; get; }

        /// <summary>
        /// Hiệu suất hoạt động ( Mục tiêu)
        /// </summary>
        public decimal? Koritu { set; get; }

        /// <summary>
        /// Hiệu suất hoạt động (  Thực tế)
        /// </summary>
        public decimal? ActKoritu { set; get; }

        /// <summary>
        /// % thay đổi số NV so với năm trước
        /// </summary>
        public decimal? ChangePercentEmp { set; get; }

        /// <summary>
        /// Số lượng NV thay đổi so với năm trước
        /// </summary>
        public int? ChangeEmp { set; get; }

        /// <summary>
        /// Số Manager
        /// </summary>
        public int? ManagerEmp { set; get; }

        /// <summary>
        /// Só Leader2 
        /// </summary>
        public int? Leader2Emp { set; get; }

        /// <summary>
        /// Số L1
        /// </summary>
        public int? Leader1Emp { set; get; }

        /// <summary>
        /// Số lượng NV SL2
        /// </summary>
        public int? SubLeader2 { set; get; }

        /// <summary>
        /// Số lượng NV SL1
        /// </summary>
        public int? SubLeader1 { set; get; }

        /// <summary>
        /// Số nhân viên lập trình ( Mục tiêu)
        /// </summary>
        public int? DevEmp { set; get; }

        /// <summary>
        /// Số nhân viên PD  ( Mục tiêu)
        /// </summary>
        public int? TransEmp { set; get; }

        /// <summary>
        /// Số nhân viên khác  ( Mục tiêu)
        /// </summary>
        public int? OtherEmp { set; get; }

        /// <summary>
        /// % NV nghỉ việc( Mục tiêu)
        /// </summary>
        public decimal? LeaveJobPercentEmp { set; get; }

        /// <summary>
        /// Số NV nghỉ việc  ( Mục tiêu)
        /// </summary>
        public int? LeaveJobEmp { set; get; }

        /// <summary>
        /// % thay đổi số NV so với năm trước (thục tế)
        /// </summary>
        public decimal? ActChangePercentEmp { set; get; }

        /// <summary>
        /// Số lượng NV thay đổi so với năm trước (thục tế)
        /// </summary>
        public int? ActChangeEmp { set; get; }

        /// <summary>
        /// Số Manager (thục tế)
        /// </summary>
        public int? ActManagerEmp { set; get; }

        /// <summary>
        /// Só Leader2  (thục tế)
        /// </summary>
        public int? ActLeader2Emp { set; get; }

        /// <summary>
        /// Số L1 (thục tế)
        /// </summary>
        public int? ActLeader1Emp { set; get; }

        /// <summary>
        /// Số lượng NV SL2 (thục tế)
        /// </summary>
        public int? ActSubLeader2 { set; get; }

        /// <summary>
        /// Số lượng NV SL1 (thục tế)
        /// </summary>
        public int? ActSubLeader1 { set; get; }


        /// <summary>
        /// Số nhân viên lập trình ( thực tích)
        /// </summary>
        public int? ActDevEmp { set; get; }

        /// <summary>
        /// Số nhân viên PD  ( thực tích)
        /// </summary>
        public int? ActTransEmp { set; get; }

        /// <summary>
        /// Số nhân viên khác  ( thực tích)
        /// </summary>
        public int? ActOtherEmp { set; get; }

        /// <summary>
        /// % NV nghỉ việc thụ tế
        /// </summary>
        public decimal? ActLeaveJobPercentEmp { set; get; }


        /// <summary>
        /// Số NV nghỉ việc thực tế ( thực tích)
        /// </summary>
        public int? ActLeaveJobEmp { set; get; }


        /// <summary>
        /// % thay đổi so với năm trước
        /// </summary>
        public decimal? ChangePercentMM { set; get; }


        /// <summary>
        /// Số MM tương ứng thay đổi so với năm trước
        /// </summary>
        public decimal? ChangeMM { set; get; }


        /// <summary>
        /// Số MM báo giá
        /// </summary>
        public decimal? QuotationMM { set; get; }


        /// <summary>
        /// Số MM lập trình ( Mục tiêu)
        /// </summary>
        public decimal? DevMM { set; get; }

        /// <summary>
        /// Số MM PD  ( Mục tiêu)
        /// </summary>
        public decimal? TransMM { set; get; }

        /// <summary>
        /// Số MM OnsiteMM  ( Mục tiêu)
        /// </summary>
        public decimal? OnsiteMM { set; get; }

        /// <summary>
        /// Số MM Quản lý  ( Mục tiêu)
        /// </summary>
        public decimal? ManMM { set; get; }
        
        /// <summary>
        /// Số MM tổng  ( Mục tiêu)
        /// </summary>
        public decimal? TotalMM { set; get; }


        /// <summary>
        /// % thay đổi so với năm trước --thục tế
        /// </summary>
        public decimal? ActChangePercentMM { set; get; }


        /// <summary>
        /// Số MM tương ứng thay đổi so với năm trước --Thục tế
        /// </summary>
        public decimal? ActChangeMM { set; get; }


        /// <summary>
        /// Số MM báo giá -thực tế
        /// </summary>
        public decimal? ActQuotationMM { set; get; }

        /// <summary>
        /// Số MM lập trình (--thực tích)
        /// </summary>
        public decimal? ActDevMM { set; get; }

        /// <summary>
        /// Số MM PD  ( --thực tích)
        /// </summary>
        public decimal? ActTransMM { set; get; }

        /// <summary>
        /// Số MM OnsiteMM  ( thực tế)
        /// </summary>
        public decimal? ActOnsiteMM { set; get; }


        /// <summary>
        /// Số MM Quản lý  ( --thực tích)
        /// </summary>
        public decimal? ActManMM { set; get; }

        /// <summary>
        /// Số MM tổng  (--thực tích)
        /// </summary>
        public decimal? ActTotalMM { set; get; }


        /// <summary>
        /// Số nhân viên có N1  (Mục tiêu)
        /// </summary>
        public int? N1 { set; get; }

        /// <summary>
        /// Số nhân viên có N2  (Mục tiêu)
        /// </summary>
        public int? N2 { set; get; }

        /// <summary>
        /// Số nhân viên có N3  (Mục tiêu)
        /// </summary>
        public int? N3 { set; get; }

        /// <summary>
        /// Số nhân viên có N4  (Mục tiêu)
        /// </summary>
        public int? N4 { set; get; }

        /// <summary>
        /// Số nhân viên có N5  (Mục tiêu)
        /// </summary>
        public int? N5 { set; get; }

        /// <summary>
        /// Số nhân viên có N1  (thực tích)
        /// </summary>
        public int? ActN1 { set; get; }

        /// <summary>
        /// Số nhân viên có N2  (thực tích)
        /// </summary>
        public int? ActN2 { set; get; }

        /// <summary>
        /// Số nhân viên có N3  (thực tích)
        /// </summary>
        public int? ActN3 { set; get; }

        /// <summary>
        /// Số nhân viên có N4  (thực tích)
        /// </summary>
        public int? ActN4 { set; get; }

        /// <summary>
        /// Số nhân viên có N5  (thực tích)
        /// </summary>
        public int? ActN5 { set; get; }

        /// <summary>
        /// Số NV onsite dài hạn (mục tiêu)
        /// </summary>
        public int? LongOnsiterNumber { set; get; }

        /// <summary>
        /// Số NV onsite ngắn hạn (mục tiêu)
        /// </summary>
        public int? ShortOnsiterNumber { set; get; }

        /// <summary>
        /// Số NV đi intership (mục tiêu)
        /// </summary>
        public int? InterShipNumber { set; get; }

        /// <summary>
        /// Số NV onsite dài hạn (thực tích)
        /// </summary>
        public int? ActLongOnsiterNumber { set; get; }

        /// <summary>
        /// Số NV onsite ngắn hạn (thực tích)
        /// </summary>
        public int? ActShortOnsiterNumber { set; get; }

        /// <summary>
        /// Số NV đi intership (thực tích)
        /// </summary>
        public int? ActInterShipNumber { set; get; }

        /// <summary>
        /// Lý do đạt / không đạt mục tiêu
        /// </summary>
        public string Reason1 { set; get; }

        /// <summary>
        /// Lý do đạt / không đạt mục tiêu
        /// </summary>
        public string Reason2 { set; get; }

        /// <summary>
        /// Lý do đạt / không đạt mục tiêu
        /// </summary>
        public string Reason3 { set; get; }

        [ForeignKey("CompanyID")]
        public virtual Company Company { set; get; }

        [ForeignKey("DeptID")]
        public virtual Dept Dept { set; get; }

        [ForeignKey("TeamID")]
        public virtual Team Team { set; get; }

    }
}

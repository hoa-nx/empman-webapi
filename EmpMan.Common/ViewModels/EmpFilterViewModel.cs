using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpMan.Common.ViewModels
{
    public class EmpFilterViewModel
    {
        public bool chkDept { set; get; }

        public int[] selectDepts { set; get; }

        public bool chkTeam { set; get; }

        public int[] selectTeams { set; get; }
        
        public bool chkPosition { set; get; }

        public int[] selectPositions { set; get; }

        public bool chkJapaneseLevel { set; get; }

        public int[] selectJapaneseLevels { set; get; }
        
        public bool chkBussinessAllowanceLevel { set; get; }

        public int[] selectBussinessAllowanceLevels { set; get; }

        public bool chkBseLevel { set; get; }

        public int[] selectBseLevels { set; get; }

        public bool chkEmpType { set; get; }

        public int[] selectEmpTypes { set; get; }
        
        public bool chkStartWorkingDate { set; get; }
        public DateTime? startWorkingDateFrom { set; get; }
        public DateTime? startWorkingDateTo { set; get; }

        public bool chkContractDate { set; get; }
        public DateTime? contractDateFrom { set; get; }
        public DateTime? contractDateTo { set; get; }

        public bool chkTrialDate { set; get; }
        public DateTime? trialDateFrom { set; get; }
        public DateTime? trialDateTo { set; get; }

        public bool chkJobLeaveDate { set; get; }
        public DateTime? jobLeaveDateFrom { set; get; }
        public DateTime? jobLeaveDateTo { set; get; }

        public bool chkLearning { set; get; }
        public bool chkTrainingInclude { set; get; }
        public bool chkExperence { set; get; }
        /// <summary>
        ///  1 : nghỉ việc 
        ///  2 : bao gồm nhân viên trong kỳ
        ///  3 : Đang làm việc
        ///  4 : Sắp nghỉ
        ///  99 : tất cả
        /// </summary>
        public string selectDataTypes { set; get; }
        /// <summary>
        /// Trình tự sort
        /// </summary>
        public string[] sort { set; get; }
        /// <summary>
        /// Trị chung của hệ thống
        /// </summary>
        public SystemValueViewModel systemValue { set; get; }
        
    }
}

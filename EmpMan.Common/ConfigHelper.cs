using EmpMan.Common.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpMan.Common
{

    public class ConfigHelper
    {

        public static string GetByKey(string key)
        {
            return ConfigurationManager.AppSettings[key].ToString();
        }
        
        public static string GetEmpColumnNameBySortName(string sortName)
        {
            string value = "";
            switch (sortName.ToLower())
            {
                case CommonConstants.SORT_NAME_ID:
                    value = "ID";
                    break;
                case CommonConstants.SORT_NAME_FULLNAME:
                    value = "FullName";
                    break;
                case CommonConstants.SORT_NAME_ACCOUNT:
                    value = "AccountName";
                    break;
                case CommonConstants.SORT_NAME_BIRTHDAY:
                    value = "BirthDay";
                    break;
                case CommonConstants.SORT_NAME_CONTRACT_DATE:
                    value = "ContractDate";
                    break;
                case CommonConstants.SORT_NAME_DEPT:
                    value = "CurrentDeptID";
                    break;
                case CommonConstants.SORT_NAME_EMPTYPE:
                    value = "EmpTypeMasterDetailID";
                    break;
                case CommonConstants.SORT_NAME_ESTIMATE:
                    value = "";
                    break;
                case CommonConstants.SORT_NAME_GENDER:
                    value = "Gender";
                    break;
                case CommonConstants.SORT_NAME_JAPANESE_LEVEL:
                    value = "JapaneseLevelMasterDetailID";
                    break;
                case CommonConstants.SORT_NAME_NAME:
                    value = "Name";
                    break;
                case CommonConstants.SORT_NAME_POSITION:
                    value = "CurrentPositionID";
                    break;
                case CommonConstants.SORT_NAME_START_WORKING_DATE:
                    value = "StartWorkingDate";
                    break;
                case CommonConstants.SORT_NAME_TEAM:
                    value = "CurrentTeamID";
                    break;
                case CommonConstants.SORT_NAME_TRANING_DATE:
                    value = "StartTrialDate";
                    break;
            }
            return value;
        }
    }
}

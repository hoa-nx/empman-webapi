using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EmpMan.Common
{
    public static class StringHelper
    {
        public static string ToUnsignString(string input)
        {
            input = input.Trim();
            for (int i = 0x20; i < 0x30; i++)
            {
                input = input.Replace(((char)i).ToString(), " ");
            }
            input = input.Replace(".", "-");
            input = input.Replace(" ", "-");
            input = input.Replace(",", "-");
            input = input.Replace(";", "-");
            input = input.Replace(":", "-");
            input = input.Replace("  ", "-");
            Regex regex = new Regex(@"\p{IsCombiningDiacriticalMarks}+");
            string str = input.Normalize(NormalizationForm.FormD);
            string str2 = regex.Replace(str, string.Empty).Replace('đ', 'd').Replace('Đ', 'D');
            while (str2.IndexOf("?") >= 0)
            {
                str2 = str2.Remove(str2.IndexOf("?"), 1);
            }
            while (str2.Contains("--"))
            {
                str2 = str2.Replace("--", "-").ToLower();
            }
            return str2;
        }
        /// <summary>
        /// Replace HTML template with values
        /// </summary>
        /// <param name="template">Template content HTML</param>
        /// <param name="replacements">Dictionary with key/value</param>
        /// <returns></returns>
        public static string Parse(this string template, Dictionary<string, string> replacements)
        {
            if (replacements.Count > 0)
            {
                template = replacements.Keys
                            .Aggregate(template, (current, key) => current.Replace(key, replacements[key]));
            }
            return template;
        }

        public static string GetSqlStringFromArrayStr(string[] param)
        {
            string sql = "";

            if (param != null && param.Count() > 0)
            {
                for (int i = 0; i < param.Length; i++)
                {
                    if (i == 0)
                    {
                        sql = "'" + param[i] + "'";
                    }
                    else
                    {
                        sql += ",'" + param[i] + "'";
                    }
                }
            }
            return sql;
        }

        public static string GetSqlStringFromArrayInt(int?[] param)
        {
            string sql = "";

            if (param != null && param.Count() > 0)
            {
                for (int i = 0; i < param.Length; i++)
                {
                    if (i == 0)
                    {
                        sql = "" + param[i] + "";
                    }
                    else
                    {
                        sql += "," + param[i] + "";
                    }
                }
            }
            return sql;
        }

        public static string ListToStringSeperated(List<string> list , string seperated)
        {
            string returnStr = null;
            if(list==null || list.Count == 0)
            {
                returnStr= null;
            }else
            {
                returnStr = string.Join(seperated, list);
            }
            return returnStr;
        }

        public static List<string> SeperatedStringToList( string strSource, char seperated)
        {
            List<string> returnList = new List<string>();
            if (strSource == null || strSource.Trim().Length == 0)
            {
                returnList = null;
            }
            else
            {
                returnList = strSource.Split(seperated).ToList();
            }
            return returnList;
        }
        
        private static readonly string[] VietNamChar = new string[]
            {
                "aAeEoOuUiIdDyY",
                "áàạảãâấầậẩẫăắằặẳẵ",
                "ÁÀẠẢÃÂẤẦẬẨẪĂẮẰẶẲẴ",
                "éèẹẻẽêếềệểễ",
                "ÉÈẸẺẼÊẾỀỆỂỄ",
                "óòọỏõôốồộổỗơớờợởỡ",
                "ÓÒỌỎÕÔỐỒỘỔỖƠỚỜỢỞỠ",
                "úùụủũưứừựửữ",
                "ÚÙỤỦŨƯỨỪỰỬỮ",
                "íìịỉĩ",
                "ÍÌỊỈĨ",
                "đ",
                "Đ",
                "ýỳỵỷỹ",
                "ÝỲỴỶỸ"
            };
        public static string ToVietnameseUnsign(string str)
        {
            //Thay thế và lọc dấu từng char      
            for (int i = 1; i < VietNamChar.Length; i++)
            {
                for (int j = 0; j < VietNamChar[i].Length; j++)
                    str = str.Replace(VietNamChar[i][j], VietNamChar[0][i - 1]);
            }
            return str;
        }

    }
}

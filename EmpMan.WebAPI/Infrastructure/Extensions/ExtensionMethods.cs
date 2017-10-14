using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;
using EmpMan.Model.Models;
using EmpMan.Service;
using EmpMan.Web.Infrastructure.Core;
using System.Linq;
using System.Linq.Expressions;
using EmpMan.Common;
using System;
using System.Data;
using System.Reflection;
using EmpMan.Common.ViewModels.Models;

namespace EmpMan.Web.Infrastructure.Extensions
{
    public static class ExtensionMethods
    {
        public static AppUserViewModel GetApplicationUser(this System.Security.Principal.IIdentity identity)
        {
            AppUserViewModel appuser = new AppUserViewModel();
            if (identity.IsAuthenticated)
            {

                //using (var db = new AppContext())
                //{
                //    var userManager = new ApplicationUserManager(new ApplicationUserStore(db));
                //    return userManager.FindByName(identity.Name);
                //}
                var claims = ((System.Security.Claims.ClaimsIdentity)identity).Claims;

                //identity.AddClaim(new Claim("fullName", user.FullName));
                //identity.AddClaim(new Claim("avatar", avatar));
                //identity.AddClaim(new Claim("email", email));
                //identity.AddClaim(new Claim("username", user.UserName));

                appuser.FullName = claims.SingleOrDefault(m => m.Type.ToLower() == "fullname").ToString();
                appuser.Avatar = claims.SingleOrDefault(m => m.Type.ToLower() == "avatar").ToString();
                appuser.Email = claims.SingleOrDefault(m => m.Type.ToLower() == "email").Value;
                appuser.UserName = claims.SingleOrDefault(m => m.Type.ToLower() == "username").ToString();
                appuser.CompanyID = claims.SingleOrDefault(m => m.Type.ToLower() == "companyid").Value.ToInt(0);
                appuser.DeptID = claims.SingleOrDefault(m => m.Type.ToLower() == "deptid").Value.ToInt(0);
                appuser.TeamID = claims.SingleOrDefault(m => m.Type.ToLower() == "teamid").Value.ToInt(0);
                appuser.EmpID = claims.SingleOrDefault(m => m.Type.ToLower() == "empid").Value.ToInt(0);
                appuser.ProcessingYear = claims.SingleOrDefault(m => m.Type.ToLower() == "processingyear").Value.ToInt(DateTime.Now.Year);
            }

            return appuser;
        }

        public static Expression<T> Compose<T>(this Expression<T> first, Expression<T> second, Func<Expression, Expression, Expression> merge)
        {

            // build parameter map (from parameters of second to parameters of first)

            var map = first.Parameters.Select((f, i) => new { f, s = second.Parameters[i] }).ToDictionary(p => p.s, p => p.f);

            // replace parameters in the second lambda expression with parameters from the first

            var secondBody = ParameterRebinder.ReplaceParameters(map, second.Body);

            // apply composition of lambda expression bodies to parameters from the first expression 

            return Expression.Lambda<T>(merge(first.Body, secondBody), first.Parameters);

        }

        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> first, Expression<Func<T, bool>> second)
        {

            return first.Compose(second, Expression.And);

        }

        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> first, Expression<Func<T, bool>> second)
        {

            return first.Compose(second, Expression.Or);

        }

        /// <summary>
        /// This method builds a <see cref="System.Data.DataTable"/> from the existing source list.
        /// </summary>
        /// <typeparam name="T">The type of the items inside the source parameter. All instance properties of this type will be transferred.</typeparam>
        /// <param name="source">The source list.</param>
        /// <returns>A <see cref="System.Data.DataTable"/> with the data of <paramref name="source"/>.</returns>
        public static DataTable ToDataTable<T>(this IEnumerable<T> source)
        {
            /*
            var props = typeof(T).GetProperties();

            var dt = new DataTable();
            dt.Columns.AddRange(
              props.Select(p => new DataColumn(p.Name, p.PropertyType)).ToArray()
            );

            source.ToList().ForEach(
              i => dt.Rows.Add(props.Select(p => p.GetValue(i, null)).ToArray())
            );

            return dt;
            */

            DataTable dtReturn = new DataTable();

            // column names 
            PropertyInfo[] oProps = null;
            FieldInfo[] oField = null;
            if (source == null) return dtReturn;

            foreach (T rec in source)
            {
                // Use reflection to get property names, to create table, Only first time, others will follow 
                if (oProps == null)
                {
                    oProps = ((Type)rec.GetType()).GetProperties();
                    foreach (PropertyInfo pi in oProps)
                    {
                        Type colType = pi.PropertyType;

                        if ((colType.IsGenericType) && (colType.GetGenericTypeDefinition() == typeof(Nullable<>)))
                        {
                            colType = colType.GetGenericArguments()[0];
                        }

                        dtReturn.Columns.Add(new DataColumn(pi.Name, colType));
                    }
                    oField = ((Type)rec.GetType()).GetFields();
                    foreach (FieldInfo fieldInfo in oField)
                    {
                        Type colType = fieldInfo.FieldType;

                        if ((colType.IsGenericType) && (colType.GetGenericTypeDefinition() == typeof(Nullable<>)))
                        {
                            colType = colType.GetGenericArguments()[0];
                        }

                        dtReturn.Columns.Add(new DataColumn(fieldInfo.Name, colType));
                    }
                }

                DataRow dr = dtReturn.NewRow();

                if (oProps != null)
                {
                    foreach (PropertyInfo pi in oProps)
                    {
                        dr[pi.Name] = pi.GetValue(rec, null) ?? DBNull.Value;
                    }
                }
                if (oField != null)
                {
                    foreach (FieldInfo fieldInfo in oField)
                    {
                        dr[fieldInfo.Name] = fieldInfo.GetValue(rec) ?? DBNull.Value;
                    }
                }
                dtReturn.Rows.Add(dr);
            }
            return dtReturn;

        }

        public static DateTime? ToDateTime(this string s)
        {
            DateTime dtr;
            var tryDtr = DateTime.TryParse(s, out dtr);
            return (tryDtr) ? dtr : new DateTime?();
        }

        public static DateTime? ToDateTime(this object s)
        {
            DateTime dtr ;
            if (s == null) return new DateTime?();

            var tryDtr = DateTime.TryParse(s.ToString(), out dtr);
            return (tryDtr) ? dtr : new DateTime?();
        }

        /// <summary>
        /// Returns a formatted date or emtpy string
        /// </summary>
        /// <param name="t">DateTime instance or null</param>
        /// <param name="format">datetime formatstring </param>
        /// <returns></returns>
        public static string ToString(this DateTime? t, string format)
        {
            if (t != null)
            {
                return t.Value.ToString(format);
            }

            return "";
        }
        public static string NullDateToString(this DateTime? dt, string format = "yyyy/MM/dd", string nullResult = "")
        {
            if (dt.HasValue)
                return dt.Value.ToString(format);
            else
                return nullResult;
        }

        /// <summary>
        /// DateDiff in SQL style. 
        /// Datepart implemented: 
        ///     "year" (abbr. "yy", "yyyy"), 
        ///     "quarter" (abbr. "qq", "q"), 
        ///     "month" (abbr. "mm", "m"), 
        ///     "day" (abbr. "dd", "d"), 
        ///     "week" (abbr. "wk", "ww"), 
        ///     "hour" (abbr. "hh"), 
        ///     "minute" (abbr. "mi", "n"), 
        ///     "second" (abbr. "ss", "s"), 
        ///     "millisecond" (abbr. "ms").
        /// </summary>
        /// <param name="DatePart"></param>
        /// <param name="EndDate"></param>
        /// <returns></returns>
        public static Int64 DateDiff(this DateTime StartDate, String DatePart, DateTime EndDate)
        {
            Int64 DateDiffVal = 0;
            System.Globalization.Calendar cal = System.Threading.Thread.CurrentThread.CurrentCulture.Calendar;
            TimeSpan ts = new TimeSpan(EndDate.Ticks - StartDate.Ticks);
            switch (DatePart.ToLower().Trim())
            {
                #region year
                case "year":
                case "yy":
                case "yyyy":
                    DateDiffVal = (Int64)(cal.GetYear(EndDate) - cal.GetYear(StartDate));
                    break;
                #endregion

                #region quarter
                case "quarter":
                case "qq":
                case "q":
                    DateDiffVal = (Int64)((((cal.GetYear(EndDate)
                                        - cal.GetYear(StartDate)) * 4)
                                        + ((cal.GetMonth(EndDate) - 1) / 3))
                                        - ((cal.GetMonth(StartDate) - 1) / 3));
                    break;
                #endregion

                #region month
                case "month":
                case "mm":
                case "m":
                    DateDiffVal = (Int64)(((cal.GetYear(EndDate)
                                        - cal.GetYear(StartDate)) * 12
                                        + cal.GetMonth(EndDate))
                                        - cal.GetMonth(StartDate));
                    break;
                #endregion

                #region day
                case "day":
                case "d":
                case "dd":
                    DateDiffVal = (Int64)ts.TotalDays;
                    break;
                #endregion

                #region week
                case "week":
                case "wk":
                case "ww":
                    DateDiffVal = (Int64)(ts.TotalDays / 7);
                    break;
                #endregion

                #region hour
                case "hour":
                case "hh":
                    DateDiffVal = (Int64)ts.TotalHours;
                    break;
                #endregion

                #region minute
                case "minute":
                case "mi":
                case "n":
                    DateDiffVal = (Int64)ts.TotalMinutes;
                    break;
                #endregion

                #region second
                case "second":
                case "ss":
                case "s":
                    DateDiffVal = (Int64)ts.TotalSeconds;
                    break;
                #endregion

                #region millisecond
                case "millisecond":
                case "ms":
                    DateDiffVal = (Int64)ts.TotalMilliseconds;
                    break;
                #endregion

                default:
                    throw new Exception(String.Format("DatePart \"{0}\" is unknown", DatePart));
            }
            return DateDiffVal;
        }
        static public int Age(this DateTime dateOfBirth)
        {
            if (DateTime.Today.Month < dateOfBirth.Month ||
            DateTime.Today.Month == dateOfBirth.Month &&
             DateTime.Today.Day < dateOfBirth.Day)
            {
                return DateTime.Today.Year - dateOfBirth.Year - 1;
            }
            else
                return DateTime.Today.Year - dateOfBirth.Year;
        }

        public static Boolean IsBetween(this DateTime dt, DateTime startDate, DateTime endDate, Boolean compareTime = false)
        {
            return compareTime ?
               dt >= startDate && dt <= endDate :
               dt.Date >= startDate.Date && dt.Date <= endDate.Date;
        }

        public static bool IsInRange(this DateTime currentDate, DateTime beginDate, DateTime endDate)
        {
            return (currentDate >= beginDate && currentDate <= endDate);
        }
        public static DateTime EndOfTheMonth(this DateTime date)
        {
            var endOfTheMonth = new DateTime(date.Year, date.Month, 1)
                .AddMonths(1)
                .AddDays(-1);

            return endOfTheMonth;
        }

        public static DateTime BeginningOfTheMonth(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, 1);
        }

        /// <summary>
        /// Compute dateTime difference
        /// Alex-LEWIS, 2015-08-11
        /// </summary>
        /// <param name="dt1"></param>
        /// <param name="dt2"></param>
        /// <returns></returns>
        public static int GetMonthDiff(this DateTime dt1, DateTime dt2)
        {
            var l = dt1 < dt2 ? dt1 : dt2;
            var r = dt1 >= dt2 ? dt1 : dt2;
            return (l.Day == r.Day ? 0 : l.Day > r.Day ? 0 : 1)
              + (l.Month == r.Month ? 0 : r.Month - l.Month)
              + (l.Year == r.Year ? 0 : (r.Year - l.Year) * 12);
        }

        static public DateTime NextAnniversary(this DateTime dt, DateTime eventDate, bool preserveMonth = false)
        {
            DateTime calcDate;

            if (dt.Date < eventDate.Date) // Return the original event date if it occurs later than initial input date.
                return new DateTime(eventDate.Year, eventDate.Month, eventDate.Day, 0, 0, 0, dt.Kind);

            calcDate = new DateTime(dt.Year + (dt.Month < eventDate.Month || dt.Month == eventDate.Month && dt.Day < eventDate.Day ? 0 : 1), eventDate.Month, 1, 0, 0, 0, dt.Kind).AddDays(eventDate.Day - 1);

            if (eventDate.Month == calcDate.Month || !preserveMonth)
                return calcDate;
            else
                return calcDate.AddYears(dt.Month == 2 && dt.Day == 28 ? 1 : 0).AddDays(-1);
        }
        static public DateTime NextAnniversary(this DateTime dt, int eventMonth, int eventDay, bool preserveMonth = false)
        {
            DateTime calcDate;

            if (eventDay > 31 || eventDay < 1 || eventMonth > 12 || eventMonth < 1 ||
               ((eventMonth == 4 || eventMonth == 6 || eventMonth == 9 || eventMonth == 11) && eventDay > 30) ||
               (eventMonth == 2 && eventDay > 29))
                throw new Exception("Invalid combination of Event Year and Event Month.");

            calcDate = new DateTime(dt.Year + (dt.Month < eventMonth || dt.Month == eventMonth && dt.Day < eventDay ? 0 : 1), eventMonth, 1, 0, 0, 0, dt.Kind).AddDays(eventDay - 1);

            if (eventMonth == calcDate.Month || !preserveMonth)
                return calcDate;
            else
                return calcDate.AddYears(dt.Month == 2 && dt.Day == 28 ? 1 : 0).AddDays(-1);
        }

        /// <summary>
        /// Compute dateTime difference precisely
        /// Alex-LEWIS, 2015-08-11
        /// </summary>
        /// <param name="dt1"></param>
        /// <param name="dt2"></param>
        /// <returns></returns>
        public static double GetTotalMonthDiff(this DateTime dt1, DateTime dt2)
        {
            var l = dt1 < dt2 ? dt1 : dt2;
            var r = dt1 >= dt2 ? dt1 : dt2;
            var lDfM = DateTime.DaysInMonth(l.Year, l.Month);
            var rDfM = DateTime.DaysInMonth(r.Year, r.Month);

            var dayFixOne = l.Day == r.Day
              ? 0d
              : l.Day > r.Day
                ? r.Day * 1d / rDfM - l.Day * 1d / lDfM
                : (r.Day - l.Day) * 1d / rDfM;

            return dayFixOne
              + (l.Month == r.Month ? 0 : r.Month - l.Month)
              + (l.Year == r.Year ? 0 : (r.Year - l.Year) * 12);
        }


        public static int ToInt(this string number, int defaultInt)
        {
            int resultNum = defaultInt;
            try
            {
                if (number != null && !number.ToString().All(char.IsDigit)) return defaultInt;
                if (!string.IsNullOrEmpty(number))
                    resultNum = Convert.ToInt32(number);
            }
            catch
            {
            }
            return resultNum;
        }

        public static int ToInt(this object number, int defaultInt)
        {
            if (number == null  ) return defaultInt;
            if (number != null && !number.ToString().All(char.IsDigit)) return defaultInt;

            int resultNum = defaultInt;
            try
            {
                if (!string.IsNullOrEmpty(number.ToString()))
                    resultNum = Convert.ToInt32(number);
            }
            catch
            {
            }
            return resultNum;
        }

        public static decimal ToDecimal(this object number, decimal defaultDecimal)
        {
            if (number == null) return defaultDecimal;
            decimal resultNum = defaultDecimal;
            try
            {
                if (!string.IsNullOrEmpty(number.ToString()))
                    resultNum = Convert.ToDecimal(number);
            }
            catch
            {
            }
            return resultNum;
        }

        public static bool IsNullOrBlank(this String text)
        {
            return text == null || text.Trim().Length == 0;
        }

        public static string NullOrBlankToValue(this String text , string nullValue)
        {
            string returnVal = text;

            if (text.IsNullOrBlank())
                returnVal = nullValue;

            return returnVal;
        }

    }

    public class ParameterRebinder : ExpressionVisitor
    {

        private readonly Dictionary<ParameterExpression, ParameterExpression> map;



        public ParameterRebinder(Dictionary<ParameterExpression, ParameterExpression> map)
        {

            this.map = map ?? new Dictionary<ParameterExpression, ParameterExpression>();

        }

        public static Expression ReplaceParameters(Dictionary<ParameterExpression, ParameterExpression> map, Expression exp)
        {

            return new ParameterRebinder(map).Visit(exp);

        }

        protected override Expression VisitParameter(ParameterExpression p)
        {

            ParameterExpression replacement;

            if (map.TryGetValue(p, out replacement))
            {

                p = replacement;

            }

            return base.VisitParameter(p);

        }

    }
}
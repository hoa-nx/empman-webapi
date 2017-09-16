using System.Collections.Generic;
using EmpMan.Common.ViewModels;
using EmpMan.Data.Repositories;
using System;

namespace EmpMan.Service
{
    public interface IStatisticService
    {
        IEnumerable<RevenueStatisticViewModel> GetRevenueStatistic(string fromDate, string toDate);

        IEnumerable<StatisticViewModel> GetCurrentTotalMMAndTargetMMStatistic();

        IEnumerable<StatisticViewModel> GetMMByTypeAndYearMonthStatistic(int?[] years, bool isUnpivotColumnToRows);

        IEnumerable<ReportStatisticViewModel> GetMMByTypeAndYearMonthStatisticReport(int year);

        IEnumerable<StatisticViewModel> GetCompareMMByTypeAndYearMonthStatistic(int prevYear, int curYear);
        IEnumerable<StatisticViewModel> GetCompareCutommerMMByTypeAndYearMonthStatistic(int curYear);
        IEnumerable<StatisticViewModel> GetCompareCutommerMMByTypeAndYearMonthStatistic(int prevYear, int curYear);
        IEnumerable<WorkEmpTypeStatisticViewModel> GetEmpCountMonthlyStatistic(int? companyID , int? deptID, int? teamID, DateTime startDate, DateTime endDate);
    }

    public class StatisticService : IStatisticService
    {
        private IOrderRepository _orderRepository;
        private ITargetRepository _targetRepository;

        public StatisticService(IOrderRepository orderRepository , ITargetRepository targetRepository)
        {
            _orderRepository = orderRepository;
            _targetRepository = targetRepository;
        }

        public IEnumerable<RevenueStatisticViewModel> GetRevenueStatistic(string fromDate, string toDate)
        {
            return _orderRepository.GetRevenueStatistic(fromDate, toDate);
        }

        public IEnumerable<StatisticViewModel> GetCurrentTotalMMAndTargetMMStatistic()
        {
            return _targetRepository.GetCurrentTotalMMAndTargetMMStatistic();
        }

        public IEnumerable<StatisticViewModel> GetMMByTypeAndYearMonthStatistic(int?[] years, bool isUnpivotColumnToRows)
        {
            return _targetRepository.GetMMByTypeAndYearMonthStatistic(years , isUnpivotColumnToRows);
        }

        public IEnumerable<ReportStatisticViewModel> GetMMByTypeAndYearMonthStatisticReport(int year)
        {
            return _targetRepository.GetMMByTypeAndYearMonthStatisticReport(year);
        }

        public IEnumerable<StatisticViewModel> GetCompareMMByTypeAndYearMonthStatistic(int prevYear, int curYear)
        {
            return _targetRepository.GetCompareMMByTypeAndYearMonthStatistic(prevYear, curYear);
        }

        public IEnumerable<StatisticViewModel> GetCompareCutommerMMByTypeAndYearMonthStatistic( int curYear)
        {
            return _targetRepository.GetCompareCutommerMMByTypeAndYearMonthStatistic(curYear);
        }

        public IEnumerable<StatisticViewModel> GetCompareCutommerMMByTypeAndYearMonthStatistic(int prevYear, int curYear)
        {
            return _targetRepository.GetCompareCutommerMMByTypeAndYearMonthStatistic(prevYear, curYear);
        }

        public IEnumerable<WorkEmpTypeStatisticViewModel> GetEmpCountMonthlyStatistic(int? companyID, int? deptID, int? teamID, DateTime startDate, DateTime endDate)
        {
            return _targetRepository.GetEmpCountMonthlyStatistic(companyID, deptID, teamID, startDate, endDate);
        }
    }
}
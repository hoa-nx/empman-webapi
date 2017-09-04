using System.Collections.Generic;
using EmpMan.Common.ViewModels;
using EmpMan.Data.Repositories;

namespace EmpMan.Service
{
    public interface IStatisticService
    {
        IEnumerable<RevenueStatisticViewModel> GetRevenueStatistic(string fromDate, string toDate);

        IEnumerable<StatisticViewModel> GetCurrentTotalMMAndTargetMMStatistic();

        IEnumerable<StatisticViewModel> GetMMByTypeAndYearMonthStatistic(int?[] years, bool isUnpivotColumnToRows);

        IEnumerable<ReportStatisticViewModel> GetMMByTypeAndYearMonthStatisticReport(int year);

        IEnumerable<StatisticViewModel> GetCompareMMByTypeAndYearMonthStatistic(int prevYear, int curYear);
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
    }
}
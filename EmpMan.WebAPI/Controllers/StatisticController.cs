using System.Net;
using System.Net.Http;
using System.Web.Http;
using EmpMan.Service;
using EmpMan.Web.Infrastructure.Core;
using Microsoft.Reporting.WebForms;
using System;
using System.Net.Http.Headers;
using System.Collections.Generic;
using EmpMan.Common.ViewModels;
using EmpMan.Web.Infrastructure.Extensions;
using System.IO;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

namespace EmpMan.Web.Controllers
{
    [RoutePrefix("api/statistic")]
    public class StatisticController : ApiControllerBase
    {
        private IStatisticService _statisticService;

        public StatisticController(IErrorService errorService, IStatisticService statisticService) : base(errorService)
        {
            _statisticService = statisticService;
        }

        [Route("getrevenue")]
        [HttpGet]
        public HttpResponseMessage GetRevenueStatistic(HttpRequestMessage request, string fromDate, string toDate)
        {
            return CreateHttpResponse(request, () =>
            {
                var model = _statisticService.GetRevenueStatistic(fromDate, toDate);
                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, model);
                return response;
            });
        }

        [Route("getcurmmandtargetmm")]
        [HttpGet]
        public HttpResponseMessage GetCurrentTotalMMAndTargetMMStatistic(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                var model = _statisticService.GetCurrentTotalMMAndTargetMMStatistic();
                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, model);
                return response;
            });
        }

        [Route("getmmbytypeandyearmonth")]
        [HttpPost]
        public HttpResponseMessage GetMMByTypeAndYearMonthStatistic(HttpRequestMessage request , SearchItemViewModel searchParams )
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.BadRequest, "Tham số không đúng");
                if (searchParams != null)
                {
                    int?[] years = searchParams.NumberItems.ToArray();
                    bool isUnpivotColumnToRows = searchParams.BoolItems[0].Value;
                    var model = _statisticService.GetMMByTypeAndYearMonthStatistic(years, isUnpivotColumnToRows);
                    response = request.CreateResponse(HttpStatusCode.OK, model);
                }
                
                return response;
            });
        }

        /// <summary>
        /// So sanh doanh giua cac nam voi nhau
        /// </summary>
        /// <param name="request"></param>
        /// <param name="searchParams"></param>
        /// <returns></returns>
        [Route("getmmbytypeandyearmonthcompare")]
        [HttpPost]
        public HttpResponseMessage GetCompareMMByTypeAndYearMonthStatistic(HttpRequestMessage request, SearchItemViewModel searchParams)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.BadRequest, "Tham số không đúng");
                if (searchParams != null)
                {
                    int?[] years = searchParams.NumberItems.ToArray();
                    var model = _statisticService.GetCompareMMByTypeAndYearMonthStatistic(years[0].Value, years[1].Value);
                    response = request.CreateResponse(HttpStatusCode.OK, model);
                }

                return response;
            });
        }

        /// <summary>
        /// Doanh so theo tung khach hang
        /// </summary>
        /// <param name="request"></param>
        /// <param name="searchParams"></param>
        /// <returns></returns>
        [Route("getmmbytypeandyearmonthcomparecustomer")]
        [HttpPost]
        public HttpResponseMessage GetCompareCutommerMMByTypeAndYearMonthStatistic(HttpRequestMessage request, SearchItemViewModel searchParams)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.BadRequest, "Tham số không đúng");
                if (searchParams != null)
                {
                    int?[] years = searchParams.NumberItems.ToArray();
                    if(years!=null && years.Length>0)
                    {
                        if (years.Length == 1)
                        {
                            //thong ke doanh so cua tung khach hang trong 1 nam
                            var model = _statisticService.GetCompareCutommerMMByTypeAndYearMonthStatistic(years[0].Value);
                            response = request.CreateResponse(HttpStatusCode.OK, model);
                        }
                        else
                        {
                            //so sanh giua 2 nam voi nhau theo tung khach hang
                            var model = _statisticService.GetCompareCutommerMMByTypeAndYearMonthStatistic(years[0].Value, years[1].Value);
                            response = request.CreateResponse(HttpStatusCode.OK, model);
                        }

                    }else
                    {
                        //lay theo tat ca cac nam co trong DB
                        //var model = _statisticService.GetCompareCutommerMMByTypeAndYearMonthStatistic(null);
                    }

                }

                return response;
            });
        }

        /// <summary>
        /// Doanh so theo tung khach hang
        /// </summary>
        /// <param name="request"></param>
        /// <param name="searchParams"></param>
        /// <returns></returns>
        [Route("getempcountbymonthly")]
        [HttpPost]
        public HttpResponseMessage GetEmpCountMonthlyStatistic(HttpRequestMessage request, SearchItemViewModel searchParams)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.BadRequest, "Tham số không đúng");
                if (searchParams != null)
                {
                    DateTime?[] date= searchParams.DateTimeItems.ToArray();
                    if (date != null && date.Length > 0)
                    {
                        var model = _statisticService.GetEmpCountMonthlyStatistic( User.Identity.GetApplicationUser().CompanyID , User.Identity.GetApplicationUser().DeptID, User.Identity.GetApplicationUser().TeamID,  date[0].Value, date[1].Value);
                        response = request.CreateResponse(HttpStatusCode.OK, model);
                    }
                    else
                    {
                        //lay theo tat ca cac nam co trong DB
                        //var model = _statisticService.GetCompareCutommerMMByTypeAndYearMonthStatistic(null);
                    }

                }

                return response;
            });
        }


        [Route("getmmbytypeandyearmonthreport")]
        [HttpGet]
        public HttpResponseMessage GenerateReport()
        {
            // Generate the report data.
            var reportData = GetReportBytes();
            /*
            //https://stackoverflow.com/questions/36042614/how-to-return-a-pdf-from-a-web-api-application
            //http://brmorris.blogspot.com/2017/02/download-pdf-in-angular-2.html
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.BadRequest);
            byte[] buffer = new byte[0];

            MemoryStream memoryStream = new MemoryStream(reportData,0, reportData.Length, true,true);
            
            //get buffer
            buffer = memoryStream.GetBuffer();
            //content length for use in header
            var contentLength = buffer.Length;

            var statuscode = HttpStatusCode.OK;
            response = Request.CreateResponse(statuscode);
            response.Content = new StreamContent(new MemoryStream(buffer));

            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");
            //response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
            response.Content.Headers.ContentLength = contentLength;
            /*
            ContentDispositionHeaderValue contentDisposition = null;
            if (ContentDispositionHeaderValue.TryParse("inline; filename=RevenueReport.pdf", out contentDisposition))
            {
                response.Content.Headers.ContentDisposition = contentDisposition;
            }
            */
                        

            // Create response using the report bytes.
            // Response headers are set to return a PDF document.

            var response = new HttpResponseMessage(HttpStatusCode.OK) { Content = new ByteArrayContent(reportData) };
            //response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment") { FileName = "RevenueReport.pdf" };
            response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("inline") { FileName = "RevenueReport.pdf" };
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");


            return response;
        }

        private  byte[] GetReportBytes()
        {
            // Get a fake datatable with dummy data.
            IEnumerable<ReportStatisticViewModel> data = _statisticService.GetMMByTypeAndYearMonthStatisticReport(2017);

            var demoReportData = data.ToDataTable<ReportStatisticViewModel>();


            // Create our local report object to build our report on.
            var localReport = new LocalReport
            {
                DisplayName = "Report",
                ReportEmbeddedResource = "EmpMan.Web.Reports.rptRevenueByCompanyDeptInYear.rdlc",
                EnableHyperlinks = true
            };

            // Pass custom parameters to our report.
            //var reportParameterCollection = new ReportParameterCollection
            //{
            //    new ReportParameter("GeneratedBy", "Ross Steytler"),
            //    new ReportParameter("GeneratedDate", DateTime.Now.ToShortDateString())
            //};
            //localReport.SetParameters(reportParameterCollection);

            // Create a ReportDataSource object to add to the local report object.
            var reportDataSource = new ReportDataSource("rptRevenueByCompanyDeptInYear", demoReportData);
            localReport.DataSources.Add(reportDataSource);

            // Render the report as PDF.
            Warning[] warnings;
            string[] streams;
            string mimeType, encoding, fileNameExtension;
            var renderedReport = localReport.Render("PDF", null, out mimeType, out encoding, out fileNameExtension, out streams, out warnings);

            return renderedReport;
        }


        [Route("getmmbytypeandyearmonthreport2")]
        [HttpGet]
        public HttpResponseMessage GenerateReport2(HttpRequestMessage request, int year)
        {
            return CreateHttpResponse(request, () =>
            {
                // Generate the report data.
                var reportData = GetPdfReport(year);

                // Create response using the report bytes.
                // Response headers are set to return a PDF document.

                var response = new HttpResponseMessage(HttpStatusCode.OK) { Content = new ByteArrayContent(reportData) };
                //response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment") { FileName = "RevenueReport.pdf" };
                response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("inline") { FileName = "RevenueReport.pdf" };
                response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");


                return response;
            });


            
        }
        private byte[] GetPdfReport(int year)
        {
            ReportDocument reportDocument = new ReportDocument();
            var rootPath = System.Web.Hosting.HostingEnvironment.MapPath("~/Reports");

            reportDocument.Load( Path.Combine(rootPath, "rptRevenueByCompanyDeptInYear.rpt"));

            IEnumerable<ReportStatisticViewModel> data = _statisticService.GetMMByTypeAndYearMonthStatisticReport(year);

            var demoReportData = data.ToDataTable<ReportStatisticViewModel>();

            reportDocument.SetDataSource(demoReportData);

            //reportDocument.SummaryInfo.ReportTitle = "BÁO CÁO DOANH SỐ THEO TỪNG BỘ PHẬN NĂM " + year;
            CrystalDecisions.CrystalReports.Engine.TextObject txtReportHeader;
            txtReportHeader = reportDocument.ReportDefinition.ReportObjects["txtReportTitle"] as TextObject;
            txtReportHeader.Text = "BÁO CÁO DOANH SỐ TỪNG BỘ PHẬN NĂM " + year;

            var stream = reportDocument.ExportToStream(ExportFormatType.PortableDocFormat);
   
            byte[] result;
            using (var streamReader = new MemoryStream())
            {
                stream.CopyTo(streamReader);
                result = streamReader.ToArray();
            }

            return result;
        }


    }
}
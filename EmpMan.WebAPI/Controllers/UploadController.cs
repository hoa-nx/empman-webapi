using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using EmpMan.Service;
using EmpMan.Web.Infrastructure.Core;
using EmpMan.Model.Models;
using EmpMan.Web.Infrastructure.Extensions;

using EmpMan.Common;
using EmpMan.Web.Providers;
using System.Configuration;
using System.Text;
using EmpMan.Common.ViewModels.Models.Common;

namespace EmpMan.Web.Controllers
{
    [RoutePrefix("api/upload")]
    [Authorize]
    public class UploadController : ApiControllerBase
    {
        private IFileStorageService _dataService;
        public UploadController(IErrorService errorService , IFileStorageService dataService) : base(errorService)
        {
            this._dataService = dataService;

        }

        [HttpPost]
        [Route("saveImage")]
        [Permission(Action = FunctionActions.CREATE, Function = FunctionConstants.FILE)]
        public HttpResponseMessage SaveImage(string type = "")
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            try
            {

                var httpRequest = HttpContext.Current.Request;

                foreach (string file in httpRequest.Files)
                {
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created);

                    var postedFile = httpRequest.Files[file];
                    if (postedFile != null && postedFile.ContentLength > 0)
                    {

                        int MaxContentLength = 1024 * 1024 * 10; //Size = 10 MB

                        IList<string> AllowedFileExtensions = new List<string> { ".jpg", ".gif", ".png" };
                        var ext = postedFile.FileName.Substring(postedFile.FileName.LastIndexOf('.'));
                        var extension = ext.ToLower();
                        if (!AllowedFileExtensions.Contains(extension))
                        {

                            var message = string.Format("Please Upload image of type .jpg,.gif,.png.");

                            dict.Add("error", message);
                            return Request.CreateResponse(HttpStatusCode.BadRequest, dict);
                        }
                        else if (postedFile.ContentLength > MaxContentLength)
                        {

                            var message = string.Format("Please Upload a file upto 1 mb.");

                            dict.Add("error", message);
                            return Request.CreateResponse(HttpStatusCode.BadRequest, dict);
                        }
                        else
                        {
                            string directory = string.Empty;
                            if (type == "avatar")
                            {
                                directory = "/UploadedFiles/Avatars/";
                            }
                            else if (type == "product")
                            {
                                directory = "/UploadedFiles/Products/";
                            }
                            else if (type == "news")
                            {
                                directory = "/UploadedFiles/News/";
                            }
                            else if (type == "banner")
                            {
                                directory = "/UploadedFiles/Banners/";
                            }
                            else
                            {
                                directory = "/UploadedFiles/";
                            }
                            if (!Directory.Exists(HttpContext.Current.Server.MapPath(directory)))
                            {
                                Directory.CreateDirectory(HttpContext.Current.Server.MapPath(directory));
                            }

                            string path = Path.Combine(HttpContext.Current.Server.MapPath(directory), postedFile.FileName);
                            //Userimage myfolder name where i want to save my image
                            postedFile.SaveAs(path);
                            return Request.CreateResponse(HttpStatusCode.OK, Path.Combine(directory, postedFile.FileName));
                        }
                    }

                    var message1 = string.Format("Image Updated Successfully.");
                    return Request.CreateErrorResponse(HttpStatusCode.Created, message1); ;
                }
                var res = string.Format("Please Upload a image.");
                dict.Add("error", res);
                
                return Request.CreateResponse(HttpStatusCode.NotFound, dict);

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex); ;

            }
        }


        /// <summary>
        /// Upload các file lên server và lưu vào DB
        /// </summary>
        /// <param name="type">Loại file upload</param>
        /// <returns>Trả về đối tượng file đã được upload (ID : ID của file trong DB , PathOnHost : path trên server</returns>
        [HttpPost]
        [Route("upload")]
        [Permission(Action = FunctionActions.CREATE, Function = FunctionConstants.FILE)]
        public HttpResponseMessage UploadFile(string type = "")
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            try
            {
                var relatedTable = "";
                var relatedKey = "";
                var empID = "";

                var httpRequest = HttpContext.Current.Request;

                foreach (string file in httpRequest.Files)
                {
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created);
                    FileResultViewModel result = new FileResultViewModel();

                    var postedFile = httpRequest.Files[file];
                    if (postedFile != null && postedFile.ContentLength > 0)
                    {

                        int MaxContentLength = 1024 * 1024 * 10; //Size = 10 MB

                        IList<string> AllowedFileExtensions = new List<string> { ".pdf", ".xls", ".xlsx", ".doc", ".docx", ".zip", ".7z", ".rar", ".txt" , ".jpg", ".gif", ".png" };
                        var ext = postedFile.FileName.Substring(postedFile.FileName.LastIndexOf('.'));
                        var extension = ext.ToLower();
                        if (!AllowedFileExtensions.Contains(extension))
                        {

                            var message = string.Format("Chỉ có thể upload các file với định dạng .pdf,.xls,.xlsx,.doc,.docx,.zip,.7z , .txt");

                            dict.Add("error", message);
                            return Request.CreateResponse(HttpStatusCode.BadRequest, dict);
                        }
                        else if (postedFile.ContentLength > MaxContentLength)
                        {

                            var message = string.Format("Kích thước file tối đa là 10 Mb.");

                            dict.Add("error", message);
                            return Request.CreateResponse(HttpStatusCode.BadRequest, dict);
                        }
                        else
                        {

                            string directory = string.Empty;
                            //get params
                            //if (httpRequest.Params["EmpID"] == null)
                            //{
                            //    Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Không xác định được mã nhân viên");
                            //}
                            //else
                            //{
                            //    empID = httpRequest.Params["EmpID"].ToString();
                            //    relatedKey = empID;
                            //}

                            //relate Key
                            if (httpRequest.Params["relatedKey"] == null)
                            {
                                //Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Không xác định được mã nhân viên");
                            }
                            else
                            {
                                relatedKey = httpRequest.Params["relatedKey"].ToString();
                            }

                            //relateTables 
                            if (httpRequest.Params["relatedTable"] == null)
                            {
                                //Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Không xác định được mã nhân viên");
                            }
                            else
                            {
                                relatedTable = httpRequest.Params["relatedTable"].ToString();
                            }

                            if (type.ToLower() == "avatar".ToLower())
                            {
                                directory = "/UploadedFiles/Avatars/";
                            }
                            else if (type.ToLower() == "product".ToLower())
                            {
                                directory = "/UploadedFiles/Products/";
                            }
                            else if (type.ToLower() == "news".ToLower())
                            {
                                directory = "/UploadedFiles/News/";
                            }
                            else if (type.ToLower() == "banner".ToLower())
                            {
                                directory = "/UploadedFiles/Banners/";
                            }
                            else if (type.ToLower() == "emp".ToLower())
                            {
                                relatedTable = "Emps";
                                directory = "/UploadedFiles/Emps/";
                            }
                            else if (type.ToLower() == "recruitment".ToLower())
                            {
                                relatedTable = "Recruitments";
                                directory = "/UploadedFiles/Recruitments/";
                            }
                            else if (type.ToLower() == "recruitmentStaff")
                            {
                                directory = "/UploadedFiles/Recruitments/";
                            }
                            else if (type.ToLower() == "revenue")
                            {
                                relatedTable = "Revenues".ToLower();
                                directory = "/UploadedFiles/Revenues/";
                            }
                            else if (type.ToLower() == "estimate".ToLower())
                            {
                                directory = "/UploadedFiles/Estimates/";
                            }
                            else if (type.ToLower() == "OrderReceived".ToLower())
                            {
                                directory = "/UploadedFiles/Estimates/";
                            }
                            else

                            {
                                directory = "/UploadedFiles/";
                            }
                            if (!Directory.Exists(HttpContext.Current.Server.MapPath(directory)))
                            {
                                Directory.CreateDirectory(HttpContext.Current.Server.MapPath(directory));
                            }

                            string path = Path.Combine(HttpContext.Current.Server.MapPath(directory), postedFile.FileName);
                            //Userimage myfolder name where i want to save my image
                            postedFile.SaveAs(path);
                            //update to database
                            result.PathOnHost = Path.Combine(directory, postedFile.FileName);

                            FileStorage fileStorage = SaveFileToDb(postedFile, relatedTable, relatedKey, result.PathOnHost);

                            result.ID = fileStorage.ID;
                            result.FileName = fileStorage.FileName;
                            result.RelatedTable = fileStorage.RelatedTable;
                            result.RelatedKey = fileStorage.RelatedKey;
                            result.FileExt = fileStorage.FileExt;
                            result.ContentType = fileStorage.ContentType;

                            //return Request.CreateResponse(HttpStatusCode.OK, result);
                        }
                    }

                }

                var message1 = string.Format("Upload file thành công.");
                return Request.CreateErrorResponse(HttpStatusCode.Created, message1); ;

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex); ;

            }
        }

        private FileStorage SaveFileToDb(HttpPostedFile file , string relatedTable ="", string relatedKey ="" , string pathOnHost="")
        {
            FileStorage newData = new FileStorage();

            /** cập nhật các thông tin chung **/
            newData.CreatedDate = DateTime.Now;
            newData.CreatedBy = User.Identity.Name;

            newData.UpdatedDate = DateTime.Now;
            newData.UpdatedBy = User.Identity.Name;
            //Người sở hữu dữ liệu
            newData.AccountData = User.Identity.GetApplicationUser().Email;

            newData.ContentType = file.ContentType;
            newData.FileName = file.FileName;
            newData.FileExt= Path.GetExtension(file.FileName).ToLower();

            newData.PathOnHost = pathOnHost;
            newData.RelatedTable = relatedTable;
            newData.RelatedKey = relatedKey;

            Stream str = file.InputStream;
            BinaryReader Br = new BinaryReader(str);
            Byte[] data = Br.ReadBytes((Int32)str.Length);
            //ma hoa file truoc khi luu 
            byte[] pw = Encoding.UTF8.GetBytes(ConfigurationManager.AppSettings["Password"].ToString());
            byte[] halt = Encoding.UTF8.GetBytes(CommonConstants.SecKeyString);
            
            newData.Data = StringCipher.AESEncryptBytes( data , pw, halt);
            newData.HaltString = CommonConstants.HaltString;

            var filestorage = _dataService.Add(newData);
            _dataService.SaveChanges();

            return filestorage;
        }


    }
}
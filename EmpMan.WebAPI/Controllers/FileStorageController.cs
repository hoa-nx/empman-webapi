using AutoMapper;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EmpMan.Model.Models;
using EmpMan.Service;
using EmpMan.Web.Infrastructure.Core;
using EmpMan.Web.Infrastructure.Extensions;

using EmpMan.Web.Providers;
using System.Linq;
using System;
using EmpMan.Common.ViewModels.Models.File;
using System.IO;
using System.Web;
using System.Net.Http.Headers;
using System.Web.Script.Serialization;
using System.IO.Compression;
using Ionic.Zip;
using System.Configuration;
using EmpMan.Common.ViewModels;
using EmpMan.Common;
using System.Text;
using Mapster;
using EmpMan.Common.ViewModels.Models.Common;

namespace EmpMan.Web.Controllers
{
    [RoutePrefix("api/filestorage")]
    [Authorize]
    public class FileStorageController : ApiControllerBase
    {
        private IFileStorageService _dataService;

        public FileStorageController(IErrorService errorService, IFileStorageService dataService) :
            base(errorService)
        {
            this._dataService = dataService;
        }

        [Route("getall")]
        [HttpGet]
        [Permission(Action = FunctionActions.READ, Function = FunctionConstants.FILE)]
        public HttpResponseMessage GetAll(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                var listData = _dataService.GetAll();

                //var listDataVm = Mapper.Map<List<FileStorageViewModel>>(listData);
                var listDataVm = listData.Adapt<List<FileStorageViewModel>>();

                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, listDataVm);

                return response;
            });
        }

        [Route("getallbykey")]
        [HttpGet]
        [Permission(Action = FunctionActions.READ, Function = FunctionConstants.FILE)]
        public HttpResponseMessage GetAllByKey(HttpRequestMessage request, string table , string key)
        {
            return CreateHttpResponse(request, () =>
            {
                var listData = _dataService.GetAllByKey(table, key).ToList();

                //ReducedAutoMapper.Instance.CreateMap<FileStorage, FileResultViewModel>();
                //var listDataVm = ReducedAutoMapper.Instance.MapList<FileStorage, FileResultViewModel>(listData.ToList());

                //var listDataVm = Mapper.Map<List<FileResultViewModel>>(listData);
                var listDataVm = listData.Adapt<List<FileStorage> , List<FileResultViewModel>>();
                
                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, listDataVm);

                return response;
            });
        }

        [Route("getallpagingfileresultmodel")]
        [HttpPost]
        [Permission(Action = FunctionActions.READ, Function = FunctionConstants.FILE)]
        public HttpResponseMessage GetAllPagingFileResultModel(HttpRequestMessage request, SearchItemViewModel searchParam)
        {
            return CreateHttpResponse(request, () =>
            {
                int totalRow = 0;
                int page = 0;
                int pageSize = 0;
                string tableName = "";
                string keyRelation ="";

                var response = request.CreateResponse(HttpStatusCode.BadRequest, "Không get được data");
                if (searchParam != null)
                {
                    page = searchParam.Page.Value;
                    pageSize = searchParam.PageSize.Value;
                    //get  table name 
                    if (searchParam.TableItems != null && searchParam.TableItems.Count>0)
                    {
                        tableName = StringHelper.GetSqlStringFromArrayStr(searchParam.TableItems.ToArray());

                    }
                    //get key items
                    if (searchParam.KeyItems!=null & searchParam.KeyItems.Count > 0)
                    {
                        keyRelation = StringHelper.GetSqlStringFromArrayStr(searchParam.KeyItems.ToArray());
                    }

                    string sql = @"SELECT 
	                                  FIL.[ID]
                                      ,FIL.[FileName]
                                      ,FIL.[Name]
                                      ,FIL.[FileExt]
                                      ,FIL.[ContentType]
                                      ,FIL.[PathOnHost]
                                      ,FIL.[RelatedTable]
                                      ,FIL.[RelatedKey]
                                    FROM
	                                    FileStorages FIL 
                                    WHERE 1 =1 
                                    ";

                    if ( tableName.Length>0){
                        sql += " AND FIL.RelatedTable IN ( " + tableName + ")";
                    }

                    if(keyRelation.Length > 0){
                        sql += " AND FIL.RelatedKey IN ( " + keyRelation + ")";
                    }
                                        
                    var model = _dataService.GetDbContext().Database.SqlQuery<FileResultViewModel>(sql).AsEnumerable();

                    totalRow = model.Count();
                    var query = model.OrderByDescending(x => x.CreatedDate).Skip((page - 1) * pageSize).Take(pageSize).ToList();
                    var paginationSet = new PaginationSet<FileResultViewModel>()
                    {
                        Items = query,
                        PageIndex = page,
                        TotalRows = totalRow,
                        PageSize = pageSize
                    };
                    response = request.CreateResponse(HttpStatusCode.OK, paginationSet);
                }

                return response;
            });
        }

        [Route("getallpaging")]
        [HttpGet]
        [Permission(Action = FunctionActions.READ, Function = FunctionConstants.FILE)]
        public HttpResponseMessage GetAll(HttpRequestMessage request, string keyword, int page, int pageSize = 20)
        {
            return CreateHttpResponse(request, () =>
            {
                int totalRow = 0;
                var model = _dataService.GetAll(keyword);

                totalRow = model.Count();

                var query = model.OrderByDescending(x => x.CreatedDate).Skip((page - 1) * pageSize).Take(pageSize).ToList();

                //var responseData = Mapper.Map<List<FileStorage>, List<FileStorageViewModel>>(query);
                var responseData = query.Adapt<List<FileStorage>, List<FileStorageViewModel>>();

                var paginationSet = new PaginationSet<FileStorageViewModel>()
                {
                    Items = responseData,
                    PageIndex = page,
                    TotalRows = totalRow,
                    PageSize = pageSize
                };
                var response = request.CreateResponse(HttpStatusCode.OK, paginationSet);
                return response;
            });
        }

        [Route("getfilebinaybyid/{id:int}")]
        [HttpGet]
        [Permission(Action = FunctionActions.READ, Function = FunctionConstants.FILE)]
        public HttpResponseMessage GetFileBinaryById(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                var model = _dataService.GetById(id);

                //ma hoa file truoc khi luu 
                byte[] pw = Encoding.UTF8.GetBytes(ConfigurationManager.AppSettings["Password"].ToString());
                byte[] halt = Encoding.UTF8.GetBytes(CommonConstants.SecKeyString);

                byte[] bData = StringCipher.AESDecryptBytes(model.Data, pw, halt);

                using (MemoryStream ms = new MemoryStream())
                {
                    //var responseData = Mapper.Map<FileStorage, FileStorageViewModel>(model);
                    var response = request.CreateResponse(HttpStatusCode.OK, ms);
                    response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(model.ContentType);

                    return response;
                }
            });
        }
        [Route("getfileusetcbyid/{id:int}")]
        [HttpGet]
        [Permission(Action = FunctionActions.READ, Function = FunctionConstants.FILE)]
        public HttpResponseMessage GetFileUseStreamContentById(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                var model = _dataService.GetById(id);
                byte[] bData = model.Data;

                if (!string.IsNullOrEmpty(model.HaltString))
                {
                    //ma hoa file truoc khi luu 
                    byte[] pw = Encoding.UTF8.GetBytes(ConfigurationManager.AppSettings["Password"].ToString());
                    byte[] halt = Encoding.UTF8.GetBytes(CommonConstants.SecKeyString);
                    bData = StringCipher.AESDecryptBytes(model.Data , pw, halt);
                }

                FileResultViewModel saveFile = saveFileToDisk(model);

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StreamContent(new FileStream(saveFile.PhysicalFilePathOnHost, FileMode.Open, FileAccess.Read));
                response.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment");
                response.Content.Headers.ContentDisposition.FileName =model.FileName  ;
                response.Content.Headers.ContentType = new MediaTypeHeaderValue( model.ContentType);

                return response;
            });
        }

        [Route("getfileusebacbyid/{id:int}")]
        [HttpGet]
        [Permission(Action = FunctionActions.READ, Function = FunctionConstants.FILE)]
        public HttpResponseMessage GetFileUseByteArrayContentById(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                var model = _dataService.GetById(id);

                byte[] bData = model.Data;

                if (!string.IsNullOrEmpty(model.HaltString))
                {
                    //ma hoa file truoc khi luu 
                    byte[] pw = Encoding.UTF8.GetBytes(ConfigurationManager.AppSettings["Password"].ToString());
                    byte[] halt = Encoding.UTF8.GetBytes(CommonConstants.SecKeyString);
                    bData = StringCipher.AESDecryptBytes(model.Data, pw, halt);
                }

                var response = new HttpResponseMessage(HttpStatusCode.OK) { Content = new ByteArrayContent(bData) };
                response.Content.Headers.ContentDisposition =
                new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment")
                {
                    FileName = model.FileName
                    
                };

                response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(model.ContentType);
                //response.Headers.Add("fileName", model.FileName);
                return response;

            });
        }

        [Route("downloadmulti")]
        [HttpPost]
        [Permission(Action = FunctionActions.READ, Function = FunctionConstants.FILE)]
        public HttpResponseMessage DownloadMulti(HttpRequestMessage request, int[] checkedItems)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                string archiveName = String.Format("archive-{0}.zip", DateTime.Now.ToString("yyyy-MMM-dd-HHmmss"));
                //get folder path 
                string directory = "/DownloadFiles/";
                string zipFilePath = Path.Combine(HttpContext.Current.Server.MapPath(directory), archiveName);

                if (!ModelState.IsValid)
                {
                    response = request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    //var listData = new JavaScriptSerializer().Deserialize<List<int>>(checkedItems);
                    List<string> listFile = new List<string>();
                    if (checkedItems.Count() > 0)
                    {
                        using (var zip = new ZipFile())
                        {
                            zip.CompressionLevel = Ionic.Zlib.CompressionLevel.BestCompression;
                            zip.Password = ConfigurationManager.AppSettings["Password"].ToString();
                            zip.Encryption = EncryptionAlgorithm.WinZipAes256;

                            foreach (var item in checkedItems)
                            {
                                //tao file zip
                                var model = _dataService.GetById(item);

                                byte[] bData = model.Data;

                                FileResultViewModel saveFile = saveFileToDisk(model);
                                listFile.Add(saveFile.PhysicalFilePathOnHost);
                                zip.AddFile(saveFile.PhysicalFilePathOnHost, "");

                            }
                            zip.Save(zipFilePath);

                            using (MemoryStream ms = new MemoryStream())
                            {
                                using (FileStream file = new FileStream(zipFilePath, FileMode.Open, FileAccess.Read))
                                {
                                    byte[] bytes = new byte[file.Length];
                                    file.Read(bytes, 0, (int)file.Length);
                                    ms.Write(bytes, 0, (int)file.Length);

                                    response = new HttpResponseMessage();
                                    response.Content = new ByteArrayContent(bytes.ToArray());
                                    response.Content.Headers.Add("x-filename", archiveName);
                                    response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                                    response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
                                    response.Content.Headers.ContentDisposition.FileName = archiveName;
                                    response.StatusCode = HttpStatusCode.OK;
                                }

                            }//using memory

                        }//if using zip

                    }//if checkItems

                }//if model

                return response;
            });
        }

        protected HttpResponseMessage ZipContentResult(ZipFile zipFile)
        {
            var pushStreamContent = new PushStreamContent((stream, content, context) =>
            {
                zipFile.Save(stream);
                stream.Close();
            }, "application/zip");

            return new HttpResponseMessage(HttpStatusCode.OK) { Content = pushStreamContent };
        }

        [Route("getfilenamebyid/{id:int}")]
        [HttpGet]
        [Permission(Action = FunctionActions.READ, Function = FunctionConstants.FILE)]
        public HttpResponseMessage GetFileNameById(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                var model = _dataService.GetById(id);

                byte[] bData = model.Data;

                if (!string.IsNullOrEmpty(model.HaltString))
                {
                    //ma hoa file truoc khi luu 
                    byte[] pw = Encoding.UTF8.GetBytes(ConfigurationManager.AppSettings["Password"].ToString());
                    byte[] halt = Encoding.UTF8.GetBytes(CommonConstants.SecKeyString);
                    bData = StringCipher.AESDecryptBytes(model.Data, pw, halt);
                }

                FileResultViewModel saveFile = saveFileToDisk(model);

                return Request.CreateResponse(HttpStatusCode.OK, saveFile.PathOnHost);
            });
        }


        /// <summary>
        /// Lưu file xuống ổ đĩa và trả về file đã được save
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        private FileResultViewModel saveFileToDisk(FileStorage file )
        {
            string directory = string.Empty;
            directory = "/DownloadFiles/";
            if (!Directory.Exists(HttpContext.Current.Server.MapPath(file.PathOnHost)))
            {
                Directory.CreateDirectory(HttpContext.Current.Server.MapPath(directory));
            }

            string path = Path.Combine(HttpContext.Current.Server.MapPath(directory), file.FileName);

            using (FileStream fs = new FileStream(path, FileMode.Create, FileAccess.ReadWrite))
            {
                using (BinaryWriter bw = new BinaryWriter(fs))
                {
                    byte[] bData = file.Data;

                    if (!string.IsNullOrEmpty(file.HaltString))
                    {
                        //giai ma file truoc khi luu 
                        byte[] pw = Encoding.UTF8.GetBytes(ConfigurationManager.AppSettings["Password"].ToString());
                        byte[] halt = Encoding.UTF8.GetBytes(CommonConstants.SecKeyString);
                        bData = StringCipher.AESDecryptBytes(file.Data, pw, halt);
                    }

                    bw.Write(bData);
                }
            }
            //return value
            return new FileResultViewModel
            {
                ID = file.ID,
                PathOnHost = file.PathOnHost,
                PhysicalFilePathOnHost = path,
                HaltString = file.HaltString
            };

        }

        [Route("detail/{id:int}")]
        [HttpGet]
        [Permission(Action = FunctionActions.READ, Function = FunctionConstants.FILE)]
        public HttpResponseMessage GetById(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                var model = _dataService.GetById(id);

                if (!string.IsNullOrEmpty(model.HaltString))
                {
                    //giai ma file truoc khi luu 
                    byte[] pw = Encoding.UTF8.GetBytes(ConfigurationManager.AppSettings["Password"].ToString());
                    byte[] halt = Encoding.UTF8.GetBytes(CommonConstants.SecKeyString);
                    model.Data = StringCipher.AESDecryptBytes(model.Data, pw, halt);
                }

                var responseData = Mapper.Map<FileStorage, FileStorageViewModel>(model);

                var response = request.CreateResponse(HttpStatusCode.OK, responseData);

                return response;
            });
        }


        [Route("add")]
        [HttpPost]
        [Permission(Action = FunctionActions.CREATE, Function = FunctionConstants.FILE)]
        public HttpResponseMessage Create(HttpRequestMessage request, FileStorageViewModel dataVm)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (!ModelState.IsValid)
                {
                    request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    FileStorage newData = new FileStorage();

                    /** cập nhật các thông tin chung **/
                    dataVm.CreatedDate = DateTime.Now;
                    dataVm.CreatedBy = User.Identity.Name;

                    dataVm.UpdatedDate = DateTime.Now;
                    dataVm.UpdatedBy = User.Identity.Name;
                    //Người sở hữu dữ liệu
                    dataVm.AccountData = User.Identity.GetApplicationUser().Email;
                    

                    newData.UpdateFileStorage(dataVm);

                    var data = _dataService.Add(newData);
                    _dataService.SaveChanges();

                    response = request.CreateResponse(HttpStatusCode.Created, data);
                }
                return response;
            });
        }

        [Route("update")]
        [HttpPut]
        [Permission(Action = FunctionActions.UPDATE, Function = FunctionConstants.FILE)]
        public HttpResponseMessage Update(HttpRequestMessage request, FileStorageViewModel dataVm)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (!ModelState.IsValid)
                {
                    request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    var dataFromDb = _dataService.GetById(dataVm.ID);

                    dataFromDb.UpdateFileStorage(dataVm);

                    dataFromDb.UpdatedDate = DateTime.Now;
                    dataFromDb.UpdatedBy = User.Identity.Name;
                    //Người sở hữu dữ liệu
                    dataFromDb.AccountData = User.Identity.GetApplicationUser().Email;

                    _dataService.Update(dataFromDb);
                    _dataService.SaveChanges();

                    var responseData = Mapper.Map<FileStorage, FileStorageViewModel>(dataFromDb);
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                }
                return response;
            });
        }

        [Route("delete")]
        [HttpDelete]
        [Permission(Action = FunctionActions.DELETE, Function = FunctionConstants.FILE)]
        public HttpResponseMessage Delete(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (!ModelState.IsValid)
                {
                    request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {

                    response = request.CreateResponse(HttpStatusCode.OK);

                    var oldDataFromDb = _dataService.Delete(id);
                    _dataService.SaveChanges();

                    var responseData = Mapper.Map<FileStorage, FileStorageViewModel>(oldDataFromDb);

                    response = request.CreateResponse(HttpStatusCode.Created, responseData);

                }
                return response;
            });
        }

        [Route("deletemulti")]
        [HttpDelete]
        [Permission(Action = FunctionActions.DELETE, Function = FunctionConstants.FILE)]
        public HttpResponseMessage DeleteMulti(HttpRequestMessage request, string checkedItems)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (!ModelState.IsValid)
                {
                    response = request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    var listData = new JavaScriptSerializer().Deserialize<List<int>>(checkedItems);
                    foreach (var item in listData)
                    {
                        _dataService.Delete(item);
                    }

                    _dataService.SaveChanges();

                    response = request.CreateResponse(HttpStatusCode.OK, listData.Count);
                }

                return response;
            });
        }

    }

    public static class ZipHelper
    {
        public readonly static string ZipPassword = ConfigurationManager.AppSettings["Password"].ToString();

        public static bool Zip(string filePath, string TargetDirectory)
        {
            if (File.Exists(filePath))
                throw new ApplicationException("");
            if (Directory.Exists(TargetDirectory))
                throw new ApplicationException("");

            bool rv = false;
            try
            {
                using (ZipFile zip = new ZipFile())
                {
                    zip.Password = ZipPassword;
                    zip.Comment = "";
                    zip.AddFile(filePath, "Files");
                    zip.Save(TargetDirectory);
                }
            }
            catch (Exception ex)
            {
                rv = false;
            }

            return rv;
        }

        public static bool Zip(string[] fileNames, string TargetDirectory)
        {
            bool rv = false;
            try
            {
                for (int i = 0; i < fileNames.Count(); i++)
                {
                    Zip(fileNames[i], TargetDirectory);
                }
            }
            catch (Exception ex)
            {
                rv = false;
            }

            return rv;
        }

        public static bool UnZip(string TargetDirectory, string filePath)
        {
            bool rv = false;
            try
            {
                using (ZipFile zip = ZipFile.Read(filePath))
                {
                    foreach (ZipEntry e in zip)
                    {
                        e.ExtractWithPassword(TargetDirectory, ZipPassword);
                    }
                }
            }
            catch (Exception ex)
            {
                rv = false;
            }

            return rv;
        }
    }
}
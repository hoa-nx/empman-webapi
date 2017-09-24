using System;
using System.Globalization;
using EmpMan.Model.Models;
using EmpMan.Web.Models;
using EmpMan.Web.Models.Emp;
using EmpMan.Web.Models.Project;
using EmpMan.Web.Models.Master;
using EmpMan.Web.Models.Revenue;
using EmpMan.Web.Models.File;
using System.Linq;
using EmpMan.Web.Models.Schedule;
using EmpMan.Common.ViewModels;

namespace EmpMan.Web.Infrastructure.Extensions
{
    public static class EntityExtensions
    {
        public static void UpdatePostCategory(this PostCategory postCategory, PostCategoryViewModel postCategoryVm)
        {
            postCategory.ID = postCategoryVm.ID;
            postCategory.Name = postCategoryVm.Name;
            postCategory.Description = postCategoryVm.Description;
            postCategory.Alias = postCategoryVm.Alias;
            postCategory.ParentID = postCategoryVm.ParentID;
            postCategory.DisplayOrder = postCategoryVm.DisplayOrder;
            postCategory.Image = postCategoryVm.Image;
            postCategory.HomeFlag = postCategoryVm.HomeFlag;

            postCategory.CreatedDate = postCategoryVm.CreatedDate;
            postCategory.CreatedBy = postCategoryVm.CreatedBy;
            postCategory.UpdatedDate = postCategoryVm.UpdatedDate;
            postCategory.UpdatedBy = postCategoryVm.UpdatedBy;
            postCategory.MetaKeyword = postCategoryVm.MetaKeyword;
            postCategory.MetaDescription = postCategoryVm.MetaDescription;
            postCategory.Status = postCategoryVm.Status;

        }

        public static void UpdateProductCategory(this ProductCategory productCategory, ProductCategoryViewModel productCategoryVm)
        {
            productCategory.ID = productCategoryVm.ID;
            productCategory.Name = productCategoryVm.Name;
            productCategory.Description = productCategoryVm.Description;
            productCategory.Alias = productCategoryVm.Alias;
            productCategory.ParentID = productCategoryVm.ParentID;
            productCategory.DisplayOrder = productCategoryVm.DisplayOrder;
            productCategory.HomeOrder = productCategoryVm.HomeOrder;
            productCategory.Image = productCategoryVm.Image;
            productCategory.HomeFlag = productCategoryVm.HomeFlag;
            productCategory.HomeOrder = productCategoryVm.HomeOrder;
            productCategory.CreatedDate = productCategoryVm.CreatedDate;
            productCategory.CreatedBy = productCategoryVm.CreatedBy;
            productCategory.UpdatedDate = productCategoryVm.UpdatedDate;
            productCategory.UpdatedBy = productCategoryVm.UpdatedBy;
            productCategory.MetaKeyword = productCategoryVm.MetaKeyword;
            productCategory.MetaDescription = productCategoryVm.MetaDescription;
            productCategory.Status = productCategoryVm.Status;
        }

        public static void UpdatePost(this Post post, PostViewModel postVm)
        {
            post.ID = postVm.ID;
            post.Name = postVm.Name;
            post.Description = postVm.Description;
            post.Alias = postVm.Alias;
            post.CategoryID = postVm.CategoryID;
            post.Content = postVm.Content;
            post.Image = postVm.Image;
            post.HomeFlag = postVm.HomeFlag;
            post.ViewCount = postVm.ViewCount;

            post.CreatedDate = postVm.CreatedDate;
            post.CreatedBy = postVm.CreatedBy;
            post.UpdatedDate = postVm.UpdatedDate;
            post.UpdatedBy = postVm.UpdatedBy;
            post.MetaKeyword = postVm.MetaKeyword;
            post.MetaDescription = postVm.MetaDescription;
            post.Status = postVm.Status;
        }

        public static void UpdateProduct(this Product product, ProductViewModel productVm)
        {
            product.ID = productVm.ID;
            product.Name = productVm.Name;
            product.Description = productVm.Description;
            product.Alias = productVm.Alias;
            product.CategoryID = productVm.CategoryID;
            product.Content = productVm.Content;
            product.ThumbnailImage = productVm.ThumbnailImage;
            product.Price = productVm.Price;
            product.PromotionPrice = productVm.PromotionPrice;
            product.Warranty = productVm.Warranty;
            product.HomeFlag = productVm.HomeFlag;
            product.HotFlag = productVm.HotFlag;
            product.ViewCount = productVm.ViewCount;

            product.CreatedDate = productVm.CreatedDate;
            product.CreatedBy = productVm.CreatedBy;
            product.UpdatedDate = productVm.UpdatedDate;
            product.UpdatedBy = productVm.UpdatedBy;
            product.MetaKeyword = productVm.MetaKeyword;
            product.MetaDescription = productVm.MetaDescription;
            product.Status = productVm.Status;
            product.Tags = productVm.Tags;
            product.OriginalPrice = productVm.OriginalPrice;
        }

        public static void UpdateFeedback(this Feedback feedback, FeedbackViewModel feedbackVm)
        {
            feedback.Name = feedbackVm.Name;
            feedback.Email = feedbackVm.Email;
            feedback.Message = feedbackVm.Message;
            feedback.Status = feedbackVm.Status;
            feedback.CreatedDate = DateTime.Now;
        }

        public static void UpdateProductQuantity(this ProductQuantity quantity, ProductQuantityViewModel quantityVm)
        {
            quantity.ColorId = quantityVm.ColorId;
            quantity.ProductId = quantityVm.ProductId;
            quantity.SizeId = quantityVm.SizeId;
            quantity.Quantity = quantityVm.Quantity;
        }
        public static void UpdateOrder(this Order order, OrderViewModel orderVm)
        {
            order.CustomerName = orderVm.CustomerName;
            order.CustomerAddress = orderVm.CustomerAddress;
            order.CustomerEmail = orderVm.CustomerEmail;
            order.CustomerMobile = orderVm.CustomerMobile;
            order.CustomerMessage = orderVm.CustomerMessage;
            order.PaymentMethod = orderVm.PaymentMethod;
            order.CreatedDate = DateTime.Now;
            order.CreatedBy = orderVm.CreatedBy;
            order.PaymentStatus = orderVm.PaymentStatus;
            order.Status = orderVm.Status;
            order.CustomerId = orderVm.CustomerId;
        }

        public static void UpdateProductImage(this ProductImage image, ProductImageViewModel imageVm)
        {
            image.ProductId = imageVm.ProductId;
            image.Path = imageVm.Path;
            image.Caption = imageVm.Caption;
        }
        public static void UpdateFunction(this Function function, FunctionViewModel functionVm)
        {
            function.Name = functionVm.Name;
            function.DisplayOrder = functionVm.DisplayOrder;
            function.IconCss = functionVm.IconCss;
            function.Status = functionVm.Status;
            function.ParentId = functionVm.ParentId;
            function.Status = functionVm.Status;
            function.URL = functionVm.URL;
            function.ID = functionVm.ID;
        }
        public static void UpdatePermission(this Permission permission, PermissionViewModel permissionVm)
        {
            permission.RoleId = permissionVm.RoleId;
            permission.FunctionId = permissionVm.FunctionId;
            permission.CanCreate = permissionVm.CanCreate;
            permission.CanDelete = permissionVm.CanDelete;
            permission.CanRead = permissionVm.CanRead;
            permission.CanUpdate = permissionVm.CanUpdate;
        }

        public static void UpdateApplicationRole(this AppRole appRole, ApplicationRoleViewModel appRoleViewModel, string action = "add")
        {
            if (action == "update")
                appRole.Id = appRoleViewModel.Id;
            else
                appRole.Id = Guid.NewGuid().ToString();
            appRole.Name = appRoleViewModel.Name;
            appRole.Description = appRoleViewModel.Description;
        }

        public static void UpdateUser(this AppUser appUser, AppUserViewModel appUserViewModel, string action = "add")
        {
            appUser.Id = appUserViewModel.Id;
            appUser.FullName = appUserViewModel.FullName;
            if (!string.IsNullOrEmpty(appUserViewModel.BirthDay))
            {
                //DateTime dateTime = DateTime.ParseExact(appUserViewModel.BirthDay, "dd/MM/yyyy", new CultureInfo("vi-VN"));
                DateTime dateTime = DateTime.ParseExact(appUserViewModel.BirthDay, "yyyy/MM/dd", new CultureInfo("jp-JP"));
                appUser.BirthDay = dateTime;
            }

            appUser.Email = appUserViewModel.Email;
            appUser.Address = appUserViewModel.Address;
            appUser.UserName = appUserViewModel.UserName;
            appUser.PhoneNumber = appUserViewModel.PhoneNumber;
            appUser.Gender = appUserViewModel.Gender == "True" ? true : false;
            appUser.Status = appUserViewModel.Status;
            appUser.Address = appUserViewModel.Address;
            appUser.Avatar = appUserViewModel.Avatar;
            appUser.AccountCompany = appUserViewModel.AccountCompany;
            appUser.CompanyID = appUserViewModel.CompanyID;
            appUser.DeptID = appUserViewModel.DeptID;
            appUser.TeamID = appUserViewModel.TeamID;
        }

        /*cap nhat cho emp man ↓*/


        /// <summary>
        /// Cập nhật thông tin các master
        /// </summary>
        /// <param name="data"> Dữ liệu đích</param>
        /// <param name="dataVm">Dữ liệu nguồn update</param>

        public static void UpdateMaster(this Master data, MasterViewModel dataVm)
        {
            data.No = dataVm.No;
            data.ID = dataVm.ID;
            data.Name = dataVm.Name;
            data.ShortName = dataVm.ShortName;
            data.DisplayReportName = dataVm.DisplayReportName;
            data.IsAllowanceType = dataVm.IsAllowanceType;
            data.Value1 = dataVm.Value1;
            data.Value2 = dataVm.Value2;
            data.Value3 = dataVm.Value3;
            data.Value4 = dataVm.Value4;
            data.Value5 = dataVm.Value5;
            data.Value6 = dataVm.Value6;
            data.Value7 = dataVm.Value7;
            data.Value8 = dataVm.Value8;
            data.Value9 = dataVm.Value9;
            data.Value10 = dataVm.Value10;
            data.Value11 = dataVm.Value11;
            data.Value12 = dataVm.Value12;
            data.Value13 = dataVm.Value13;
            data.Value14 = dataVm.Value14;
            data.Value15 = dataVm.Value15;
            data.Value16 = dataVm.Value16;
            data.Value17 = dataVm.Value17;
            data.Value18 = dataVm.Value18;
            data.Value19 = dataVm.Value19;
            data.Value20 = dataVm.Value20;


            //item dùng chung-Start
            data.RowVersion = dataVm.RowVersion;
            data.DisplayOrder = dataVm.DisplayOrder;
            data.AccountData = dataVm.AccountData;
            data.Note = dataVm.Note;
            
            data.AccessDataLevel = dataVm.AccessDataLevel;
            data.CreatedDate = dataVm.CreatedDate;
            data.CreatedBy = dataVm.CreatedBy;
            data.UpdatedDate = dataVm.UpdatedDate;
            data.UpdatedBy = dataVm.UpdatedBy;
            data.MetaKeyword = dataVm.MetaKeyword;
            data.MetaDescription = dataVm.MetaDescription;
            data.Status = dataVm.Status;
            data.DataStatus = dataVm.DataStatus;

            data.RequestBy = dataVm.RequestBy;
            data.RequestDate = dataVm.RequestDate;
            data.ApprovedBy = dataVm.ApprovedBy;
            data.ApprovedDate = dataVm.ApprovedDate;
            data.ApprovedStatus = dataVm.ApprovedStatus;
            
            //data.Yobi_Text1 = dataVm.Yobi_Text1;
            //data.Yobi_Text2 = dataVm.Yobi_Text2;
            //data.Yobi_Text3 = dataVm.Yobi_Text3;
            //data.Yobi_Text4 = dataVm.Yobi_Text4;
            //data.Yobi_Text5 = dataVm.Yobi_Text5;
            //data.Yobi_Number1 = dataVm.Yobi_Number1;
            //data.Yobi_Number2 = dataVm.Yobi_Number2;
            //data.Yobi_Number3 = dataVm.Yobi_Number3;
            //data.Yobi_Number4 = dataVm.Yobi_Number4;
            //data.Yobi_Number5 = dataVm.Yobi_Number5;
            //data.Yobi_Decimal1 = dataVm.Yobi_Decimal1;
            //data.Yobi_Decimal2 = dataVm.Yobi_Decimal2;
            //data.Yobi_Decimal3 = dataVm.Yobi_Decimal3;
            //data.Yobi_Decimal4 = dataVm.Yobi_Decimal4;
            //data.Yobi_Decimal5 = dataVm.Yobi_Decimal5;
            //data.Yobi_Date1 = dataVm.Yobi_Date1;
            //data.Yobi_Date2 = dataVm.Yobi_Date2;
            //data.Yobi_Date3 = dataVm.Yobi_Date3;
            //data.Yobi_Date4 = dataVm.Yobi_Date4;
            //data.Yobi_Date5 = dataVm.Yobi_Date5;

            //item dùng chung-End
        }
        /// <summary>
        /// Cập nhật thông tin các master chi tiết
        /// </summary>
        /// <param name="data"> Dữ liệu đích</param>
        /// <param name="dataVm">Dữ liệu nguồn update</param>

        public static void UpdateMasterDetail(this MasterDetail data, MasterDetailViewModel dataVm)
        {
            data.ID = dataVm.ID;
            data.MasterID = dataVm.MasterID;
            data.MasterDetailCode = dataVm.MasterDetailCode;
            data.Name = dataVm.Name;
            data.ShortName = dataVm.ShortName;
            data.DisplayReportName = dataVm.DisplayReportName;
            data.IsAllowanceType = dataVm.IsAllowanceType;
            data.Allowance = dataVm.Allowance;
            data.StartDate = dataVm.StartDate;
            data.EndDate = dataVm.EndDate;
            data.Value1Title = dataVm.Value1Title;
            data.Value1Data = dataVm.Value1Data;
            data.Value2Title = dataVm.Value2Title;
            data.Value2Data = dataVm.Value2Data;
            data.Value3Title = dataVm.Value3Title;
            data.Value3Data = dataVm.Value3Data;
            data.Value4Title = dataVm.Value4Title;
            data.Value4Data = dataVm.Value4Data;
            data.Value5Title = dataVm.Value5Title;
            data.Value5Data = dataVm.Value5Data;
            data.Value6Title = dataVm.Value6Title;
            data.Value6Data = dataVm.Value6Data;
            data.Value7Title = dataVm.Value7Title;
            data.Value7Data = dataVm.Value7Data;
            data.Value8Title = dataVm.Value8Title;
            data.Value8Data = dataVm.Value8Data;
            data.Value9Title = dataVm.Value9Title;
            data.Value9Data = dataVm.Value9Data;
            data.Value10Title = dataVm.Value10Title;
            data.Value10Data = dataVm.Value10Data;

            //item dùng chung-Start
            data.RowVersion = dataVm.RowVersion;
            data.DisplayOrder = dataVm.DisplayOrder;
            data.AccountData = dataVm.AccountData;
            data.Note = dataVm.Note;
            
            data.AccessDataLevel = dataVm.AccessDataLevel;
            data.CreatedDate = dataVm.CreatedDate;
            data.CreatedBy = dataVm.CreatedBy;
            data.UpdatedDate = dataVm.UpdatedDate;
            data.UpdatedBy = dataVm.UpdatedBy;
            data.MetaKeyword = dataVm.MetaKeyword;
            data.MetaDescription = dataVm.MetaDescription;
            data.Status = dataVm.Status;
            data.DataStatus = dataVm.DataStatus;

            data.RequestBy = dataVm.RequestBy;
            data.RequestDate = dataVm.RequestDate;
            data.ApprovedBy = dataVm.ApprovedBy;
            data.ApprovedDate = dataVm.ApprovedDate;
            data.ApprovedStatus = dataVm.ApprovedStatus;

            //data.Yobi_Text1 = dataVm.Yobi_Text1;
            //data.Yobi_Text2 = dataVm.Yobi_Text2;
            //data.Yobi_Text3 = dataVm.Yobi_Text3;
            //data.Yobi_Text4 = dataVm.Yobi_Text4;
            //data.Yobi_Text5 = dataVm.Yobi_Text5;
            //data.Yobi_Number1 = dataVm.Yobi_Number1;
            //data.Yobi_Number2 = dataVm.Yobi_Number2;
            //data.Yobi_Number3 = dataVm.Yobi_Number3;
            //data.Yobi_Number4 = dataVm.Yobi_Number4;
            //data.Yobi_Number5 = dataVm.Yobi_Number5;
            //data.Yobi_Decimal1 = dataVm.Yobi_Decimal1;
            //data.Yobi_Decimal2 = dataVm.Yobi_Decimal2;
            //data.Yobi_Decimal3 = dataVm.Yobi_Decimal3;
            //data.Yobi_Decimal4 = dataVm.Yobi_Decimal4;
            //data.Yobi_Decimal5 = dataVm.Yobi_Decimal5;
            //data.Yobi_Date1 = dataVm.Yobi_Date1;
            //data.Yobi_Date2 = dataVm.Yobi_Date2;
            //data.Yobi_Date3 = dataVm.Yobi_Date3;
            //data.Yobi_Date4 = dataVm.Yobi_Date4;
            //data.Yobi_Date5 = dataVm.Yobi_Date5;
            //item dùng chung-End

        }

        /// <summary>
        /// Cập nhật thông tin công ty
        /// </summary>
        /// <param name="data"> Dữ liệu đích</param>
        /// <param name="dataVm">Dữ liệu nguồn update</param>

        public static void UpdateCompany(this Company data, CompanyViewModel dataVm)
        {
            data.No = dataVm.No;
            data.ID = dataVm.ID;
            data.Name = dataVm.Name;
            data.ShortName = dataVm.ShortName;
            data.CeoID = dataVm.CeoID;
            data.DirectorID = dataVm.DirectorID;
            data.DeputyDirectorID = dataVm.DeputyDirectorID;
            data.Address1 = dataVm.Address1;
            data.Address2 = dataVm.Address2;
            data.Phone1 = dataVm.Phone1;
            data.Phone2 = dataVm.Phone2;
            data.Fax = dataVm.Fax;
            data.ContactEmail = dataVm.ContactEmail;
            data.WebSiteUrl = dataVm.WebSiteUrl;
            data.TaxCode = dataVm.TaxCode;
            data.TaxAddress = dataVm.TaxAddress;
            data.CreateDate = dataVm.CreateDate;
            data.Captital = dataVm.Captital;
            data.DomainEmail = dataVm.DomainEmail;
            data.GlobalIpList = dataVm.GlobalIpList;

            //item dùng chung-Start
            data.RowVersion = dataVm.RowVersion;
            data.DisplayOrder = dataVm.DisplayOrder;
            data.AccountData = dataVm.AccountData;
            data.Note = dataVm.Note;
            
            data.AccessDataLevel = dataVm.AccessDataLevel;
            data.CreatedDate = dataVm.CreatedDate;
            data.CreatedBy = dataVm.CreatedBy;
            data.UpdatedDate = dataVm.UpdatedDate;
            data.UpdatedBy = dataVm.UpdatedBy;
            data.MetaKeyword = dataVm.MetaKeyword;
            data.MetaDescription = dataVm.MetaDescription;
            data.Status = dataVm.Status;
            data.DataStatus = dataVm.DataStatus;

            data.RequestBy = dataVm.RequestBy;
            data.RequestDate = dataVm.RequestDate;
            data.ApprovedBy = dataVm.ApprovedBy;
            data.ApprovedDate = dataVm.ApprovedDate;
            data.ApprovedStatus = dataVm.ApprovedStatus;

            //data.Yobi_Text1 = dataVm.Yobi_Text1;
            //data.Yobi_Text2 = dataVm.Yobi_Text2;
            //data.Yobi_Text3 = dataVm.Yobi_Text3;
            //data.Yobi_Text4 = dataVm.Yobi_Text4;
            //data.Yobi_Text5 = dataVm.Yobi_Text5;
            //data.Yobi_Number1 = dataVm.Yobi_Number1;
            //data.Yobi_Number2 = dataVm.Yobi_Number2;
            //data.Yobi_Number3 = dataVm.Yobi_Number3;
            //data.Yobi_Number4 = dataVm.Yobi_Number4;
            //data.Yobi_Number5 = dataVm.Yobi_Number5;
            //data.Yobi_Decimal1 = dataVm.Yobi_Decimal1;
            //data.Yobi_Decimal2 = dataVm.Yobi_Decimal2;
            //data.Yobi_Decimal3 = dataVm.Yobi_Decimal3;
            //data.Yobi_Decimal4 = dataVm.Yobi_Decimal4;
            //data.Yobi_Decimal5 = dataVm.Yobi_Decimal5;
            //data.Yobi_Date1 = dataVm.Yobi_Date1;
            //data.Yobi_Date2 = dataVm.Yobi_Date2;
            //data.Yobi_Date3 = dataVm.Yobi_Date3;
            //data.Yobi_Date4 = dataVm.Yobi_Date4;
            //data.Yobi_Date5 = dataVm.Yobi_Date5;
            //item dùng chung-End

        }

        /// <summary>
        /// Cập nhật thông tin công ty --cac qui dinh
        /// </summary>
        /// <param name="data"> Dữ liệu đích</param>
        /// <param name="dataVm">Dữ liệu nguồn update</param>

        public static void UpdateCompanyRule(this CompanyRule data, CompanyRuleViewModel dataVm)
        {

            data.ID = dataVm.ID;
            data.CompanyID = dataVm.CompanyID;
            data.NoticeDate = dataVm.NoticeDate;
            data.SenderID = dataVm.SenderID;
            data.SenderName = dataVm.SenderName;
            data.Name = dataVm.Name;
            data.Content = dataVm.Content;
            data.FileID = dataVm.FileID;
            data.RuleTypeMasterID = dataVm.RuleTypeMasterID;
            data.RuleTypeMasterDetailID = dataVm.RuleTypeMasterDetailID;
            data.ValidDateStart = dataVm.ValidDateStart;
            data.ValidDateEnd = dataVm.ValidDateEnd;
            data.ActionObject = dataVm.ActionObject;

            //item dùng chung-Start
            data.RowVersion = dataVm.RowVersion;
            data.DisplayOrder = dataVm.DisplayOrder;
            data.AccountData = dataVm.AccountData;
            data.Note = dataVm.Note;
            
            data.AccessDataLevel = dataVm.AccessDataLevel;
            data.CreatedDate = dataVm.CreatedDate;
            data.CreatedBy = dataVm.CreatedBy;
            data.UpdatedDate = dataVm.UpdatedDate;
            data.UpdatedBy = dataVm.UpdatedBy;
            data.MetaKeyword = dataVm.MetaKeyword;
            data.MetaDescription = dataVm.MetaDescription;
            data.Status = dataVm.Status;
            data.DataStatus = dataVm.DataStatus;

            data.RequestBy = dataVm.RequestBy;
            data.RequestDate = dataVm.RequestDate;
            data.ApprovedBy = dataVm.ApprovedBy;
            data.ApprovedDate = dataVm.ApprovedDate;
            data.ApprovedStatus = dataVm.ApprovedStatus;

            //data.Yobi_Text1 = dataVm.Yobi_Text1;
            //data.Yobi_Text2 = dataVm.Yobi_Text2;
            //data.Yobi_Text3 = dataVm.Yobi_Text3;
            //data.Yobi_Text4 = dataVm.Yobi_Text4;
            //data.Yobi_Text5 = dataVm.Yobi_Text5;
            //data.Yobi_Number1 = dataVm.Yobi_Number1;
            //data.Yobi_Number2 = dataVm.Yobi_Number2;
            //data.Yobi_Number3 = dataVm.Yobi_Number3;
            //data.Yobi_Number4 = dataVm.Yobi_Number4;
            //data.Yobi_Number5 = dataVm.Yobi_Number5;
            //data.Yobi_Decimal1 = dataVm.Yobi_Decimal1;
            //data.Yobi_Decimal2 = dataVm.Yobi_Decimal2;
            //data.Yobi_Decimal3 = dataVm.Yobi_Decimal3;
            //data.Yobi_Decimal4 = dataVm.Yobi_Decimal4;
            //data.Yobi_Decimal5 = dataVm.Yobi_Decimal5;
            //data.Yobi_Date1 = dataVm.Yobi_Date1;
            //data.Yobi_Date2 = dataVm.Yobi_Date2;
            //data.Yobi_Date3 = dataVm.Yobi_Date3;
            //data.Yobi_Date4 = dataVm.Yobi_Date4;
            //data.Yobi_Date5 = dataVm.Yobi_Date5;

        }


        /// <summary>
        /// Cập nhật thông tin phòng ban
        /// </summary>
        /// <param name="data"> Dữ liệu đích</param>
        /// <param name="dataVm">Dữ liệu nguồn update</param>

        public static void UpdateDept(this Dept data, DeptViewModel dataVm)
        {

            data.No = dataVm.No;
            data.ID = dataVm.ID;
            data.Name = dataVm.Name;
            data.ShortName = dataVm.ShortName;
            data.CreateDate = dataVm.CreateDate;
            data.TopManagerID = dataVm.TopManagerID;
            data.Manager1ID = dataVm.Manager1ID;
            data.Manager2ID = dataVm.Manager2ID;
            data.ViceManager1ID = dataVm.ViceManager1ID;
            data.ViceManager2ID = dataVm.ViceManager2ID;
            data.ViceManager3ID = dataVm.ViceManager3ID;
            data.CompanyID = dataVm.CompanyID;
            data.MailGroup = dataVm.MailGroup;


            //item dùng chung-Start
            data.RowVersion = dataVm.RowVersion;
            data.DisplayOrder = dataVm.DisplayOrder;
            data.AccountData = dataVm.AccountData;
            data.Note = dataVm.Note;
            
            data.AccessDataLevel = dataVm.AccessDataLevel;
            data.CreatedDate = dataVm.CreatedDate;
            data.CreatedBy = dataVm.CreatedBy;
            data.UpdatedDate = dataVm.UpdatedDate;
            data.UpdatedBy = dataVm.UpdatedBy;
            data.MetaKeyword = dataVm.MetaKeyword;
            data.MetaDescription = dataVm.MetaDescription;
            data.Status = dataVm.Status;
            data.DataStatus = dataVm.DataStatus;

            data.RequestBy = dataVm.RequestBy;
            data.RequestDate = dataVm.RequestDate;
            data.ApprovedBy = dataVm.ApprovedBy;
            data.ApprovedDate = dataVm.ApprovedDate;
            data.ApprovedStatus = dataVm.ApprovedStatus;

            //data.Yobi_Text1 = dataVm.Yobi_Text1;
            //data.Yobi_Text2 = dataVm.Yobi_Text2;
            //data.Yobi_Text3 = dataVm.Yobi_Text3;
            //data.Yobi_Text4 = dataVm.Yobi_Text4;
            //data.Yobi_Text5 = dataVm.Yobi_Text5;
            //data.Yobi_Number1 = dataVm.Yobi_Number1;
            //data.Yobi_Number2 = dataVm.Yobi_Number2;
            //data.Yobi_Number3 = dataVm.Yobi_Number3;
            //data.Yobi_Number4 = dataVm.Yobi_Number4;
            //data.Yobi_Number5 = dataVm.Yobi_Number5;
            //data.Yobi_Decimal1 = dataVm.Yobi_Decimal1;
            //data.Yobi_Decimal2 = dataVm.Yobi_Decimal2;
            //data.Yobi_Decimal3 = dataVm.Yobi_Decimal3;
            //data.Yobi_Decimal4 = dataVm.Yobi_Decimal4;
            //data.Yobi_Decimal5 = dataVm.Yobi_Decimal5;
            //data.Yobi_Date1 = dataVm.Yobi_Date1;
            //data.Yobi_Date2 = dataVm.Yobi_Date2;
            //data.Yobi_Date3 = dataVm.Yobi_Date3;
            //data.Yobi_Date4 = dataVm.Yobi_Date4;
            //data.Yobi_Date5 = dataVm.Yobi_Date5;
            //item dùng chung-End

        }

        /// <summary>
        /// Cập nhật thông tin nhóm
        /// </summary>
        /// <param name="data"> Dữ liệu đích</param>
        /// <param name="dataVm">Dữ liệu nguồn update</param>

        public static void UpdateTeam(this Team data, TeamViewModel dataVm)
        {

            data.No = dataVm.No;
            data.ID = dataVm.ID;
            data.Name = dataVm.Name;
            data.ShortName = dataVm.ShortName;
            data.CreateDate = dataVm.CreateDate;
            data.TopLeaderID = dataVm.TopLeaderID;
            data.SubLeaderID = dataVm.SubLeaderID;
            data.DeptID = dataVm.DeptID;
            data.MailGroup = dataVm.MailGroup;

            //item dùng chung-Start
            //item dùng chung-Start
            data.RowVersion = dataVm.RowVersion;
            data.DisplayOrder = dataVm.DisplayOrder;
            data.AccountData = dataVm.AccountData;
            data.Note = dataVm.Note;
            
            data.AccessDataLevel = dataVm.AccessDataLevel;
            data.CreatedDate = dataVm.CreatedDate;
            data.CreatedBy = dataVm.CreatedBy;
            data.UpdatedDate = dataVm.UpdatedDate;
            data.UpdatedBy = dataVm.UpdatedBy;
            data.MetaKeyword = dataVm.MetaKeyword;
            data.MetaDescription = dataVm.MetaDescription;
            data.Status = dataVm.Status;
            data.DataStatus = dataVm.DataStatus;

            data.RequestBy = dataVm.RequestBy;
            data.RequestDate = dataVm.RequestDate;
            data.ApprovedBy = dataVm.ApprovedBy;
            data.ApprovedDate = dataVm.ApprovedDate;
            data.ApprovedStatus = dataVm.ApprovedStatus;

            //data.Yobi_Text1 = dataVm.Yobi_Text1;
            //data.Yobi_Text2 = dataVm.Yobi_Text2;
            //data.Yobi_Text3 = dataVm.Yobi_Text3;
            //data.Yobi_Text4 = dataVm.Yobi_Text4;
            //data.Yobi_Text5 = dataVm.Yobi_Text5;
            //data.Yobi_Number1 = dataVm.Yobi_Number1;
            //data.Yobi_Number2 = dataVm.Yobi_Number2;
            //data.Yobi_Number3 = dataVm.Yobi_Number3;
            //data.Yobi_Number4 = dataVm.Yobi_Number4;
            //data.Yobi_Number5 = dataVm.Yobi_Number5;
            //data.Yobi_Decimal1 = dataVm.Yobi_Decimal1;
            //data.Yobi_Decimal2 = dataVm.Yobi_Decimal2;
            //data.Yobi_Decimal3 = dataVm.Yobi_Decimal3;
            //data.Yobi_Decimal4 = dataVm.Yobi_Decimal4;
            //data.Yobi_Decimal5 = dataVm.Yobi_Decimal5;
            //data.Yobi_Date1 = dataVm.Yobi_Date1;
            //data.Yobi_Date2 = dataVm.Yobi_Date2;
            //data.Yobi_Date3 = dataVm.Yobi_Date3;
            //data.Yobi_Date4 = dataVm.Yobi_Date4;
            //data.Yobi_Date5 = dataVm.Yobi_Date5;
            //item dùng chung-End

        }

        public static void UpdatePosition(this Position data, PositionViewModel dataVm)
        {

            data.No = dataVm.No;
            data.ID = dataVm.ID;
            data.Name = dataVm.Name;
            data.ShortName = dataVm.ShortName;
            data.RoleId = dataVm.RoleId;
            data.MonthAvg = dataVm.MonthAvg;
            data.Allowance = dataVm.Allowance;
            data.ParentID = dataVm.ParentID;
            data.NextLevelID = dataVm.NextLevelID;
            data.PositionGroupMasterID = dataVm.PositionGroupMasterID;
            data.PositionGroupMasterDetailID = dataVm.PositionGroupMasterDetailID;
            data.MM = dataVm.MM;
            data.StandardMoneyIncrease = dataVm.StandardMoneyIncrease;


            //item dùng chung-Start
            data.DisplayOrder = dataVm.DisplayOrder;
            data.AccountData = dataVm.AccountData;
            data.Note = dataVm.Note;
            
            data.AccessDataLevel = dataVm.AccessDataLevel;
            //item dùng chung-Start
            data.RowVersion = dataVm.RowVersion;
            data.DisplayOrder = dataVm.DisplayOrder;
            data.AccountData = dataVm.AccountData;
            data.Note = dataVm.Note;
            
            data.AccessDataLevel = dataVm.AccessDataLevel;
            data.CreatedDate = dataVm.CreatedDate;
            data.CreatedBy = dataVm.CreatedBy;
            data.UpdatedDate = dataVm.UpdatedDate;
            data.UpdatedBy = dataVm.UpdatedBy;
            data.MetaKeyword = dataVm.MetaKeyword;
            data.MetaDescription = dataVm.MetaDescription;
            data.Status = dataVm.Status;
            data.DataStatus = dataVm.DataStatus;

            data.RequestBy = dataVm.RequestBy;
            data.RequestDate = dataVm.RequestDate;
            data.ApprovedBy = dataVm.ApprovedBy;
            data.ApprovedDate = dataVm.ApprovedDate;
            data.ApprovedStatus = dataVm.ApprovedStatus;

            //data.Yobi_Text1 = dataVm.Yobi_Text1;
            //data.Yobi_Text2 = dataVm.Yobi_Text2;
            //data.Yobi_Text3 = dataVm.Yobi_Text3;
            //data.Yobi_Text4 = dataVm.Yobi_Text4;
            //data.Yobi_Text5 = dataVm.Yobi_Text5;
            //data.Yobi_Number1 = dataVm.Yobi_Number1;
            //data.Yobi_Number2 = dataVm.Yobi_Number2;
            //data.Yobi_Number3 = dataVm.Yobi_Number3;
            //data.Yobi_Number4 = dataVm.Yobi_Number4;
            //data.Yobi_Number5 = dataVm.Yobi_Number5;
            //data.Yobi_Decimal1 = dataVm.Yobi_Decimal1;
            //data.Yobi_Decimal2 = dataVm.Yobi_Decimal2;
            //data.Yobi_Decimal3 = dataVm.Yobi_Decimal3;
            //data.Yobi_Decimal4 = dataVm.Yobi_Decimal4;
            //data.Yobi_Decimal5 = dataVm.Yobi_Decimal5;
            //data.Yobi_Date1 = dataVm.Yobi_Date1;
            //data.Yobi_Date2 = dataVm.Yobi_Date2;
            //data.Yobi_Date3 = dataVm.Yobi_Date3;
            //data.Yobi_Date4 = dataVm.Yobi_Date4;
            //data.Yobi_Date5 = dataVm.Yobi_Date5;
            //item dùng chung-End

        }

        /// <summary>
        /// Cập nhật thông tin cơ bản của nhân viên
        /// </summary>
        /// <param name="data"></param>
        /// <param name="dataVm"></param>
        public static void UpdateEmp(this Emp data, EmpViewModel dataVm)
        {
            data.No = dataVm.No;
            data.ID = dataVm.ID;
            data.FullName = dataVm.FullName;
            data.Name = dataVm.FullName.Split(' ').Last();
            data.Furigana = dataVm.Furigana;
            data.Gender = dataVm.Gender;
            data.IdentNo = dataVm.IdentNo;
            data.IdentIssueDate = dataVm.IdentIssueDate;
            data.IdentIssuePlace = dataVm.IdentIssuePlace;
            data.TaxCode = dataVm.TaxCode;
            data.TaxCodeIssueDate= dataVm.TaxCodeIssueDate;
            data.ExtLinkNo = dataVm.ExtLinkNo ;
            data.TrainingProfileNo   = dataVm.TrainingProfileNo;
            data.BornPlace = dataVm.BornPlace;
            data.Avatar = dataVm.Avatar;
            data.ShowAvatar = dataVm.ShowAvatar;
            data.WorkingEmail = dataVm.WorkingEmail;
            data.PersonalEmail = dataVm.PersonalEmail;
            data.BirthDay = dataVm.BirthDay;
            data.AccountName = dataVm.AccountName;
            data.PhoneNumber1 = dataVm.PhoneNumber1;
            data.PhoneNumber2 = dataVm.PhoneNumber2;
            data.PhoneNumber3 = dataVm.PhoneNumber3;
            data.Address1 = dataVm.Address1;
            data.Address2 = dataVm.Address2;
            data.CurrentCompanyID = dataVm.CurrentCompanyID;
            data.CurrentDeptID = dataVm.CurrentDeptID;
            data.CurrentTeamID = dataVm.CurrentTeamID;
            data.CurrentPositionID = dataVm.CurrentPositionID;
            data.InterviewDate = dataVm.InterviewDate;
            data.InterviewEmp = dataVm.InterviewEmp;
            data.WorkingConditionTalkDate = dataVm.WorkingConditionTalkDate;
            data.StartIntershipDate = dataVm.StartIntershipDate;
            data.EndIntershipDate = dataVm.EndIntershipDate;
            data.StartWorkingDate = dataVm.StartWorkingDate;
            data.StartLearningDate = dataVm.StartLearningDate;
            data.EndLearningDate = dataVm.EndLearningDate;
            data.StartTrialDate = dataVm.StartTrialDate;
            data.EndTrialDate = dataVm.EndTrialDate;
            data.TrialResult = dataVm.TrialResult;
            data.ContractDate = dataVm.ContractDate;
            data.BabyBornStartDate = dataVm.BabyBornStartDate;
            data.BabyBornScheduleEndDate = dataVm.BabyBornScheduleEndDate;
            data.BabyBornActualEndDate = dataVm.BabyBornActualEndDate;
            data.BabyBornStartDate2 = dataVm.BabyBornStartDate2;
            data.BabyBornScheduleEndDate2 = dataVm.BabyBornScheduleEndDate2;
            data.BabyBornActualEndDate2 = dataVm.BabyBornActualEndDate2;
            data.ContractTypeMasterID = dataVm.ContractTypeMasterID;
            data.ContractTypeMasterDetailID = dataVm.ContractTypeMasterDetailID;
            data.JobLeaveDate = dataVm.JobLeaveDate;
            data.IsJobLeave = dataVm.IsJobLeave;
            data.JobLeaveReason = dataVm.JobLeaveReason;
            data.GoogleId = dataVm.GoogleId;
            data.MarriedDate = dataVm.MarriedDate;
            data.ExperienceBeforeContent = dataVm.ExperienceBeforeContent;
            data.ExperienceBeforeConvert = dataVm.ExperienceBeforeConvert;
            data.ExperienceConvert = dataVm.ExperienceConvert;
            data.EmpTypeMasterID = dataVm.EmpTypeMasterID;
            data.EmpTypeMasterDetailID = dataVm.EmpTypeMasterDetailID;
            data.IsBSE = dataVm.IsBSE;

            data.JapaneseLevelMasterID = dataVm.JapaneseLevelMasterID;
            data.JapaneseLevelMasterDetailID = dataVm.JapaneseLevelMasterDetailID;
            data.BusinessAllowanceLevelMasterID = dataVm.BusinessAllowanceLevelMasterID;
            data.BusinessAllowanceLevelMasterDetailID = dataVm.BusinessAllowanceLevelMasterDetailID;
            data.RoomWithInternetAllowanceLevelMasterID = dataVm.RoomWithInternetAllowanceLevelMasterID;
            data.RoomWithInternetAllowanceLevelMasterDetailID = dataVm.RoomWithInternetAllowanceLevelMasterDetailID;
            data.RoomNoInternetAllowanceLevelMasterID = dataVm.RoomNoInternetAllowanceLevelMasterID;

            data.RoomNoInternetAllowanceLevelMasterDetailID = dataVm.RoomNoInternetAllowanceLevelMasterDetailID;
            data.BseAllowanceLevelMasterID = dataVm.BseAllowanceLevelMasterID;
            data.BseAllowanceLevelMasterDetailID = dataVm.BseAllowanceLevelMasterDetailID;

            data.CollectMasterID = dataVm.CollectMasterID;
            data.CollectMasterDetailID = dataVm.CollectMasterDetailID;
            data.EducationLevelMasterID = dataVm.EducationLevelMasterID;
            data.EducationLevelMasterDetailID = dataVm.EducationLevelMasterDetailID;
            data.Temperament = dataVm.Temperament;
            data.Introductor = dataVm.Introductor;
            data.BloodGroup = dataVm.BloodGroup;
            data.Hobby = dataVm.Hobby;
            data.Objective = dataVm.Objective;
            data.FileID = dataVm.FileID;
            data.ProfileAttachmentID = dataVm.ProfileAttachmentID;


            //item dùng chung-Start
            //item dùng chung-Start
            data.RowVersion = dataVm.RowVersion;
            data.DisplayOrder = dataVm.DisplayOrder;
            data.AccountData = dataVm.AccountData;
            data.Note = dataVm.Note;
            
            data.AccessDataLevel = dataVm.AccessDataLevel;
            data.CreatedDate = dataVm.CreatedDate;
            data.CreatedBy = dataVm.CreatedBy;
            data.UpdatedDate = dataVm.UpdatedDate;
            data.UpdatedBy = dataVm.UpdatedBy;
            data.MetaKeyword = dataVm.MetaKeyword;
            data.MetaDescription = dataVm.MetaDescription;
            data.Status = dataVm.Status;
            data.DataStatus = dataVm.DataStatus;

            data.RequestBy = dataVm.RequestBy;
            data.RequestDate = dataVm.RequestDate;
            data.ApprovedBy = dataVm.ApprovedBy;
            data.ApprovedDate = dataVm.ApprovedDate;
            data.ApprovedStatus = dataVm.ApprovedStatus;

            //data.Yobi_Text1 = dataVm.Yobi_Text1;
            //data.Yobi_Text2 = dataVm.Yobi_Text2;
            //data.Yobi_Text3 = dataVm.Yobi_Text3;
            //data.Yobi_Text4 = dataVm.Yobi_Text4;
            //data.Yobi_Text5 = dataVm.Yobi_Text5;
            //data.Yobi_Number1 = dataVm.Yobi_Number1;
            //data.Yobi_Number2 = dataVm.Yobi_Number2;
            //data.Yobi_Number3 = dataVm.Yobi_Number3;
            //data.Yobi_Number4 = dataVm.Yobi_Number4;
            //data.Yobi_Number5 = dataVm.Yobi_Number5;
            //data.Yobi_Decimal1 = dataVm.Yobi_Decimal1;
            //data.Yobi_Decimal2 = dataVm.Yobi_Decimal2;
            //data.Yobi_Decimal3 = dataVm.Yobi_Decimal3;
            //data.Yobi_Decimal4 = dataVm.Yobi_Decimal4;
            //data.Yobi_Decimal5 = dataVm.Yobi_Decimal5;
            //data.Yobi_Date1 = dataVm.Yobi_Date1;
            //data.Yobi_Date2 = dataVm.Yobi_Date2;
            //data.Yobi_Date3 = dataVm.Yobi_Date3;
            //data.Yobi_Date4 = dataVm.Yobi_Date4;
            //data.Yobi_Date5 = dataVm.Yobi_Date5;
            //item dùng chung-End

        }

        public static void UpdateEmpProfile(this EmpProfile data, EmpProfileViewModel dataVm)
        {

            data.ID = dataVm.ID;
            data.EmpID = dataVm.EmpID;
            data.WorkGood = dataVm.WorkGood;
            data.Keikaku = dataVm.Keikaku;
            data.EnglishLevel = dataVm.EnglishLevel;
            data.Collect = dataVm.Collect;
            data.FileID = dataVm.FileID;


            //item dùng chung-Start
            data.RowVersion = dataVm.RowVersion;
            data.DisplayOrder = dataVm.DisplayOrder;
            data.AccountData = dataVm.AccountData;
            data.Note = dataVm.Note;
            
            data.AccessDataLevel = dataVm.AccessDataLevel;
            data.CreatedDate = dataVm.CreatedDate;
            data.CreatedBy = dataVm.CreatedBy;
            data.UpdatedDate = dataVm.UpdatedDate;
            data.UpdatedBy = dataVm.UpdatedBy;
            data.MetaKeyword = dataVm.MetaKeyword;
            data.MetaDescription = dataVm.MetaDescription;
            data.Status = dataVm.Status;
            data.DataStatus = dataVm.DataStatus;

            data.RequestBy = dataVm.RequestBy;
            data.RequestDate = dataVm.RequestDate;
            data.ApprovedBy = dataVm.ApprovedBy;
            data.ApprovedDate = dataVm.ApprovedDate;
            data.ApprovedStatus = dataVm.ApprovedStatus;

            //data.Yobi_Text1 = dataVm.Yobi_Text1;
            //data.Yobi_Text2 = dataVm.Yobi_Text2;
            //data.Yobi_Text3 = dataVm.Yobi_Text3;
            //data.Yobi_Text4 = dataVm.Yobi_Text4;
            //data.Yobi_Text5 = dataVm.Yobi_Text5;
            //data.Yobi_Number1 = dataVm.Yobi_Number1;
            //data.Yobi_Number2 = dataVm.Yobi_Number2;
            //data.Yobi_Number3 = dataVm.Yobi_Number3;
            //data.Yobi_Number4 = dataVm.Yobi_Number4;
            //data.Yobi_Number5 = dataVm.Yobi_Number5;
            //data.Yobi_Decimal1 = dataVm.Yobi_Decimal1;
            //data.Yobi_Decimal2 = dataVm.Yobi_Decimal2;
            //data.Yobi_Decimal3 = dataVm.Yobi_Decimal3;
            //data.Yobi_Decimal4 = dataVm.Yobi_Decimal4;
            //data.Yobi_Decimal5 = dataVm.Yobi_Decimal5;
            //data.Yobi_Date1 = dataVm.Yobi_Date1;
            //data.Yobi_Date2 = dataVm.Yobi_Date2;
            //data.Yobi_Date3 = dataVm.Yobi_Date3;
            //data.Yobi_Date4 = dataVm.Yobi_Date4;
            //data.Yobi_Date5 = dataVm.Yobi_Date5;
            //item dùng chung-End

        }

        public static void UpdateEmpProfileTech(this EmpProfileTech data, EmpProfileTechViewModel dataVm)
        {

            data.ID = dataVm.ID;
            data.EmpID = dataVm.EmpID;
            data.EmpProfileID = dataVm.EmpProfileID;
            data.Lang = dataVm.Lang;
            data.Kikan = dataVm.Kikan;
            data.IsUnitMonth = dataVm.IsUnitMonth;
            data.IsUnitYear = dataVm.IsUnitYear;
            data.Level = dataVm.Level;


            //item dùng chung-Start
            data.RowVersion = dataVm.RowVersion;
            data.DisplayOrder = dataVm.DisplayOrder;
            data.AccountData = dataVm.AccountData;
            data.Note = dataVm.Note;
            
            data.AccessDataLevel = dataVm.AccessDataLevel;
            data.CreatedDate = dataVm.CreatedDate;
            data.CreatedBy = dataVm.CreatedBy;
            data.UpdatedDate = dataVm.UpdatedDate;
            data.UpdatedBy = dataVm.UpdatedBy;
            data.MetaKeyword = dataVm.MetaKeyword;
            data.MetaDescription = dataVm.MetaDescription;
            data.Status = dataVm.Status;
            data.DataStatus = dataVm.DataStatus;

            data.RequestBy = dataVm.RequestBy;
            data.RequestDate = dataVm.RequestDate;
            data.ApprovedBy = dataVm.ApprovedBy;
            data.ApprovedDate = dataVm.ApprovedDate;
            data.ApprovedStatus = dataVm.ApprovedStatus;

            //data.Yobi_Text1 = dataVm.Yobi_Text1;
            //data.Yobi_Text2 = dataVm.Yobi_Text2;
            //data.Yobi_Text3 = dataVm.Yobi_Text3;
            //data.Yobi_Text4 = dataVm.Yobi_Text4;
            //data.Yobi_Text5 = dataVm.Yobi_Text5;
            //data.Yobi_Number1 = dataVm.Yobi_Number1;
            //data.Yobi_Number2 = dataVm.Yobi_Number2;
            //data.Yobi_Number3 = dataVm.Yobi_Number3;
            //data.Yobi_Number4 = dataVm.Yobi_Number4;
            //data.Yobi_Number5 = dataVm.Yobi_Number5;
            //data.Yobi_Decimal1 = dataVm.Yobi_Decimal1;
            //data.Yobi_Decimal2 = dataVm.Yobi_Decimal2;
            //data.Yobi_Decimal3 = dataVm.Yobi_Decimal3;
            //data.Yobi_Decimal4 = dataVm.Yobi_Decimal4;
            //data.Yobi_Decimal5 = dataVm.Yobi_Decimal5;
            //data.Yobi_Date1 = dataVm.Yobi_Date1;
            //data.Yobi_Date2 = dataVm.Yobi_Date2;
            //data.Yobi_Date3 = dataVm.Yobi_Date3;
            //data.Yobi_Date4 = dataVm.Yobi_Date4;
            //data.Yobi_Date5 = dataVm.Yobi_Date5;
            //item dùng chung-End

        }

        public static void UpdateEmpProfileWork(this EmpProfileWork data, EmpProfileWorkViewModel dataVm)
        {
            data.ID = dataVm.ID;
            data.EmpID = dataVm.EmpID;
            data.EmpProfileID = dataVm.EmpProfileID;
            data.StartDate = dataVm.StartDate;
            data.EndDate = dataVm.EndDate;
            data.WorkContent = dataVm.WorkContent;
            data.Os = dataVm.Os;
            data.LangTool = dataVm.LangTool;
            data.WorkType = dataVm.WorkType;
            data.TemplateID = dataVm.TemplateID;

            //item dùng chung-Start
            data.RowVersion = dataVm.RowVersion;
            data.DisplayOrder = dataVm.DisplayOrder;
            data.AccountData = dataVm.AccountData;
            data.Note = dataVm.Note;
            
            data.AccessDataLevel = dataVm.AccessDataLevel;
            data.CreatedDate = dataVm.CreatedDate;
            data.CreatedBy = dataVm.CreatedBy;
            data.UpdatedDate = dataVm.UpdatedDate;
            data.UpdatedBy = dataVm.UpdatedBy;
            data.MetaKeyword = dataVm.MetaKeyword;
            data.MetaDescription = dataVm.MetaDescription;
            data.Status = dataVm.Status;
            data.DataStatus = dataVm.DataStatus;

            data.RequestBy = dataVm.RequestBy;
            data.RequestDate = dataVm.RequestDate;
            data.ApprovedBy = dataVm.ApprovedBy;
            data.ApprovedDate = dataVm.ApprovedDate;
            data.ApprovedStatus = dataVm.ApprovedStatus;

            //data.Yobi_Text1 = dataVm.Yobi_Text1;
            //data.Yobi_Text2 = dataVm.Yobi_Text2;
            //data.Yobi_Text3 = dataVm.Yobi_Text3;
            //data.Yobi_Text4 = dataVm.Yobi_Text4;
            //data.Yobi_Text5 = dataVm.Yobi_Text5;
            //data.Yobi_Number1 = dataVm.Yobi_Number1;
            //data.Yobi_Number2 = dataVm.Yobi_Number2;
            //data.Yobi_Number3 = dataVm.Yobi_Number3;
            //data.Yobi_Number4 = dataVm.Yobi_Number4;
            //data.Yobi_Number5 = dataVm.Yobi_Number5;
            //data.Yobi_Decimal1 = dataVm.Yobi_Decimal1;
            //data.Yobi_Decimal2 = dataVm.Yobi_Decimal2;
            //data.Yobi_Decimal3 = dataVm.Yobi_Decimal3;
            //data.Yobi_Decimal4 = dataVm.Yobi_Decimal4;
            //data.Yobi_Decimal5 = dataVm.Yobi_Decimal5;
            //data.Yobi_Date1 = dataVm.Yobi_Date1;
            //data.Yobi_Date2 = dataVm.Yobi_Date2;
            //data.Yobi_Date3 = dataVm.Yobi_Date3;
            //data.Yobi_Date4 = dataVm.Yobi_Date4;
            //data.Yobi_Date5 = dataVm.Yobi_Date5;
            //item dùng chung-End

        }

        public static void UpdateEmpContract(this EmpContract data, EmpContractViewModel dataVm)
        {

            data.ID = dataVm.ID;
            data.EmpID = dataVm.EmpID;
            data.ContractNo = dataVm.ContractNo;
            data.StartDate = dataVm.StartDate;
            data.EndDate = dataVm.EndDate;
            data.ContractTypeMasterID = dataVm.ContractTypeMasterID;
            data.ContractTypeMasterDetailID = dataVm.ContractTypeMasterDetailID;
            data.SignDate = dataVm.SignDate;
            data.SalaryUnit = dataVm.SalaryUnit;
            data.NetSalary = dataVm.NetSalary;
            data.NetAllowance = dataVm.NetAllowance;
            data.OtherAllowance = dataVm.OtherAllowance;
            data.SubContractSignCount = dataVm.SubContractSignCount;
            data.Result = dataVm.Result;
            data.Action = dataVm.Action;
            data.Content = dataVm.Content;
            data.FileID = dataVm.FileID;

            //item dùng chung-Start
            data.RowVersion = dataVm.RowVersion;
            data.DisplayOrder = dataVm.DisplayOrder;
            data.AccountData = dataVm.AccountData;
            data.Note = dataVm.Note;
            
            data.AccessDataLevel = dataVm.AccessDataLevel;
            data.CreatedDate = dataVm.CreatedDate;
            data.CreatedBy = dataVm.CreatedBy;
            data.UpdatedDate = dataVm.UpdatedDate;
            data.UpdatedBy = dataVm.UpdatedBy;
            data.MetaKeyword = dataVm.MetaKeyword;
            data.MetaDescription = dataVm.MetaDescription;
            data.Status = dataVm.Status;
            data.DataStatus = dataVm.DataStatus;

            data.RequestBy = dataVm.RequestBy;
            data.RequestDate = dataVm.RequestDate;
            data.ApprovedBy = dataVm.ApprovedBy;
            data.ApprovedDate = dataVm.ApprovedDate;
            data.ApprovedStatus = dataVm.ApprovedStatus;

            //data.Yobi_Text1 = dataVm.Yobi_Text1;
            //data.Yobi_Text2 = dataVm.Yobi_Text2;
            //data.Yobi_Text3 = dataVm.Yobi_Text3;
            //data.Yobi_Text4 = dataVm.Yobi_Text4;
            //data.Yobi_Text5 = dataVm.Yobi_Text5;
            //data.Yobi_Number1 = dataVm.Yobi_Number1;
            //data.Yobi_Number2 = dataVm.Yobi_Number2;
            //data.Yobi_Number3 = dataVm.Yobi_Number3;
            //data.Yobi_Number4 = dataVm.Yobi_Number4;
            //data.Yobi_Number5 = dataVm.Yobi_Number5;
            //data.Yobi_Decimal1 = dataVm.Yobi_Decimal1;
            //data.Yobi_Decimal2 = dataVm.Yobi_Decimal2;
            //data.Yobi_Decimal3 = dataVm.Yobi_Decimal3;
            //data.Yobi_Decimal4 = dataVm.Yobi_Decimal4;
            //data.Yobi_Decimal5 = dataVm.Yobi_Decimal5;
            //data.Yobi_Date1 = dataVm.Yobi_Date1;
            //data.Yobi_Date2 = dataVm.Yobi_Date2;
            //data.Yobi_Date3 = dataVm.Yobi_Date3;
            //data.Yobi_Date4 = dataVm.Yobi_Date4;
            //data.Yobi_Date5 = dataVm.Yobi_Date5;
            //item dùng chung-End

        }

        public static void UpdateEmpSalary(this EmpSalary data, EmpSalaryViewModel dataVm)
        {
            data.ID = dataVm.ID;
            data.EmpID = dataVm.EmpID;
            data.ContractID = dataVm.ContractID;
            data.StartDate = dataVm.StartDate;
            data.EndDate = dataVm.EndDate;
            data.PreSalaryUnit = dataVm.PreSalaryUnit;
            data.PreNetSalary = dataVm.PreNetSalary;
            data.PreAdditionMoney = dataVm.PreAdditionMoney;
            data.PreAdditionPercent = dataVm.PreAdditionPercent;
            data.PreNetAllowance = dataVm.PreNetAllowance;
            data.PreYMSalary = dataVm.PreYMSalary;
            data.KonSalaryUnit = dataVm.KonSalaryUnit;
            data.KonNetSalary = dataVm.KonNetSalary;
            data.KonAdditionMoney = dataVm.KonAdditionMoney;
            data.KonAdditionPercent = dataVm.KonAdditionPercent;
            data.KonNetAllowance = dataVm.KonNetAllowance;
            data.KonYMSalary = dataVm.KonYMSalary;
            data.NextYMSalary = dataVm.NextYMSalary;
            data.SalaryIncreaseTypeMasterID = dataVm.SalaryIncreaseTypeMasterID;
            data.SalaryIncreaseTypeMasterDetailID = dataVm.SalaryIncreaseTypeMasterDetailID;
            data.SignDate = dataVm.SignDate;
            data.Result = dataVm.Result;
            data.Action = dataVm.Action;
            data.FileID = dataVm.FileID;

            //item dùng chung-Start
            data.RowVersion = dataVm.RowVersion;
            data.DisplayOrder = dataVm.DisplayOrder;
            data.AccountData = dataVm.AccountData;
            data.Note = dataVm.Note;
            
            data.AccessDataLevel = dataVm.AccessDataLevel;
            data.CreatedDate = dataVm.CreatedDate;
            data.CreatedBy = dataVm.CreatedBy;
            data.UpdatedDate = dataVm.UpdatedDate;
            data.UpdatedBy = dataVm.UpdatedBy;
            data.MetaKeyword = dataVm.MetaKeyword;
            data.MetaDescription = dataVm.MetaDescription;
            data.Status = dataVm.Status;
            data.DataStatus = dataVm.DataStatus;

            data.RequestBy = dataVm.RequestBy;
            data.RequestDate = dataVm.RequestDate;
            data.ApprovedBy = dataVm.ApprovedBy;
            data.ApprovedDate = dataVm.ApprovedDate;
            data.ApprovedStatus = dataVm.ApprovedStatus;

            //data.Yobi_Text1 = dataVm.Yobi_Text1;
            //data.Yobi_Text2 = dataVm.Yobi_Text2;
            //data.Yobi_Text3 = dataVm.Yobi_Text3;
            //data.Yobi_Text4 = dataVm.Yobi_Text4;
            //data.Yobi_Text5 = dataVm.Yobi_Text5;
            //data.Yobi_Number1 = dataVm.Yobi_Number1;
            //data.Yobi_Number2 = dataVm.Yobi_Number2;
            //data.Yobi_Number3 = dataVm.Yobi_Number3;
            //data.Yobi_Number4 = dataVm.Yobi_Number4;
            //data.Yobi_Number5 = dataVm.Yobi_Number5;
            //data.Yobi_Decimal1 = dataVm.Yobi_Decimal1;
            //data.Yobi_Decimal2 = dataVm.Yobi_Decimal2;
            //data.Yobi_Decimal3 = dataVm.Yobi_Decimal3;
            //data.Yobi_Decimal4 = dataVm.Yobi_Decimal4;
            //data.Yobi_Decimal5 = dataVm.Yobi_Decimal5;
            //data.Yobi_Date1 = dataVm.Yobi_Date1;
            //data.Yobi_Date2 = dataVm.Yobi_Date2;
            //data.Yobi_Date3 = dataVm.Yobi_Date3;
            //data.Yobi_Date4 = dataVm.Yobi_Date4;
            //data.Yobi_Date5 = dataVm.Yobi_Date5;
            //item dùng chung-End

        }

        public static void UpdateEmpAllowance(this EmpAllowance data, EmpAllowanceViewModel dataVm)
        {

            data.ID = dataVm.ID;
            data.EmpID = dataVm.EmpID;
            data.StartDate = dataVm.StartDate;
            data.EndDate = dataVm.EndDate;
            data.AllowanceTypeMasterID = dataVm.AllowanceTypeMasterID;
            data.AllowanceTypeMasterDetailID = dataVm.AllowanceTypeMasterDetailID;
            data.AllowanceMoney = dataVm.AllowanceMoney;
            data.SignDate = dataVm.SignDate;
            data.Result = dataVm.Result;
            data.Action = dataVm.Action;
            data.FileID = dataVm.FileID;

            //item dùng chung-Start
            data.RowVersion = dataVm.RowVersion;
            data.DisplayOrder = dataVm.DisplayOrder;
            data.AccountData = dataVm.AccountData;
            data.Note = dataVm.Note;
            
            data.AccessDataLevel = dataVm.AccessDataLevel;
            data.CreatedDate = dataVm.CreatedDate;
            data.CreatedBy = dataVm.CreatedBy;
            data.UpdatedDate = dataVm.UpdatedDate;
            data.UpdatedBy = dataVm.UpdatedBy;
            data.MetaKeyword = dataVm.MetaKeyword;
            data.MetaDescription = dataVm.MetaDescription;
            data.Status = dataVm.Status;
            data.DataStatus = dataVm.DataStatus;

            data.RequestBy = dataVm.RequestBy;
            data.RequestDate = dataVm.RequestDate;
            data.ApprovedBy = dataVm.ApprovedBy;
            data.ApprovedDate = dataVm.ApprovedDate;
            data.ApprovedStatus = dataVm.ApprovedStatus;

            //data.Yobi_Text1 = dataVm.Yobi_Text1;
            //data.Yobi_Text2 = dataVm.Yobi_Text2;
            //data.Yobi_Text3 = dataVm.Yobi_Text3;
            //data.Yobi_Text4 = dataVm.Yobi_Text4;
            //data.Yobi_Text5 = dataVm.Yobi_Text5;
            //data.Yobi_Number1 = dataVm.Yobi_Number1;
            //data.Yobi_Number2 = dataVm.Yobi_Number2;
            //data.Yobi_Number3 = dataVm.Yobi_Number3;
            //data.Yobi_Number4 = dataVm.Yobi_Number4;
            //data.Yobi_Number5 = dataVm.Yobi_Number5;
            //data.Yobi_Decimal1 = dataVm.Yobi_Decimal1;
            //data.Yobi_Decimal2 = dataVm.Yobi_Decimal2;
            //data.Yobi_Decimal3 = dataVm.Yobi_Decimal3;
            //data.Yobi_Decimal4 = dataVm.Yobi_Decimal4;
            //data.Yobi_Decimal5 = dataVm.Yobi_Decimal5;
            //data.Yobi_Date1 = dataVm.Yobi_Date1;
            //data.Yobi_Date2 = dataVm.Yobi_Date2;
            //data.Yobi_Date3 = dataVm.Yobi_Date3;
            //data.Yobi_Date4 = dataVm.Yobi_Date4;
            //data.Yobi_Date5 = dataVm.Yobi_Date5;
            //item dùng chung-End

        }

        public static void UpdateEmpDetailWork(this EmpDetailWork data, EmpDetailWorkViewModel dataVm)
        {
            data.ID = dataVm.ID;
            data.EmpID = dataVm.EmpID;
            data.StartDate = dataVm.StartDate;
            data.EndDate = dataVm.EndDate;

            data.CompanyID = dataVm.CompanyID;
            data.IsChangeCompanyID = dataVm.IsChangeCompanyID;
            data.DeptID = dataVm.DeptID;
            data.IsChangeDeptID = dataVm.IsChangeDeptID;
            data.TeamID = dataVm.TeamID;
            data.IsChangeTeamID = dataVm.IsChangeTeamID;
            data.PositionID = dataVm.PositionID;
            data.IsChangePositionID = dataVm.IsChangePositionID;
            data.Company2ID = dataVm.Company2ID;
            data.IsChangeCompany2ID = dataVm.IsChangeCompany2ID;
            data.Dept2ID = dataVm.Dept2ID;
            data.IsChangeDept2ID = dataVm.IsChangeDept2ID;
            data.Team2ID = dataVm.Team2ID;
            data.IsChangeTeam2ID = dataVm.IsChangeTeam2ID;
            data.Position2ID = dataVm.Position2ID;
            data.IsChangePosition2ID = dataVm.IsChangePosition2ID;
            data.WorkEmpTypeMasterID = dataVm.WorkEmpTypeMasterID;
            data.WorkEmpTypeMasterDetailID = dataVm.WorkEmpTypeMasterDetailID;
            data.IsChangeWorkEmpType = dataVm.IsChangeWorkEmpType;
            data.EmpTypeMasterID = dataVm.EmpTypeMasterID;
            data.EmpTypeMasterDetailID = dataVm.EmpTypeMasterDetailID;
            data.IsChangeEmpType = dataVm.IsChangeEmpType;
            data.JapaneseLevelMasterID = dataVm.JapaneseLevelMasterID;
            data.JapaneseLevelMasterDetailID = dataVm.JapaneseLevelMasterDetailID;
            data.IsChangeJapaneseLevel = dataVm.IsChangeJapaneseLevel;
            data.BusinessAllowanceLevelMasterID = dataVm.BusinessAllowanceLevelMasterID;
            data.BusinessAllowanceLevelMasterDetailID = dataVm.BusinessAllowanceLevelMasterDetailID;
            data.IsChangeBusinessAllowanceLevel = dataVm.IsChangeBusinessAllowanceLevel;
            data.RoomWithInternetAllowanceLevelMasterID = dataVm.RoomWithInternetAllowanceLevelMasterID;
            data.RoomWithInternetAllowanceLevelMasterDetailID = dataVm.RoomWithInternetAllowanceLevelMasterDetailID;
            data.IsChangeRoomWithInternetAllowanceLevel = dataVm.IsChangeRoomWithInternetAllowanceLevel;
            data.RoomNoInternetAllowanceLevelMasterID = dataVm.RoomNoInternetAllowanceLevelMasterID;
            data.RoomNoInternetAllowanceLevelMasterDetailID = dataVm.RoomNoInternetAllowanceLevelMasterDetailID;
            data.IsChangeRoomNoInternetAllowanceLevel = dataVm.IsChangeRoomNoInternetAllowanceLevel;
            data.BseAllowanceLevelMasterID = dataVm.BseAllowanceLevelMasterID;
            data.BseAllowanceLevelMasterDetailID = dataVm.BseAllowanceLevelMasterDetailID;
            data.IsChangeBseAllowanceLevel = dataVm.IsChangeBseAllowanceLevel;
            data.CollectMasterID = dataVm.CollectMasterID;
            data.CollectMasterDetailID = dataVm.CollectMasterDetailID;
            data.IsChangeCollect = dataVm.IsChangeCollect;
            data.EducationLevelMasterID = dataVm.EducationLevelMasterID;
            data.EducationLevelMasterDetailID = dataVm.EducationLevelMasterDetailID;
            data.IsChangeEducationLevel = dataVm.IsChangeEducationLevel;
            data.ContractTypeMasterID = dataVm.ContractTypeMasterID;
            data.ContractTypeMasterDetailID = dataVm.ContractTypeMasterDetailID;
            data.IsChangeContractType = dataVm.IsChangeContractType;
            data.OnsiteCustomerID = dataVm.OnsiteCustomerID;
            data.IsChangeOnsiteCustomerID = dataVm.IsChangeOnsiteCustomerID;
            data.SignDate = dataVm.SignDate;
            data.Result = dataVm.Result;

            data.Action = dataVm.Action;

            //item dùng chung-Start
            data.RowVersion = dataVm.RowVersion;
            data.DisplayOrder = dataVm.DisplayOrder;
            data.AccountData = dataVm.AccountData;
            data.Note = dataVm.Note;
            
            data.AccessDataLevel = dataVm.AccessDataLevel;
            data.CreatedDate = dataVm.CreatedDate;
            data.CreatedBy = dataVm.CreatedBy;
            data.UpdatedDate = dataVm.UpdatedDate;
            data.UpdatedBy = dataVm.UpdatedBy;
            data.MetaKeyword = dataVm.MetaKeyword;
            data.MetaDescription = dataVm.MetaDescription;
            data.Status = dataVm.Status;
            data.DataStatus = dataVm.DataStatus;

            data.RequestBy = dataVm.RequestBy;
            data.RequestDate = dataVm.RequestDate;
            data.ApprovedBy = dataVm.ApprovedBy;
            data.ApprovedDate = dataVm.ApprovedDate;
            data.ApprovedStatus = dataVm.ApprovedStatus;

            //data.Yobi_Text1 = dataVm.Yobi_Text1;
            //data.Yobi_Text2 = dataVm.Yobi_Text2;
            //data.Yobi_Text3 = dataVm.Yobi_Text3;
            //data.Yobi_Text4 = dataVm.Yobi_Text4;
            //data.Yobi_Text5 = dataVm.Yobi_Text5;
            //data.Yobi_Number1 = dataVm.Yobi_Number1;
            //data.Yobi_Number2 = dataVm.Yobi_Number2;
            //data.Yobi_Number3 = dataVm.Yobi_Number3;
            //data.Yobi_Number4 = dataVm.Yobi_Number4;
            //data.Yobi_Number5 = dataVm.Yobi_Number5;
            //data.Yobi_Decimal1 = dataVm.Yobi_Decimal1;
            //data.Yobi_Decimal2 = dataVm.Yobi_Decimal2;
            //data.Yobi_Decimal3 = dataVm.Yobi_Decimal3;
            //data.Yobi_Decimal4 = dataVm.Yobi_Decimal4;
            //data.Yobi_Decimal5 = dataVm.Yobi_Decimal5;
            //data.Yobi_Date1 = dataVm.Yobi_Date1;
            //data.Yobi_Date2 = dataVm.Yobi_Date2;
            //data.Yobi_Date3 = dataVm.Yobi_Date3;
            //data.Yobi_Date4 = dataVm.Yobi_Date4;
            //data.Yobi_Date5 = dataVm.Yobi_Date5;

        }

        public static void UpdateEmpOnsite(this EmpOnsite data, EmpOnsiteViewModel dataVm)
        {
            data.ID = dataVm.ID;
            data.EmpID = dataVm.EmpID;
            data.StartDate = dataVm.StartDate;
            data.EndDate = dataVm.EndDate;
            data.OnsiteTypeMasterID = dataVm.OnsiteTypeMasterID;
            data.OnsiteTypeMasterDetailID = dataVm.OnsiteTypeMasterDetailID;
            data.OnsiteKikan = dataVm.OnsiteKikan;
            data.PromiseWorkKikan = dataVm.PromiseWorkKikan;
            data.OnsiteKikanTimeUnitMasterID = dataVm.OnsiteKikanTimeUnitMasterID;
            data.OnsiteKikanTimeUnitMasterDetailID = dataVm.OnsiteKikanTimeUnitMasterDetailID;
            data.IsContractSign = dataVm.IsContractSign;
            data.SignDate = dataVm.SignDate;
            data.JapanTeamID = dataVm.JapanTeamID;
            data.CustomerID = dataVm.CustomerID;
            data.Result = dataVm.Result;
            data.Action = dataVm.Action;
            data.FileID = dataVm.FileID;

            //item dùng chung-Start
            data.RowVersion = dataVm.RowVersion;
            data.DisplayOrder = dataVm.DisplayOrder;
            data.AccountData = dataVm.AccountData;
            data.Note = dataVm.Note;
            
            data.AccessDataLevel = dataVm.AccessDataLevel;
            data.CreatedDate = dataVm.CreatedDate;
            data.CreatedBy = dataVm.CreatedBy;
            data.UpdatedDate = dataVm.UpdatedDate;
            data.UpdatedBy = dataVm.UpdatedBy;
            data.MetaKeyword = dataVm.MetaKeyword;
            data.MetaDescription = dataVm.MetaDescription;
            data.Status = dataVm.Status;
            data.DataStatus = dataVm.DataStatus;

            data.RequestBy = dataVm.RequestBy;
            data.RequestDate = dataVm.RequestDate;
            data.ApprovedBy = dataVm.ApprovedBy;
            data.ApprovedDate = dataVm.ApprovedDate;
            data.ApprovedStatus = dataVm.ApprovedStatus;

            //data.Yobi_Text1 = dataVm.Yobi_Text1;
            //data.Yobi_Text2 = dataVm.Yobi_Text2;
            //data.Yobi_Text3 = dataVm.Yobi_Text3;
            //data.Yobi_Text4 = dataVm.Yobi_Text4;
            //data.Yobi_Text5 = dataVm.Yobi_Text5;
            //data.Yobi_Number1 = dataVm.Yobi_Number1;
            //data.Yobi_Number2 = dataVm.Yobi_Number2;
            //data.Yobi_Number3 = dataVm.Yobi_Number3;
            //data.Yobi_Number4 = dataVm.Yobi_Number4;
            //data.Yobi_Number5 = dataVm.Yobi_Number5;
            //data.Yobi_Decimal1 = dataVm.Yobi_Decimal1;
            //data.Yobi_Decimal2 = dataVm.Yobi_Decimal2;
            //data.Yobi_Decimal3 = dataVm.Yobi_Decimal3;
            //data.Yobi_Decimal4 = dataVm.Yobi_Decimal4;
            //data.Yobi_Decimal5 = dataVm.Yobi_Decimal5;
            //data.Yobi_Date1 = dataVm.Yobi_Date1;
            //data.Yobi_Date2 = dataVm.Yobi_Date2;
            //data.Yobi_Date3 = dataVm.Yobi_Date3;
            //data.Yobi_Date4 = dataVm.Yobi_Date4;
            //data.Yobi_Date5 = dataVm.Yobi_Date5;

        }

        public static void UpdateCustomer(this Customer data, CustomerViewModel dataVm)
        {
            data.No = dataVm.No;
            data.ID = dataVm.ID;
            data.Name = dataVm.Name;
            data.ShortName = dataVm.ShortName;
            data.NameUseInReport = dataVm.NameUseInReport;
            data.ContractDate = dataVm.ContractDate;
            data.Sime = dataVm.Sime;
            data.Phone1 = dataVm.Phone1;
            data.Phone2 = dataVm.Phone2;
            data.Fax = dataVm.Fax;
            data.EmailDomain = dataVm.EmailDomain;
            data.WebSiteUrl = dataVm.WebSiteUrl;
            data.Address1 = dataVm.Address1;
            data.Address2 = dataVm.Address2;
            data.GlobalIpList = dataVm.GlobalIpList;
            data.MangRate = dataVm.MangRate;
            data.TransRate = dataVm.TransRate;
            data.GroupName = dataVm.GroupName;
            data.DefaultOrderUnitMasterID= dataVm.DefaultOrderUnitMasterID;
            data.DefaultOrderUnitMasterDetailID = dataVm.DefaultOrderUnitMasterDetailID;
            //item dùng chung-Start
            data.RowVersion = dataVm.RowVersion;
            data.DisplayOrder = dataVm.DisplayOrder;
            data.AccountData = dataVm.AccountData;
            data.Note = dataVm.Note;
            
            data.AccessDataLevel = dataVm.AccessDataLevel;
            data.CreatedDate = dataVm.CreatedDate;
            data.CreatedBy = dataVm.CreatedBy;
            data.UpdatedDate = dataVm.UpdatedDate;
            data.UpdatedBy = dataVm.UpdatedBy;
            data.MetaKeyword = dataVm.MetaKeyword;
            data.MetaDescription = dataVm.MetaDescription;
            data.Status = dataVm.Status;
            data.DataStatus = dataVm.DataStatus;

            data.RequestBy = dataVm.RequestBy;
            data.RequestDate = dataVm.RequestDate;
            data.ApprovedBy = dataVm.ApprovedBy;
            data.ApprovedDate = dataVm.ApprovedDate;
            data.ApprovedStatus = dataVm.ApprovedStatus;

            //data.Yobi_Text1 = dataVm.Yobi_Text1;
            //data.Yobi_Text2 = dataVm.Yobi_Text2;
            //data.Yobi_Text3 = dataVm.Yobi_Text3;
            //data.Yobi_Text4 = dataVm.Yobi_Text4;
            //data.Yobi_Text5 = dataVm.Yobi_Text5;
            //data.Yobi_Number1 = dataVm.Yobi_Number1;
            //data.Yobi_Number2 = dataVm.Yobi_Number2;
            //data.Yobi_Number3 = dataVm.Yobi_Number3;
            //data.Yobi_Number4 = dataVm.Yobi_Number4;
            //data.Yobi_Number5 = dataVm.Yobi_Number5;
            //data.Yobi_Decimal1 = dataVm.Yobi_Decimal1;
            //data.Yobi_Decimal2 = dataVm.Yobi_Decimal2;
            //data.Yobi_Decimal3 = dataVm.Yobi_Decimal3;
            //data.Yobi_Decimal4 = dataVm.Yobi_Decimal4;
            //data.Yobi_Decimal5 = dataVm.Yobi_Decimal5;
            //data.Yobi_Date1 = dataVm.Yobi_Date1;
            //data.Yobi_Date2 = dataVm.Yobi_Date2;
            //data.Yobi_Date3 = dataVm.Yobi_Date3;
            //data.Yobi_Date4 = dataVm.Yobi_Date4;
            //data.Yobi_Date5 = dataVm.Yobi_Date5;

        }

        public static void UpdateProject(this Project data, ProjectViewModel dataVm)
        {
            data.No = dataVm.No;
            data.ID = dataVm.ID;
            data.CustomerID = dataVm.CustomerID;
            data.OrderReceivedID = dataVm.OrderReceivedID;
            data.Name = dataVm.Name;
            data.NameJp = dataVm.NameJp;
            data.ShortName = dataVm.ShortName;
            data.CompanyProjectID = dataVm.CompanyProjectID;
            data.EstimateManMonth = dataVm.EstimateManMonth;
            data.ActualManMonth = dataVm.ActualManMonth;

            //item dùng chung-Start
            data.RowVersion = dataVm.RowVersion;
            data.DisplayOrder = dataVm.DisplayOrder;
            data.AccountData = dataVm.AccountData;
            data.Note = dataVm.Note;
            
            data.AccessDataLevel = dataVm.AccessDataLevel;
            data.CreatedDate = dataVm.CreatedDate;
            data.CreatedBy = dataVm.CreatedBy;
            data.UpdatedDate = dataVm.UpdatedDate;
            data.UpdatedBy = dataVm.UpdatedBy;
            data.MetaKeyword = dataVm.MetaKeyword;
            data.MetaDescription = dataVm.MetaDescription;
            data.Status = dataVm.Status;
            data.DataStatus = dataVm.DataStatus;

            data.RequestBy = dataVm.RequestBy;
            data.RequestDate = dataVm.RequestDate;
            data.ApprovedBy = dataVm.ApprovedBy;
            data.ApprovedDate = dataVm.ApprovedDate;
            data.ApprovedStatus = dataVm.ApprovedStatus;

            //data.Yobi_Text1 = dataVm.Yobi_Text1;
            //data.Yobi_Text2 = dataVm.Yobi_Text2;
            //data.Yobi_Text3 = dataVm.Yobi_Text3;
            //data.Yobi_Text4 = dataVm.Yobi_Text4;
            //data.Yobi_Text5 = dataVm.Yobi_Text5;
            //data.Yobi_Number1 = dataVm.Yobi_Number1;
            //data.Yobi_Number2 = dataVm.Yobi_Number2;
            //data.Yobi_Number3 = dataVm.Yobi_Number3;
            //data.Yobi_Number4 = dataVm.Yobi_Number4;
            //data.Yobi_Number5 = dataVm.Yobi_Number5;
            //data.Yobi_Decimal1 = dataVm.Yobi_Decimal1;
            //data.Yobi_Decimal2 = dataVm.Yobi_Decimal2;
            //data.Yobi_Decimal3 = dataVm.Yobi_Decimal3;
            //data.Yobi_Decimal4 = dataVm.Yobi_Decimal4;
            //data.Yobi_Decimal5 = dataVm.Yobi_Decimal5;
            //data.Yobi_Date1 = dataVm.Yobi_Date1;
            //data.Yobi_Date2 = dataVm.Yobi_Date2;
            //data.Yobi_Date3 = dataVm.Yobi_Date3;
            //data.Yobi_Date4 = dataVm.Yobi_Date4;
            //data.Yobi_Date5 = dataVm.Yobi_Date5;
            //item dùng chung-End

        }

        public static void UpdateProjectDetail(this ProjectDetail data, ProjectDetailViewModel dataVm)
        {
            data.ID = dataVm.ID;
            data.ProjectID = dataVm.ProjectID;
            data.ProjectDetailID = dataVm.ProjectDetailID;
            data.OrderReceivedID = dataVm.OrderReceivedID;
            data.CustomerID = dataVm.CustomerID;
            data.CompanyID = dataVm.CompanyID;
            data.Name = dataVm.Name;
            data.NameJp = dataVm.NameJp;
            data.ShortName = dataVm.ShortName;
            data.EstimateManMonth = dataVm.EstimateManMonth;
            data.ActualManMonth = dataVm.ActualManMonth;
            data.PMID = dataVm.PMID;
            data.PLID = dataVm.PLID;
            data.PlanStartDate = dataVm.PlanStartDate;
            data.PlanEndDate = dataVm.PlanEndDate;
            data.ActualStartDate = dataVm.ActualStartDate;
            data.ActualEndDate = dataVm.ActualEndDate;
            data.TotalQACount = dataVm.TotalQACount;
            data.AfterDelBugCount = dataVm.AfterDelBugCount;
            data.ChangeDetailDesignBugCount = dataVm.ChangeDetailDesignBugCount;
            data.MyCompanyBugCount = dataVm.MyCompanyBugCount;
            data.NotBugCount = dataVm.NotBugCount;
            data.MissDetailDesignBugCount = dataVm.MissDetailDesignBugCount;
            data.PgCount = dataVm.PgCount;
            data.ProjectTypeMasterID = dataVm.ProjectTypeMasterID;
            data.ProjectTypeMasterDetailID = dataVm.ProjectTypeMasterDetailID;
            data.Language = dataVm.Language;
            data.EstLocCount = dataVm.EstLocCount;
            data.ActualLocCount = dataVm.ActualLocCount;
            data.EstPerformance = dataVm.EstPerformance;
            data.ActualPerformance = dataVm.ActualPerformance;
            data.IsScheduleProject = dataVm.IsScheduleProject;

            //item dùng chung-Start
            data.RowVersion = dataVm.RowVersion;
            data.DisplayOrder = dataVm.DisplayOrder;
            data.AccountData = dataVm.AccountData;
            data.Note = dataVm.Note;
            
            data.AccessDataLevel = dataVm.AccessDataLevel;
            data.CreatedDate = dataVm.CreatedDate;
            data.CreatedBy = dataVm.CreatedBy;
            data.UpdatedDate = dataVm.UpdatedDate;
            data.UpdatedBy = dataVm.UpdatedBy;
            data.MetaKeyword = dataVm.MetaKeyword;
            data.MetaDescription = dataVm.MetaDescription;
            data.Status = dataVm.Status;
            data.DataStatus = dataVm.DataStatus;

            data.RequestBy = dataVm.RequestBy;
            data.RequestDate = dataVm.RequestDate;
            data.ApprovedBy = dataVm.ApprovedBy;
            data.ApprovedDate = dataVm.ApprovedDate;
            data.ApprovedStatus = dataVm.ApprovedStatus;

            //data.Yobi_Text1 = dataVm.Yobi_Text1;
            //data.Yobi_Text2 = dataVm.Yobi_Text2;
            //data.Yobi_Text3 = dataVm.Yobi_Text3;
            //data.Yobi_Text4 = dataVm.Yobi_Text4;
            //data.Yobi_Text5 = dataVm.Yobi_Text5;
            //data.Yobi_Number1 = dataVm.Yobi_Number1;
            //data.Yobi_Number2 = dataVm.Yobi_Number2;
            //data.Yobi_Number3 = dataVm.Yobi_Number3;
            //data.Yobi_Number4 = dataVm.Yobi_Number4;
            //data.Yobi_Number5 = dataVm.Yobi_Number5;
            //data.Yobi_Decimal1 = dataVm.Yobi_Decimal1;
            //data.Yobi_Decimal2 = dataVm.Yobi_Decimal2;
            //data.Yobi_Decimal3 = dataVm.Yobi_Decimal3;
            //data.Yobi_Decimal4 = dataVm.Yobi_Decimal4;
            //data.Yobi_Decimal5 = dataVm.Yobi_Decimal5;
            //data.Yobi_Date1 = dataVm.Yobi_Date1;
            //data.Yobi_Date2 = dataVm.Yobi_Date2;
            //data.Yobi_Date3 = dataVm.Yobi_Date3;
            //data.Yobi_Date4 = dataVm.Yobi_Date4;
            //data.Yobi_Date5 = dataVm.Yobi_Date5;
            //item dùng chung-End

        }

        public static void UpdateProjectDetailResource(this ProjectDetailResource data, ProjectDetailResourceViewModel dataVm)
        {

            data.ID = dataVm.ID;
            data.ProjectID = dataVm.ProjectID;
            data.ProjectDetailID = dataVm.ProjectDetailID;
            data.FtpAccout = dataVm.FtpAccout;
            data.FtpPassword = dataVm.FtpPassword;
            data.FtpPort = dataVm.FtpPort;
            data.FtpLocalPath = dataVm.FtpLocalPath;
            data.TransMailAccount = dataVm.TransMailAccount;
            data.TransMailPassword = dataVm.TransMailPassword;
            data.EmailManagementGroup = dataVm.EmailManagementGroup;
            data.EmailDevGroup = dataVm.EmailDevGroup;
            data.QAMSAccount = dataVm.QAMSAccount;
            data.QAMSPassword = dataVm.QAMSPassword;
            data.CustomerGlobalIpList = dataVm.CustomerGlobalIpList;
            data.MyCompanyGlobalIpList = dataVm.MyCompanyGlobalIpList;
            data.VirtualPc1 = dataVm.VirtualPc1;
            data.VirtualPc1EndDate = dataVm.VirtualPc1EndDate;
            data.VirtualPc2 = dataVm.VirtualPc2;
            data.VirtualPc2EndDate = dataVm.VirtualPc2EndDate;

            //item dùng chung-Start
            data.RowVersion = dataVm.RowVersion;
            data.DisplayOrder = dataVm.DisplayOrder;
            data.AccountData = dataVm.AccountData;
            data.Note = dataVm.Note;
            
            data.AccessDataLevel = dataVm.AccessDataLevel;
            data.CreatedDate = dataVm.CreatedDate;
            data.CreatedBy = dataVm.CreatedBy;
            data.UpdatedDate = dataVm.UpdatedDate;
            data.UpdatedBy = dataVm.UpdatedBy;
            data.MetaKeyword = dataVm.MetaKeyword;
            data.MetaDescription = dataVm.MetaDescription;
            data.Status = dataVm.Status;
            data.DataStatus = dataVm.DataStatus;

            data.RequestBy = dataVm.RequestBy;
            data.RequestDate = dataVm.RequestDate;
            data.ApprovedBy = dataVm.ApprovedBy;
            data.ApprovedDate = dataVm.ApprovedDate;
            data.ApprovedStatus = dataVm.ApprovedStatus;

            //data.Yobi_Text1 = dataVm.Yobi_Text1;
            //data.Yobi_Text2 = dataVm.Yobi_Text2;
            //data.Yobi_Text3 = dataVm.Yobi_Text3;
            //data.Yobi_Text4 = dataVm.Yobi_Text4;
            //data.Yobi_Text5 = dataVm.Yobi_Text5;
            //data.Yobi_Number1 = dataVm.Yobi_Number1;
            //data.Yobi_Number2 = dataVm.Yobi_Number2;
            //data.Yobi_Number3 = dataVm.Yobi_Number3;
            //data.Yobi_Number4 = dataVm.Yobi_Number4;
            //data.Yobi_Number5 = dataVm.Yobi_Number5;
            //data.Yobi_Decimal1 = dataVm.Yobi_Decimal1;
            //data.Yobi_Decimal2 = dataVm.Yobi_Decimal2;
            //data.Yobi_Decimal3 = dataVm.Yobi_Decimal3;
            //data.Yobi_Decimal4 = dataVm.Yobi_Decimal4;
            //data.Yobi_Decimal5 = dataVm.Yobi_Decimal5;
            //data.Yobi_Date1 = dataVm.Yobi_Date1;
            //data.Yobi_Date2 = dataVm.Yobi_Date2;
            //data.Yobi_Date3 = dataVm.Yobi_Date3;
            //data.Yobi_Date4 = dataVm.Yobi_Date4;
            //data.Yobi_Date5 = dataVm.Yobi_Date5;
            //item dùng chung-End

        }

        public static void UpdateRevenue(this Revenue data, RevenueViewModel dataVm)
        {

            data.ID = dataVm.ID;
            data.CompanyID = dataVm.CompanyID;
            data.DeptID = dataVm.DeptID;
            data.TeamID = dataVm.TeamID;
            data.ReporterID = dataVm.ReporterID;
            data.ReportDate = dataVm.ReportDate;
            data.ReportYearMonth = dataVm.ReportYearMonth;
            data.OrderNo = dataVm.OrderNo;
            data.EstimateNo = dataVm.EstimateNo;
            data.ReportTitle = dataVm.ReportTitle;
            data.ProjectID = dataVm.ProjectID;
            data.ProjectDetailID = dataVm.ProjectDetailID;
            data.ProjectName = dataVm.ProjectName;
            data.ProjectContent = dataVm.ProjectContent;
            data.EstimateTypeMasterID = dataVm.EstimateTypeMasterID;
            data.EstimateTypeMasterDetailID = dataVm.EstimateTypeMasterDetailID;
            data.CustomerID = dataVm.CustomerID;
            data.CustomerName = dataVm.CustomerName;
            data.OrderStartDate = dataVm.OrderStartDate;
            data.OrderEndDate = dataVm.OrderEndDate;
            data.OrderProjectSumMM = dataVm.OrderProjectSumMM;
            data.OrderUnitMasterID = dataVm.OrderUnitMasterID;
            data.OrderUnitMasterDetailID = dataVm.OrderUnitMasterDetailID;
            data.ExchangeRateID = dataVm.ExchangeRateID;
            data.CustomerUnitPriceID = dataVm.CustomerUnitPriceID;
            data.OrderPrice = dataVm.OrderPrice;
            data.OrderPriceToUsd = dataVm.OrderPriceToUsd;
            data.AccPreMonthSumMM = dataVm.AccPreMonthSumMM;
            data.AccPreMonthSumToUsd = dataVm.AccPreMonthSumToUsd;
            data.InMonthDevMM = dataVm.InMonthDevMM;
            data.InMonthTransMM = dataVm.InMonthTransMM;
            data.InMonthManagementMM = dataVm.InMonthManagementMM;
            data.InMonthOnsiteMM = dataVm.InMonthOnsiteMM;
            data.InMonthSumMM = dataVm.InMonthSumMM;
            data.InMonthSumIncludeOnsiteMM = dataVm.InMonthSumIncludeOnsiteMM;
            data.InMonthDevSumExcludeTransMM = dataVm.InMonthDevSumExcludeTransMM;
            data.InMonthToUsd = dataVm.InMonthToUsd;
            data.InMonthToVnd = dataVm.InMonthToVnd;
            data.NextMonth = dataVm.NextMonth;
            data.NextMonthMM = dataVm.NextMonthMM;
            data.NextMonthToUsd = dataVm.NextMonthToUsd;
            data.PMID = dataVm.PMID;
            data.PLID = dataVm.PLID;

            //item dùng chung-Start
            data.RowVersion = dataVm.RowVersion;
            data.DisplayOrder = dataVm.DisplayOrder;
            data.AccountData = dataVm.AccountData;
            data.Note = dataVm.Note;
            
            data.AccessDataLevel = dataVm.AccessDataLevel;
            data.CreatedDate = dataVm.CreatedDate;
            data.CreatedBy = dataVm.CreatedBy;
            data.UpdatedDate = dataVm.UpdatedDate;
            data.UpdatedBy = dataVm.UpdatedBy;
            data.MetaKeyword = dataVm.MetaKeyword;
            data.MetaDescription = dataVm.MetaDescription;
            data.Status = dataVm.Status;
            data.DataStatus = dataVm.DataStatus;

            data.RequestBy = dataVm.RequestBy;
            data.RequestDate = dataVm.RequestDate;
            data.ApprovedBy = dataVm.ApprovedBy;
            data.ApprovedDate = dataVm.ApprovedDate;
            data.ApprovedStatus = dataVm.ApprovedStatus;

            //data.Yobi_Text1 = dataVm.Yobi_Text1;
            //data.Yobi_Text2 = dataVm.Yobi_Text2;
            //data.Yobi_Text3 = dataVm.Yobi_Text3;
            //data.Yobi_Text4 = dataVm.Yobi_Text4;
            //data.Yobi_Text5 = dataVm.Yobi_Text5;
            //data.Yobi_Number1 = dataVm.Yobi_Number1;
            //data.Yobi_Number2 = dataVm.Yobi_Number2;
            //data.Yobi_Number3 = dataVm.Yobi_Number3;
            //data.Yobi_Number4 = dataVm.Yobi_Number4;
            //data.Yobi_Number5 = dataVm.Yobi_Number5;
            //data.Yobi_Decimal1 = dataVm.Yobi_Decimal1;
            //data.Yobi_Decimal2 = dataVm.Yobi_Decimal2;
            //data.Yobi_Decimal3 = dataVm.Yobi_Decimal3;
            //data.Yobi_Decimal4 = dataVm.Yobi_Decimal4;
            //data.Yobi_Decimal5 = dataVm.Yobi_Decimal5;
            //data.Yobi_Date1 = dataVm.Yobi_Date1;
            //data.Yobi_Date2 = dataVm.Yobi_Date2;
            //data.Yobi_Date3 = dataVm.Yobi_Date3;
            //data.Yobi_Date4 = dataVm.Yobi_Date4;
            //data.Yobi_Date5 = dataVm.Yobi_Date5;
            //item dùng chung-End

        }

        public static void UpdateFileStorage(this FileStorage data, FileStorageViewModel dataVm)
        {
            data.ID = dataVm.ID;
            data.FileName = dataVm.FileName;
            data.Name= dataVm.Name;
            data.FileExt = dataVm.FileExt;
            data.ContentType = dataVm.ContentType;
            data.PathOnHost = dataVm.PathOnHost;
            data.RelatedTable = dataVm.RelatedTable;
            data.RelatedKey = dataVm.RelatedKey;

            data.Data = dataVm.Data;
            data.Password = dataVm.Password;
            data.HaltString = dataVm.HaltString;

            //item dùng chung-Start
            data.RowVersion = dataVm.RowVersion;
            data.DisplayOrder = dataVm.DisplayOrder;
            data.AccountData = dataVm.AccountData;
            data.Note = dataVm.Note;
            
            data.AccessDataLevel = dataVm.AccessDataLevel;
            data.CreatedDate = dataVm.CreatedDate;
            data.CreatedBy = dataVm.CreatedBy;
            data.UpdatedDate = dataVm.UpdatedDate;
            data.UpdatedBy = dataVm.UpdatedBy;
            data.MetaKeyword = dataVm.MetaKeyword;
            data.MetaDescription = dataVm.MetaDescription;
            data.Status = dataVm.Status;
            data.DataStatus = dataVm.DataStatus;

            data.RequestBy = dataVm.RequestBy;
            data.RequestDate = dataVm.RequestDate;
            data.ApprovedBy = dataVm.ApprovedBy;
            data.ApprovedDate = dataVm.ApprovedDate;
            data.ApprovedStatus = dataVm.ApprovedStatus;

            //data.Yobi_Text1 = dataVm.Yobi_Text1;
            //data.Yobi_Text2 = dataVm.Yobi_Text2;
            //data.Yobi_Text3 = dataVm.Yobi_Text3;
            //data.Yobi_Text4 = dataVm.Yobi_Text4;
            //data.Yobi_Text5 = dataVm.Yobi_Text5;
            //data.Yobi_Number1 = dataVm.Yobi_Number1;
            //data.Yobi_Number2 = dataVm.Yobi_Number2;
            //data.Yobi_Number3 = dataVm.Yobi_Number3;
            //data.Yobi_Number4 = dataVm.Yobi_Number4;
            //data.Yobi_Number5 = dataVm.Yobi_Number5;
            //data.Yobi_Decimal1 = dataVm.Yobi_Decimal1;
            //data.Yobi_Decimal2 = dataVm.Yobi_Decimal2;
            //data.Yobi_Decimal3 = dataVm.Yobi_Decimal3;
            //data.Yobi_Decimal4 = dataVm.Yobi_Decimal4;
            //data.Yobi_Decimal5 = dataVm.Yobi_Decimal5;
            //data.Yobi_Date1 = dataVm.Yobi_Date1;
            //data.Yobi_Date2 = dataVm.Yobi_Date2;
            //data.Yobi_Date3 = dataVm.Yobi_Date3;
            //data.Yobi_Date4 = dataVm.Yobi_Date4;
            //data.Yobi_Date5 = dataVm.Yobi_Date5;
            //item dùng chung-End

        }

        public static void UpdateSchedule(this Schedule data, ScheduleViewModel dataVm)
        {
            data.ID = dataVm.ID;
            data.CreatorID = dataVm.CreatorID;
            data.Title = dataVm.Title;
            data.Description = dataVm.Description;
            data.Type = dataVm.Type;
            data.Location= dataVm.Location;
            data.TimeStart = dataVm.TimeStart;
            data.TimeEnd = dataVm.TimeEnd;

            //item dùng chung-Start
            data.RowVersion = dataVm.RowVersion;
            data.DisplayOrder = dataVm.DisplayOrder;
            data.AccountData = dataVm.AccountData;
            data.Note = dataVm.Note;
            
            data.AccessDataLevel = dataVm.AccessDataLevel;
            data.CreatedDate = dataVm.CreatedDate;
            data.CreatedBy = dataVm.CreatedBy;
            data.UpdatedDate = dataVm.UpdatedDate;
            data.UpdatedBy = dataVm.UpdatedBy;
            data.MetaKeyword = dataVm.MetaKeyword;
            data.MetaDescription = dataVm.MetaDescription;
            data.Status = dataVm.Status;
            data.DataStatus = dataVm.DataStatus;

            data.RequestBy = dataVm.RequestBy;
            data.RequestDate = dataVm.RequestDate;
            data.ApprovedBy = dataVm.ApprovedBy;
            data.ApprovedDate = dataVm.ApprovedDate;
            data.ApprovedStatus = dataVm.ApprovedStatus;

            //data.Yobi_Text1 = dataVm.Yobi_Text1;
            //data.Yobi_Text2 = dataVm.Yobi_Text2;
            //data.Yobi_Text3 = dataVm.Yobi_Text3;
            //data.Yobi_Text4 = dataVm.Yobi_Text4;
            //data.Yobi_Text5 = dataVm.Yobi_Text5;
            //data.Yobi_Number1 = dataVm.Yobi_Number1;
            //data.Yobi_Number2 = dataVm.Yobi_Number2;
            //data.Yobi_Number3 = dataVm.Yobi_Number3;
            //data.Yobi_Number4 = dataVm.Yobi_Number4;
            //data.Yobi_Number5 = dataVm.Yobi_Number5;
            //data.Yobi_Decimal1 = dataVm.Yobi_Decimal1;
            //data.Yobi_Decimal2 = dataVm.Yobi_Decimal2;
            //data.Yobi_Decimal3 = dataVm.Yobi_Decimal3;
            //data.Yobi_Decimal4 = dataVm.Yobi_Decimal4;
            //data.Yobi_Decimal5 = dataVm.Yobi_Decimal5;
            //data.Yobi_Date1 = dataVm.Yobi_Date1;
            //data.Yobi_Date2 = dataVm.Yobi_Date2;
            //data.Yobi_Date3 = dataVm.Yobi_Date3;
            //data.Yobi_Date4 = dataVm.Yobi_Date4;
            //data.Yobi_Date5 = dataVm.Yobi_Date5;

        }

        /// <summary>
        /// cập nhật mục tiêu
        /// </summary>
        /// <param name="data"></param>
        /// <param name="dataVm"></param>
        public static void UpdateTarget(this Target data, TargetViewModel dataVm)
        {
            data.ID = dataVm.ID;
            data.CompanyID = dataVm.CompanyID;
            data.DeptID = dataVm.DeptID;
            data.TeamID = dataVm.TeamID;
            data.YearMonth = dataVm.YearMonth;
            data.Name = dataVm.Name;
            data.CreatorBy = dataVm.CreatorBy;
            data.CreateDate = dataVm.CreateDate;


            data.Koritu = dataVm.Koritu;
            data.ActKoritu = dataVm.ActKoritu;
            data.ChangePercentEmp = dataVm.ChangePercentEmp;
            data.ChangeEmp = dataVm.ChangeEmp;
            data.ManagerEmp = dataVm.ManagerEmp;
            data.Leader2Emp = dataVm.Leader2Emp;
            data.Leader1Emp = dataVm.Leader1Emp;
            data.SubLeader2 = dataVm.SubLeader2;
            data.SubLeader1 = dataVm.SubLeader1;
            data.DevEmp = dataVm.DevEmp;
            data.TransEmp = dataVm.TransEmp;
            data.OtherEmp = dataVm.OtherEmp;
            data.LeaveJobPercentEmp = dataVm.LeaveJobPercentEmp;
            data.LeaveJobEmp = dataVm.LeaveJobEmp;
            data.ActChangePercentEmp = dataVm.ActChangePercentEmp;
            data.ActChangeEmp = dataVm.ActChangeEmp;
            data.ActManagerEmp = dataVm.ActManagerEmp;
            data.ActLeader2Emp = dataVm.ActLeader2Emp;
            data.ActLeader1Emp = dataVm.ActLeader1Emp;
            data.ActSubLeader2 = dataVm.ActSubLeader2;
            data.ActSubLeader1 = dataVm.ActSubLeader1;
            data.ActDevEmp = dataVm.ActDevEmp;
            data.ActTransEmp = dataVm.ActTransEmp;
            data.ActOtherEmp = dataVm.ActOtherEmp;
            data.ActLeaveJobPercentEmp = dataVm.ActLeaveJobPercentEmp;
            data.ActLeaveJobEmp = dataVm.ActLeaveJobEmp;
            data.ChangePercentMM = dataVm.ChangePercentMM;
            data.ChangeMM = dataVm.ChangeMM;
            data.QuotationMM = dataVm.QuotationMM;
            data.DevMM = dataVm.DevMM;
            data.TransMM = dataVm.TransMM;
            data.OnsiteMM = dataVm.OnsiteMM;
            data.ManMM = dataVm.ManMM;
            data.TotalMM = dataVm.TotalMM;
            data.ActChangePercentMM = dataVm.ActChangePercentMM;
            data.ActChangeMM = dataVm.ActChangeMM;
            data.ActQuotationMM = dataVm.ActQuotationMM;
            data.ActDevMM = dataVm.ActDevMM;
            data.ActTransMM = dataVm.ActTransMM;
            data.ActOnsiteMM = dataVm.ActOnsiteMM;
            data.ActManMM = dataVm.ActManMM;
            data.ActTotalMM = dataVm.ActTotalMM;
            data.N1 = dataVm.N1;
            data.N2 = dataVm.N2;
            data.N3 = dataVm.N3;
            data.N4 = dataVm.N4;
            data.N5 = dataVm.N5;
            data.ActN1 = dataVm.ActN1;
            data.ActN2 = dataVm.ActN2;
            data.ActN3 = dataVm.ActN3;
            data.ActN4 = dataVm.ActN4;
            data.ActN5 = dataVm.ActN5;
            data.LongOnsiterNumber = dataVm.LongOnsiterNumber;
            data.ShortOnsiterNumber = dataVm.ShortOnsiterNumber;
            data.InterShipNumber = dataVm.InterShipNumber;
            data.ActLongOnsiterNumber = dataVm.ActLongOnsiterNumber;
            data.ActShortOnsiterNumber = dataVm.ActShortOnsiterNumber;
            data.ActInterShipNumber = dataVm.ActInterShipNumber;
            data.Reason1 = dataVm.Reason1;
            data.Reason2 = dataVm.Reason2;
            data.Reason3 = dataVm.Reason3;


            //item dùng chung-Start
            data.RowVersion = dataVm.RowVersion;
            data.DisplayOrder = dataVm.DisplayOrder;
            data.AccountData = dataVm.AccountData;
            data.Note = dataVm.Note;
            
            data.AccessDataLevel = dataVm.AccessDataLevel;
            data.CreatedDate = dataVm.CreatedDate;
            data.CreatedBy = dataVm.CreatedBy;
            data.UpdatedDate = dataVm.UpdatedDate;
            data.UpdatedBy = dataVm.UpdatedBy;
            data.MetaKeyword = dataVm.MetaKeyword;
            data.MetaDescription = dataVm.MetaDescription;
            data.Status = dataVm.Status;
            data.DataStatus = dataVm.DataStatus;

            data.RequestBy = dataVm.RequestBy;
            data.RequestDate = dataVm.RequestDate;
            data.ApprovedBy = dataVm.ApprovedBy;
            data.ApprovedDate = dataVm.ApprovedDate;
            data.ApprovedStatus = dataVm.ApprovedStatus;

            //data.Yobi_Text1 = dataVm.Yobi_Text1;
            //data.Yobi_Text2 = dataVm.Yobi_Text2;
            //data.Yobi_Text3 = dataVm.Yobi_Text3;
            //data.Yobi_Text4 = dataVm.Yobi_Text4;
            //data.Yobi_Text5 = dataVm.Yobi_Text5;
            //data.Yobi_Number1 = dataVm.Yobi_Number1;
            //data.Yobi_Number2 = dataVm.Yobi_Number2;
            //data.Yobi_Number3 = dataVm.Yobi_Number3;
            //data.Yobi_Number4 = dataVm.Yobi_Number4;
            //data.Yobi_Number5 = dataVm.Yobi_Number5;
            //data.Yobi_Decimal1 = dataVm.Yobi_Decimal1;
            //data.Yobi_Decimal2 = dataVm.Yobi_Decimal2;
            //data.Yobi_Decimal3 = dataVm.Yobi_Decimal3;
            //data.Yobi_Decimal4 = dataVm.Yobi_Decimal4;
            //data.Yobi_Decimal5 = dataVm.Yobi_Decimal5;
            //data.Yobi_Date1 = dataVm.Yobi_Date1;
            //data.Yobi_Date2 = dataVm.Yobi_Date2;
            //data.Yobi_Date3 = dataVm.Yobi_Date3;
            //data.Yobi_Date4 = dataVm.Yobi_Date4;
            //data.Yobi_Date5 = dataVm.Yobi_Date5;
            //item dùng chung-End

        }

        /// <summary>
        /// cập nhật course
        /// </summary>
        /// <param name="data"></param>
        /// <param name="dataVm"></param>
        public static void UpdateSeminarCourse(this SeminarCourse data, SeminarCourseViewModel dataVm)
        {
            data.ID = dataVm.ID;
            data.SeminarMasterID = dataVm.SeminarMasterID;
            data.SeminarMasterDetailID = dataVm.SeminarMasterDetailID;
            data.Name = dataVm.Name;
            data.ShortName = dataVm.ShortName;
            data.SeminarDate = dataVm.SeminarDate;
            data.SeminarStaffID = dataVm.SeminarStaffID;
            data.SeminarTopic = dataVm.SeminarTopic;
            data.Location = dataVm.Location;
            data.Content = dataVm.Content;
            data.CondRequired = dataVm.CondRequired;
            data.PositionIDList = dataVm.PositionIDList;
            data.Cost = dataVm.Cost;
            data.TestRequired = dataVm.TestRequired;
            data.AnsSeminarDeptDeadlineDate = dataVm.AnsSeminarDeptDeadlineDate;
            data.AnsLocalDeadlineDate = dataVm.AnsLocalDeadlineDate;
            data.IsNotification = dataVm.IsNotification;
            data.ExpireDate = dataVm.ExpireDate;
            data.IsFinished = dataVm.IsFinished;
            data.FileID = dataVm.FileID;
            data.HaveGift = dataVm.HaveGift;
            data.IsInternalCourse = dataVm.IsInternalCourse;

            //item dùng chung-Start
            //item dùng chung-Start
            data.RowVersion = dataVm.RowVersion;
            data.DisplayOrder = dataVm.DisplayOrder;
            data.AccountData = dataVm.AccountData;
            data.Note = dataVm.Note;
            
            data.AccessDataLevel = dataVm.AccessDataLevel;
            data.CreatedDate = dataVm.CreatedDate;
            data.CreatedBy = dataVm.CreatedBy;
            data.UpdatedDate = dataVm.UpdatedDate;
            data.UpdatedBy = dataVm.UpdatedBy;
            data.MetaKeyword = dataVm.MetaKeyword;
            data.MetaDescription = dataVm.MetaDescription;
            data.Status = dataVm.Status;
            data.DataStatus = dataVm.DataStatus;

            data.RequestBy = dataVm.RequestBy;
            data.RequestDate = dataVm.RequestDate;
            data.ApprovedBy = dataVm.ApprovedBy;
            data.ApprovedDate = dataVm.ApprovedDate;
            data.ApprovedStatus = dataVm.ApprovedStatus;

            //data.Yobi_Text1 = dataVm.Yobi_Text1;
            //data.Yobi_Text2 = dataVm.Yobi_Text2;
            //data.Yobi_Text3 = dataVm.Yobi_Text3;
            //data.Yobi_Text4 = dataVm.Yobi_Text4;
            //data.Yobi_Text5 = dataVm.Yobi_Text5;
            //data.Yobi_Number1 = dataVm.Yobi_Number1;
            //data.Yobi_Number2 = dataVm.Yobi_Number2;
            //data.Yobi_Number3 = dataVm.Yobi_Number3;
            //data.Yobi_Number4 = dataVm.Yobi_Number4;
            //data.Yobi_Number5 = dataVm.Yobi_Number5;
            //data.Yobi_Decimal1 = dataVm.Yobi_Decimal1;
            //data.Yobi_Decimal2 = dataVm.Yobi_Decimal2;
            //data.Yobi_Decimal3 = dataVm.Yobi_Decimal3;
            //data.Yobi_Decimal4 = dataVm.Yobi_Decimal4;
            //data.Yobi_Decimal5 = dataVm.Yobi_Decimal5;
            //data.Yobi_Date1 = dataVm.Yobi_Date1;
            //data.Yobi_Date2 = dataVm.Yobi_Date2;
            //data.Yobi_Date3 = dataVm.Yobi_Date3;
            //data.Yobi_Date4 = dataVm.Yobi_Date4;
            //data.Yobi_Date5 = dataVm.Yobi_Date5;
            //item dùng chung-End

        }

        /// <summary>
        /// cập nhật course
        /// </summary>
        /// <param name="data"></param>
        /// <param name="dataVm"></param>
        public static void UpdateSeminarRecord(this SeminarRecord data, SeminarRecordViewModel dataVm)
        {
            data.ID = dataVm.ID;
            data.SeminarCourseID = dataVm.SeminarCourseID;
            data.EmpID = dataVm.EmpID;
            data.IsParticipation = dataVm.IsParticipation;
            data.IsPresent = dataVm.IsPresent;
            data.ActualSeminarDate = dataVm.ActualSeminarDate;
            data.IsPassedTest = dataVm.IsPassedTest;

            //item dùng chung-Start
            data.RowVersion = dataVm.RowVersion;
            data.DisplayOrder = dataVm.DisplayOrder;
            data.AccountData = dataVm.AccountData;
            data.Note = dataVm.Note;
            
            data.AccessDataLevel = dataVm.AccessDataLevel;
            data.CreatedDate = dataVm.CreatedDate;
            data.CreatedBy = dataVm.CreatedBy;
            data.UpdatedDate = dataVm.UpdatedDate;
            data.UpdatedBy = dataVm.UpdatedBy;
            data.MetaKeyword = dataVm.MetaKeyword;
            data.MetaDescription = dataVm.MetaDescription;
            data.Status = dataVm.Status;
            data.DataStatus = dataVm.DataStatus;

            data.RequestBy = dataVm.RequestBy;
            data.RequestDate = dataVm.RequestDate;
            data.ApprovedBy = dataVm.ApprovedBy;
            data.ApprovedDate = dataVm.ApprovedDate;
            data.ApprovedStatus = dataVm.ApprovedStatus;

            //data.Yobi_Text1 = dataVm.Yobi_Text1;
            //data.Yobi_Text2 = dataVm.Yobi_Text2;
            //data.Yobi_Text3 = dataVm.Yobi_Text3;
            //data.Yobi_Text4 = dataVm.Yobi_Text4;
            //data.Yobi_Text5 = dataVm.Yobi_Text5;
            //data.Yobi_Number1 = dataVm.Yobi_Number1;
            //data.Yobi_Number2 = dataVm.Yobi_Number2;
            //data.Yobi_Number3 = dataVm.Yobi_Number3;
            //data.Yobi_Number4 = dataVm.Yobi_Number4;
            //data.Yobi_Number5 = dataVm.Yobi_Number5;
            //data.Yobi_Decimal1 = dataVm.Yobi_Decimal1;
            //data.Yobi_Decimal2 = dataVm.Yobi_Decimal2;
            //data.Yobi_Decimal3 = dataVm.Yobi_Decimal3;
            //data.Yobi_Decimal4 = dataVm.Yobi_Decimal4;
            //data.Yobi_Decimal5 = dataVm.Yobi_Decimal5;
            //data.Yobi_Date1 = dataVm.Yobi_Date1;
            //data.Yobi_Date2 = dataVm.Yobi_Date2;
            //data.Yobi_Date3 = dataVm.Yobi_Date3;
            //data.Yobi_Date4 = dataVm.Yobi_Date4;
            //data.Yobi_Date5 = dataVm.Yobi_Date5;
            //item dùng chung-End

        }

        /// <summary>
        /// cập nhật ExchangeRate
        /// </summary>
        /// <param name="data"></param>
        /// <param name="dataVm"></param>
        public static void UpdateExchangeRate(this ExchangeRate data, ExchangeRateViewModel dataVm)
        {

            data.No = dataVm.No;
            data.ID = dataVm.ID;
            data.Name = dataVm.Name;
            data.StartDate = dataVm.StartDate;
            data.EndDate = dataVm.EndDate;
            data.UsdToVnd = dataVm.UsdToVnd;
            data.YenToVnd = dataVm.YenToVnd;
            data.UsdToYen = dataVm.UsdToYen;

            //item dùng chung-Start
            data.RowVersion = dataVm.RowVersion;
            data.DisplayOrder = dataVm.DisplayOrder;
            data.AccountData = dataVm.AccountData;
            data.Note = dataVm.Note;
            
            data.AccessDataLevel = dataVm.AccessDataLevel;
            data.CreatedDate = dataVm.CreatedDate;
            data.CreatedBy = dataVm.CreatedBy;
            data.UpdatedDate = dataVm.UpdatedDate;
            data.UpdatedBy = dataVm.UpdatedBy;
            data.MetaKeyword = dataVm.MetaKeyword;
            data.MetaDescription = dataVm.MetaDescription;
            data.Status = dataVm.Status;
            data.DataStatus = dataVm.DataStatus;

            data.RequestBy = dataVm.RequestBy;
            data.RequestDate = dataVm.RequestDate;
            data.ApprovedBy = dataVm.ApprovedBy;
            data.ApprovedDate = dataVm.ApprovedDate;
            data.ApprovedStatus = dataVm.ApprovedStatus;

            //data.Yobi_Text1 = dataVm.Yobi_Text1;
            //data.Yobi_Text2 = dataVm.Yobi_Text2;
            //data.Yobi_Text3 = dataVm.Yobi_Text3;
            //data.Yobi_Text4 = dataVm.Yobi_Text4;
            //data.Yobi_Text5 = dataVm.Yobi_Text5;
            //data.Yobi_Number1 = dataVm.Yobi_Number1;
            //data.Yobi_Number2 = dataVm.Yobi_Number2;
            //data.Yobi_Number3 = dataVm.Yobi_Number3;
            //data.Yobi_Number4 = dataVm.Yobi_Number4;
            //data.Yobi_Number5 = dataVm.Yobi_Number5;
            //data.Yobi_Decimal1 = dataVm.Yobi_Decimal1;
            //data.Yobi_Decimal2 = dataVm.Yobi_Decimal2;
            //data.Yobi_Decimal3 = dataVm.Yobi_Decimal3;
            //data.Yobi_Decimal4 = dataVm.Yobi_Decimal4;
            //data.Yobi_Decimal5 = dataVm.Yobi_Decimal5;
            //data.Yobi_Date1 = dataVm.Yobi_Date1;
            //data.Yobi_Date2 = dataVm.Yobi_Date2;
            //data.Yobi_Date3 = dataVm.Yobi_Date3;
            //data.Yobi_Date4 = dataVm.Yobi_Date4;
            //data.Yobi_Date5 = dataVm.Yobi_Date5;
            //item dùng chung-End

        }

        /// <summary>
        /// cập nhật EmpSupport
        /// </summary>
        /// <param name="data"></param>
        /// <param name="dataVm"></param>
        public static void UpdateEmpSupport(this EmpSupport data, EmpSupportViewModel dataVm)
        {

            data.ID = dataVm.ID;
            data.EmpID = dataVm.EmpID;
            data.StartDate = dataVm.StartDate;
            data.EndDate = dataVm.EndDate;
            data.SupportTypeMasterID = dataVm.SupportTypeMasterID;
            data.SupportTypeMasterDetailID = dataVm.SupportTypeMasterDetailID;
            data.ReceivedSupportFeeDate1 = dataVm.ReceivedSupportFeeDate1;
            data.ReceivedSupportFeeDate2 = dataVm.ReceivedSupportFeeDate2;
            data.ReceivedSupportFeeDate3 = dataVm.ReceivedSupportFeeDate3;
            data.ReceivedSupportFee1 = dataVm.ReceivedSupportFee1;
            data.ReceivedSupportFee2 = dataVm.ReceivedSupportFee2;
            data.ReceivedSupportFee3 = dataVm.ReceivedSupportFee3;
            data.TraineeID = dataVm.TraineeID;
            data.Result = dataVm.Result;
            data.Action = dataVm.Action;


            //item dùng chung-Start
            data.RowVersion = dataVm.RowVersion;
            data.DisplayOrder = dataVm.DisplayOrder;
            data.AccountData = dataVm.AccountData;
            data.Note = dataVm.Note;
            
            data.AccessDataLevel = dataVm.AccessDataLevel;
            data.CreatedDate = dataVm.CreatedDate;
            data.CreatedBy = dataVm.CreatedBy;
            data.UpdatedDate = dataVm.UpdatedDate;
            data.UpdatedBy = dataVm.UpdatedBy;
            data.MetaKeyword = dataVm.MetaKeyword;
            data.MetaDescription = dataVm.MetaDescription;
            data.Status = dataVm.Status;
            data.DataStatus = dataVm.DataStatus;

            data.RequestBy = dataVm.RequestBy;
            data.RequestDate = dataVm.RequestDate;
            data.ApprovedBy = dataVm.ApprovedBy;
            data.ApprovedDate = dataVm.ApprovedDate;
            data.ApprovedStatus = dataVm.ApprovedStatus;

            //data.Yobi_Text1 = dataVm.Yobi_Text1;
            //data.Yobi_Text2 = dataVm.Yobi_Text2;
            //data.Yobi_Text3 = dataVm.Yobi_Text3;
            //data.Yobi_Text4 = dataVm.Yobi_Text4;
            //data.Yobi_Text5 = dataVm.Yobi_Text5;
            //data.Yobi_Number1 = dataVm.Yobi_Number1;
            //data.Yobi_Number2 = dataVm.Yobi_Number2;
            //data.Yobi_Number3 = dataVm.Yobi_Number3;
            //data.Yobi_Number4 = dataVm.Yobi_Number4;
            //data.Yobi_Number5 = dataVm.Yobi_Number5;
            //data.Yobi_Decimal1 = dataVm.Yobi_Decimal1;
            //data.Yobi_Decimal2 = dataVm.Yobi_Decimal2;
            //data.Yobi_Decimal3 = dataVm.Yobi_Decimal3;
            //data.Yobi_Decimal4 = dataVm.Yobi_Decimal4;
            //data.Yobi_Decimal5 = dataVm.Yobi_Decimal5;
            //data.Yobi_Date1 = dataVm.Yobi_Date1;
            //data.Yobi_Date2 = dataVm.Yobi_Date2;
            //data.Yobi_Date3 = dataVm.Yobi_Date3;
            //data.Yobi_Date4 = dataVm.Yobi_Date4;
            //data.Yobi_Date5 = dataVm.Yobi_Date5;
            //item dùng chung-End

        }

        /// <summary>
        /// cập nhật EmpSupport
        /// </summary>
        /// <param name="data"></param>
        /// <param name="dataVm"></param>
        public static void UpdateCustomerUnitPrice(this CustomerUnitPrice data, CustomerUnitPriceViewModel dataVm)
        {

            data.ID = dataVm.ID;
            data.CustomerID = dataVm.CustomerID;
            data.Name = dataVm.Name;
            data.StartDate = dataVm.StartDate;
            data.EndDate = dataVm.EndDate;
            data.MangRate= dataVm.MangRate;
            data.TransRate = dataVm.TransRate;
            data.OrderUnitMasterID = dataVm.OrderUnitMasterID;
            data.OrderUnitMasterDetailID = dataVm.OrderUnitMasterDetailID;
            data.OrderPrice = dataVm.OrderPrice;
            data.Discount = dataVm.Discount;
            data.PayMethod = dataVm.PayMethod;


            //item dùng chung-Start
            data.RowVersion = dataVm.RowVersion;
            data.DisplayOrder = dataVm.DisplayOrder;
            data.AccountData = dataVm.AccountData;
            data.Note = dataVm.Note;

            data.AccessDataLevel = dataVm.AccessDataLevel;
            data.CreatedDate = dataVm.CreatedDate;
            data.CreatedBy = dataVm.CreatedBy;
            data.UpdatedDate = dataVm.UpdatedDate;
            data.UpdatedBy = dataVm.UpdatedBy;
            data.MetaKeyword = dataVm.MetaKeyword;
            data.MetaDescription = dataVm.MetaDescription;
            data.Status = dataVm.Status;
            data.DataStatus = dataVm.DataStatus;

            data.RequestBy = dataVm.RequestBy;
            data.RequestDate = dataVm.RequestDate;
            data.ApprovedBy = dataVm.ApprovedBy;
            data.ApprovedDate = dataVm.ApprovedDate;
            data.ApprovedStatus = dataVm.ApprovedStatus;

            //data.Yobi_Text1 = dataVm.Yobi_Text1;
            //data.Yobi_Text2 = dataVm.Yobi_Text2;
            //data.Yobi_Text3 = dataVm.Yobi_Text3;
            //data.Yobi_Text4 = dataVm.Yobi_Text4;
            //data.Yobi_Text5 = dataVm.Yobi_Text5;
            //data.Yobi_Number1 = dataVm.Yobi_Number1;
            //data.Yobi_Number2 = dataVm.Yobi_Number2;
            //data.Yobi_Number3 = dataVm.Yobi_Number3;
            //data.Yobi_Number4 = dataVm.Yobi_Number4;
            //data.Yobi_Number5 = dataVm.Yobi_Number5;
            //data.Yobi_Decimal1 = dataVm.Yobi_Decimal1;
            //data.Yobi_Decimal2 = dataVm.Yobi_Decimal2;
            //data.Yobi_Decimal3 = dataVm.Yobi_Decimal3;
            //data.Yobi_Decimal4 = dataVm.Yobi_Decimal4;
            //data.Yobi_Decimal5 = dataVm.Yobi_Decimal5;
            //data.Yobi_Date1 = dataVm.Yobi_Date1;
            //data.Yobi_Date2 = dataVm.Yobi_Date2;
            //data.Yobi_Date3 = dataVm.Yobi_Date3;
            //data.Yobi_Date4 = dataVm.Yobi_Date4;
            //data.Yobi_Date5 = dataVm.Yobi_Date5;
            //item dùng chung-End

        }

        /// <summary>
        /// cập nhật Thông tin hệ số đánh giá hàng tháng và thông tin effort 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="dataVm"></param>
        public static void UpdateEmpEstimate(this EmpEstimate data, EmpEstimateViewModel dataVm)
        {

            data.ID = dataVm.ID;
            data.EmpID = dataVm.EmpID;
            data.YearMonth = dataVm.YearMonth;
            data.EstimatePoint = dataVm.EstimatePoint;
            data.EffortMM = dataVm.EffortMM;
            data.BonusUsd = dataVm.BonusUsd;


            //item dùng chung-Start
            data.RowVersion = dataVm.RowVersion;
            data.DisplayOrder = dataVm.DisplayOrder;
            data.AccountData = dataVm.AccountData;
            data.Note = dataVm.Note;

            data.AccessDataLevel = dataVm.AccessDataLevel;
            data.CreatedDate = dataVm.CreatedDate;
            data.CreatedBy = dataVm.CreatedBy;
            data.UpdatedDate = dataVm.UpdatedDate;
            data.UpdatedBy = dataVm.UpdatedBy;
            data.MetaKeyword = dataVm.MetaKeyword;
            data.MetaDescription = dataVm.MetaDescription;
            data.Status = dataVm.Status;
            data.DataStatus = dataVm.DataStatus;

            data.RequestBy = dataVm.RequestBy;
            data.RequestDate = dataVm.RequestDate;
            data.ApprovedBy = dataVm.ApprovedBy;
            data.ApprovedDate = dataVm.ApprovedDate;
            data.ApprovedStatus = dataVm.ApprovedStatus;

            //data.Yobi_Text1 = dataVm.Yobi_Text1;
            //data.Yobi_Text2 = dataVm.Yobi_Text2;
            //data.Yobi_Text3 = dataVm.Yobi_Text3;
            //data.Yobi_Text4 = dataVm.Yobi_Text4;
            //data.Yobi_Text5 = dataVm.Yobi_Text5;
            //data.Yobi_Number1 = dataVm.Yobi_Number1;
            //data.Yobi_Number2 = dataVm.Yobi_Number2;
            //data.Yobi_Number3 = dataVm.Yobi_Number3;
            //data.Yobi_Number4 = dataVm.Yobi_Number4;
            //data.Yobi_Number5 = dataVm.Yobi_Number5;
            //data.Yobi_Decimal1 = dataVm.Yobi_Decimal1;
            //data.Yobi_Decimal2 = dataVm.Yobi_Decimal2;
            //data.Yobi_Decimal3 = dataVm.Yobi_Decimal3;
            //data.Yobi_Decimal4 = dataVm.Yobi_Decimal4;
            //data.Yobi_Decimal5 = dataVm.Yobi_Decimal5;
            //data.Yobi_Date1 = dataVm.Yobi_Date1;
            //data.Yobi_Date2 = dataVm.Yobi_Date2;
            //data.Yobi_Date3 = dataVm.Yobi_Date3;
            //data.Yobi_Date4 = dataVm.Yobi_Date4;
            //data.Yobi_Date5 = dataVm.Yobi_Date5;
            //item dùng chung-End

        }

        /// <summary>
        /// cập nhật system confirg
        /// </summary>
        /// <param name="data"></param>
        /// <param name="dataVm"></param>
        public static void UpdateSystemConfig(this SystemConfig data, SystemConfigViewModel dataVm)
        {

            data.ID = dataVm.ID;
            data.Code = dataVm.Code;
            data.Name = dataVm.Name;
            data.ShortName = dataVm.ShortName;
            data.ProcessingYear = dataVm.ProcessingYear;
            data.ExpMonth = dataVm.ExpMonth;
            data.MailAccountName = dataVm.MailAccountName;
            data.MailAccountPassword = dataVm.MailAccountPassword;
            data.MailAccountHalt = dataVm.MailAccountHalt;
            data.EmpOrderBy = dataVm.EmpOrderBy;
            data.IsShowSalaryValue = dataVm.IsShowSalaryValue;
            data.IsShowMoneyValue = dataVm.IsShowMoneyValue;
            data.EmpFilterDataValue = dataVm.EmpFilterDataValue;
            data.ValueString = dataVm.ValueString;
            data.ValueInt = dataVm.ValueInt;
            data.SystemValue= dataVm.SystemValue;
            //item dùng chung-Start
            data.RowVersion = dataVm.RowVersion;
            data.DisplayOrder = dataVm.DisplayOrder;
            data.AccountData = dataVm.AccountData;
            data.Note = dataVm.Note;

            data.AccessDataLevel = dataVm.AccessDataLevel;
            data.CreatedDate = dataVm.CreatedDate;
            data.CreatedBy = dataVm.CreatedBy;
            data.UpdatedDate = dataVm.UpdatedDate;
            data.UpdatedBy = dataVm.UpdatedBy;
            data.MetaKeyword = dataVm.MetaKeyword;
            data.MetaDescription = dataVm.MetaDescription;
            data.Status = dataVm.Status;
            data.DataStatus = dataVm.DataStatus;

            data.RequestBy = dataVm.RequestBy;
            data.RequestDate = dataVm.RequestDate;
            data.ApprovedBy = dataVm.ApprovedBy;
            data.ApprovedDate = dataVm.ApprovedDate;
            data.ApprovedStatus = dataVm.ApprovedStatus;

            //data.Yobi_Text1 = dataVm.Yobi_Text1;
            //data.Yobi_Text2 = dataVm.Yobi_Text2;
            //data.Yobi_Text3 = dataVm.Yobi_Text3;
            //data.Yobi_Text4 = dataVm.Yobi_Text4;
            //data.Yobi_Text5 = dataVm.Yobi_Text5;
            //data.Yobi_Number1 = dataVm.Yobi_Number1;
            //data.Yobi_Number2 = dataVm.Yobi_Number2;
            //data.Yobi_Number3 = dataVm.Yobi_Number3;
            //data.Yobi_Number4 = dataVm.Yobi_Number4;
            //data.Yobi_Number5 = dataVm.Yobi_Number5;
            //data.Yobi_Decimal1 = dataVm.Yobi_Decimal1;
            //data.Yobi_Decimal2 = dataVm.Yobi_Decimal2;
            //data.Yobi_Decimal3 = dataVm.Yobi_Decimal3;
            //data.Yobi_Decimal4 = dataVm.Yobi_Decimal4;
            //data.Yobi_Decimal5 = dataVm.Yobi_Decimal5;
            //data.Yobi_Date1 = dataVm.Yobi_Date1;
            //data.Yobi_Date2 = dataVm.Yobi_Date2;
            //data.Yobi_Date3 = dataVm.Yobi_Date3;
            //data.Yobi_Date4 = dataVm.Yobi_Date4;
            //data.Yobi_Date5 = dataVm.Yobi_Date5;
            //item dùng chung-End

        }

        /// <summary>
        /// cập nhật Recruitment
        /// </summary>
        /// <param name="data"></param>
        /// <param name="dataVm"></param>
        public static void UpdateRecruitment(this Recruitment data, RecruitmentViewModel dataVm)
        {

            data.No = dataVm.No;
            data.ID = dataVm.ID;
            data.Name = dataVm.Name;
            data.ShortName = dataVm.ShortName;
            data.RecruitmentTypeMasterID = dataVm.RecruitmentTypeMasterID;
            data.RecruitmentTypeMasterDetailID = dataVm.RecruitmentTypeMasterDetailID;
            data.CvCompanyFolderPath = dataVm.CvCompanyFolderPath;
            data.CvDeptFolderPath = dataVm.CvDeptFolderPath;
            data.CvCount = dataVm.CvCount;
            data.SendMailFromEmpID = dataVm.SendMailFromEmpID;
            data.SendMailToEmpID = dataVm.SendMailToEmpID;
            data.AnsRecruitDeptDeadlineDate = dataVm.AnsRecruitDeptDeadlineDate;
            data.AnsLocalDeadlineDate = dataVm.AnsLocalDeadlineDate;
            data.IsNotification = dataVm.IsNotification;
            data.ExpireDate = dataVm.ExpireDate;
            data.Content = dataVm.Content;
            data.IsFinished = dataVm.IsFinished;
            data.FileID = dataVm.FileID;
            //item dùng chung-Start
            data.RowVersion = dataVm.RowVersion;
            data.DisplayOrder = dataVm.DisplayOrder;
            data.AccountData = dataVm.AccountData;
            data.Note = dataVm.Note;

            data.AccessDataLevel = dataVm.AccessDataLevel;
            data.CreatedDate = dataVm.CreatedDate;
            data.CreatedBy = dataVm.CreatedBy;
            data.UpdatedDate = dataVm.UpdatedDate;
            data.UpdatedBy = dataVm.UpdatedBy;
            data.MetaKeyword = dataVm.MetaKeyword;
            data.MetaDescription = dataVm.MetaDescription;
            data.Status = dataVm.Status;
            data.DataStatus = dataVm.DataStatus;

            data.RequestBy = dataVm.RequestBy;
            data.RequestDate = dataVm.RequestDate;
            data.ApprovedBy = dataVm.ApprovedBy;
            data.ApprovedDate = dataVm.ApprovedDate;
            data.ApprovedStatus = dataVm.ApprovedStatus;

            //data.Yobi_Text1 = dataVm.Yobi_Text1;
            //data.Yobi_Text2 = dataVm.Yobi_Text2;
            //data.Yobi_Text3 = dataVm.Yobi_Text3;
            //data.Yobi_Text4 = dataVm.Yobi_Text4;
            //data.Yobi_Text5 = dataVm.Yobi_Text5;
            //data.Yobi_Number1 = dataVm.Yobi_Number1;
            //data.Yobi_Number2 = dataVm.Yobi_Number2;
            //data.Yobi_Number3 = dataVm.Yobi_Number3;
            //data.Yobi_Number4 = dataVm.Yobi_Number4;
            //data.Yobi_Number5 = dataVm.Yobi_Number5;
            //data.Yobi_Decimal1 = dataVm.Yobi_Decimal1;
            //data.Yobi_Decimal2 = dataVm.Yobi_Decimal2;
            //data.Yobi_Decimal3 = dataVm.Yobi_Decimal3;
            //data.Yobi_Decimal4 = dataVm.Yobi_Decimal4;
            //data.Yobi_Decimal5 = dataVm.Yobi_Decimal5;
            //data.Yobi_Date1 = dataVm.Yobi_Date1;
            //data.Yobi_Date2 = dataVm.Yobi_Date2;
            //data.Yobi_Date3 = dataVm.Yobi_Date3;
            //data.Yobi_Date4 = dataVm.Yobi_Date4;
            //data.Yobi_Date5 = dataVm.Yobi_Date5;
            //item dùng chung-End

        }

        /// <summary>
        /// cập nhật RecruitmentStaff
        /// </summary>
        /// <param name="data"></param>
        /// <param name="dataVm"></param>
        public static void UpdateRecruitmentStaff(this RecruitmentStaff data, RecruitmentStaffViewModel dataVm)
        {
            data.ID = dataVm.ID;
            data.RecruitmentID = dataVm.RecruitmentID;
            data.RecruitmentStaffID = dataVm.RecruitmentStaffID;
            data.Name = dataVm.Name;
            data.ShortName = dataVm.ShortName;
            data.RecruitmentTypeMasterID = dataVm.RecruitmentTypeMasterID;
            data.RecruitmentTypeMasterDetailID = dataVm.RecruitmentTypeMasterDetailID;
            data.RequestInCompanyDate = dataVm.RequestInCompanyDate;
            data.InterviewResult = dataVm.InterviewResult;
            data.RequestInterviewDate = dataVm.RequestInterviewDate;
            data.InterViewTime = dataVm.InterViewTime;
            data.ExamRound1 = dataVm.ExamRound1;
            data.ExamResult = dataVm.ExamResult;
            data.CompanyCvNo = dataVm.CompanyCvNo;
            data.Pharse = dataVm.Pharse;
            data.FullName = dataVm.FullName;
            data.BirthDay = dataVm.BirthDay;
            data.Gender = dataVm.Gender;
            data.National = dataVm.National;
            data.IdentNo = dataVm.IdentNo;
            data.PhoneNumber = dataVm.PhoneNumber;
            data.Email = dataVm.Email;
            data.KiboSalary = dataVm.KiboSalary;
            data.EducationLevel = dataVm.EducationLevel;
            data.CollectName = dataVm.CollectName;
            data.ProfessionalKbn = dataVm.ProfessionalKbn;
            data.EducationType = dataVm.EducationType;
            data.Grade = dataVm.Grade;
            data.IsCertificated = dataVm.IsCertificated;
            data.DebtSubjectCount = dataVm.DebtSubjectCount;
            data.DebtSubjectReason = dataVm.DebtSubjectReason;
            data.CertificatedDateTime = dataVm.CertificatedDateTime;
            data.OtherCertificated = dataVm.OtherCertificated;
            data.JapaneseLevel = dataVm.JapaneseLevel;
            data.EnglishLevel = dataVm.EnglishLevel;
            data.OtherSkill = dataVm.OtherSkill;
            data.MarriedStatus = dataVm.MarriedStatus;
            data.Objective = dataVm.Objective;
            data.CvNote = dataVm.CvNote;
            data.Comment1 = dataVm.Comment1;
            data.Comment2 = dataVm.Comment2;
            data.CvCreateDate = dataVm.CvCreateDate;
            data.CvUpdateDate = dataVm.CvUpdateDate;
            data.CvSendCount = dataVm.CvSendCount;
            data.CvSendList = dataVm.CvSendList;
            data.StartWorkingDate = dataVm.StartWorkingDate;
            data.AdddressPlace = dataVm.AdddressPlace;
            data.BornPlace = dataVm.BornPlace;
            data.Hobby = dataVm.Hobby;
            data.IsTestRound1ByPass = dataVm.IsTestRound1ByPass;
            data.GradeTestRound1 = dataVm.GradeTestRound1;
            data.EngGradeTestRound1 = dataVm.EngGradeTestRound1;
            data.ProfessionalKbnGradeTestRound1 = dataVm.ProfessionalKbnGradeTestRound1;
            data.GradeTestRound2 = dataVm.GradeTestRound2;
            data.CvStatus = dataVm.CvStatus;
            data.EmpType = dataVm.EmpType;
            data.TrainingClassConditionTalkDate = dataVm.TrainingClassConditionTalkDate;
            data.WorkingConditionTalkDate = dataVm.WorkingConditionTalkDate;
            data.Avatar = dataVm.Avatar;
            data.IsSendSMS = dataVm.IsSendSMS;
            data.SMSCount = dataVm.SMSCount;
            data.SMSContent = dataVm.SMSContent;
            data.IsTrainingIntroduction = dataVm.IsTrainingIntroduction;
            data.DeptReceived = dataVm.DeptReceived;
            data.TeamReceived = dataVm.TeamReceived;
            data.TrialStartDate = dataVm.TrialStartDate;
            data.SupportEmpID = dataVm.SupportEmpID;
            data.GhostPC = dataVm.GhostPC;
            data.ItMailNotificationDate = dataVm.ItMailNotificationDate;
            data.ResourceDeptMailNotificationDate = dataVm.ResourceDeptMailNotificationDate;
            data.SystemEmpID = dataVm.SystemEmpID;
            data.FileID = dataVm.FileID;
            data.IsRegister = dataVm.IsRegister;
            data.InterviewRoom = dataVm.InterviewRoom;
            data.InterviewDate = dataVm.InterviewDate;
            data.InterviewComment = dataVm.InterviewComment;
            data.IsFinished = dataVm.IsFinished;

            //item dùng chung-Start
            data.RowVersion = dataVm.RowVersion;
            data.DisplayOrder = dataVm.DisplayOrder;
            data.AccountData = dataVm.AccountData;
            data.Note = dataVm.Note;

            data.AccessDataLevel = dataVm.AccessDataLevel;
            data.CreatedDate = dataVm.CreatedDate;
            data.CreatedBy = dataVm.CreatedBy;
            data.UpdatedDate = dataVm.UpdatedDate;
            data.UpdatedBy = dataVm.UpdatedBy;
            data.MetaKeyword = dataVm.MetaKeyword;
            data.MetaDescription = dataVm.MetaDescription;
            data.Status = dataVm.Status;
            data.DataStatus = dataVm.DataStatus;

            data.RequestBy = dataVm.RequestBy;
            data.RequestDate = dataVm.RequestDate;
            data.ApprovedBy = dataVm.ApprovedBy;
            data.ApprovedDate = dataVm.ApprovedDate;
            data.ApprovedStatus = dataVm.ApprovedStatus;

            //data.Yobi_Text1 = dataVm.Yobi_Text1;
            //data.Yobi_Text2 = dataVm.Yobi_Text2;
            //data.Yobi_Text3 = dataVm.Yobi_Text3;
            //data.Yobi_Text4 = dataVm.Yobi_Text4;
            //data.Yobi_Text5 = dataVm.Yobi_Text5;
            //data.Yobi_Number1 = dataVm.Yobi_Number1;
            //data.Yobi_Number2 = dataVm.Yobi_Number2;
            //data.Yobi_Number3 = dataVm.Yobi_Number3;
            //data.Yobi_Number4 = dataVm.Yobi_Number4;
            //data.Yobi_Number5 = dataVm.Yobi_Number5;
            //data.Yobi_Decimal1 = dataVm.Yobi_Decimal1;
            //data.Yobi_Decimal2 = dataVm.Yobi_Decimal2;
            //data.Yobi_Decimal3 = dataVm.Yobi_Decimal3;
            //data.Yobi_Decimal4 = dataVm.Yobi_Decimal4;
            //data.Yobi_Decimal5 = dataVm.Yobi_Decimal5;
            //data.Yobi_Date1 = dataVm.Yobi_Date1;
            //data.Yobi_Date2 = dataVm.Yobi_Date2;
            //data.Yobi_Date3 = dataVm.Yobi_Date3;
            //data.Yobi_Date4 = dataVm.Yobi_Date4;
            //data.Yobi_Date5 = dataVm.Yobi_Date5;
            //item dùng chung-End

        }

        /// <summary>
        /// cập nhật RecruitmentInterview
        /// </summary>
        /// <param name="data"></param>
        /// <param name="dataVm"></param>
        public static void UpdateRecruitmentInterview(this RecruitmentInterview data, RecruitmentInterviewViewModel dataVm)
        {

            data.ID = dataVm.ID;
            data.RecruitmentID = dataVm.RecruitmentID;
            data.RecruitmentStaffID = dataVm.RecruitmentStaffID;
            data.Name = dataVm.Name;
            data.ShortName = dataVm.ShortName;
            data.RegInterviewEmpID = dataVm.RegInterviewEmpID;
            data.IsInterviewed = dataVm.IsInterviewed;
            data.IsStaffCancel = dataVm.IsStaffCancel;
            data.ScheduleInterviewDate = dataVm.ScheduleInterviewDate;
            data.ActualInterviewDate = dataVm.ActualInterviewDate;
            data.ScheduleInterviewRoom = dataVm.ScheduleInterviewRoom;
            data.ActualInterviewRoom = dataVm.ActualInterviewRoom;
            data.InterviewContent = dataVm.InterviewContent;
            data.InterviewComment = dataVm.InterviewComment;
            data.InterviewResult = dataVm.InterviewResult;
            data.IsFinished = dataVm.IsFinished;
            data.ReportDate = dataVm.ReportDate;
            data.IsTrainingIntroduction = dataVm.IsTrainingIntroduction;
            data.IsSendSMS = dataVm.IsSendSMS;
            data.SMSCount = dataVm.SMSCount;
            data.SMSContent = dataVm.SMSContent;
            data.FileID = dataVm.FileID;
            
            //item dùng chung-Start
            data.RowVersion = dataVm.RowVersion;
            data.DisplayOrder = dataVm.DisplayOrder;
            data.AccountData = dataVm.AccountData;
            data.Note = dataVm.Note;

            data.AccessDataLevel = dataVm.AccessDataLevel;
            data.CreatedDate = dataVm.CreatedDate;
            data.CreatedBy = dataVm.CreatedBy;
            data.UpdatedDate = dataVm.UpdatedDate;
            data.UpdatedBy = dataVm.UpdatedBy;
            data.MetaKeyword = dataVm.MetaKeyword;
            data.MetaDescription = dataVm.MetaDescription;
            data.Status = dataVm.Status;
            data.DataStatus = dataVm.DataStatus;

            data.RequestBy = dataVm.RequestBy;
            data.RequestDate = dataVm.RequestDate;
            data.ApprovedBy = dataVm.ApprovedBy;
            data.ApprovedDate = dataVm.ApprovedDate;
            data.ApprovedStatus = dataVm.ApprovedStatus;

            //data.Yobi_Text1 = dataVm.Yobi_Text1;
            //data.Yobi_Text2 = dataVm.Yobi_Text2;
            //data.Yobi_Text3 = dataVm.Yobi_Text3;
            //data.Yobi_Text4 = dataVm.Yobi_Text4;
            //data.Yobi_Text5 = dataVm.Yobi_Text5;
            //data.Yobi_Number1 = dataVm.Yobi_Number1;
            //data.Yobi_Number2 = dataVm.Yobi_Number2;
            //data.Yobi_Number3 = dataVm.Yobi_Number3;
            //data.Yobi_Number4 = dataVm.Yobi_Number4;
            //data.Yobi_Number5 = dataVm.Yobi_Number5;
            //data.Yobi_Decimal1 = dataVm.Yobi_Decimal1;
            //data.Yobi_Decimal2 = dataVm.Yobi_Decimal2;
            //data.Yobi_Decimal3 = dataVm.Yobi_Decimal3;
            //data.Yobi_Decimal4 = dataVm.Yobi_Decimal4;
            //data.Yobi_Decimal5 = dataVm.Yobi_Decimal5;
            //data.Yobi_Date1 = dataVm.Yobi_Date1;
            //data.Yobi_Date2 = dataVm.Yobi_Date2;
            //data.Yobi_Date3 = dataVm.Yobi_Date3;
            //data.Yobi_Date4 = dataVm.Yobi_Date4;
            //data.Yobi_Date5 = dataVm.Yobi_Date5;
            //item dùng chung-End

        }

        /// <summary>
        /// cập nhật Estimate
        /// </summary>
        /// <param name="data"></param>
        /// <param name="dataVm"></param>
        public static void UpdateEstimate(this Estimate data, EstimateViewModel dataVm)
        {

            data.No = dataVm.No;
            data.ID = dataVm.ID;
            data.Name = dataVm.Name;
            data.ShortName = dataVm.ShortName;
            data.NameUseInReport = dataVm.NameUseInReport;
            data.CustomerID = dataVm.CustomerID;
            data.CustomerYosanMM = dataVm.CustomerYosanMM;
            data.CustomerRequestDate = dataVm.CustomerRequestDate;
            data.CustomerKiboSendDate = dataVm.CustomerKiboSendDate;
            data.SchedulePojectStartDate = dataVm.SchedulePojectStartDate;
            data.SchedulePojectEndDate = dataVm.SchedulePojectEndDate;
            data.CustomerKiboLastDeliveryDate = dataVm.CustomerKiboLastDeliveryDate;
            data.ScheduleSendDate = dataVm.ScheduleSendDate;
            data.StartEstimateDate = dataVm.StartEstimateDate;
            data.ActualSendFirstDate = dataVm.ActualSendFirstDate;
            data.ActualSendLastDate = dataVm.ActualSendLastDate;
            data.SendEstimateCount = dataVm.SendEstimateCount;
            data.EstimateEmpID = dataVm.EstimateEmpID;
            data.EstimateEmpList = dataVm.EstimateEmpList;
            data.EstimateEmpReportDate = dataVm.EstimateEmpReportDate;
            data.TransEmpID = dataVm.TransEmpID;
            data.EstimateContent = dataVm.EstimateContent;
            data.BseID = dataVm.BseID;
            data.EstimateRequireMM = dataVm.EstimateRequireMM;
            data.EstimateBasicMM = dataVm.EstimateBasicMM;
            data.EstimateDetailMM = dataVm.EstimateDetailMM;
            data.EstimateDevMM = dataVm.EstimateDevMM;
            data.EstimateTransMM = dataVm.EstimateTransMM;
            data.EstimateManMM = dataVm.EstimateManMM;
            data.EstimateUtMM = dataVm.EstimateUtMM;
            data.EstimateCombineTestMM = dataVm.EstimateCombineTestMM;
            data.EstimateSystemTestMM = dataVm.EstimateSystemTestMM;
            data.EstimateUserTestMM = dataVm.EstimateUserTestMM;
            data.TotalMM = dataVm.TotalMM;
            data.EstimateResultMasterID = dataVm.EstimateResultMasterID;
            data.EstimateResultMasterDetailID = dataVm.EstimateResultMasterDetailID;
            data.ResultNote = dataVm.ResultNote;
            data.OS = dataVm.OS;
            data.Language = dataVm.Language;
            data.OtherSofts = dataVm.OtherSofts;
            data.WarrantyMonths = dataVm.WarrantyMonths;
            data.WarrantyStartDate = dataVm.WarrantyStartDate;
            data.EstimateTypeMasterID = dataVm.EstimateTypeMasterID;
            data.EstimateTypeMasterDetailID = dataVm.EstimateTypeMasterDetailID;
            data.OrderReceivedID = dataVm.OrderReceivedID;
            data.FileID = dataVm.FileID;

            //item dùng chung-Start
            data.RowVersion = dataVm.RowVersion;
            data.DisplayOrder = dataVm.DisplayOrder;
            data.AccountData = dataVm.AccountData;
            data.Note = dataVm.Note;

            data.AccessDataLevel = dataVm.AccessDataLevel;
            data.CreatedDate = dataVm.CreatedDate;
            data.CreatedBy = dataVm.CreatedBy;
            data.UpdatedDate = dataVm.UpdatedDate;
            data.UpdatedBy = dataVm.UpdatedBy;
            data.MetaKeyword = dataVm.MetaKeyword;
            data.MetaDescription = dataVm.MetaDescription;
            data.Status = dataVm.Status;
            data.DataStatus = dataVm.DataStatus;

            data.RequestBy = dataVm.RequestBy;
            data.RequestDate = dataVm.RequestDate;
            data.ApprovedBy = dataVm.ApprovedBy;
            data.ApprovedDate = dataVm.ApprovedDate;
            data.ApprovedStatus = dataVm.ApprovedStatus;

            //data.Yobi_Text1 = dataVm.Yobi_Text1;
            //data.Yobi_Text2 = dataVm.Yobi_Text2;
            //data.Yobi_Text3 = dataVm.Yobi_Text3;
            //data.Yobi_Text4 = dataVm.Yobi_Text4;
            //data.Yobi_Text5 = dataVm.Yobi_Text5;
            //data.Yobi_Number1 = dataVm.Yobi_Number1;
            //data.Yobi_Number2 = dataVm.Yobi_Number2;
            //data.Yobi_Number3 = dataVm.Yobi_Number3;
            //data.Yobi_Number4 = dataVm.Yobi_Number4;
            //data.Yobi_Number5 = dataVm.Yobi_Number5;
            //data.Yobi_Decimal1 = dataVm.Yobi_Decimal1;
            //data.Yobi_Decimal2 = dataVm.Yobi_Decimal2;
            //data.Yobi_Decimal3 = dataVm.Yobi_Decimal3;
            //data.Yobi_Decimal4 = dataVm.Yobi_Decimal4;
            //data.Yobi_Decimal5 = dataVm.Yobi_Decimal5;
            //data.Yobi_Date1 = dataVm.Yobi_Date1;
            //data.Yobi_Date2 = dataVm.Yobi_Date2;
            //data.Yobi_Date3 = dataVm.Yobi_Date3;
            //data.Yobi_Date4 = dataVm.Yobi_Date4;
            //data.Yobi_Date5 = dataVm.Yobi_Date5;
            //item dùng chung-End

        }

        /// <summary>
        /// cập nhật OrderReceived
        /// </summary>
        /// <param name="data"></param>
        /// <param name="dataVm"></param>
        public static void UpdateOrderReceived(this OrderReceived data, OrderReceivedViewModel dataVm)
        {
            data.No = dataVm.No;
            data.ID = dataVm.ID;
            data.Name = dataVm.Name;
            data.ShortName = dataVm.ShortName;
            data.NameUseInReport = dataVm.NameUseInReport;
            data.EstimateID = dataVm.EstimateID;
            data.TotalOrderMM = dataVm.TotalOrderMM;
            data.OrderDate = dataVm.OrderDate;
            data.SchedulePojectStartDate = dataVm.SchedulePojectStartDate;
            data.SchedulePojectEndDate = dataVm.SchedulePojectEndDate;
            data.CustomerKiboLastDeliveryDate = dataVm.CustomerKiboLastDeliveryDate;
            data.PMEmpID = dataVm.PMEmpID;
            data.TransEmpID = dataVm.TransEmpID;
            data.OrderContent = dataVm.OrderContent;
            data.BseID = dataVm.BseID;
            data.OrderRequireMM = dataVm.OrderRequireMM;
            data.OrderBasicMM = dataVm.OrderBasicMM;
            data.OrderDetailMM = dataVm.OrderDetailMM;
            data.OrderDevMM = dataVm.OrderDevMM;
            data.OrderTransMM = dataVm.OrderTransMM;
            data.OrderManMM = dataVm.OrderManMM;
            data.OrderUtMM = dataVm.OrderUtMM;
            data.OrderCombineTestMM = dataVm.OrderCombineTestMM;
            data.OrderSystemTestMM = dataVm.OrderSystemTestMM;
            data.OrderUserTestMM = dataVm.OrderUserTestMM;
            data.OS = dataVm.OS;
            data.Language = dataVm.Language;
            data.OtherSofts = dataVm.OtherSofts;
            data.WarrantyMonths = dataVm.WarrantyMonths;
            data.WarrantyStartDate = dataVm.WarrantyStartDate;
            data.CustomerConfirmDate = dataVm.CustomerConfirmDate;
            data.FileID = dataVm.FileID;

            //item dùng chung-Start
            data.RowVersion = dataVm.RowVersion;
            data.DisplayOrder = dataVm.DisplayOrder;
            data.AccountData = dataVm.AccountData;
            data.Note = dataVm.Note;

            data.AccessDataLevel = dataVm.AccessDataLevel;
            data.CreatedDate = dataVm.CreatedDate;
            data.CreatedBy = dataVm.CreatedBy;
            data.UpdatedDate = dataVm.UpdatedDate;
            data.UpdatedBy = dataVm.UpdatedBy;
            data.MetaKeyword = dataVm.MetaKeyword;
            data.MetaDescription = dataVm.MetaDescription;
            data.Status = dataVm.Status;
            data.DataStatus = dataVm.DataStatus;

            data.RequestBy = dataVm.RequestBy;
            data.RequestDate = dataVm.RequestDate;
            data.ApprovedBy = dataVm.ApprovedBy;
            data.ApprovedDate = dataVm.ApprovedDate;
            data.ApprovedStatus = dataVm.ApprovedStatus;

            //data.Yobi_Text1 = dataVm.Yobi_Text1;
            //data.Yobi_Text2 = dataVm.Yobi_Text2;
            //data.Yobi_Text3 = dataVm.Yobi_Text3;
            //data.Yobi_Text4 = dataVm.Yobi_Text4;
            //data.Yobi_Text5 = dataVm.Yobi_Text5;
            //data.Yobi_Number1 = dataVm.Yobi_Number1;
            //data.Yobi_Number2 = dataVm.Yobi_Number2;
            //data.Yobi_Number3 = dataVm.Yobi_Number3;
            //data.Yobi_Number4 = dataVm.Yobi_Number4;
            //data.Yobi_Number5 = dataVm.Yobi_Number5;
            //data.Yobi_Decimal1 = dataVm.Yobi_Decimal1;
            //data.Yobi_Decimal2 = dataVm.Yobi_Decimal2;
            //data.Yobi_Decimal3 = dataVm.Yobi_Decimal3;
            //data.Yobi_Decimal4 = dataVm.Yobi_Decimal4;
            //data.Yobi_Decimal5 = dataVm.Yobi_Decimal5;
            //data.Yobi_Date1 = dataVm.Yobi_Date1;
            //data.Yobi_Date2 = dataVm.Yobi_Date2;
            //data.Yobi_Date3 = dataVm.Yobi_Date3;
            //data.Yobi_Date4 = dataVm.Yobi_Date4;
            //data.Yobi_Date5 = dataVm.Yobi_Date5;
            //item dùng chung-End

        }
    }
}
using AutoMapper;
using EmpMan.Model.Models;
using EmpMan.Common.ViewModels.Models.Common;
using EmpMan.Common.ViewModels.Models.Emp;
using EmpMan.Common.ViewModels.Models.File;
using EmpMan.Common.ViewModels.Models.Master;
using EmpMan.Common.ViewModels.Models.Project;
using EmpMan.Common.ViewModels.Models.Revenue;
using EmpMan.Common.ViewModels.Models.Post;
using EmpMan.Common.ViewModels.Models.Product;
using EmpMan.Common.ViewModels.Models;
using EmpMan.Web.Common.ViewModels.Project;

namespace EmpMan.Web.Mappings
{
    public class AutoMapperConfiguration
    {
        public static void Configure()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Post, PostViewModel>();
                cfg.CreateMap<PostCategory, PostCategoryViewModel>();
                cfg.CreateMap<Tag, TagViewModel>();
                cfg.CreateMap<ProductCategory, ProductCategoryViewModel>();
                cfg.CreateMap<Product, ProductViewModel>();
                cfg.CreateMap<ProductTag, ProductTagViewModel>();
                cfg.CreateMap<Footer, FooterViewModel>();
                cfg.CreateMap<Slide, SlideViewModel>();
                cfg.CreateMap<Page, PageViewModel>();
                cfg.CreateMap<ContactDetail, ContactDetailViewModel>();
                cfg.CreateMap<AppRole, ApplicationRoleViewModel>();
                cfg.CreateMap<AppUser, AppUserViewModel>();
                cfg.CreateMap<Function, FunctionViewModel>();
                cfg.CreateMap<Permission, PermissionViewModel>();
                cfg.CreateMap<ProductImage, ProductImageViewModel>();
                cfg.CreateMap<ProductQuantity, ProductQuantityViewModel>();
                cfg.CreateMap<Color, ColorViewModel>();
                cfg.CreateMap<Size, SizeViewModel>();
                cfg.CreateMap<Order, OrderViewModel>();
                cfg.CreateMap<OrderDetail, OrderDetailViewModel>();
                cfg.CreateMap<Announcement, AnnouncementViewModel>();
                cfg.CreateMap<AnnouncementUser, AnnouncementUserViewModel>();

                //add thêm table đối tượng
                //https://stackoverflow.com/questions/26066275/an-unhandled-exception-of-type-system-stackoverflowexception-using-automapper
                //MaxDepth(3)

                cfg.CreateMap<Company, CompanyViewModel>();
                cfg.CreateMap<CompanyRule, CompanyRuleViewModel>();
                cfg.CreateMap<Master, MasterViewModel>();
                cfg.CreateMap<MasterDetail, MasterDetailViewModel>();
                cfg.CreateMap<Dept, DeptViewModel>();
                cfg.CreateMap<Team, TeamViewModel>();
                cfg.CreateMap<Position, PositionViewModel>();

                cfg.CreateMap<Emp, EmpViewModel>();
                cfg.CreateMap<EmpProfile, EmpProfileViewModel>();
                cfg.CreateMap<EmpProfileTech, EmpProfileTechViewModel>();
                cfg.CreateMap<EmpProfileWork, EmpProfileWorkViewModel>();
                cfg.CreateMap<EmpContract, EmpContractViewModel>();
                cfg.CreateMap<EmpSalary, EmpSalaryViewModel>();
                cfg.CreateMap<EmpAllowance, EmpAllowanceViewModel>();
                cfg.CreateMap<EmpDetailWork, EmpDetailWorkViewModel>();
                cfg.CreateMap<EmpOnsite, EmpOnsiteViewModel>();
                cfg.CreateMap<Customer, CustomerViewModel>();
                cfg.CreateMap<Project, ProjectViewModel>();
                cfg.CreateMap<ProjectDetail, ProjectDetailViewModel>();
                cfg.CreateMap<ProjectDetailResource, ProjectDetailResourceViewModel>();
                cfg.CreateMap<Revenue, RevenueViewModel>().MaxDepth(2).DisableCtorValidation().PreserveReferences();
                cfg.CreateMap<FileStorage, FileStorageViewModel>();
                cfg.CreateMap<FileStorage, FileResultViewModel>();

                cfg.CreateMap<FileStorageViewModel, FileResultViewModel>();
                cfg.CreateMap<ExchangeRate, ExchangeRateViewModel>();
                cfg.CreateMap<EmpSupport, EmpSupportViewModel>();
                cfg.CreateMap<DataFile, DataFileViewModel>();
                cfg.CreateMap<CustomerUnitPrice, CustomerUnitPriceViewModel>();
                cfg.CreateMap<EmpEstimate, EmpEstimateViewModel>();
                cfg.CreateMap<Target, TargetViewModel>();
                cfg.CreateMap<SystemConfig, SystemConfigViewModel>();
                cfg.CreateMap<Estimate, EstimateViewModel>();
                cfg.CreateMap<OrderReceived, OrderReceivedViewModel>();
                cfg.CreateMap<Recruitment, RecruitmentViewModel>();
                cfg.CreateMap<RecruitmentStaff, RecruitmentStaffViewModel>();
                cfg.CreateMap<RecruitmentInterview, RecruitmentInterviewViewModel>();
                cfg.CreateMap<JobScheduler, JobSchedulerViewModel>();

            });
        }
    }
}
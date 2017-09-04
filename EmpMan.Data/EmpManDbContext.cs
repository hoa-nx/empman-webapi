using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using EmpMan.Model.Models;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace EmpMan.Data
{
    public class EmpManDbContext : IdentityDbContext<AppUser>
    {
        public EmpManDbContext() : base("EmpManConnection")
        {
            this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.ProxyCreationEnabled = false;
        }

        public DbSet<Footer> Footers { set; get; }
        public DbSet<Order> Orders { set; get; }
        public DbSet<OrderDetail> OrderDetails { set; get; }
        public DbSet<Page> Pages { set; get; }
        public DbSet<Post> Posts { set; get; }
        public DbSet<PostCategory> PostCategories { set; get; }
        public DbSet<PostTag> PostTags { set; get; }
        public DbSet<Product> Products { set; get; }

        public DbSet<ProductCategory> ProductCategories { set; get; }
        public DbSet<ProductTag> ProductTags { set; get; }
        public DbSet<Slide> Slides { set; get; }
        public DbSet<SupportOnline> SupportOnlines { set; get; }
        public DbSet<SystemConfig> SystemConfigs { set; get; }

        public DbSet<Tag> Tags { set; get; }

        public DbSet<VisitorStatistic> VisitorStatistics { set; get; }
        public DbSet<Error> Errors { set; get; }
        public DbSet<ContactDetail> ContactDetails { set; get; }
        public DbSet<Feedback> Feedbacks { set; get; }

        public DbSet<Function> Functions { set; get; }
        public DbSet<Permission> Permissions { set; get; }
        public DbSet<AppRole> AppRoles { set; get; }
        public DbSet<IdentityUserRole> UserRoles { set; get; }


        public DbSet<Color> Colors { set; get; }
        public DbSet<Size> Sizes { set; get; }
        public DbSet<ProductQuantity> ProductQuantities { set; get; }
        public DbSet<ProductImage> ProductImages { set; get; }

        public DbSet<Announcement> Announcements { set; get; }
        public DbSet<AnnouncementUser> AnnouncementUsers { set; get; }

        /* add thêm for emp man*/
        public DbSet<Master> Masters { set; get; }
        public DbSet<MasterDetail> MasterDetails { set; get; }
        public DbSet<Company> Companys { set; get; }
        public DbSet<CompanyRule> CompanyRules { set; get; }
        public DbSet<Dept> Depts { set; get; }
        public DbSet<Team> Teams { set; get; }
        public DbSet<Position> Positions { set; get; }
        public DbSet<Emp> Emps { set; get; }
        public DbSet<EmpProfile> EmpProfiles { set; get; }
        public DbSet<EmpProfileTech> EmpProfileTechs { set; get; }

        public DbSet<EmpProfileWork> EmpProfileWorks { set; get; }
        public DbSet<EmpContract> EmpContracts { set; get; }

        public DbSet<EmpSalary> EmpSalarys { set; get; }
        public DbSet<EmpDetailWork> EmpDetailWorks { set; get; }
        public DbSet<EmpAllowance> EmpAllowances { set; get; }
        public DbSet<EmpOnsite> EmpOnsites { set; get; }
        public DbSet<Customer> Customers { set; get; }
        public DbSet<Project> Projects { set; get; }
        public DbSet<ProjectDetail> ProjectDetails { set; get; }
        public DbSet<ProjectDetailResource> ProjectDetailResources { set; get; }
        public DbSet<Revenue> Revenues { set; get; }
        public DbSet<FileStorage> FileStorages { set; get; }
        public DbSet<ExchangeRate> ExchangeRates{ set; get; }
        public DbSet<EmpSupport> EmpSupports { set; get; }
        public DbSet<DataFile> DataFiles { set; get; }
        public DbSet<CustomerUnitPrice> CustomerUnitPrices{ set; get; }
        public DbSet<EmpEstimate> EmpEstimates { set; get; }
        public DbSet<Target> Targets { set; get; }
        public DbSet<SearchEmpFilter> SearchEmpFilters { set; get; }
        public DbSet<Estimate> Estimates { set; get; }
        public DbSet<OrderReceived> OrderReceiveds { set; get; }

        public DbSet<Recruitment> Recruitments { set; get; }
        public DbSet<RecruitmentStaff> RecruitmentStaffs { set; get; }
        public DbSet<RecruitmentInterview> RecruitmentInterviews { set; get; }
        public DbSet<JobScheduler> JobSchedulers { set; get; }

        public static EmpManDbContext Create()
        {
            return new EmpManDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder builder)
        {
            builder.Entity<IdentityRole>().HasKey<string>(r => r.Id).ToTable("AppRoles");
            builder.Entity<IdentityUserRole>().HasKey(i => new { i.UserId, i.RoleId }).ToTable("AppUserRoles");
            builder.Entity<IdentityUserLogin>().HasKey(i => i.UserId).ToTable("AppUserLogins");
            builder.Entity<IdentityUserClaim>().HasKey(i => i.UserId).ToTable("AppUserClaims");
            
            builder.Entity<Emp>()
                .HasOptional(p => p.ContractType)
                .WithMany()
                .HasForeignKey(p => new { p.ContractTypeMasterID, p.ContractTypeMasterDetailID });
            //.WillCascadeOnDelete(true);

            builder.Entity<Emp>()
                .HasOptional(p => p.EmpType)
                .WithMany()
                .HasForeignKey(p => new { p.EmpTypeMasterID, p.EmpTypeMasterDetailID});
            //.WillCascadeOnDelete(true);

            builder.Entity<Emp>()
                .HasOptional(p => p.JapaneseLevel)
                .WithMany()
                .HasForeignKey(p => new { p.JapaneseLevelMasterID, p.JapaneseLevelMasterDetailID });
            //.WillCascadeOnDelete(true);

            builder.Entity<Emp>()
                .HasOptional(p => p.BusinessAllowanceLevel)
                .WithMany()
                .HasForeignKey(p => new { p.BusinessAllowanceLevelMasterID, p.BusinessAllowanceLevelMasterDetailID });
            //.WillCascadeOnDelete(true);

            builder.Entity<Emp>()
                .HasOptional(p => p.RoomWithInternetAllowanceLevel)
                .WithMany()
                .HasForeignKey(p => new { p.RoomWithInternetAllowanceLevelMasterID, p.RoomWithInternetAllowanceLevelMasterDetailID });
            //.WillCascadeOnDelete(true);

            builder.Entity<Emp>()
                .HasOptional(p => p.RoomNoInternetAllowanceLevel)
                .WithMany()
                .HasForeignKey(p => new { p.RoomNoInternetAllowanceLevelMasterID, p.RoomNoInternetAllowanceLevelMasterDetailID });
            //.WillCascadeOnDelete(true);


            builder.Entity<Emp>()
                .HasOptional(p => p.BseAllowanceLevel)
                .WithMany()
                .HasForeignKey(p => new { p.BseAllowanceLevelMasterID, p.BseAllowanceLevelMasterDetailID });
            //.WillCascadeOnDelete(true);

            builder.Entity<Emp>()
                .HasOptional(p => p.Collect)
                .WithMany()
                .HasForeignKey(p => new { p.CollectMasterID, p.CollectMasterDetailID});
            //.WillCascadeOnDelete(true);

            builder.Entity<Emp>()
                .HasOptional(p => p.EducationLevel)
                .WithMany()
                .HasForeignKey(p => new { p.EducationLevelMasterID, p.EducationLevelMasterDetailID});
            //.WillCascadeOnDelete(true);

            //---
            builder.Entity<EmpDetailWork>()
                .HasOptional(p => p.WorkEmpType)
                .WithMany()
                .HasForeignKey(p => new { p.WorkEmpTypeMasterID, p.WorkEmpTypeMasterDetailID});
            //.WillCascadeOnDelete(true);

            //---
            builder.Entity<EmpDetailWork>()
                .HasOptional(p => p.ContractType)
                .WithMany()
                .HasForeignKey(p => new { p.ContractTypeMasterID, p.ContractTypeMasterDetailID });
            //.WillCascadeOnDelete(true);

            builder.Entity<EmpDetailWork>()
                .HasOptional(p => p.EmpType)
                .WithMany()
                .HasForeignKey(p => new { p.EmpTypeMasterID, p.EmpTypeMasterDetailID });
            //.WillCascadeOnDelete(true);

            builder.Entity<EmpDetailWork>()
                .HasOptional(p => p.JapaneseLevel)
                .WithMany()
                .HasForeignKey(p => new { p.JapaneseLevelMasterID, p.JapaneseLevelMasterDetailID });
            //.WillCascadeOnDelete(true);

            builder.Entity<EmpDetailWork>()
                .HasOptional(p => p.BusinessAllowanceLevel)
                .WithMany()
                .HasForeignKey(p => new { p.BusinessAllowanceLevelMasterID, p.BusinessAllowanceLevelMasterDetailID });
            //.WillCascadeOnDelete(true);

            builder.Entity<EmpDetailWork>()
                .HasOptional(p => p.RoomWithInternetAllowanceLevel)
                .WithMany()
                .HasForeignKey(p => new { p.RoomWithInternetAllowanceLevelMasterID, p.RoomWithInternetAllowanceLevelMasterDetailID });
            //.WillCascadeOnDelete(true);

            builder.Entity<EmpDetailWork>()
                .HasOptional(p => p.RoomNoInternetAllowanceLevel)
                .WithMany()
                .HasForeignKey(p => new { p.RoomNoInternetAllowanceLevelMasterID, p.RoomNoInternetAllowanceLevelMasterDetailID });
            //.WillCascadeOnDelete(true);

            builder.Entity<EmpDetailWork>()
                .HasOptional(p => p.BseAllowanceLevel)
                .WithMany()
                .HasForeignKey(p => new { p.BseAllowanceLevelMasterID, p.BseAllowanceLevelMasterDetailID });
            //.WillCascadeOnDelete(true);

            builder.Entity<EmpDetailWork>()
                .HasOptional(p => p.Collect)
                .WithMany()
                .HasForeignKey(p => new { p.CollectMasterID, p.CollectMasterDetailID });
            //.WillCascadeOnDelete(true);

            builder.Entity<EmpDetailWork>()
                .HasOptional(p => p.EducationLevel)
                .WithMany()
                .HasForeignKey(p => new { p.EducationLevelMasterID, p.EducationLevelMasterDetailID });
            //.WillCascadeOnDelete(true);

            builder.Entity<EmpOnsite>()
                .HasRequired(p => p.OnsiteType)
                .WithMany()
                .HasForeignKey(p => new { p.OnsiteTypeMasterID, p.OnsiteTypeMasterDetailID });
            //.WillCascadeOnDelete(true);

            builder.Entity<EmpOnsite>()
                .HasOptional(p => p.OnsiteKikanTimeUnit)
                .WithMany()
                .HasForeignKey(p => new { p.OnsiteKikanTimeUnitMasterID, p.OnsiteKikanTimeUnitMasterDetailID });
            //.WillCascadeOnDelete(true);

            builder.Entity<ProjectDetail>()
                .HasOptional(p => p.ProjectType)
                .WithMany()
                .HasForeignKey(p => new { p.ProjectTypeMasterID, p.ProjectTypeMasterDetailID });
            //.WillCascadeOnDelete(true);


            builder.Entity<Revenue>()
               .HasOptional(p => p.ProjectDetail)
               .WithMany()
               .HasForeignKey(p => new { p.ProjectID, p.ProjectDetailID });
            //.WillCascadeOnDelete(true);

            builder.Entity<Revenue>()
               .HasOptional(p => p.EstimateType)
               .WithMany()
               .HasForeignKey(p => new { p.EstimateTypeMasterID, p.EstimateTypeMasterDetailID });
            //.WillCascadeOnDelete(true);

            builder.Entity<Revenue>()
               .HasOptional(p => p.OrderUnit)
               .WithMany()
               .HasForeignKey(p => new { p.OrderUnitMasterID, p.OrderUnitMasterDetailID });
            //.WillCascadeOnDelete(true);

            builder.Entity<EmpSupport>()
               .HasOptional(p => p.SupportType)
               .WithMany()
               .HasForeignKey(p => new { p.SupportTypeMasterID, p.SupportTypeMasterDetailID });
            //.WillCascadeOnDelete(true);

            builder.Entity<CustomerUnitPrice>()
               .HasOptional(p => p.OrderUnit)
               .WithMany()
               .HasForeignKey(p => new { p.OrderUnitMasterID, p.OrderUnitMasterDetailID});
            //.WillCascadeOnDelete(true);

            builder.Entity<Customer>()
               .HasOptional(p => p.DefaultOrderUnit)
               .WithMany()
               .HasForeignKey(p => new { p.DefaultOrderUnitMasterID, p.DefaultOrderUnitMasterDetailID });
            //.WillCascadeOnDelete(true);

            builder.Entity<Position>()
               .HasOptional(p => p.PositionGroup)
               .WithMany()
               .HasForeignKey(p => new { p.PositionGroupMasterID, p.PositionGroupMasterDetailID});

            builder.Entity<Estimate>()
              .HasOptional(p => p.EstimateResult)
              .WithMany()
              .HasForeignKey(p => new { p.EstimateResultMasterID, p.EstimateResultMasterDetailID });

            builder.Entity<Estimate>()
                .HasOptional(p => p.EstimateType)
                .WithMany()
                .HasForeignKey(p => new { p.EstimateTypeMasterID, p.EstimateTypeMasterDetailID });

            //builder.Entity<Recruitment>()
            //  .HasOptional(p => p.RecruitmentType)
            //  .WithMany()
            //  .HasForeignKey(p => new { p.RecruitmentTypeMasterID, p.RecruitmentTypeMasterDetailID });

            //Concurrency check
            //builder.Entity<Emp>().Property(p => p.RowVersion).IsConcurrencyToken();
            //builder.Entity<EmpDetailWork>().Property(p => p.RowVersion).IsConcurrencyToken();
            //builder.Entity<EmpAllowance>().Property(p => p.RowVersion).IsConcurrencyToken();
            //builder.Entity<EmpSalary>().Property(p => p.RowVersion).IsConcurrencyToken();
            //builder.Entity<Revenue>().Property(p => p.RowVersion).IsConcurrencyToken();

            //setting decimal config
            DecimalPrecisionScaleConfig(builder);

            //remove multiple cascade 
            builder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
            builder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }

        private void DecimalPrecisionScaleConfig(DbModelBuilder builder)
        {
            builder.Entity<CustomerUnitPrice>().Property(x => x.MangRate).HasPrecision(24, 10);
            builder.Entity<CustomerUnitPrice>().Property(x => x.TransRate).HasPrecision(24, 10);
            builder.Entity<CustomerUnitPrice>().Property(x => x.OrderPrice).HasPrecision(24, 10);
            builder.Entity<CustomerUnitPrice>().Property(x => x.Discount).HasPrecision(24, 10);

            builder.Entity<ExchangeRate>().Property(x => x.UsdToYen).HasPrecision(24, 10);

            builder.Entity<Project>().Property(x => x.EstimateManMonth).HasPrecision(24, 10);
            builder.Entity<Project>().Property(x => x.ActualManMonth).HasPrecision(24, 10);
            

            builder.Entity<ProjectDetail>().Property(x => x.EstimateManMonth).HasPrecision(24, 10);
            builder.Entity<ProjectDetail>().Property(x => x.ActualManMonth).HasPrecision(24, 10);
            builder.Entity<ProjectDetail>().Property(x => x.EstPerformance).HasPrecision(24, 10);
            builder.Entity<ProjectDetail>().Property(x => x.ActualPerformance).HasPrecision(24, 10);

            builder.Entity<Revenue>().Property(x => x.OrderProjectSumMM).HasPrecision(24, 10);
            builder.Entity<Revenue>().Property(x => x.OrderPrice).HasPrecision(24, 10);
            builder.Entity<Revenue>().Property(x => x.OrderPriceToUsd).HasPrecision(24, 10);
            builder.Entity<Revenue>().Property(x => x.AccPreMonthSumMM).HasPrecision(24, 10);
            builder.Entity<Revenue>().Property(x => x.AccPreMonthSumToUsd).HasPrecision(24, 10);
            builder.Entity<Revenue>().Property(x => x.InMonthDevMM).HasPrecision(24, 10);
            builder.Entity<Revenue>().Property(x => x.InMonthTransMM).HasPrecision(24, 10);
            builder.Entity<Revenue>().Property(x => x.InMonthManagementMM).HasPrecision(24, 10);
            builder.Entity<Revenue>().Property(x => x.InMonthOnsiteMM).HasPrecision(24, 10);
            builder.Entity<Revenue>().Property(x => x.InMonthSumMM).HasPrecision(24, 10);
            builder.Entity<Revenue>().Property(x => x.InMonthSumIncludeOnsiteMM).HasPrecision(24, 10);
            builder.Entity<Revenue>().Property(x => x.InMonthDevSumExcludeTransMM).HasPrecision(24, 10);
            builder.Entity<Revenue>().Property(x => x.InMonthToUsd).HasPrecision(24, 10);
            builder.Entity<Revenue>().Property(x => x.InMonthToVnd).HasPrecision(24, 10);
            builder.Entity<Revenue>().Property(x => x.NextMonthMM).HasPrecision(24, 10);
            builder.Entity<Revenue>().Property(x => x.NextMonthToUsd).HasPrecision(24, 10);

            builder.Entity<Target>().Property(x => x.ActDevMM).HasPrecision(24, 10);
            builder.Entity<Target>().Property(x => x.ActManMM).HasPrecision(24, 10);
            builder.Entity<Target>().Property(x => x.ActTotalMM).HasPrecision(24, 10);
            builder.Entity<Target>().Property(x => x.ActTransMM).HasPrecision(24, 10);
            builder.Entity<Target>().Property(x => x.DevMM).HasPrecision(24, 10);
            builder.Entity<Target>().Property(x => x.ManMM).HasPrecision(24, 10);
            builder.Entity<Target>().Property(x => x.TotalMM).HasPrecision(24, 10);
            builder.Entity<Target>().Property(x => x.TransMM).HasPrecision(24, 10);

            builder.Entity<EmpSalary>().Property(x => x.PreSalaryUnit).HasPrecision(24, 10);
            builder.Entity<EmpSalary>().Property(x => x.KonSalaryUnit).HasPrecision(24, 10);

            builder.Entity<EmpEstimate>().Property(x => x.EstimatePoint).HasPrecision(24, 10);
            builder.Entity<EmpEstimate>().Property(x => x.EffortMM).HasPrecision(24, 10);
            builder.Entity<EmpEstimate>().Property(x => x.BonusUsd).HasPrecision(24, 10);

            builder.Entity<Estimate>().Property(x => x.CustomerYosanMM).HasPrecision(24, 10);
            builder.Entity<Estimate>().Property(x => x.EstimateRequireMM).HasPrecision(24, 10);
            builder.Entity<Estimate>().Property(x => x.EstimateBasicMM).HasPrecision(24, 10);
            builder.Entity<Estimate>().Property(x => x.EstimateDetailMM).HasPrecision(24, 10);
            builder.Entity<Estimate>().Property(x => x.EstimateDevMM).HasPrecision(24, 10);
            builder.Entity<Estimate>().Property(x => x.EstimateTransMM).HasPrecision(24, 10);
            builder.Entity<Estimate>().Property(x => x.EstimateManMM).HasPrecision(24, 10);
            builder.Entity<Estimate>().Property(x => x.EstimateUtMM).HasPrecision(24, 10);
            builder.Entity<Estimate>().Property(x => x.EstimateCombineTestMM).HasPrecision(24, 10);
            builder.Entity<Estimate>().Property(x => x.EstimateSystemTestMM).HasPrecision(24, 10);
            builder.Entity<Estimate>().Property(x => x.EstimateUserTestMM).HasPrecision(24, 10);
            builder.Entity<Estimate>().Property(x => x.TotalMM).HasPrecision(24, 10);


            builder.Entity<OrderReceived>().Property(x => x.TotalOrderMM).HasPrecision(24, 10);
            builder.Entity<OrderReceived>().Property(x => x.OrderRequireMM).HasPrecision(24, 10);
            builder.Entity<OrderReceived>().Property(x => x.OrderBasicMM).HasPrecision(24, 10);
            builder.Entity<OrderReceived>().Property(x => x.OrderDetailMM).HasPrecision(24, 10);
            builder.Entity<OrderReceived>().Property(x => x.OrderDevMM).HasPrecision(24, 10);
            builder.Entity<OrderReceived>().Property(x => x.OrderTransMM).HasPrecision(24, 10);
            builder.Entity<OrderReceived>().Property(x => x.OrderManMM).HasPrecision(24, 10);
            builder.Entity<OrderReceived>().Property(x => x.OrderUtMM).HasPrecision(24, 10);
            builder.Entity<OrderReceived>().Property(x => x.OrderCombineTestMM).HasPrecision(24, 10);
            builder.Entity<OrderReceived>().Property(x => x.OrderSystemTestMM).HasPrecision(24, 10);
            builder.Entity<OrderReceived>().Property(x => x.OrderUserTestMM).HasPrecision(24, 10);

        }
    }
}
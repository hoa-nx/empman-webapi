namespace EmpMan.Data.Migrations
{
    using Common;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Model.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    using System.Data.Entity.Validation;
    using System.Diagnostics;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<EmpMan.Data.EmpManDbContext>
    {
        private bool isResetMasterData = false;

        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(EmpMan.Data.EmpManDbContext context)
        {
            CreateProductCategorySample(context);
            CreateSlide(context);
            CreatePage(context);
            CreateContactDetail(context);

            CreateConfigTitle(context);
            CreateFooter(context);
            CreateUser(context);
            CreateSize(context);
            CreateColor(context);
            CreateFunction(context);

            /* them thong tin master */

            CreateCompany(context);
            CreateDept(context);
            CreateTeam(context);
            CreatePosition(context);
            CreateMaster(context);
            CreateMasterDetail(context);
            CreateCustomer(context);
            //CreateCustomerUnitPriceSample(context);
            CreateEmp(context);

        }

        private void CreateFunction(EmpManDbContext context)
        {
            if (context.Functions.Count() == 0)
            {
                context.Functions.AddRange(new List<Function>()
                {
                    new Function() {ID = "MASTER_DATA", Name = "Thông tin chung",ParentId = null,DisplayOrder = 1,Status = true,URL = "/",IconCss = "fa-desktop-2x"  },
                    new Function() {ID = "COMPANY", Name = "Công ty",ParentId = "MASTER_DATA",DisplayOrder =1,Status = true,URL = "/main/company/index",IconCss = "fa-home"  },
                    new Function() {ID = "DEPT", Name = "Phòng ban",ParentId = "MASTER_DATA",DisplayOrder =2,Status = true,URL = "/main/dept/index",IconCss = "fa-sitemap"  },
                    new Function() {ID = "TEAM", Name = "Tổ nhóm",ParentId = "MASTER_DATA",DisplayOrder =3,Status = true,URL = "/main/team/index",IconCss = "fa-users"  },
                    new Function() {ID = "POSITION", Name = "Chức vụ",ParentId = "MASTER_DATA",DisplayOrder =4,Status = true,URL = "/main/position/index",IconCss = "fa-line-chart"  },
                    new Function() {ID = "MASTER", Name = "Dữ liệu chung",ParentId = "MASTER_DATA",DisplayOrder =5,Status = true,URL = "/main/master/index",IconCss = "fa-th"  },
                    new Function() {ID = "MASTER_DETAIL", Name = "Dữ liệu chi tiết",ParentId = "MASTER_DATA",DisplayOrder =6,Status = true,URL = "/main/master-detail/index",IconCss = "fa-th-list"  },
                    new Function() {ID = "CUSTOMER", Name = "Khách hàng",ParentId = "MASTER_DATA",DisplayOrder =7,Status = true,URL = "/main/customer/index",IconCss = "fa-braille"  },

                    new Function() {ID = "PROJECT",Name = "Thông tin dự án",ParentId = null,DisplayOrder = 2,Status = true,URL = "/",IconCss = "fa-calendar"  },
                    new Function() {ID = "PROJECT_LIST",Name = "Dự án",ParentId = "PROJECT",DisplayOrder =1,Status = true,URL = "/main/project/index",IconCss = "fa-chevron-down"  },
                    new Function() {ID = "PROJECT_DETAIL_LIST",Name = "Chi tiết dự án",ParentId = "PROJECT",DisplayOrder =2,Status = true,URL = "/main/project-detail/index",IconCss = "fa-chevron-down"  },

                    new Function() {ID = "EMP",Name = "Nhân sự",ParentId = null,DisplayOrder = 3,Status = true,URL = "/",IconCss = "fa-vcard"  },
                    new Function() {ID = "EMP_LIST",Name = "DS Nhân viên",ParentId = "EMP",DisplayOrder =1,Status = true,URL = "/main/emp/index",IconCss = "fa-address-book"  },
                    new Function() {ID = "EMP_CARD",Name = "Thẻ nhân viên",ParentId = "EMP",DisplayOrder =2,Status = true,URL = "/main/emp/card",IconCss = "fa-user-plus"  },
                    new Function() {ID = "EMP_BASIC",Name = "TT cơ bản",ParentId = "EMP",DisplayOrder =3,Status = true,URL = "/main/emp/basic",IconCss = "fa-info-circle"  },
                    new Function() {ID = "EMP_CONTRACT",Name = "Hợp đồng",ParentId = "EMP",DisplayOrder =4,Status = true,URL = "/main/emp/contract",IconCss = "fa-circle-o"  },
                    new Function() {ID = "EMP_WORK",Name = "CV-Chức vụ",ParentId = "EMP",DisplayOrder =5,Status = true,URL = "/main/emp/work",IconCss = "fa-keyboard-o"  },
                    new Function() {ID = "EMP_ALLOWANCE",Name = "Phụ cấp",ParentId = "EMP",DisplayOrder =6,Status = true,URL = "/main/emp/allowance",IconCss = "fa-circle-o"  },
                    new Function() {ID = "EMP_PROFILE",Name = "Profile",ParentId = "EMP",DisplayOrder =7,Status = true,URL = "/main/emp/profile",IconCss = "fa-circle-o"  },
                    new Function() {ID = "EMP_SALARY",Name = "Lương",ParentId = "EMP",DisplayOrder =8,Status = true,URL = "/main/emp/salary",IconCss = "fa-money"  },
                    new Function() {ID = "EMP_ONSITE",Name = "Tu nghiệp",ParentId = "EMP",DisplayOrder =9,Status = true,URL = "/main/emp/onsite",IconCss = "fa-fighter-jet"  },


                    new Function() {ID = "REV",Name = "Kinh doanh",ParentId = null,DisplayOrder = 4,Status = true,URL = "/",IconCss = "fa-dollar"  },
                    new Function() {ID = "REV_INPUT",Name = "Nhập doanh số",ParentId = "REV",DisplayOrder =1,Status = true,URL = "/main/revenue/index",IconCss = "fa-dollar"  },
                    new Function() {ID = "REV_LIST",Name = "Tham chiếu doanh số",ParentId = "REV",DisplayOrder =2,Status = true,URL = "/main/revenue/list",IconCss = "fa-dollar"  },

                    new Function() {ID = "FILE",Name = "File",ParentId = null,DisplayOrder = 5,Status = true,URL = "/",IconCss = "fa-file"  },
                    new Function() {ID = "FILE_LIST",Name = "Danh sách file",ParentId = "FILE",DisplayOrder =1,Status = true,URL = "/main/file/index",IconCss = "fa-file"  },

                    new Function() {ID = "SYSTEM",Name = "Hệ thống",ParentId = null,DisplayOrder = 6,Status = true,URL = "/",IconCss = "fa-cog"  },
                    new Function() {ID = "ROLE", Name = "Phân nhóm user",ParentId = "SYSTEM",DisplayOrder = 1,Status = true,URL = "/main/role/index",IconCss = "fa-arrow-right"  },
                    new Function() {ID = "FUNCTION", Name = "Chức năng sử dụng",ParentId = "SYSTEM",DisplayOrder = 2,Status = true,URL = "/main/function/index",IconCss = "fa-arrow-right"  },
                    new Function() {ID = "USER", Name = "Người dùng",ParentId = "SYSTEM",DisplayOrder =3,Status = true,URL = "/main/user/index",IconCss = "fa-arrow-right"  },
                    new Function() {ID = "COMPANY_RULE", Name = "Qui định công ty",ParentId = "SYSTEM",DisplayOrder =4,Status = true,URL = "/main/user/index",IconCss = "fa-arrow-right"  },
                    new Function() {ID = "CONFIG", Name = "Thiết lập hệ thống",ParentId = "SYSTEM",DisplayOrder =5,Status = true,URL = "/main/setting/index",IconCss = "fa-arrow-right"  },
                    new Function() {ID = "ACTIVITY", Name = "Nhật ký",ParentId = "SYSTEM",DisplayOrder = 6,Status = true,URL = "/main/activity/index",IconCss = "fa-arrow-right"  },
                    new Function() {ID = "ERROR", Name = "Lỗi hệ thống",ParentId = "SYSTEM",DisplayOrder = 7,Status = true,URL = "/main/error/index",IconCss = "fa-arrow-right"  },

                    new Function() {ID = "REPORT",Name = "Báo cáo",ParentId = null,DisplayOrder = 7,Status = true,URL = "/",IconCss = "fa-bar-chart-o"  },
                    new Function() {ID = "REVENUES",Name = "Báo cáo doanh thu",ParentId = "REPORT",DisplayOrder = 1,Status = true,URL = "/main/revenue/index",IconCss = "fa-bar-chart-o"  },
                    new Function() {ID = "ACCESS",Name = "Báo cáo truy cập",ParentId = "REPORT",DisplayOrder = 2,Status = true,URL = "/main/visitor/index",IconCss = "fa-bar-chart-o"  },
                    new Function() {ID = "READER",Name = "Báo cáo độc giả",ParentId = "REPORT",DisplayOrder = 3,Status = true,URL = "/main/reader/index",IconCss = "fa-bar-chart-o"  },


                    new Function() {ID = "PRODUCT",Name = "Sản phẩm",ParentId = null,DisplayOrder = 8,Status = true,URL = "/",IconCss = "fa-chevron-down"  },
                    new Function() {ID = "PRODUCT_CATEGORY",Name = "Danh mục",ParentId = "PRODUCT",DisplayOrder =1,Status = true,URL = "/main/product-category/index",IconCss = "fa-chevron-down"  },
                    new Function() {ID = "PRODUCT_LIST",Name = "Sản phẩm",ParentId = "PRODUCT",DisplayOrder = 2,Status = true,URL = "/main/product/index",IconCss = "fa-chevron-down"  },
                    new Function() {ID = "ORDER",Name = "Hóa đơn",ParentId = "PRODUCT",DisplayOrder = 3,Status = true,URL = "/main/order/index",IconCss = "fa-chevron-down"  },

                    new Function() {ID = "CONTENT",Name = "Nội dung",ParentId = null,DisplayOrder = 9,Status = true,URL = "/",IconCss = "fa-table"  },
                    new Function() {ID = "POST_CATEGORY",Name = "Danh mục",ParentId = "CONTENT",DisplayOrder = 1,Status = true,URL = "/main/post-category/index",IconCss = "fa-table"  },
                    new Function() {ID = "POST",Name = "Bài viết",ParentId = "CONTENT",DisplayOrder = 2,Status = true,URL = "/main/post/index",IconCss = "fa-table"  },

                    new Function() {ID = "UTILITY",Name = "Tiện ích",ParentId = null,DisplayOrder = 10,Status = true,URL = "/",IconCss = "fa-clone"  },
                    new Function() {ID = "FOOTER",Name = "Footer",ParentId = "UTILITY",DisplayOrder = 1,Status = true,URL = "/main/footer/index",IconCss = "fa-clone"  },
                    new Function() {ID = "FEEDBACK",Name = "Phản hồi",ParentId = "UTILITY",DisplayOrder = 2,Status = true,URL = "/main/feedback/index",IconCss = "fa-clone"  },
                    new Function() {ID = "ANNOUNEMENT",Name = "Thông báo",ParentId = "UTILITY",DisplayOrder = 3,Status = true,URL = "/main/announement/index",IconCss = "fa-clone"  },
                    new Function() {ID = "CONTACT",Name = "Liên hệ",ParentId = "UTILITY",DisplayOrder = 4,Status = true,URL = "/main/contact/index",IconCss = "fa-clone"  }

                });
                context.SaveChanges();
            }
        }

        private void CreateConfigTitle(EmpManDbContext context)
        {
            if (!context.SystemConfigs.Any(x => x.Code == "HomeTitle"))
            {
                context.SystemConfigs.Add(new SystemConfig()
                {
                    Code = "HomeTitle",
                    Name = "Trang chủ EmpMan",
                    ValueString = "Trang chủ EmpMan"
                    
                });
            }
            if (!context.SystemConfigs.Any(x => x.Code == "HomeMetaKeyword"))
            {
                context.SystemConfigs.Add(new SystemConfig()
                {
                    Code = "HomeMetaKeyword",
                    Name = "Trang chủ EmpMan",
                    ValueString = "Trang chủ EmpMan",
                });
            }
            if (!context.SystemConfigs.Any(x => x.Code == "HomeMetaDescription"))
            {
                context.SystemConfigs.Add(new SystemConfig()
                {
                    Code = "HomeMetaDescription",
                    Name = "Trang chủ EmpMan",
                    ValueString = "Trang chủ EmpMan",
                });
            }
        }

        private void CreateUser(EmpManDbContext context)
        {
            var manager = new UserManager<AppUser>(new UserStore<AppUser>(new EmpManDbContext()));
            if (manager.Users.Count() == 0)
            {
                var roleManager = new RoleManager<AppRole>(new RoleStore<AppRole>(new EmpManDbContext()));

                var user = new AppUser()
                {
                    UserName = "admin",
                    Email = "xuanhoa97@gmail.com",
                    EmailConfirmed = true,
                    BirthDay = DateTime.Now,
                    FullName = "Nguyễn Xuân Hòa",
                    CompanyID = 1,
                    DeptID =1,
                    TeamID =0,
                    Avatar = "/UploadedFiles/Avatars/profile-default.png",
                    Gender = true,
                    Status = true
                };

                if (manager.Users.Count(x => x.UserName.ToLower() == CommonConstants.AdminUser.ToLower()) == 0)
                {
                    manager.Create(user, "123654$");

                    if (!roleManager.Roles.Any())
                    {
                        roleManager.Create(new AppRole { Name = "Admin", Description = "Quản trị hệ thống" });
                        roleManager.Create(new AppRole { Name = CommonConstants.DirectoryBoardUser, Description = CommonConstants.DirectoryBoardUser });
                        roleManager.Create(new AppRole { Name = CommonConstants.GeneralManagerUser, Description = CommonConstants.GeneralManagerUser });
                        roleManager.Create(new AppRole { Name = CommonConstants.ManagerUser, Description = CommonConstants.ManagerUser });
                        roleManager.Create(new AppRole { Name = CommonConstants.ViceManagerUser, Description = CommonConstants.ViceManagerUser });
                        roleManager.Create(new AppRole { Name = CommonConstants.LeaderUser, Description = CommonConstants.LeaderUser});
                        roleManager.Create(new AppRole { Name = CommonConstants.SubLeaderUser, Description = CommonConstants.SubLeaderUser});
                        roleManager.Create(new AppRole { Name = CommonConstants.MemberUser, Description = CommonConstants.MemberUser });
                    }

                    var adminUser = manager.FindByName(CommonConstants.AdminUser.ToLower());

                    manager.AddToRoles(adminUser.Id, new string[] { "Admin", "Member" });
                }
            }
        }

        private void CreateProductCategorySample(EmpMan.Data.EmpManDbContext context)
        {
            if (context.ProductCategories.Count() == 0)
            {
                List<ProductCategory> listProductCategory = new List<ProductCategory>()
                {
                    new ProductCategory() { Name="Áo nam",Alias="ao-nam",Status=true },
                    new ProductCategory() { Name="Áo nữ",Alias="ao-nu",Status=true },
                    new ProductCategory() { Name="Giày nam",Alias="giay-nam",Status=true },
                    new ProductCategory() { Name="Giày nữ",Alias="giay-nu",Status=true }
                };
                context.ProductCategories.AddRange(listProductCategory);
                context.SaveChanges();
            }
        }

        private void CreateSize(EmpMan.Data.EmpManDbContext context)
        {
            if (context.Sizes.Count() == 0)
            {
                List<Size> listSize = new List<Size>()
                {
                    new Size() { Name="XXL" },
                    new Size() { Name="XL"},
                    new Size() { Name="L" },
                    new Size() { Name="M" },
                    new Size() { Name="S" },
                    new Size() { Name="XS" }
                };
                context.Sizes.AddRange(listSize);
                context.SaveChanges();
            }
        }

        private void CreateColor(EmpMan.Data.EmpManDbContext context)
        {
            if (context.Colors.Count() == 0)
            {
                List<Color> listColor = new List<Color>()
                {
                    new Color() {Name="Đen", Code="#000000" },
                    new Color() {Name="Trắng", Code="#FFFFFF"},
                    new Color() {Name="Đỏ", Code="#ff0000" },
                    new Color() {Name="Xanh", Code="#1000ff" },
                };
                context.Colors.AddRange(listColor);
                context.SaveChanges();
            }
        }

        private void CreateFooter(EmpManDbContext context)
        {
            if (context.Footers.Count(x => x.ID == CommonConstants.DefaultFooterId) == 0)
            {
                string content = "Footer";
                context.Footers.Add(new Footer()
                {
                    ID = CommonConstants.DefaultFooterId,
                    Content = content
                });
                try
                {
                    context.SaveChanges();
                }
                catch (System.Data.Entity.Validation.DbEntityValidationException e)
                {
                    var outputLines = new List<string>();
                    foreach (var eve in e.EntityValidationErrors)
                    {
                        outputLines.Add(string.Format(
                            "{0}: Entity of type \"{1}\" in state \"{2}\" has the following validation errors:",
                            DateTime.Now, eve.Entry.Entity.GetType().Name, eve.Entry.State));
                        foreach (var ve in eve.ValidationErrors)
                        {
                            outputLines.Add(string.Format(
                                "- Property: \"{0}\", Error: \"{1}\"",
                                ve.PropertyName, ve.ErrorMessage));
                        }
                    }
                    //Write to file
                    System.IO.File.AppendAllLines(@"c:\temp\errors.txt", outputLines);
                    throw;

                    // Showing it on screen
                    throw new Exception(string.Join(",", outputLines.ToArray()));

                }
            }
        }

        private void CreateSlide(EmpManDbContext context)
        {
            if (context.Slides.Count() == 0)
            {
                List<Slide> listSlide = new List<Slide>()
                {
                    new Slide() {
                        Name ="Slide 1",
                        DisplayOrder =1,
                        Status =true,
                        Url ="#",
                        Image ="/Assets/client/images/bag.jpg",
                        Content =@"	<h2>FLAT 50% 0FF</h2>
                                <label>FOR ALL PURCHASE <b>VALUE</b></label>
                                <p>Lorem ipsum dolor sit amet, consectetur
                            adipisicing elit, sed do eiusmod tempor incididunt ut labore et </ p >
                        <span class=""on-get"">GET NOW</span>" },
                    new Slide() {
                        Name ="Slide 2",
                        DisplayOrder =2,
                        Status =true,
                        Url ="#",
                        Image ="/Assets/client/images/bag1.jpg",
                    Content=@"<h2>FLAT 50% 0FF</h2>
                                <label>FOR ALL PURCHASE <b>VALUE</b></label>

                                <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et </ p >

                                <span class=""on-get"">GET NOW</span>"},
                };
                context.Slides.AddRange(listSlide);
                context.SaveChanges();
            }
        }

        private void CreatePage(EmpManDbContext context)
        {
            if (context.Pages.Count() == 0)
            {
                try
                {
                    var page = new Page()
                    {
                        Name = "Giới thiệu",
                        Alias = "gioi-thieu",
                        Content = @"Nội dung giới thiệu",
                        Status = true
                    };
                    context.Pages.Add(page);
                    context.SaveChanges();
                }
                catch (DbEntityValidationException ex)
                {
                    foreach (var eve in ex.EntityValidationErrors)
                    {
                        Trace.WriteLine($"Entity of type \"{eve.Entry.Entity.GetType().Name}\" in state \"{eve.Entry.State}\" has the following validation error.");
                        foreach (var ve in eve.ValidationErrors)
                        {
                            Trace.WriteLine($"- Property: \"{ve.PropertyName}\", Error: \"{ve.ErrorMessage}\"");
                        }
                    }
                }
            }
        }

        private void CreateContactDetail(EmpManDbContext context)
        {
            if (context.ContactDetails.Count() == 0)
            {
                try
                {
                    var contactDetail = new EmpMan.Model.Models.ContactDetail()
                    {
                        Name = "NGUYEN XUAN HOA",
                        Address = "Nhat Chi Mai ",
                        Email = "xuanhoa97@gmail.com",
                        Lat = 1,
                        Lng = 2,
                        Phone = "096799909",
                        Website = "http://azweb-solutions.com",
                        Other = "",
                        Status = true
                    };
                    context.ContactDetails.Add(contactDetail);
                    context.SaveChanges();
                }
                catch (DbEntityValidationException ex)
                {
                    foreach (var eve in ex.EntityValidationErrors)
                    {
                        Trace.WriteLine($"Entity of type \"{eve.Entry.Entity.GetType().Name}\" in state \"{eve.Entry.State}\" has the following validation error.");
                        foreach (var ve in eve.ValidationErrors)
                        {
                            Trace.WriteLine($"- Property: \"{ve.PropertyName}\", Error: \"{ve.ErrorMessage}\"");
                        }
                    }
                }
            }
        }

        /** add new */

        private void CreateCompany(EmpManDbContext context)
        {
            if (context.Companys.Count() == 0 || isResetMasterData )
            {

                //reset identity column 
                //context.Database.ExecuteSqlCommand("DBCC CHECKIDENT (Companys, RESEED, 0)");

                List<Company> listData = new List<Company>()
                {
                    new Company() {ID =0,Name = "Không chỉ định",ShortName="Không chỉ định",Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Company() {ID =1,Name = "CÔNG TY FUJINET SYSTEMS JSC",ShortName="FJS",Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"}
                };
                context.Companys.AddRange(listData);

                try
                {
                    context.SaveChanges();
                }
                catch (System.Data.Entity.Validation.DbEntityValidationException e)
                {
                    var outputLines = new List<string>();
                    foreach (var eve in e.EntityValidationErrors)
                    {
                        outputLines.Add(string.Format(
                            "{0}: Entity of type \"{1}\" in state \"{2}\" has the following validation errors:",
                            DateTime.Now, eve.Entry.Entity.GetType().Name, eve.Entry.State));
                        foreach (var ve in eve.ValidationErrors)
                        {
                            outputLines.Add(string.Format(
                                "- Property: \"{0}\", Error: \"{1}\"",
                                ve.PropertyName, ve.ErrorMessage));
                        }
                    }
                    //Write to file
                    System.IO.File.AppendAllLines(@"c:\temp\errors.txt", outputLines);
                    throw;

                    // Showing it on screen
                    throw new Exception(string.Join(",", outputLines.ToArray()));

                }
            }
        }

        private void CreateDept(EmpManDbContext context)
        {
            if (context.Depts.Count() == 0 || isResetMasterData)
            {
                //reset identity column 
                //context.Database.ExecuteSqlCommand("DBCC CHECKIDENT (Depts, RESEED, 0)");
                List<Dept> listData = new List<Dept>()
                {
                    new Dept() {ID =0,Name = "Không chỉ định",ShortName="Không chỉ định",Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Dept() {ID =1,Name = "Bộ phận lập trình 1",ShortName="Dept 1",CompanyID=1,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Dept() {ID =2,Name = "Bộ phận lập trình 2",ShortName="Dept 2",CompanyID=1,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Dept() {ID =3,Name = "Bộ phận lập trình 3",ShortName="Dept 3",CompanyID=1,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Dept() {ID =4,Name = "Bộ phận lập trình 4",ShortName="Dept 4",CompanyID=1,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Dept() {ID =5,Name = "Bộ phận lập trình 5(VINX)",ShortName="Dept 5(VINX)",CompanyID=1,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Dept() {ID =6,Name = "Bộ phận quản lý sản suất",ShortName="Quản lý SX",CompanyID=1,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Dept() {ID =7,Name = "Bộ phận training - tuyển dụng",ShortName="Training center",CompanyID=1,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Dept() {ID =8,Name = "Bộ phận phiên dịch",ShortName="Phiên dịch",CompanyID=1,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Dept() {ID =9,Name = "Bộ phận kinh doanh",ShortName="BP Kinh doanh",CompanyID=1,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Dept() {ID =10,Name = "Bộ phận kế toán",ShortName="BP kế toán",CompanyID=1,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Dept() {ID =11,Name = "Bộ phận nhân sự - tiền lương",ShortName="BP nhân sự tiền lương",CompanyID=1,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Dept() {ID =12,Name = "Bộ phận admin",ShortName="BP Admin",CompanyID=1,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"}
                };
                context.Depts.AddRange(listData);

                try
                {
                    context.SaveChanges();
                }
                catch (System.Data.Entity.Validation.DbEntityValidationException e)
                {
                    var outputLines = new List<string>();
                    foreach (var eve in e.EntityValidationErrors)
                    {
                        outputLines.Add(string.Format(
                            "{0}: Entity of type \"{1}\" in state \"{2}\" has the following validation errors:",
                            DateTime.Now, eve.Entry.Entity.GetType().Name, eve.Entry.State));
                        foreach (var ve in eve.ValidationErrors)
                        {
                            outputLines.Add(string.Format(
                                "- Property: \"{0}\", Error: \"{1}\"",
                                ve.PropertyName, ve.ErrorMessage));
                        }
                    }
                    //Write to file
                    System.IO.File.AppendAllLines(@"c:\temp\errors.txt", outputLines);
                    throw;

                    // Showing it on screen
                    throw new Exception(string.Join(",", outputLines.ToArray()));

                }
            }
        }

        private void CreateTeam(EmpManDbContext context)
        {
            if (context.Teams.Count() == 0 || isResetMasterData)
            {
                //reset identity column 
                //context.Database.ExecuteSqlCommand("DBCC CHECKIDENT (Teams, RESEED, 0)");

                List<Team> listData = new List<Team>()
                {
                    new Team() {ID =0,Name = "Không chỉ định",ShortName="Không chỉ định",   DeptID=0,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Team() {ID =1,Name = "Team lập trình 1.1",ShortName="Team 1.1",     DeptID=1,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Team() {ID =2,Name = "Team lập trình 1.2",ShortName="Team 1.2",     DeptID=1,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Team() {ID =3,Name = "Team lập trình 1.3",ShortName="Team 1.3",     DeptID=1,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Team() {ID =4,Name = "Team lập trình 1.4",ShortName="Team 1.4",     DeptID=1,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Team() {ID =5,Name = "Team lập trình 1.5",ShortName="Team 1.5",     DeptID=1,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Team() {ID =6,Name = "Team lập trình 1.6",ShortName="Team 1.6",     DeptID=1,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Team() {ID =7,Name = "Team lập trình 1.7",ShortName="Team 1.7",     DeptID=1,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Team() {ID =8,Name = "Team lập trình 1.8",ShortName="Team 1.8",     DeptID=1,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Team() {ID =99,Name = "Team lập trình 1.0",ShortName="Team 1.0",    DeptID=1,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},

                    //new Team() {ID =200,Name = "Không chỉ định",ShortName="Không chỉ định" ,  DeptID=2,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Team() {ID =201,Name = "Team lập trình 2.1",ShortName="Team 2.1",     DeptID=2,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Team() {ID =202,Name = "Team lập trình 2.2",ShortName="Team 2.2",     DeptID=2,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Team() {ID =203,Name = "Team lập trình 2.3",ShortName="Team 2.3",     DeptID=2,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Team() {ID =204,Name = "Team lập trình 2.4",ShortName="Team 2.4",     DeptID=2,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Team() {ID =205,Name = "Team lập trình 2.5",ShortName="Team 2.5",     DeptID=2,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Team() {ID =206,Name = "Team lập trình 2.6",ShortName="Team 2.6",     DeptID=2,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Team() {ID =207,Name = "Team lập trình 2.7",ShortName="Team 2.7",     DeptID=2,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Team() {ID =208,Name = "Team lập trình 2.8",ShortName="Team 2.8",     DeptID=2,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},

                    //new Team() {ID =300,Name = "Không chỉ định",ShortName="Không chỉ định" ,  DeptID=3,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Team() {ID =301,Name = "Team lập trình 3.1",ShortName="Team 3.1",     DeptID=3,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Team() {ID =302,Name = "Team lập trình 3.2",ShortName="Team 3.2",     DeptID=3,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Team() {ID =303,Name = "Team lập trình 3.3",ShortName="Team 3.3",     DeptID=3,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Team() {ID =304,Name = "Team lập trình 3.4",ShortName="Team 3.4",     DeptID=3,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Team() {ID =305,Name = "Team lập trình 3.5",ShortName="Team 3.5",     DeptID=3,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Team() {ID =306,Name = "Team lập trình 3.6",ShortName="Team 3.6",     DeptID=3,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Team() {ID =307,Name = "Team lập trình 3.7",ShortName="Team 3.7",     DeptID=3,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Team() {ID =308,Name = "Team lập trình 3.8",ShortName="Team 3.8",     DeptID=3,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Team() {ID =309,Name = "Team lập trình 3.9",ShortName="Team 3.9",     DeptID=3,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},

                    //new Team() {ID =400,Name = "Không chỉ định",ShortName="Không chỉ định" ,  DeptID=4,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Team() {ID =401,Name = "Team lập trình 4.1",ShortName="Team 4.1",     DeptID=4,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Team() {ID =402,Name = "Team lập trình 4.2",ShortName="Team 4.2",     DeptID=4,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Team() {ID =403,Name = "Team lập trình 4.3",ShortName="Team 4.3",     DeptID=4,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Team() {ID =404,Name = "Team lập trình 4.4",ShortName="Team 4.4",     DeptID=4,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Team() {ID =405,Name = "Team lập trình 4.5",ShortName="Team 4.5",     DeptID=4,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Team() {ID =406,Name = "Team lập trình 4.6",ShortName="Team 4.6",     DeptID=4,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Team() {ID =407,Name = "Team lập trình 4.7",ShortName="Team 4.7",     DeptID=4,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Team() {ID =408,Name = "Team lập trình 4.8",ShortName="Team 4.8",     DeptID=4,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},

                    //new Team() {ID =500,Name = "Không chỉ định",ShortName="Không chỉ định" ,  DeptID=5,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Team() {ID =501,Name = "Team lập trình 5.1",ShortName="Team 5.1",     DeptID=5,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Team() {ID =502,Name = "Team lập trình 5.2",ShortName="Team 5.2",     DeptID=5,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Team() {ID =503,Name = "Team lập trình 5.3",ShortName="Team 5.3",     DeptID=5,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Team() {ID =504,Name = "Team lập trình 5.4",ShortName="Team 5.4",     DeptID=5,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Team() {ID =505,Name = "Team lập trình 5.5",ShortName="Team 5.5",     DeptID=5,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Team() {ID =506,Name = "Team lập trình 5.6",ShortName="Team 5.6",     DeptID=5,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Team() {ID =507,Name = "Team lập trình 5.7",ShortName="Team 5.7",     DeptID=5,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Team() {ID =508,Name = "Team lập trình 5.8",ShortName="Team 5.8",     DeptID=5,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"}



                };
                context.Teams.AddRange(listData);

                try
                {
                    context.SaveChanges();
                }
                catch (System.Data.Entity.Validation.DbEntityValidationException e)
                {
                    var outputLines = new List<string>();
                    foreach (var eve in e.EntityValidationErrors)
                    {
                        outputLines.Add(string.Format(
                            "{0}: Entity of type \"{1}\" in state \"{2}\" has the following validation errors:",
                            DateTime.Now, eve.Entry.Entity.GetType().Name, eve.Entry.State));
                        foreach (var ve in eve.ValidationErrors)
                        {
                            outputLines.Add(string.Format(
                                "- Property: \"{0}\", Error: \"{1}\"",
                                ve.PropertyName, ve.ErrorMessage));
                        }
                    }
                    //Write to file
                    System.IO.File.AppendAllLines(@"c:\temp\errors.txt", outputLines);
                    throw;

                    // Showing it on screen
                    throw new Exception(string.Join(",", outputLines.ToArray()));

                }
            }
        }

        private void CreatePosition(EmpManDbContext context)
        {
            if (context.Positions.Count() == 0 || isResetMasterData)
            {
                //reset identity column 
                //context.Database.ExecuteSqlCommand("DBCC CHECKIDENT (Positions, RESEED, 0)");
                List<Position> listData = new List<Position>()
                {
                    new Position() {ID = 0 ,Name = "Không chỉ định",ShortName="Không chỉ định",Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Position() {ID = 1 ,Name = "System Administrator",ShortName="System Administrator",Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Position() {ID =10,Name = "Director",ShortName="Director",Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Position() {ID =20,Name = "Deputy Director",ShortName="Deputy Director",Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Position() {ID =30,Name = "Director Board Member",ShortName="Director Board Member",Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Position() {ID =40,Name = "Manager 2",ShortName="Manager 2",Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Position() {ID =50,Name = "Manager 1",ShortName="Manager 1",Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Position() {ID =60,Name = "Vice Manager",ShortName="Vice Manager",Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Position() {ID =70,Name = "Leader 2",ShortName="Leader 2",Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Position() {ID =80,Name = "Technical Leader 2",ShortName="Technical Leader 2",Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Position() {ID =90,Name = "Leader 1",ShortName="Leader 1",Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Position() {ID =100,Name = "Technical Leader 1",ShortName="Technical Leader 1",Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Position() {ID =110,Name = "SubLeader 2",ShortName="SubLeader 2",Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Position() {ID =120,Name = "Technical SubLeader 2",ShortName="TS2",Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Position() {ID =130,Name = "SubLeader 1",ShortName="SL1",Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Position() {ID =140,Name = "Technical SubLeader 1",ShortName="TS1",Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Position() {ID =150,Name = "Senior Staff 4",ShortName="SS4",Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Position() {ID =160,Name = "Senior Staff 3",ShortName="SS3",Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Position() {ID =170,Name = "Senior Staff 2",ShortName="SS2",Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Position() {ID =180,Name = "Senior Staff 1",ShortName="SS1",Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Position() {ID =190,Name = "Junior Staff 2",ShortName="JS2",Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Position() {ID =200,Name = "Junior Staff 1",ShortName="JS1",Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Position() {ID =210,Name = "Trial Staff",ShortName="Trial Staff",Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Position() {ID =220,Name = "Học việc",ShortName="Học việc",Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Position() {ID =230,Name = "Part Time",ShortName="Bán thời gian",Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Position() {ID =240,Name = "Intership",ShortName="Thực tập",Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"}

                };
                context.Positions.AddRange(listData);

                try
                {
                    context.SaveChanges();
                }
                catch (System.Data.Entity.Validation.DbEntityValidationException e)
                {
                    var outputLines = new List<string>();
                    foreach (var eve in e.EntityValidationErrors)
                    {
                        outputLines.Add(string.Format(
                            "{0}: Entity of type \"{1}\" in state \"{2}\" has the following validation errors:",
                            DateTime.Now, eve.Entry.Entity.GetType().Name, eve.Entry.State));
                        foreach (var ve in eve.ValidationErrors)
                        {
                            outputLines.Add(string.Format(
                                "- Property: \"{0}\", Error: \"{1}\"",
                                ve.PropertyName, ve.ErrorMessage));
                        }
                    }
                    //Write to file
                    System.IO.File.AppendAllLines(@"c:\temp\errors.txt", outputLines);
                    throw;

                    // Showing it on screen
                    throw new Exception(string.Join(",", outputLines.ToArray()));

                }
            }
        }

        private void CreateMaster(EmpManDbContext context)
        {
            if (context.Masters.Count() == 0 || isResetMasterData)
            {
                //reset identity column 
                //context.Database.ExecuteSqlCommand("DBCC CHECKIDENT (Masters, RESEED, 0)");

                List<Master> listData = new List<Master>()
                {
                    new Master() {ID =10,Name = "Chứng chỉ tiếng Nhật",IsAllowanceType=true,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Master() {ID =11,Name = "Các loại phụ cấp nghiệp vụ",IsAllowanceType=true,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Master() {ID =12,Name = "Các loại phụ cấp phòng chuyên biệt--có kết nối internet",IsAllowanceType=true,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Master() {ID =13,Name = "Các loại phụ cấp phòng chuyên biệt--không có kết nối internet",IsAllowanceType=true,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Master() {ID =14,Name = "Danh sách các trường cao đẳng / đại học",IsAllowanceType=false,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Master() {ID =15,Name = "Các loại phụ cấp BSE",IsAllowanceType=true,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Master() {ID =16,Name = "Các loại phụ cấp qui trình",IsAllowanceType=true,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Master() {ID =17,Name = "Loại hợp đồng lao động",IsAllowanceType=false,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Master() {ID =18,Name = "Loại nhân viên công ty ( ví dụ như lập trình viên, phiên dịch , nhân viên qui trình , tổng vụ ...",IsAllowanceType=false,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Master() {ID =19,Name = "Hệ tốt nghiệp cao nhất ( Cđ/ đại học / cao học)",IsAllowanceType=false,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Master() {ID =20,Name = "Loại báo giá dùng trong báo cáo doanh số",IsAllowanceType=false,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Master() {ID =21,Name = "Loại xét lương : 6 tháng 1 lần / 1 năm 1 lần...",IsAllowanceType=false,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Master() {ID =22,Name = "Loại onsite : intership / 3 tháng / 6 tháng / 1 năm/ 2 năm...",IsAllowanceType=false,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Master() {ID =23,Name = "Đơn vị tính thời gian onsite( ngày/ tuần / tháng / năm)",IsAllowanceType=false,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Master() {ID =24,Name = "Phân loại dự án",IsAllowanceType=true,Status = false,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Master() {ID =25,Name = "Phân loại đơn vị tiền tệ",IsAllowanceType=true,Status = false,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Master() {ID =26,Name = "Loại support  (thực tập / thử việc ...)",IsAllowanceType=true,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Master() {ID =27,Name = "Loại thông báo ví dụ như nhân sự / qui định oniste…",IsAllowanceType=false,Status = false,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Master() {ID =28,Name = "Nhóm chức danh",IsAllowanceType=true,Status = false,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"}

                };
                context.Masters.AddRange(listData);

                try
                {
                    context.SaveChanges();
                }
                catch (System.Data.Entity.Validation.DbEntityValidationException e)
                {
                    var outputLines = new List<string>();
                    foreach (var eve in e.EntityValidationErrors)
                    {
                        outputLines.Add(string.Format(
                            "{0}: Entity of type \"{1}\" in state \"{2}\" has the following validation errors:",
                            DateTime.Now, eve.Entry.Entity.GetType().Name, eve.Entry.State));
                        foreach (var ve in eve.ValidationErrors)
                        {
                            outputLines.Add(string.Format(
                                "- Property: \"{0}\", Error: \"{1}\"",
                                ve.PropertyName, ve.ErrorMessage));
                        }
                    }
                    //Write to file
                    System.IO.File.AppendAllLines(@"c:\temp\errors.txt", outputLines);
                    throw;

                    // Showing it on screen
                    throw new Exception(string.Join(",", outputLines.ToArray()));

                }
            }
        }

        private void CreateMasterDetail(EmpManDbContext context)
        {
            if (context.MasterDetails.Count() == 0 || isResetMasterData)
            {
                //context.Database.ExecuteSqlCommand("DELETE FROM MasterDetails");
                //reset identity column 
                //context.Database.ExecuteSqlCommand("DBCC CHECKIDENT (MasterDetails, RESEED, 0)");

                List<MasterDetail> listData = new List<MasterDetail>()
                {
                    new MasterDetail() {MasterID =10,MasterDetailCode=0,Name = "Không chỉ định",IsAllowanceType=true,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new MasterDetail() {MasterID =10,MasterDetailCode=1,Name = "N1",IsAllowanceType=true,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new MasterDetail() {MasterID =10,MasterDetailCode=2,Name = "N2",IsAllowanceType=true,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new MasterDetail() {MasterID =10,MasterDetailCode=3,Name = "N3",IsAllowanceType=true,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new MasterDetail() {MasterID =10,MasterDetailCode=4,Name = "N4",IsAllowanceType=true,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new MasterDetail() {MasterID =10,MasterDetailCode=5,Name = "N5",IsAllowanceType=true,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new MasterDetail() {MasterID =10,MasterDetailCode=6,Name = "Cơ bản",IsAllowanceType=true,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},

                    new MasterDetail() {MasterID =11,MasterDetailCode=0,Name = "Không chỉ định",ShortName = "Không chỉ định",IsAllowanceType=true,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new MasterDetail() {MasterID =11,MasterDetailCode=1,Name = "PC Nghiệp vụ bậc 1",ShortName = "Nghiệp vụ bậc 1",IsAllowanceType=true,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new MasterDetail() {MasterID =11,MasterDetailCode=2,Name = "PC Nghiệp vụ bậc 2",ShortName = "Nghiệp vụ bậc 2",IsAllowanceType=true,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new MasterDetail() {MasterID =11,MasterDetailCode=3,Name = "PC Nghiệp vụ bậc 3",ShortName = "Nghiệp vụ bậc 3",IsAllowanceType=true,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new MasterDetail() {MasterID =11,MasterDetailCode=4,Name = "PC Nghiệp vụ bậc 4",ShortName = "Nghiệp vụ bậc 4",IsAllowanceType=true,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new MasterDetail() {MasterID =11,MasterDetailCode=5,Name = "PC Nghiệp vụ bậc 5",ShortName = "Nghiệp vụ bậc 5",IsAllowanceType=true,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},

                    new MasterDetail() {MasterID =12,MasterDetailCode=0,Name = "Không chỉ định",ShortName = "Không chỉ định",IsAllowanceType=true,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new MasterDetail() {MasterID =12,MasterDetailCode=1,Name = "PC phòng chuyên biệt--internet 1",ShortName = "Phòng chuyên biệt--internet 1",IsAllowanceType=true,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new MasterDetail() {MasterID =12,MasterDetailCode=2,Name = "PC phòng chuyên biệt--internet 2",ShortName = "Phòng chuyên biệt--internet 2",IsAllowanceType=true,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new MasterDetail() {MasterID =12,MasterDetailCode=3,Name = "PC phòng chuyên biệt--internet 3",ShortName = "Phòng chuyên biệt--internet 3",IsAllowanceType=true,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},

                    new MasterDetail() {MasterID =13,MasterDetailCode=0,Name = "Không chỉ định",ShortName = "Không chỉ định",IsAllowanceType=true,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new MasterDetail() {MasterID =13,MasterDetailCode=1,Name = "PC phòng chuyên biệt--không internet 1",ShortName = "Phòng chuyên biệt--không internet 1",IsAllowanceType=true,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new MasterDetail() {MasterID =13,MasterDetailCode=2,Name = "PC phòng chuyên biệt--không internet 2",ShortName = "Phòng chuyên biệt--không internet 2",IsAllowanceType=true,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new MasterDetail() {MasterID =13,MasterDetailCode=3,Name = "PC phòng chuyên biệt--không internet 3",ShortName = "Phòng chuyên biệt--không internet 3",IsAllowanceType=true,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},

                    new MasterDetail() {MasterID =14,MasterDetailCode=0,Name = "Không chỉ định",IsAllowanceType=false,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new MasterDetail() {MasterID =14,MasterDetailCode=1,Name = "Trường ĐH Bách Khoa TPHCM",IsAllowanceType=false,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new MasterDetail() {MasterID =14,MasterDetailCode=2,Name = "Trường ĐH Khoa Học Tự Nhiên TPHCM",IsAllowanceType=false,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new MasterDetail() {MasterID =14,MasterDetailCode=3,Name = "Trường ĐH Công Nghệ Thông Tin TPHCM",IsAllowanceType=false,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new MasterDetail() {MasterID =14,MasterDetailCode=4,Name = "Trường ĐH Nông Lâm TPHCM",IsAllowanceType=false,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new MasterDetail() {MasterID =14,MasterDetailCode=5,Name = "Trường ĐH Cần Thơ",IsAllowanceType=false,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new MasterDetail() {MasterID =14,MasterDetailCode=6,Name = "Trường ĐH Trà Vinh",IsAllowanceType=false,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new MasterDetail() {MasterID =14,MasterDetailCode=7,Name = "Trường ĐH An Giang",IsAllowanceType=false,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new MasterDetail() {MasterID =14,MasterDetailCode=8,Name = "Trường ĐH Sư Phạm TPHCM",IsAllowanceType=false,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new MasterDetail() {MasterID =14,MasterDetailCode=9,Name = "Trường ĐH GTVT TPHCM",IsAllowanceType=false,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new MasterDetail() {MasterID =14,MasterDetailCode=10,Name = "Trường ĐH Công Nghiệp TPHCM",IsAllowanceType=false,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new MasterDetail() {MasterID =14,MasterDetailCode=11,Name = "Trường ĐH Bách Khoa Đà Nẵng",IsAllowanceType=false,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new MasterDetail() {MasterID =14,MasterDetailCode=12,Name = "Trường ĐH Quy Nhơn",IsAllowanceType=false,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new MasterDetail() {MasterID =14,MasterDetailCode=13,Name = "Trường ĐH Huế",IsAllowanceType=false,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new MasterDetail() {MasterID =14,MasterDetailCode=14,Name = "Trường ĐH Osaka",IsAllowanceType=false,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new MasterDetail() {MasterID =14,MasterDetailCode=15,Name = "Trường ĐH Tohoku",IsAllowanceType=false,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new MasterDetail() {MasterID =14,MasterDetailCode=16,Name = "Trường ĐH Nagoya",IsAllowanceType=false,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new MasterDetail() {MasterID =14,MasterDetailCode=17,Name = "Trường ĐH Hokkaido",IsAllowanceType=false,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new MasterDetail() {MasterID =14,MasterDetailCode=18,Name = "Trường ĐH Kobe",IsAllowanceType=false,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new MasterDetail() {MasterID =14,MasterDetailCode=19,Name = "Trường ĐH Tokyo Institute of Technology",IsAllowanceType=false,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new MasterDetail() {MasterID =14,MasterDetailCode=20,Name = "Trường ĐH Kyoto",IsAllowanceType=false,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},

                    new MasterDetail() {MasterID =15,MasterDetailCode=0,Name = "Không chỉ định",ShortName="Không chỉ định", IsAllowanceType=true,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new MasterDetail() {MasterID =15,MasterDetailCode=1,Name = "PC BSE bậc 1",ShortName="BSE bậc 1",IsAllowanceType=true,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new MasterDetail() {MasterID =15,MasterDetailCode=2,Name = "PC BSE bậc 2",ShortName="BSE bậc 2",IsAllowanceType=true,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new MasterDetail() {MasterID =15,MasterDetailCode=3,Name = "PC BSE bậc 3",ShortName="BSE bậc 3",IsAllowanceType=true,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new MasterDetail() {MasterID =15,MasterDetailCode=4,Name = "PC BSE bậc 4",ShortName="BSE bậc 4",IsAllowanceType=true,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new MasterDetail() {MasterID =15,MasterDetailCode=5,Name = "PC BSE bậc 5",ShortName="BSE bậc 5",IsAllowanceType=true,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},

                    new MasterDetail() {MasterID =16,MasterDetailCode=0,Name = "Không chỉ định",IsAllowanceType=true,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new MasterDetail() {MasterID =16,MasterDetailCode=1,Name = "PC qui trình bậc 1",IsAllowanceType=true,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new MasterDetail() {MasterID =16,MasterDetailCode=2,Name = "PC qui trình bậc 2",IsAllowanceType=true,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},

                    new MasterDetail() {MasterID =17,MasterDetailCode=0,Name = "Không chỉ định",IsAllowanceType=false,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new MasterDetail() {MasterID =17,MasterDetailCode=1,Name = "HĐ học việc",IsAllowanceType=false,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new MasterDetail() {MasterID =17,MasterDetailCode=2,Name = "HĐ thử việc",IsAllowanceType=false,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new MasterDetail() {MasterID =17,MasterDetailCode=3,Name = "HĐ cộng tác viên",IsAllowanceType=false,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new MasterDetail() {MasterID =17,MasterDetailCode=4,Name = "HĐ chính thức ngắn hạn 12 tháng ",IsAllowanceType=false,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new MasterDetail() {MasterID =17,MasterDetailCode=5,Name = "HĐ chính thức ngắn hạn 36 tháng ",IsAllowanceType=false,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new MasterDetail() {MasterID =17,MasterDetailCode=6,Name = "HĐ chính thức vô thời hạn",IsAllowanceType=false,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new MasterDetail() {MasterID =17,MasterDetailCode=7,Name = "HĐ đào tạo",IsAllowanceType=false,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},

                    new MasterDetail() {MasterID =18,MasterDetailCode=0,Name = "Không chỉ định",IsAllowanceType=false,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new MasterDetail() {MasterID =18,MasterDetailCode=1,Name = "Lập trình viên",IsAllowanceType=false,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new MasterDetail() {MasterID =18,MasterDetailCode=2,Name = "Phiên dịch",IsAllowanceType=false,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new MasterDetail() {MasterID =18,MasterDetailCode=3,Name = "Hỗ trợ qui trình",IsAllowanceType=false,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new MasterDetail() {MasterID =18,MasterDetailCode=4,Name = "Tổng vụ",IsAllowanceType=false,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new MasterDetail() {MasterID =18,MasterDetailCode=5,Name = "Thực tập",IsAllowanceType=false,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new MasterDetail() {MasterID =18,MasterDetailCode=6,Name = "Bán thời gian",IsAllowanceType=false,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new MasterDetail() {MasterID =18,MasterDetailCode=7,Name = "Công ty khác chuyển tới",IsAllowanceType=false,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new MasterDetail() {MasterID =18,MasterDetailCode=8,Name = "Nhân viên kinh doanh",IsAllowanceType=false,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new MasterDetail() {MasterID =18,MasterDetailCode=9,Name = "Nhân viên khác",IsAllowanceType=false,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},

                    new MasterDetail() {MasterID =19,MasterDetailCode=0,Name = "Không chỉ định",IsAllowanceType=false,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new MasterDetail() {MasterID =19,MasterDetailCode=1,Name = "Hệ cao đẳng",IsAllowanceType=false,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new MasterDetail() {MasterID =19,MasterDetailCode=2,Name = "Đại học",IsAllowanceType=false,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new MasterDetail() {MasterID =19,MasterDetailCode=3,Name = "Cao học",IsAllowanceType=false,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new MasterDetail() {MasterID =19,MasterDetailCode=4,Name = "Tiến sĩ",IsAllowanceType=false,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},

                    new MasterDetail() {MasterID =20,MasterDetailCode=0,Name = "Không chỉ định",ShortName="Không chỉ định",IsAllowanceType=false,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new MasterDetail() {MasterID =20,MasterDetailCode=1,Name = "Quotaion", ShortName="Q", IsAllowanceType=false,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new MasterDetail() {MasterID =20,MasterDetailCode=2,Name = "KeepLabor",ShortName="L",IsAllowanceType=false,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new MasterDetail() {MasterID =20,MasterDetailCode=3,Name = "Uchida Quotaion",ShortName="UQ",IsAllowanceType=false,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new MasterDetail() {MasterID =20,MasterDetailCode=4,Name = "Uchida KeepLabor",ShortName="UL",IsAllowanceType=false,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},

                    new MasterDetail() {MasterID =21,MasterDetailCode=0,Name = "Không chỉ định",IsAllowanceType=false,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new MasterDetail() {MasterID =21,MasterDetailCode=1,Name = "Xét lương 6 tháng",IsAllowanceType=false,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new MasterDetail() {MasterID =21,MasterDetailCode=2,Name = "Xét lương 12 tháng",IsAllowanceType=false,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},

                    new MasterDetail() {MasterID =22,MasterDetailCode=0,Name = "Không chỉ định",IsAllowanceType=false,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new MasterDetail() {MasterID =22,MasterDetailCode=1,Name = "Onsite dạng intership",IsAllowanceType=false,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new MasterDetail() {MasterID =22,MasterDetailCode=2,Name = "Onsite ngắn hạn từ 1-3 tháng",IsAllowanceType=false,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new MasterDetail() {MasterID =22,MasterDetailCode=3,Name = "Onsite ngắn hạn từ 3 - 6 tháng",IsAllowanceType=false,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new MasterDetail() {MasterID =22,MasterDetailCode=4,Name = "Onsite dài hạn từ 1 năm trở lên",IsAllowanceType=false,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},

                    new MasterDetail() {MasterID =23,MasterDetailCode=0,Name = "Không chỉ định",IsAllowanceType=false,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new MasterDetail() {MasterID =23,MasterDetailCode=1,Name = "Ngày",IsAllowanceType=false,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new MasterDetail() {MasterID =23,MasterDetailCode=2,Name = "Tuần",IsAllowanceType=false,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new MasterDetail() {MasterID =23,MasterDetailCode=3,Name = "Tháng",IsAllowanceType=false,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new MasterDetail() {MasterID =23,MasterDetailCode=4,Name = "Năm",IsAllowanceType=false,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},

                    new MasterDetail() {MasterID =24,MasterDetailCode=0,Name = "Không chỉ định",IsAllowanceType=false,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new MasterDetail() {MasterID =24,MasterDetailCode=1,Name = "Dự án keeplabor",IsAllowanceType=false,Status = false,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new MasterDetail() {MasterID =24,MasterDetailCode=2,Name = "Dự án báo giá",IsAllowanceType=false,Status = false,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},

                    new MasterDetail() {MasterID =25,MasterDetailCode=0,Name = "Không chỉ định",IsAllowanceType=false,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new MasterDetail() {MasterID =25,MasterDetailCode=1,Name = "VND",IsAllowanceType=false,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new MasterDetail() {MasterID =25,MasterDetailCode=2,Name = "USD",IsAllowanceType=false,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new MasterDetail() {MasterID =25,MasterDetailCode=3,Name = "YEN",IsAllowanceType=false,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},

                    new MasterDetail() {MasterID =26,MasterDetailCode=0,Name = "Không chỉ định",ShortName = "Không chỉ định",IsAllowanceType=true,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new MasterDetail() {MasterID =26,MasterDetailCode=1,Name = "Support thực tập",ShortName = "Support thực tập",IsAllowanceType=true,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new MasterDetail() {MasterID =26,MasterDetailCode=2,Name = "Support thử việc",ShortName = "Support thử việc",IsAllowanceType=true,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new MasterDetail() {MasterID =26,MasterDetailCode=3,Name = "Support nhân viên chính thức",ShortName = "Support nhân viên chính thức",IsAllowanceType=true,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},

                    new MasterDetail() {MasterID =27,MasterDetailCode=0,Name = "Không chỉ định",ShortName = "Không chỉ định",IsAllowanceType=false,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new MasterDetail() {MasterID =27,MasterDetailCode=1,Name = "Qui định về nhân sự",ShortName = "Qui định nhân sự",IsAllowanceType=false,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new MasterDetail() {MasterID =27,MasterDetailCode=2,Name = "Qui định onsite",ShortName = "Qui định onsite",IsAllowanceType=false,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new MasterDetail() {MasterID =27,MasterDetailCode=3,Name = "Qui định thử việc",ShortName = "Qui định thử việc",IsAllowanceType=false,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new MasterDetail() {MasterID =27,MasterDetailCode=4,Name = "Qui định nghỉ việc",ShortName = "Qui định nghỉ việc",IsAllowanceType=false,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new MasterDetail() {MasterID =27,MasterDetailCode=5,Name = "Qui định học BSE",ShortName = "Qui định học BSE",IsAllowanceType=false,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new MasterDetail() {MasterID =27,MasterDetailCode=6,Name = "Qui định tuyển dụng",ShortName = "Qui định tuyển dụng",IsAllowanceType=false,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new MasterDetail() {MasterID =27,MasterDetailCode=7,Name = "Qui định phối hợp trong công việc",ShortName = "Qui định phối hợp trong công việc",IsAllowanceType=false,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new MasterDetail() {MasterID =27,MasterDetailCode=8,Name = "Qui định phụ cấp",ShortName = "Qui định phụ cấp",IsAllowanceType=false,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new MasterDetail() {MasterID =27,MasterDetailCode=9,Name = "Qui định khi đi công tác",ShortName = "Qui định khi đi công tác",IsAllowanceType=false,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},

                    new MasterDetail() {MasterID =28,MasterDetailCode=0,Name = "Không chỉ định",ShortName = "Không chỉ định",IsAllowanceType=false,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new MasterDetail() {MasterID =28,MasterDetailCode=1,Name = "Thành viên hội đồng quản trị",ShortName = "Ban quản trị",IsAllowanceType=false,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new MasterDetail() {MasterID =28,MasterDetailCode=2,Name = "Thành viên Ban giám đốc",ShortName = "Ban giám đốc",IsAllowanceType=false,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new MasterDetail() {MasterID =28,MasterDetailCode=3,Name = "Hội đồng điều hành",ShortName = "Hội đồng điềuhành",IsAllowanceType=false,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new MasterDetail() {MasterID =28,MasterDetailCode=4,Name = "Manager",ShortName = "Manager",IsAllowanceType=false,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new MasterDetail() {MasterID =28,MasterDetailCode=5,Name = "Leader",ShortName = "Leader",IsAllowanceType=false,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new MasterDetail() {MasterID =28,MasterDetailCode=6,Name = "Developer",ShortName = "Developer",IsAllowanceType=false,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new MasterDetail() {MasterID =28,MasterDetailCode=7,Name = "BSE",ShortName = "BSE",IsAllowanceType=false,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new MasterDetail() {MasterID =28,MasterDetailCode=8,Name = "Phiên dịch",ShortName = "Phiên dịch",IsAllowanceType=false,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new MasterDetail() {MasterID =28,MasterDetailCode=9,Name = "Tổng vụ",ShortName = "Tổng vụ",IsAllowanceType=false,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"}

                };
                context.MasterDetails.AddRange(listData);

                try
                {
                    context.SaveChanges();
                }
                catch (System.Data.Entity.Validation.DbEntityValidationException e)
                {
                    var outputLines = new List<string>();
                    foreach (var eve in e.EntityValidationErrors)
                    {
                        outputLines.Add(string.Format(
                            "{0}: Entity of type \"{1}\" in state \"{2}\" has the following validation errors:",
                            DateTime.Now, eve.Entry.Entity.GetType().Name, eve.Entry.State));
                        foreach (var ve in eve.ValidationErrors)
                        {
                            outputLines.Add(string.Format(
                                "- Property: \"{0}\", Error: \"{1}\"",
                                ve.PropertyName, ve.ErrorMessage));
                        }
                    }
                    //Write to file
                    System.IO.File.AppendAllLines(@"c:\temp\errors.txt", outputLines);
                    throw;

                    // Showing it on screen
                    throw new Exception(string.Join(",", outputLines.ToArray()));

                }
            }
        }

        private void CreateEmp(EmpManDbContext context)
        {
            if (context.Emps.Count() == 0 || isResetMasterData)
            {
                //reset identity column 
                //context.Database.ExecuteSqlCommand("DBCC CHECKIDENT (Emps, RESEED, 0)");

                List<Emp> listData = new List<Emp>()
                {
                    new Emp() {ID=0,FullName = "Admin",Name="Admin",ExtLinkNo="E0000", WorkingEmail="", Gender=true,Status = false,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    /*
                    new Emp() {ID=1,FullName = "Nguyễn Đăng Phong",Name="Phong",ExtLinkNo="E0001", WorkingEmail="fujinet.net", Gender=true,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Emp() {ID=2,FullName = "Nguyễn Đăng Phú",Name="Phú",ExtLinkNo="E0002", WorkingEmail="fujinet.net", Gender=true,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Emp() {ID=3,FullName = "Phạm Nguyễn Mạnh",Name="Mạnh",ExtLinkNo="E0003", WorkingEmail="fujinet.net", Gender=true,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Emp() {ID=4,FullName = "Nguyễn Ngọc Hồng Hạnh",Name="Hạnh",ExtLinkNo="E0004", WorkingEmail="fujinet.net", Gender=false,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Emp() {ID=5,FullName = "Thân Minh Trung",Name="Trung",ExtLinkNo="E0005", WorkingEmail="fujinet.net", Gender=true,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Emp() {ID=6,FullName = "Lý Vĩnh Nhân Liêm",Name="Liêm",ExtLinkNo="E0006", WorkingEmail="fujinet.net", Gender=true,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Emp() {ID=7,FullName = "Phạm Quốc Vỹ",Name="Vỹ",ExtLinkNo="E0007",WorkingEmail="fujinet.net", Gender=true,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Emp() {ID=8,FullName = "Hoàng Đình Lệ Ngân",Name="Ngân",ExtLinkNo="E0008",WorkingEmail="fujinet.net", Gender=false,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Emp() {ID=9,FullName = "Đinh Bảo Tuyên",Name="Tuyên",ExtLinkNo="E0009", WorkingEmail="fujinet.net", Gender=true,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Emp() {ID=10,FullName = "Mai Xuân Hiếu",Name="Hiếu",ExtLinkNo="E0010",WorkingEmail="fujinet.net", Gender=true,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Emp() {ID=11,FullName = "Nguyễn Xuân Hòa",Name="Hòa",ExtLinkNo="E0011",WorkingEmail="fujinet.net", Gender=true,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Emp() {ID=12,FullName = "Nguyễn Thị Mỹ Hương",Name="Hương",ExtLinkNo="E0012", WorkingEmail="fujinet.net", Gender=false,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Emp() {ID=13,FullName = "Tạ Ngọc Huy Đông",Name="Đông",ExtLinkNo="E0013" ,WorkingEmail="fujinet.net", Gender=true,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Emp() {ID=14,FullName = "Trần Thị Huyền Trang",Name="Trang",ExtLinkNo="E0014", WorkingEmail="fujinet.net", Gender=false,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Emp() {ID=15,FullName = "Phạm Nguyễn Uyên Trầm",Name="Trầm",ExtLinkNo="E0015", WorkingEmail="fujinet.net", Gender=false,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Emp() {ID=16,FullName = "Dương Thái Trung",Name="Trung",ExtLinkNo="E0016", WorkingEmail="fujinet.net", Gender=false,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Emp() {ID=17,FullName = "Nguyễn Minh Hải",Name="Hải",ExtLinkNo="E0017", WorkingEmail="fujinet.net", Gender=false,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Emp() {ID=18,FullName = "Lê Minh Đạt",Name="Đạt",ExtLinkNo="E0018", WorkingEmail="fujinet.net", Gender=false,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Emp() {ID=19,FullName = "Cao Thị Như Huỳnh",Name="Huỳnh",ExtLinkNo="E0019", WorkingEmail="fujinet.net", Gender=false,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Emp() {ID=20,FullName = "Nguyễn Hồng Vinh",Name="Vinh",ExtLinkNo="E0020", WorkingEmail="fujinet.net", Gender=false,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},

                    new Emp() {ID=21,FullName = "Lương Văn Thiện",ExtLinkNo="E0021",PhoneNumber1 ="0938486649", WorkingEmail="fujinet.net", Gender=true,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Emp() {ID=22,FullName = "Nguyễn Viết Lộc",ExtLinkNo="E0022",PhoneNumber1 ="0975185785", WorkingEmail="fujinet.net", Gender=true,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Emp() {ID=23,FullName = "Hồ Thị Mỹ Hạnh",ExtLinkNo="E0023",PhoneNumber1 ="1695999192", WorkingEmail="fujinet.net", Gender=true,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Emp() {ID=24,FullName = "Vũ Thị Xuyến",ExtLinkNo="E0024",PhoneNumber1 ="01682065803", WorkingEmail="fujinet.net", Gender=true,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Emp() {ID=25,FullName = "Nguyễn Văn Thi",ExtLinkNo="E0025",PhoneNumber1 ="914193991", WorkingEmail="fujinet.net", Gender=true,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Emp() {ID=26,FullName = "Đàm Đức Trung",ExtLinkNo="E0026",PhoneNumber1 ="906093731", WorkingEmail="fujinet.net", Gender=true,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Emp() {ID=27,FullName = "Đoàn Việt Khải",ExtLinkNo="E0027",PhoneNumber1 ="935443855", WorkingEmail="fujinet.net", Gender=true,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Emp() {ID=28,FullName = "Nguyễn Lê Thanh Tú",ExtLinkNo="E0028",PhoneNumber1 ="1223130389", WorkingEmail="fujinet.net", Gender=true,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Emp() {ID=29,FullName = "Phan Thị Thùy Vân",ExtLinkNo="E0029",PhoneNumber1 ="985719890", WorkingEmail="fujinet.net", Gender=true,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Emp() {ID=30,FullName = "Lê Luân",ExtLinkNo="E0030",PhoneNumber1 ="987777520", WorkingEmail="fujinet.net", Gender=true,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Emp() {ID=31,FullName = "Du Phát",ExtLinkNo="E0031",PhoneNumber1 ="937587526", WorkingEmail="fujinet.net", Gender=true,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Emp() {ID=32,FullName = "Nguyễn Văn Diểu",ExtLinkNo="E0032",PhoneNumber1 ="976240779", WorkingEmail="fujinet.net", Gender=true,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Emp() {ID=33,FullName = "Nguyễn Thành Luân (2)",ExtLinkNo="E0033",PhoneNumber1 ="988112589", WorkingEmail="fujinet.net", Gender=true,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Emp() {ID=34,FullName = "Trần Nguyễn Hướng",ExtLinkNo="E0034",PhoneNumber1 ="935381586", WorkingEmail="fujinet.net", Gender=true,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Emp() {ID=35,FullName = "Huỳnh Lê Duy",ExtLinkNo="E0035",PhoneNumber1 ="903144218", WorkingEmail="fujinet.net", Gender=true,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Emp() {ID=36,FullName = "Nguyễn Văn Đạt",ExtLinkNo="E0036",PhoneNumber1 ="982800643", WorkingEmail="fujinet.net", Gender=true,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Emp() {ID=37,FullName = "Nguyễn Xung Quan",ExtLinkNo="E0037",PhoneNumber1 ="936403140", WorkingEmail="fujinet.net", Gender=true,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Emp() {ID=38,FullName = "Phạm Thị Thanh Tâm",ExtLinkNo="E0038",PhoneNumber1 ="0986018463", WorkingEmail="fujinet.net", Gender=true,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Emp() {ID=39,FullName = "Nguyễn Thị Thanh Mai (2)",ExtLinkNo="E0039",PhoneNumber1 ="", WorkingEmail="fujinet.net", Gender=true,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Emp() {ID=40,FullName = "Phạm Thanh Luân",ExtLinkNo="E0040",PhoneNumber1 ="988105350", WorkingEmail="fujinet.net", Gender=true,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Emp() {ID=41,FullName = "Lưu Hậu Thanh Quang",ExtLinkNo="E0041",PhoneNumber1 ="974507492", WorkingEmail="fujinet.net", Gender=true,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Emp() {ID=42,FullName = "Phạm Thanh Phúc",ExtLinkNo="E0042",PhoneNumber1 ="932199111", WorkingEmail="fujinet.net", Gender=true,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Emp() {ID=43,FullName = "Trần Huỳnh Quốc Nam",ExtLinkNo="E0043",PhoneNumber1 ="1688493670", WorkingEmail="fujinet.net", Gender=true,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Emp() {ID=44,FullName = "Lê Trung Hiếu",ExtLinkNo="E0044",PhoneNumber1 ="934003486", WorkingEmail="fujinet.net", Gender=true,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Emp() {ID=45,FullName = "Trịnh Bá Ân",ExtLinkNo="E0045",PhoneNumber1 ="989621512", WorkingEmail="fujinet.net", Gender=true,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Emp() {ID=46,FullName = "Ngô Ngọc Điệp",ExtLinkNo="E0046",PhoneNumber1 ="973831357", WorkingEmail="fujinet.net", Gender=true,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Emp() {ID=47,FullName = "Nguyễn Minh Nhật",ExtLinkNo="E0047",PhoneNumber1 ="62621421", WorkingEmail="fujinet.net", Gender=true,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Emp() {ID=48,FullName = "Đào Xuân Kiên",ExtLinkNo="E0048",PhoneNumber1 ="934040047", WorkingEmail="fujinet.net", Gender=true,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Emp() {ID=49,FullName = "Trương Thái Sơn",ExtLinkNo="E0049",PhoneNumber1 ="938437858", WorkingEmail="fujinet.net", Gender=true,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Emp() {ID=50,FullName = "Nguyễn Thị Hiền",ExtLinkNo="E0050",PhoneNumber1 ="1683566766", WorkingEmail="fujinet.net", Gender=true,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Emp() {ID=51,FullName = "Nguyễn Thị Kim Yến",ExtLinkNo="E0051",PhoneNumber1 ="905462453", WorkingEmail="fujinet.net", Gender=true,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Emp() {ID=52,FullName = "Trương Ngọc Phượng Quân",ExtLinkNo="E0052",PhoneNumber1 ="902029869", WorkingEmail="fujinet.net", Gender=true,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Emp() {ID=53,FullName = "Trần Văn Niên",ExtLinkNo="E0053",PhoneNumber1 ="1688999063", WorkingEmail="fujinet.net", Gender=true,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Emp() {ID=54,FullName = "K' Văn Vân",ExtLinkNo="E0054",PhoneNumber1 ="909795256", WorkingEmail="fujinet.net", Gender=true,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Emp() {ID=55,FullName = "Nguyễn Cao Thăng",ExtLinkNo="E0055",PhoneNumber1 ="984996563", WorkingEmail="fujinet.net", Gender=true,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Emp() {ID=56,FullName = "Lê Ngọc Linh",ExtLinkNo="E0056",PhoneNumber1 ="0903760265", WorkingEmail="fujinet.net", Gender=true,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Emp() {ID=57,FullName = "Trịnh Thị Kim Tình",ExtLinkNo="E0057",PhoneNumber1 ="1654535492", WorkingEmail="fujinet.net", Gender=true,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Emp() {ID=58,FullName = "Hà Quốc Hùng",ExtLinkNo="E0058",PhoneNumber1 ="1665995121", WorkingEmail="fujinet.net", Gender=true,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Emp() {ID=59,FullName = "Trần Minh Thiện",ExtLinkNo="E0059",PhoneNumber1 ="1694600412", WorkingEmail="fujinet.net", Gender=true,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Emp() {ID=60,FullName = "Phạm Tùng Lâm",ExtLinkNo="E0060",PhoneNumber1 ="973678477", WorkingEmail="fujinet.net", Gender=true,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Emp() {ID=61,FullName = "Nguyễn Thị Phương Duyên",ExtLinkNo="E0061",PhoneNumber1 ="1226753047", WorkingEmail="fujinet.net", Gender=true,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Emp() {ID=62,FullName = "Nguyễn Quyết Tâm",ExtLinkNo="E0062",PhoneNumber1 ="972902400", WorkingEmail="fujinet.net", Gender=true,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Emp() {ID=63,FullName = "Trần Duy Linh",ExtLinkNo="E0063",PhoneNumber1 ="974645255", WorkingEmail="fujinet.net", Gender=true,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Emp() {ID=64,FullName = "Nguyễn Duy Linh",ExtLinkNo="E0064",PhoneNumber1 ="0972708787", WorkingEmail="fujinet.net", Gender=true,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Emp() {ID=65,FullName = "Bùi Trường An",ExtLinkNo="E0065",PhoneNumber1 ="969345119", WorkingEmail="fujinet.net", Gender=true,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Emp() {ID=66,FullName = "Bùi Trần Thái Phong",ExtLinkNo="E0066",PhoneNumber1 ="1686165790", WorkingEmail="fujinet.net", Gender=true,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Emp() {ID=67,FullName = "Phan Nguyễn Tuyết Trinh",ExtLinkNo="E0067",PhoneNumber1 ="1269692966", WorkingEmail="fujinet.net", Gender=true,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Emp() {ID=68,FullName = "Ngô Thị Hồng Sen",ExtLinkNo="E0068",PhoneNumber1 ="1678514536", WorkingEmail="fujinet.net", Gender=true,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Emp() {ID=69,FullName = "Lý Quốc Hào",ExtLinkNo="E0069",PhoneNumber1 ="933356894", WorkingEmail="fujinet.net", Gender=true,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Emp() {ID=70,FullName = "Ngô Phú Cường",ExtLinkNo="E0070",PhoneNumber1 ="01633894788", WorkingEmail="fujinet.net", Gender=true,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Emp() {ID=71,FullName = "Lê Hoàng Vũ",ExtLinkNo="E0071",PhoneNumber1 ="971196061", WorkingEmail="fujinet.net", Gender=true,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Emp() {ID=72,FullName = "Nguyễn Phạm Vi",ExtLinkNo="E0072",PhoneNumber1 ="939941737", WorkingEmail="fujinet.net", Gender=true,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Emp() {ID=73,FullName = "Trần Quốc Thái",ExtLinkNo="E0073",PhoneNumber1 ="1688000836", WorkingEmail="fujinet.net", Gender=true,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Emp() {ID=74,FullName = "Nguyễn Tấn Phúc",ExtLinkNo="E0074",PhoneNumber1 ="1667109777", WorkingEmail="fujinet.net", Gender=true,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Emp() {ID=75,FullName = "Lâm Minh Nhật",ExtLinkNo="E0075",PhoneNumber1 ="913732713", WorkingEmail="fujinet.net", Gender=true,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Emp() {ID=76,FullName = "Tăng Thanh Phước",ExtLinkNo="E0076",PhoneNumber1 ="1658747858", WorkingEmail="fujinet.net", Gender=true,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Emp() {ID=77,FullName = "Lê Thành Phúc",ExtLinkNo="E0077",PhoneNumber1 ="1223768152", WorkingEmail="fujinet.net", Gender=true,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Emp() {ID=78,FullName = "Trần Thái Bình",ExtLinkNo="E0078",PhoneNumber1 ="988284955", WorkingEmail="fujinet.net", Gender=true,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Emp() {ID=79,FullName = "Đặng Đình Duy",ExtLinkNo="E0079",PhoneNumber1 ="939626010", WorkingEmail="fujinet.net", Gender=true,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Emp() {ID=80,FullName = "Lê Quang Hiếu",ExtLinkNo="E0080",PhoneNumber1 ="989513531", WorkingEmail="fujinet.net", Gender=true,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Emp() {ID=81,FullName = "Lăng Hoài Sang",ExtLinkNo="E0081",PhoneNumber1 ="938558158", WorkingEmail="fujinet.net", Gender=true,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Emp() {ID=82,FullName = "Nguyễn Phúc Thịnh",ExtLinkNo="E0082",PhoneNumber1 ="0944 255 832", WorkingEmail="fujinet.net", Gender=true,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Emp() {ID=83,FullName = "Bùi Hữu Lộc",ExtLinkNo="E0083",PhoneNumber1 ="1647766355", WorkingEmail="fujinet.net", Gender=true,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Emp() {ID=84,FullName = "Võ Thị Hồng Gấm",ExtLinkNo="E0084",PhoneNumber1 ="1694507830", WorkingEmail="fujinet.net", Gender=true,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Emp() {ID=85,FullName = "Nguyễn Văn Cương",ExtLinkNo="E0085",PhoneNumber1 ="1652924803", WorkingEmail="fujinet.net", Gender=true,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Emp() {ID=86,FullName = "Trần Văn Tiến",ExtLinkNo="E0086",PhoneNumber1 ="1657281521", WorkingEmail="fujinet.net", Gender=true,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Emp() {ID=87,FullName = "Trần Văn Nhực",ExtLinkNo="E0087",PhoneNumber1 ="1663246690", WorkingEmail="fujinet.net", Gender=true,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Emp() {ID=88,FullName = "Nguyễn Đình Hiển",ExtLinkNo="E0088",PhoneNumber1 ="1649795684", WorkingEmail="fujinet.net", Gender=true,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Emp() {ID=89,FullName = "Nguyễn Thị Yến",ExtLinkNo="E0089",PhoneNumber1 ="966991114", WorkingEmail="fujinet.net", Gender=true,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Emp() {ID=90,FullName = "Nguyễn Thị Kim Anh",ExtLinkNo="E0090",PhoneNumber1 ="935009137", WorkingEmail="fujinet.net", Gender=true,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Emp() {ID=91,FullName = "Lê Văn Tâm (2)",ExtLinkNo="E0091",PhoneNumber1 ="1282515851", WorkingEmail="fujinet.net", Gender=true,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Emp() {ID=92,FullName = "Võ Thị Thanh Thủy",ExtLinkNo="E0092",PhoneNumber1 ="1693977659", WorkingEmail="fujinet.net", Gender=true,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Emp() {ID=93,FullName = "Lục Thụy Mai Trâm",ExtLinkNo="E0093",PhoneNumber1 ="0166 886 8977", WorkingEmail="fujinet.net", Gender=true,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Emp() {ID=94,FullName = "Nguyễn Trần Huỳnh Như",ExtLinkNo="E0094",PhoneNumber1 ="903060749", WorkingEmail="fujinet.net", Gender=true,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Emp() {ID=95,FullName = "Trịnh Thị Kim Quyên",ExtLinkNo="E0095",PhoneNumber1 ="1635722748", WorkingEmail="fujinet.net", Gender=true,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Emp() {ID=96,FullName = "Phan Huỳnh Tuấn Cường",ExtLinkNo="E0096",PhoneNumber1 ="944900972", WorkingEmail="fujinet.net", Gender=true,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Emp() {ID=97,FullName = "Đào Ngọc Hậu",ExtLinkNo="E0097",PhoneNumber1 ="1689091090", WorkingEmail="fujinet.net", Gender=true,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Emp() {ID=98,FullName = "Phan Thanh Nhân",ExtLinkNo="E0098",PhoneNumber1 ="901432031", WorkingEmail="fujinet.net", Gender=true,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Emp() {ID=99,FullName = "Phan Văn Tân",ExtLinkNo="E0099",PhoneNumber1 ="902939087", WorkingEmail="fujinet.net", Gender=true,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Emp() {ID=100,FullName = "Trần Ngọc Dân",ExtLinkNo="E0100",PhoneNumber1 ="989687184", WorkingEmail="fujinet.net", Gender=true,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Emp() {ID=101,FullName = "Đỗ Hoàng Phương",ExtLinkNo="E0101",PhoneNumber1 ="1686879292", WorkingEmail="fujinet.net", Gender=true,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Emp() {ID=102,FullName = "Nguyễn Quốc Trinh",ExtLinkNo="E0102",PhoneNumber1 ="974109605", WorkingEmail="fujinet.net", Gender=true,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Emp() {ID=103,FullName = "Bùi Ngọc Việt",ExtLinkNo="E0103",PhoneNumber1 ="1678930159", WorkingEmail="fujinet.net", Gender=true,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Emp() {ID=104,FullName = "Lữ Hoàng Thành",ExtLinkNo="E0104",PhoneNumber1 ="1658088334", WorkingEmail="fujinet.net", Gender=true,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Emp() {ID=105,FullName = "Nguyễn Cao Phương Duy",ExtLinkNo="E0105",PhoneNumber1 ="1666692364", WorkingEmail="fujinet.net", Gender=true,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Emp() {ID=106,FullName = "Vũ Anh Kiệt",ExtLinkNo="E0106",PhoneNumber1 ="939205530", WorkingEmail="fujinet.net", Gender=true,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Emp() {ID=107,FullName = "Bùi Hữu Tiến",ExtLinkNo="E0107",PhoneNumber1 ="933000142", WorkingEmail="fujinet.net", Gender=true,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Emp() {ID=108,FullName = "Lê Hà Tiên",ExtLinkNo="E0108",PhoneNumber1 ="1642420940", WorkingEmail="fujinet.net", Gender=true,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Emp() {ID=109,FullName = "Huỳnh Minh Phụng",ExtLinkNo="E0109",PhoneNumber1 ="1654865365", WorkingEmail="fujinet.net", Gender=true,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Emp() {ID=110,FullName = "Phan Thế Linh",ExtLinkNo="E0110",PhoneNumber1 ="906945468", WorkingEmail="fujinet.net", Gender=true,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Emp() {ID=111,FullName = "Lê Văn Thân",ExtLinkNo="E0111",PhoneNumber1 ="1644431614", WorkingEmail="fujinet.net", Gender=true,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Emp() {ID=112,FullName = "Trần Quang Việt",ExtLinkNo="E0112",PhoneNumber1 ="1693474485", WorkingEmail="fujinet.net", Gender=true,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Emp() {ID=113,FullName = "Nguyễn Minh Dũng",ExtLinkNo="E0113",PhoneNumber1 ="0967872636", WorkingEmail="fujinet.net", Gender=true,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Emp() {ID=114,FullName = "Nguyễn Nhật Truyền",ExtLinkNo="E0114",PhoneNumber1 ="01674954554", WorkingEmail="fujinet.net", Gender=true,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Emp() {ID=115,FullName = "Nguyễn Tiến Dũng",ExtLinkNo="E0115",PhoneNumber1 ="0906886408", WorkingEmail="fujinet.net", Gender=true,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Emp() {ID=116,FullName = "Nguyễn Văn Sũng",ExtLinkNo="E0116",PhoneNumber1 ="", WorkingEmail="fujinet.net", Gender=true,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Emp() {ID=117,FullName = "Nguyễn Thị Lan Hương",ExtLinkNo="E0117",PhoneNumber1 ="0969776485", WorkingEmail="fujinet.net", Gender=true,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Emp() {ID=118,FullName = "Nguyễn Trần Minh Thư",ExtLinkNo="E0118",PhoneNumber1 ="903781043", WorkingEmail="fujinet.net", Gender=true,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"}
                    */
                };
                context.Emps.AddRange(listData);

                try
                {
                    context.SaveChanges();
                }
                catch (System.Data.Entity.Validation.DbEntityValidationException e)
                {
                    var outputLines = new List<string>();
                    foreach (var eve in e.EntityValidationErrors)
                    {
                        outputLines.Add(string.Format(
                            "{0}: Entity of type \"{1}\" in state \"{2}\" has the following validation errors:",
                            DateTime.Now, eve.Entry.Entity.GetType().Name, eve.Entry.State));
                        foreach (var ve in eve.ValidationErrors)
                        {
                            outputLines.Add(string.Format(
                                "- Property: \"{0}\", Error: \"{1}\"",
                                ve.PropertyName, ve.ErrorMessage));
                        }
                    }
                    //Write to file
                    System.IO.File.AppendAllLines(@"c:\temp\errors.txt", outputLines);
                    throw;

                    // Showing it on screen
                    throw new Exception(string.Join(",", outputLines.ToArray()));

                }
            }
        }

        private void CreateCustomer(EmpManDbContext context)
        {
            if (context.Customers.Count() == 0 || isResetMasterData)
            {
                //reset identity column 
                //context.Database.ExecuteSqlCommand("DBCC CHECKIDENT (Customers, RESEED, 0)");

                List<Customer> listData = new List<Customer>()
                {
                    new Customer() {ID=0,GroupName="Không chỉ định", Name= "Không chỉ định",ShortName="Không chỉ định" , MangRate=0 , TransRate =0m , DefaultOrderUnitMasterID=25, DefaultOrderUnitMasterDetailID = 3,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Customer() {ID=1,GroupName="Uchida Yoko", Name= "Uchida IT Solutions",ShortName="ITS" , MangRate=10 , TransRate =12.5m , DefaultOrderUnitMasterID=25, DefaultOrderUnitMasterDetailID = 3,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Customer() {ID=2,GroupName="EX", Name= "EX Corporation",ShortName="EX", MangRate=10 , TransRate =15m , DefaultOrderUnitMasterID=25, DefaultOrderUnitMasterDetailID = 2,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Customer() {ID=3,GroupName="Uchida Yoko", Name= "Uchida ESCO",ShortName="ESCO", MangRate=10 , TransRate =12.5m , DefaultOrderUnitMasterID=25, DefaultOrderUnitMasterDetailID = 3,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Customer() {ID=4,GroupName="Mitsubishi", Name= "MDBS",ShortName="MDBS", MangRate=10 , TransRate =12.5m , DefaultOrderUnitMasterID=25, DefaultOrderUnitMasterDetailID = 2,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Customer() {ID=5,GroupName="", Name= "NTK",ShortName="NTK", MangRate=10 , TransRate =12.5m , DefaultOrderUnitMasterID=25, DefaultOrderUnitMasterDetailID = 2,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Customer() {ID=6,GroupName="", Name= "Chiyoda",ShortName="CHYD", MangRate=10 , TransRate =15m , DefaultOrderUnitMasterID=25, DefaultOrderUnitMasterDetailID = 2,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Customer() {ID=7,GroupName="", Name= "MEC",ShortName="MEC", MangRate=10 , TransRate =12.5m , DefaultOrderUnitMasterID=25, DefaultOrderUnitMasterDetailID = 2,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Customer() {ID=8,GroupName="Mitsubishi", Name= "MDIS",ShortName="MDIS", MangRate=10 , TransRate =12.5m , DefaultOrderUnitMasterID=25, DefaultOrderUnitMasterDetailID = 2,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Customer() {ID=9,GroupName="", Name= "HMT",ShortName="HMT", MangRate=10 , TransRate =12.5m , DefaultOrderUnitMasterID=25, DefaultOrderUnitMasterDetailID = 2,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Customer() {ID=10,GroupName="EX", Name= "EX-Engineering ",ShortName="EX", MangRate=10 , TransRate =15m , DefaultOrderUnitMasterID=25, DefaultOrderUnitMasterDetailID = 2,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new Customer() {ID=11,GroupName="", Name= "Canon IT Solution",ShortName="CITS", MangRate=0 , TransRate =0m , DefaultOrderUnitMasterID=25, DefaultOrderUnitMasterDetailID = 2,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"}

                };
                context.Customers.AddRange(listData);

                try
                {
                    context.SaveChanges();
                }
                catch (System.Data.Entity.Validation.DbEntityValidationException e)
                {
                    var outputLines = new List<string>();
                    foreach (var eve in e.EntityValidationErrors)
                    {
                        outputLines.Add(string.Format(
                            "{0}: Entity of type \"{1}\" in state \"{2}\" has the following validation errors:",
                            DateTime.Now, eve.Entry.Entity.GetType().Name, eve.Entry.State));
                        foreach (var ve in eve.ValidationErrors)
                        {
                            outputLines.Add(string.Format(
                                "- Property: \"{0}\", Error: \"{1}\"",
                                ve.PropertyName, ve.ErrorMessage));
                        }
                    }
                    //Write to file
                    System.IO.File.AppendAllLines(@"c:\temp\errors.txt", outputLines);
                    throw;

                    // Showing it on screen
                    throw new Exception(string.Join(",", outputLines.ToArray()));

                }
            }
        }

        private void CreateCustomerUnitPriceSample(EmpManDbContext context)
        {
            if (context.CustomerUnitPrices.Count() == 0 || isResetMasterData)
            {
                //reset identity column 
                //context.Database.ExecuteSqlCommand("DBCC CHECKIDENT (CustomerUnitPrices, RESEED, 0)");

                List<CustomerUnitPrice> listData = new List<CustomerUnitPrice>()
                {
                    new CustomerUnitPrice() {ID=1, CustomerID=1, Name= "ITS đơn giá tháng 4/2017",StartDate= DateTime.Parse("2017/04/21") , MangRate=10 , TransRate =12.5m , OrderUnitMasterID=25, OrderUnitMasterDetailID = 3, OrderPrice=1000,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new CustomerUnitPrice() {ID=2, CustomerID=2, Name= "EX đơn giá tháng 1/2017",StartDate= DateTime.Parse("2017/01/01") , MangRate=10 , TransRate =15m , OrderUnitMasterID=25, OrderUnitMasterDetailID = 2, OrderPrice=1000,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new CustomerUnitPrice() {ID=3, CustomerID=3, Name= "ESCO đơn giá tháng 4/2017",StartDate= DateTime.Parse("2017/04/21") , MangRate=10 , TransRate =12.5m , OrderUnitMasterID=25, OrderUnitMasterDetailID = 3, OrderPrice=1000,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new CustomerUnitPrice() {ID=4, CustomerID=5, Name= "NTK đơn giá tháng 1/2017",StartDate= DateTime.Parse("2017/01/01") , MangRate=10 , TransRate =12.5m , OrderUnitMasterID=25, OrderUnitMasterDetailID = 2, OrderPrice=1000,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new CustomerUnitPrice() {ID=5, CustomerID=6, Name= "CYD-LTV đơn giá có kinh nghiệm tháng 1/2017",StartDate= DateTime.Parse("2017/01/01") , MangRate=10 , TransRate =15m , OrderUnitMasterID=25, OrderUnitMasterDetailID = 2, OrderPrice=1000,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new CustomerUnitPrice() {ID=6, CustomerID=6, Name= "CYD-LTV đơn giá chưa có kinh nghiệm tháng 1/2017",StartDate= DateTime.Parse("2017/01/01") , MangRate=10 , TransRate =15m , OrderUnitMasterID=25, OrderUnitMasterDetailID = 2, OrderPrice=1000,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new CustomerUnitPrice() {ID=7, CustomerID=6, Name= "CYD-QL đơn giá tháng 1/2017",StartDate= DateTime.Parse("2017/01/01") , MangRate=10 , TransRate =15m , OrderUnitMasterID=25, OrderUnitMasterDetailID = 2, OrderPrice=1000,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new CustomerUnitPrice() {ID=8, CustomerID=7, Name= "MEC đơn giá 1/2017",StartDate= DateTime.Parse("2017/01/01") , MangRate=10 , TransRate =12.5m , OrderUnitMasterID=25, OrderUnitMasterDetailID = 2, OrderPrice=1000,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new CustomerUnitPrice() {ID=9, CustomerID=8, Name= "MDIS đơn giá tháng 1/2017",StartDate= DateTime.Parse("2017/01/01") , MangRate=10 , TransRate =12.5m , OrderUnitMasterID=25, OrderUnitMasterDetailID = 3, OrderPrice=1000,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new CustomerUnitPrice() {ID=10, CustomerID=9, Name= "HMT đơn giá tháng 6/2017",StartDate= DateTime.Parse("2017/06/01") , MangRate=10 , TransRate =12.5m , OrderUnitMasterID=25, OrderUnitMasterDetailID = 2, OrderPrice=1000,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"},
                    new CustomerUnitPrice() {ID=11, CustomerID=4, Name= "MDBS đơn giá tháng 6/2017",StartDate= DateTime.Parse("2017/06/01") , MangRate=10 , TransRate =12.5m , OrderUnitMasterID=25, OrderUnitMasterDetailID = 2, OrderPrice=1000,Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"}

                };
                context.CustomerUnitPrices.AddRange(listData);

                try
                {
                    context.SaveChanges();
                }
                catch (System.Data.Entity.Validation.DbEntityValidationException e)
                {
                    var outputLines = new List<string>();
                    foreach (var eve in e.EntityValidationErrors)
                    {
                        outputLines.Add(string.Format(
                            "{0}: Entity of type \"{1}\" in state \"{2}\" has the following validation errors:",
                            DateTime.Now, eve.Entry.Entity.GetType().Name, eve.Entry.State));
                        foreach (var ve in eve.ValidationErrors)
                        {
                            outputLines.Add(string.Format(
                                "- Property: \"{0}\", Error: \"{1}\"",
                                ve.PropertyName, ve.ErrorMessage));
                        }
                    }
                    //Write to file
                    System.IO.File.AppendAllLines(@"c:\temp\errors.txt", outputLines);
                    throw;

                    // Showing it on screen
                    throw new Exception(string.Join(",", outputLines.ToArray()));

                }
            }
        }

        private void CreateSystemConfig(EmpManDbContext context)
        {
            if (context.SystemConfigs.Count() == 0 || isResetMasterData)
            {

                //reset identity column 
                //context.Database.ExecuteSqlCommand("DBCC CHECKIDENT (SystemConfigs, RESEED, 0)");

                List<SystemConfig> listData = new List<SystemConfig>()
                {
                    new SystemConfig() {ID =1 , Code = "admin" ,Name = "Cấu hình hệ thống",ShortName="Cấu hình hệ thống",Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"}

                };
                context.SystemConfigs.AddRange(listData);

                try
                {
                    context.SaveChanges();
                }
                catch (System.Data.Entity.Validation.DbEntityValidationException e)
                {
                    var outputLines = new List<string>();
                    foreach (var eve in e.EntityValidationErrors)
                    {
                        outputLines.Add(string.Format(
                            "{0}: Entity of type \"{1}\" in state \"{2}\" has the following validation errors:",
                            DateTime.Now, eve.Entry.Entity.GetType().Name, eve.Entry.State));
                        foreach (var ve in eve.ValidationErrors)
                        {
                            outputLines.Add(string.Format(
                                "- Property: \"{0}\", Error: \"{1}\"",
                                ve.PropertyName, ve.ErrorMessage));
                        }
                    }
                    //Write to file
                    System.IO.File.AppendAllLines(@"c:\temp\errors.txt", outputLines);
                    throw;

                    // Showing it on screen
                    throw new Exception(string.Join(",", outputLines.ToArray()));

                }
            }
        }
    }
}


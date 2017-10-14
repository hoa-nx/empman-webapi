using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using EmpMan.Model.Models;
using EmpMan.Service;
using EmpMan.Web.Infrastructure.Core;

using System.DirectoryServices.AccountManagement;
using System;
using EmpMan.Common.ViewModels.Models;

namespace EmpMan.Web.Providers
{
    public class AuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        public AuthorizationServerProvider()
        {
        }
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
            await Task.FromResult<object>(null);
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {

            UserManager<AppUser> userManager = context.OwinContext.GetUserManager<UserManager<AppUser>>();
            AppUser user;
            try
            {
                //PrincipalContext AD = new PrincipalContext(ContextType.Domain, "fujinet.vn");
                //UserPrincipal u = new UserPrincipal(AD);
                //u.SamAccountName = "hoa-nx";
                //PrincipalSearcher search = new PrincipalSearcher(u);
                //UserPrincipal result = (UserPrincipal)search.FindOne();
                //search.Dispose();
                bool useDomainAccount = CheckCredentials(context.UserName, context.Password, "FUJINET");

                if (useDomainAccount)
                {
                    //get thong tin cua user

                    user = await userManager.FindAsync(context.UserName, "Abc12345");

                }else
                {
                    //login su dung account he thong
                    user = await userManager.FindAsync(context.UserName, context.Password);
                }
            }
            catch
            {
                // Could not retrieve the user due to error.
                context.SetError("server_error", "Lỗi trong quá trình xử lý.");
                context.Rejected();
                return;
            }
            string empid = "0";
            string processingYear = "";

            if (user != null)
            {
                var permissions = ServiceFactory.Get<IPermissionService>().GetByUserId(user.Id);
                var permissionViewModels = AutoMapper.Mapper.Map<ICollection<Permission>, ICollection<PermissionViewModel>>(permissions);
                var roles = userManager.GetRoles(user.Id);
                var emps = ServiceFactory.Get<IEmpService>().GetByAccount(user.UserName);
                var systemConfig = ServiceFactory.Get<ISystemConfigService>().GetByAccount(user.UserName);
                var systemConfigViewModel = AutoMapper.Mapper.Map<SystemConfig, SystemConfigViewModel>(systemConfig);

                ClaimsIdentity identity = await userManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ExternalBearer);
                string avatar = string.IsNullOrEmpty(user.Avatar) ? "" : user.Avatar;
                string email = string.IsNullOrEmpty(user.Email) ? "" : user.Email;

                identity.AddClaim(new Claim("fullname", user.FullName));
                identity.AddClaim(new Claim("avatar", avatar));
                identity.AddClaim(new Claim("email", email));
                identity.AddClaim(new Claim("username", user.UserName));
                identity.AddClaim(new Claim("companyid", user.CompanyID.ToString()));
                identity.AddClaim(new Claim("deptid", user.DeptID.ToString()));
                identity.AddClaim(new Claim("teamid", user.TeamID.ToString()));

                if (emps != null)
                {
                    empid = emps.ID.ToString();
                }
                identity.AddClaim(new Claim("empid", empid));

                if (systemConfig != null && systemConfig.ProcessingYear.HasValue)
                {
                    processingYear = systemConfig.ProcessingYear.Value.Year.ToString();
                }
                else
                {
                    processingYear= DateTime.Now.Year.ToString();
                }
                identity.AddClaim(new Claim("processingyear", processingYear));

                identity.AddClaim(new Claim("roles", JsonConvert.SerializeObject(roles)));
                identity.AddClaim(new Claim("permissions", JsonConvert.SerializeObject(permissionViewModels)));
                identity.AddClaim(new Claim("systemconfigs", JsonConvert.SerializeObject(systemConfigViewModel)));


                var props = new AuthenticationProperties(new Dictionary<string, string>
                    {
                        {"fullname", user.FullName},
                        {"avatar", avatar },
                        {"email", email},
                        {"username", user.UserName},
                        {"companyid", user.CompanyID.ToString()},
                        {"deptid", user.DeptID.ToString()},
                        {"teamid", user.TeamID.ToString()},
                        {"empid", empid},
                        {"processingyear", processingYear},
                        {"permissions",JsonConvert.SerializeObject(permissionViewModels) },
                        {"roles",JsonConvert.SerializeObject(roles) },
                        {"systemconfigs",JsonConvert.SerializeObject(systemConfigViewModel) }

                    });
                context.Validated(new AuthenticationTicket(identity, props));
            }
            else
            {
                context.SetError("invalid_grant", "Tài khoản hoặc mật khẩu không đúng.");
                context.Rejected();
            }
        }
        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (KeyValuePair<string, string> property in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }
            return Task.FromResult<object>(null);
        }

        static public bool CheckCredentials( string userName, string password, string domain)
        {
            string userPrincipalName = userName + "@" + domain + ".vn";

            try
            {
                using (var context = new PrincipalContext(ContextType.Domain, domain))
                {
                    return context.ValidateCredentials(userPrincipalName, password);
                }
            }
            catch // a bogus domain causes an LDAP error
            {
                return false;
            }
        }
    }
}
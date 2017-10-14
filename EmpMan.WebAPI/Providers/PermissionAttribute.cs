using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;
using System.Web.Http.Controllers;
using EmpMan.Common.Enums;
using EmpMan.Common.ViewModels.Models;

namespace EmpMan.Web.Providers
{
    public class PermissionAttribute : AuthorizeAttribute
    {
        public string Function;
        public string Action;

        public PermissionAttribute(params string[] functions)
        {
            Function = string.Join(",", functions);
        }

        public override void OnAuthorization(HttpActionContext actionContext)
        {
            base.OnAuthorization(actionContext);
            var principal = actionContext.RequestContext.Principal as ClaimsPrincipal;

            if (!principal.Identity.IsAuthenticated)
            {
                actionContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
            }
            else
            {
                var roles = JsonConvert.DeserializeObject<List<string>>(principal.FindFirst("roles").Value);

                if (roles.Count > 0)
                {
                    if (!roles.Contains(RoleEnum.Admin.ToString()))
                    {
                        var permissions = JsonConvert.DeserializeObject<List<PermissionViewModel>>(principal.FindFirst("permissions").Value);
                        if (!permissions.Exists(x => Function.Contains(x.FunctionId) && x.CanCreate) && Action == ActionEnum.Create.ToString())
                        {
                            actionContext.Response = new HttpResponseMessage(HttpStatusCode.Forbidden);

                        }
                        else if (!permissions.Exists(x => Function.Contains(x.FunctionId) && x.CanRead) && Action == ActionEnum.Read.ToString())
                        {
                            actionContext.Response = new HttpResponseMessage(HttpStatusCode.Forbidden);

                        }
                        else if (!permissions.Exists(x => Function.Contains(x.FunctionId) && x.CanDelete) && Action == ActionEnum.Delete.ToString())
                        {
                            actionContext.Response = new HttpResponseMessage(HttpStatusCode.Forbidden);

                        }
                        else if (!permissions.Exists(x => Function.Contains(x.FunctionId) && x.CanUpdate) && Action == ActionEnum.Update.ToString())
                        {
                            actionContext.Response = new HttpResponseMessage(HttpStatusCode.Forbidden);
                        }
                    }
                }
                else
                {
                    actionContext.Response = new HttpResponseMessage(HttpStatusCode.Forbidden);
                }
            }
        }
    }
}
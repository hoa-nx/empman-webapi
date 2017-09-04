using EmpMan.Web.Infrastructure.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;

namespace EmpMan.Web.Infrastructure.CustomAttribute
{
    public sealed class JilSerializationAttribute : Attribute, IControllerConfiguration
    {
        public void Initialize(HttpControllerSettings controllerSettings, HttpControllerDescriptor controllerDescriptor)
        {
            // Register JilMediaTypeFormatter for controller
            controllerSettings.Formatters.Insert(0, new JilMediaTypeFormatter());
        }
    }
}
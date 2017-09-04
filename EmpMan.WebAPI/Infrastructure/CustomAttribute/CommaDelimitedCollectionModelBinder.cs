using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.ModelBinding;

namespace EmpMan.Web.Infrastructure.CustomAttribute
{
    public class CommaDelimitedCollectionModelBinder : IModelBinder
    {
        public bool BindModel(HttpActionContext actionContext, ModelBindingContext bindingContext)
        {
            var key = bindingContext.ModelName;
            var val = bindingContext.ValueProvider.GetValue(key);
            if (val != null && !string.IsNullOrEmpty(val.AttemptedValue))
            {
                var s = val.AttemptedValue;
                if (s != null && s.IndexOf(",", System.StringComparison.Ordinal) > 0)
                {
                    var stringArray = s.Split(new[] { "," }, StringSplitOptions.None);
                    bindingContext.Model = stringArray;
                }
                else
                {
                    bindingContext.Model = new[] { s };
                }
                return true;
            }
            return false;
        }
    }
}
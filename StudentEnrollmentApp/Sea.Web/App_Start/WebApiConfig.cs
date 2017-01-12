using Microsoft.Data.Edm;
using Sea.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.OData.Builder;
using System.Web.Http.OData.Extensions;

namespace Sea.Web
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            //OData end points
            config.Routes.MapODataServiceRoute("configOData", "OData", GenerateEdmModel());

            //To produce JSON format add this line of code
            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));
        }


        private static IEdmModel GenerateEdmModel()
        {
            var builder = new ODataConventionModelBuilder();
            builder.EntitySet<ClassModel>("ClassApi");
            builder.EntitySet<StudentModel>("Students");
            return builder.GetEdmModel();

        }
    }
}

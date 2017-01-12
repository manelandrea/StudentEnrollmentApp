using AutoMapper;
using Sea.Core;
using Sea.Service;
using Sea.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.OData;
using System.Web.Mvc;

namespace Sea.Web.Controllers
{
    public class ClassApiController : EntitySetController<ClassModel, int>
    {

        private IClassService classService;


        public ClassApiController(IClassService classService)
        {
            this.classService = classService;
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<ClassDetail, ClassModel>();
                cfg.CreateMap<ClassModel, ClassDetail>();
                cfg.CreateMap<StudentDetail, StudentModel>();
                cfg.CreateMap<StudentModel, StudentDetail>();
            });
        }


        // GET: OData/ClassApi
        [EnableQuery(PageSize = 10)]
        public override IQueryable<ClassModel> Get()
        {
            var details = Mapper.Map<IList<ClassDetail>, IList<ClassModel>>(classService.GetClasses().ToList());
            return details.ToList().AsQueryable();
        }
    }
}
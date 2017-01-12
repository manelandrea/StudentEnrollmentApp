using Sea.Core;
using Sea.Service;
using Sea.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sea.Web.Controllers
{
    public class ClassDetailController : Controller
    {

        private IClassService classService;


        public ClassDetailController(IClassService classService)
        {
            this.classService = classService;
        }

        // GET: ClassDetail
        public ActionResult Index()
        {
            IEnumerable<ClassModel> classes = classService.GetClasses().Select(u => new ClassModel
            {
                ID = u.ID,
                Name = u.Name,
                Location=u.Location,
                Teacher=u.Teacher
            });

           
            return View("Index",classes);
        }

        public ActionResult Create()
        {
            return View("Create");
        }

        //
        // POST: /ClassDetail/Create
        [HttpPost]
        public ActionResult Create([Bind(Exclude = "Id")] ClassModel classModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ClassDetail classDetail = new ClassDetail();
                    classDetail.Name = classModel.Name;
                    classDetail.Location = classModel.Location;
                    classDetail.Teacher = classModel.Teacher;
                    classService.InsertClass(classDetail);
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex);
                ViewData["CreateError"] = "Unable to create; view innerexception";
            }

            return View("Create");
        }

        // GET: /ClassDetail/Edit/1
        public ActionResult Edit(int id = 0)
        {
            var classToEdit = classService.GetClass(id);
            ClassModel classModel = new ClassModel();
            classModel.ID = classToEdit.ID;
            classModel.Location = classToEdit.Location;
            classModel.Name = classToEdit.Name;
            classModel.Teacher = classToEdit.Teacher;
            return View(classModel);
        }

        //
        // GET: /ClassDetail/Edit/1
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            ClassDetail cnt = classService.GetClass(id);
            try
            {
                if (TryUpdateModel(cnt))
                {
                    classService.UpdateClass(cnt);

                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                    ViewData["EditError"] = ex.InnerException.ToString();
                else
                    ViewData["EditError"] = ex.ToString();
            }
#if DEBUG
            foreach (var modelState in ModelState.Values)
            {
                foreach (var error in modelState.Errors)
                {
                    if (error.Exception != null)
                    {
                        throw modelState.Errors[0].Exception;
                    }
                }
            }
#endif
            return View(cnt);
        }

    }
}
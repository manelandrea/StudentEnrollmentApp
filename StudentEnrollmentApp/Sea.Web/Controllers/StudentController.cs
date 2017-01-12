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
    public class StudentController : Controller
    {

        private IStudentService studentService;
        private IClassService classService;

        public StudentController(IStudentService studentService, IClassService classService)
        {
            this.studentService = studentService;
            this.classService = classService;
        }

        // GET: Student
        public ActionResult Index()
        {
            IEnumerable<StudentModel> students = studentService.GetStudents().Select(u => new StudentModel
            {
                ID = u.ID,
                Name = u.Name,
                Surname = u.Surname,
                DOB = u.DOB,
                GPA = u.GPA,
                ClassName = u.ClassDetail.Name
            });

            return View("Index", students);

        }

        public ActionResult Create()
        {
            ViewBag.ClassList = new SelectList(classService.GetClasses(), "ID", "Name");
            return View("Create");
        }

        //
        // POST: /Student/Create
        [HttpPost]
        public ActionResult Create([Bind(Exclude = "Id")] StudentModel studentModel, string ddlClass)
        {
            try
            {
                ViewBag.ClassList = new SelectList(classService.GetClasses(), "ID", "Name");
                if (!CheckIfSurnameExists(studentModel.Surname, Convert.ToInt32(ddlClass)))
                {
                    if (ModelState.IsValid)
                    {

                        StudentDetail studentDetail = new StudentDetail();
                        studentDetail.Name = studentModel.Name;
                        studentDetail.Surname = studentModel.Surname;
                        studentDetail.DOB = studentModel.DOB;
                        studentDetail.GPA = studentModel.GPA;
                        studentDetail.ClassId = Convert.ToInt32(ddlClass);
                        studentService.InsertStudent(studentDetail);
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    ModelState.AddModelError("Surname", " Surname already exists in this class!");

                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex);
                ViewData["CreateError"] = "Unable to create; view innerexception";
            }

            return View("Create");
        }

        private bool CheckIfSurnameExists(string surname, int? classId)
        {
            var students = studentService.GetStudents();
            var lastNameExist = students.FirstOrDefault(x => x.Surname == surname && x.ClassId == classId);

            return !(lastNameExist == null);
        }

        // GET: /Student/Edit/1
        public ActionResult Edit(int id = 0)
        {
            ViewBag.ClassList = new SelectList(classService.GetClasses(), "ID", "Name");
            var studentToEdit = studentService.GetStudent(id);
            StudentModel studentModel = new StudentModel();
            studentModel.ID = studentToEdit.ID;
            studentModel.Name = studentToEdit.Name;
            studentModel.Surname = studentToEdit.Surname;
            studentModel.DOB = studentToEdit.DOB;
            studentModel.GPA = studentToEdit.GPA;
            return View(studentModel);
        }

        //
        // GET: /Student/Edit/1
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection, string ddlClass)
        {
            ViewBag.ClassList = new SelectList(classService.GetClasses(), "ID", "Name");
            StudentDetail cnt = studentService.GetStudent(id);
            try
            {
                if (TryUpdateModel(cnt))
                {
                    cnt.ClassId = Convert.ToInt32(ddlClass);
                    if (!CheckIfSurnameExists(cnt.Surname, Convert.ToInt32(ddlClass)))
                    {
                        studentService.UpdateStudent(cnt);
                    }
                    else
                    {
                        ModelState.AddModelError("Surname", " Surname already exists in this class!");
                    }

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
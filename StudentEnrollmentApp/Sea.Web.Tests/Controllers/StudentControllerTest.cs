using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sea.Web.Tests.Models;
using System.Web.Mvc;
using Sea.Web.Controllers;
using Sea.Service;
using System.Web.Routing;
using Sea.Core;
using Sea.Web.Models;
using System.Linq;
using System.Collections.Generic;

namespace Sea.Web.Tests.Controllers
{
    [TestClass]
    public class StudentControllerTest
    {
        /// <summary>
        /// Test Method for Index View
        /// </summary>
        /// <param name="repository"></param>
        /// <returns></returns>
        [TestMethod]
        public void IndexView()
        {
            var studentontroller = GetStudentController(new InMemoryStudentRepository(), new InMemoryClassRepository());
            ActionResult result = studentontroller.Index();
            Assert.AreEqual("Index", ((ViewResultBase)result).ViewName);
        }

        /// <summary>
        /// This method used to get student controller
        /// </summary>
        /// <param name="service"></param>
        /// <returns></returns>
        private static StudentController GetStudentController(IStudentService studentService, IClassService classService)
        {
            StudentController studentController = new StudentController(studentService, classService);

            studentController.ControllerContext = new ControllerContext()
            {
                RequestContext = new RequestContext(new MockHttpContext(), new RouteData())
            };

            return studentController;

        }



        /// This method used to get class model
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dob"></param>
        /// <param name="gpa"></param>
        /// <param name="classid"></param>
        /// <returns></returns>
        StudentDetail GetStudentName(int id, string name,string surname, DateTime? dob, double? gpa, int? classId)
        {
            return new StudentDetail
            {
                ID = id,
                Name = name,
                Surname=surname,
                DOB = dob,
                GPA = gpa,
                ClassId = classId
            };
        }

        /// This method used to get class model
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dob"></param>
        /// <param name="gpa"></param>
        /// <param name="classid"></param>
        /// <returns></returns>
        StudentModel GetStudentModel(int id, string name, string surname, DateTime? dob, double? gpa, int? classId)
        {
            return new StudentModel
            {
                ID = id,
                Name = name,
                Surname = surname,
                DOB = dob,
                GPA = gpa,
                ClassId = classId
            };
        }

        /// <summary>
        /// This test method used to post student
        /// </summary>
        [TestMethod]
        public void Create_PostEmployeeInRepository()
        {
            InMemoryStudentRepository studentrepository = new InMemoryStudentRepository();
            InMemoryClassRepository classrepository = new InMemoryClassRepository();
            var controller = GetStudentController(studentrepository, classrepository);

            StudentDetail student1 = GetStudentName(1, "Manel", "Cementina 1", DateTime.Today, 2, 1);
            StudentModel studentmodel1 = GetStudentModel(student1.ID, student1.Name, student1.Surname, student1.DOB, student1.GPA, student1.ClassId);
            controller.Create(studentmodel1,"1");
            IEnumerable<StudentDetail> students = studentrepository.GetStudents();
            Assert.IsFalse(students.ToList().Contains(student1));
        }
    }
}

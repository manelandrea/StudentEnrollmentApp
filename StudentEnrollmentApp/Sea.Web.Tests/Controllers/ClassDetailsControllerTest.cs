using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sea.Web.Controllers;
using Sea.Service;
using System.Web.Routing;
using Sea.Web.Tests.Models;
using System.Web.Mvc;
using Sea.Core;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using Sea.Web.Models;

namespace Sea.Web.Tests.Controllers
{
    [TestClass]
    public class ClassDetailsControllerTest
    {
        /// <summary>
        /// Test Method for Index View
        /// </summary>
        /// <param name="repository"></param>
        /// <returns></returns>
        [TestMethod]
        public void IndexView()
        {
            var classController = GetClassController(new InMemoryClassRepository());
            ActionResult result = classController.Index();
            Assert.AreEqual("Index", ((ViewResultBase)result).ViewName);
        }

        /// <summary>
        /// This method used to get class controller
        /// </summary>
        /// <param name="service"></param>
        /// <returns></returns>
        private static ClassDetailController GetClassController(IClassService classService)
        {
            ClassDetailController classController = new ClassDetailController(classService);

            classController.ControllerContext = new ControllerContext()
            {
                RequestContext = new RequestContext(new MockHttpContext(), new RouteData())
            };

            return classController;

        }

        /// <summary>
        ///  This method used to get all classes listing
        /// </summary>
        [TestMethod]
        public void GetAllClassesFromRepository()
        {
            // Arrange
            ClassDetail class1 = GetClassName(1, "Rizal", "SV", "Mr Jones");
            ClassModel classmodel1 = GetClassModel(class1.ID, class1.Name, class1.Location, class1.Teacher);
            InMemoryClassRepository classrepository = new InMemoryClassRepository();
            classrepository.InsertClass(class1);
            var controller = GetClassController(classrepository);
            var result = controller.Index();

            var datamodel = ((ViewResultBase)result).ViewData.Model;
            var datacollection = ((EnumerableQuery<ClassModel>)datamodel).ToList();
            CollectionAssert.DoesNotContain(datacollection, classmodel1);
        }

        /// <summary>
        /// This method used to get class name
        /// </summary>
        /// <param name="id"></param>
        /// <param name="lName"></param>
        /// <param name="location"></param>
        /// <returns></returns>
        ClassDetail GetClassName(int id, string name, string location, string teacher)
        {
            return new ClassDetail
            {
                ID = id,
                Name = name,
                Location = location,
                Teacher = teacher
            };
        }

        /// This method used to get class model
        /// </summary>
        /// <param name="id"></param>
        /// <param name="lName"></param>
        /// <param name="location"></param>
        /// <returns></returns>
        ClassModel GetClassModel(int id, string name, string location, string teacher)
        {
            return new ClassModel
            {
                ID = id,
                Name = name,
                Location = location,
                Teacher = teacher
            };
        }

        /// <summary>
        /// This test method used to post class
        /// </summary>
        [TestMethod]
        public void Create_PostClassInRepository()
        {
            InMemoryClassRepository classrepository = new InMemoryClassRepository();
            var controller = GetClassController(classrepository);
            ClassDetail class1 = GetClassName(1, "Rizal", "SV", "Mr Jones");
            ClassModel classmodel1 = GetClassModel(class1.ID, class1.Name, class1.Location, class1.Teacher);
            controller.Create(classmodel1);
            IEnumerable<ClassDetail> classes = classrepository.GetClasses();
            Assert.IsFalse(classes.ToList().Contains(class1));
        }
    }
}



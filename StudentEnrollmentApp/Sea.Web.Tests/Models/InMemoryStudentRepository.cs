using Sea.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sea.Core;

namespace Sea.Web.Tests.Models
{
    class InMemoryStudentRepository : IStudentService
    {
        private List<StudentDetail> _db = new List<StudentDetail>();

        public Exception ExceptionToThrow { get; set; }

        public StudentDetail GetStudent(long id)
        {
            return _db.FirstOrDefault(d => d.ID == id);
        }

        public IQueryable<StudentDetail> GetStudents()
        {
            return _db.AsQueryable();
        }

        public void InsertStudent(StudentDetail student)
        {
            if (ExceptionToThrow != null)
                throw ExceptionToThrow;

            _db.Add(student);
        }

        public void UpdateStudent(StudentDetail student)
        {
            foreach (StudentDetail detail in _db)
            {
                if (detail.ID == student.ID)
                {
                    _db.Remove(detail);
                    _db.Add(student);
                    break;
                }
            }
        }
    }
}

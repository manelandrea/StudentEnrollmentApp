using Sea.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sea.Service
{
    public interface IStudentService
    {
        IQueryable<StudentDetail> GetStudents();
        StudentDetail GetStudent(long id);
        void InsertStudent(StudentDetail student);
        void UpdateStudent(StudentDetail student);
    }
}

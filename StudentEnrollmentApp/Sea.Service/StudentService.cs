using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sea.Core;
using Sea.Data;

namespace Sea.Service
{
    public class StudentService : IStudentService
    {
        private IRepository<StudentDetail> studentRepository;

        public StudentService(IRepository<StudentDetail> studentRepository)
        {
            this.studentRepository = studentRepository;
        }

        public StudentDetail GetStudent(long id)
        {
            return studentRepository.GetById(id);
        }

        public IQueryable<StudentDetail> GetStudents()
        {
            return studentRepository.Table;
        }

        public void InsertStudent(StudentDetail student)
        {
            studentRepository.Insert(student);
        }

        public void UpdateStudent(StudentDetail student)
        {
            studentRepository.Update(student);
        }

    }
}

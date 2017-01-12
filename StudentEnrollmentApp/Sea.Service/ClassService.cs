using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sea.Core;
using Sea.Data;

namespace Sea.Service
{
    public class ClassService : IClassService
    {

        private IRepository<ClassDetail> classRepository;

        public ClassService(IRepository<ClassDetail> classRepository)
        {
            this.classRepository = classRepository;
        }

        public ClassDetail GetClass(int? id)
        {
            return classRepository.GetById(id);
        }

        public IQueryable<ClassDetail> GetClasses()
        {
            return classRepository.Table;
        }

        public void InsertClass(ClassDetail classDetail)
        {
            classRepository.Insert(classDetail);
        }

        public void UpdateClass(ClassDetail classDetail)
        {
            classRepository.Update(classDetail);
        }
    }
}

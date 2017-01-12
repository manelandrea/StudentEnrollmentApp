using Sea.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sea.Core;

namespace Sea.Web.Tests.Models
{
    class InMemoryClassRepository : IClassService
    {
        private List<ClassDetail> _db = new List<ClassDetail>();

        public Exception ExceptionToThrow { get; set; }

        public ClassDetail GetClass(int? id)
        {
            return _db.FirstOrDefault(d => d.ID == id);
        }

        public IQueryable<ClassDetail> GetClasses()
        {
            return _db.AsQueryable();
        }

        public void InsertClass(ClassDetail classDetail)
        {
            if (ExceptionToThrow != null)
                throw ExceptionToThrow;

            _db.Add(classDetail);
        }

        public void UpdateClass(ClassDetail classDetail)
        {
            foreach (ClassDetail detail in _db)
            {
                if (detail.ID == classDetail.ID)
                {
                    _db.Remove(detail);
                    _db.Add(classDetail);
                    break;
                }
            }
        }
    }
}

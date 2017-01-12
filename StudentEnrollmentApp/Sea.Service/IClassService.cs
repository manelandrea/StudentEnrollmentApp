using Sea.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sea.Service
{
   public interface IClassService
    {
        IQueryable<ClassDetail> GetClasses();
        ClassDetail GetClass(int? id);
        void InsertClass(ClassDetail classDetail);
        void UpdateClass(ClassDetail classDetail);
       
    }
}

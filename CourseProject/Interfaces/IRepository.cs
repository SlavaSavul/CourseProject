using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseProject.Interfaces
{
    public interface IRepository<T>
    {
        T Get(string id);
        void Create(T t);
        void Delete(T t);
        void Update(T t);

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseProject.Interfaces
{
    public interface IRepository<T>
    {
        T Get(Guid id);
        void Create(T t);
        void Delete(Guid id);
        void Update(T t);

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CourseProject.Interfaces
{
    public interface IRepository<T>
    {
        T Get(Guid id);
        IEnumerable<T> GetAll();
        void Create(T t);
        void Delete(Guid id);
        void Update(T t);
        IEnumerable<T> Find(Expression<Func<T, bool>> expression);

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Issuing.Persistence
{
    public interface ICrudRepository<T>
    {
        void Insert(T entitiy);
        void Delete(T entitiy);
        void Update(T entitiy);
        T GetById(int i);

        IList<T> GetAll();

    }
}

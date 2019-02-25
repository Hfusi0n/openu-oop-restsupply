using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestSupplyMVC.Repositories
{
    public interface IRepository<T>
    {
        IEnumerable<T> GetAll();
        T GetById<TKey>(TKey id);
        void Add(T item);
        void Remove(T item);
    }
}

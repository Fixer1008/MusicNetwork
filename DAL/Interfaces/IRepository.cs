using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IRepository<T>
    {
        List<T> All { get; } 
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
        void Delete(int id);
        T GetById(int id);
    }
}

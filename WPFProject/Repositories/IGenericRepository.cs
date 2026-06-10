using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace WPFProject.Repositories
{
    public interface IGenericRepository
    {
        public interface IGenericRepository<T> { List<T> GetAll(); void Add(T entity); void Update(T entity); void Delete(T entity); }
    }
}

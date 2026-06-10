using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static WPFProject.Repositories.IGenericRepository;


namespace WPFProject.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : new() { private readonly SQLiteConnection connection; public GenericRepository(SQLiteConnection connection) { this.connection = connection; } public List<T> GetAll() { return connection.Table<T>().ToList(); } public void Add(T entity) { connection.Insert(entity); } public void Update(T entity) { connection.Update(entity); } public void Delete(T entity) { connection.Delete(entity); } }
}

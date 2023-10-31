using DbContextClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionRepository
{
    public class TeacherRepository : IRepository<Teacher>
    {
        private readonly DataBaseContextClass _context;
        public TeacherRepository(DataBaseContextClass context)
        {
            _context = context;
        }

        public void Add(Teacher entity)
        {
            throw new NotImplementedException();
        }

        public void AddRange(IEnumerable<Teacher> entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Teacher entity)
        {
            throw new NotImplementedException();
        }

        public List<Teacher> GetAll()
        {
            throw new NotImplementedException();
        }

        public Teacher GetId(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public void Update(Teacher entity)
        {
            throw new NotImplementedException();
        }
    }
}

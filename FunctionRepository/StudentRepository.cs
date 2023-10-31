using DbContextClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionRepository
{
    public class StudentRepository : IRepository<Student>
    {
        private readonly DataBaseContextClass _context;
        public StudentRepository(DataBaseContextClass context)
        {
            _context = context;
        }

        public void Add(Student entity)
        {
            throw new NotImplementedException();
        }

        public void AddRange(IEnumerable<Student> entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Student entity)
        {
            throw new NotImplementedException();
        }

        public List<Student> GetAll()
        {
            throw new NotImplementedException();
        }

        public Student GetId(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public void Update(Student entity)
        {
            throw new NotImplementedException();
        }
    }
}

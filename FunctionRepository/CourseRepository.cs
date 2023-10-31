using DbContextClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionRepository
{
    public class CourseRepository : IRepository<Course>
    {
        private readonly DataBaseContextClass _context;
        public CourseRepository(DataBaseContextClass context)
        {
            _context = context;
        }
        public void Add(Course entity)
        {
            throw new NotImplementedException();
        }

        public void AddRange(IEnumerable<Course> entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Course entity)
        {
            throw new NotImplementedException();
        }

        public List<Course> GetAll()
        {
            throw new NotImplementedException();
        }

        public Course GetId(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public void Update(Course entity)
        {
            throw new NotImplementedException();
        }
    }
}

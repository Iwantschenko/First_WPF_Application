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
        
        public void Add(Course entity) => _context.Courses.Add(entity);
        public void AddRange(IEnumerable<Course> entity) => _context.Courses.AddRange(entity);
        public void Delete(Course entity)
        {
            throw new NotImplementedException();
        }
        public void Update(Course entity)
        {
            throw new NotImplementedException();
        }

        public List<Course> GetAll() => _context.Courses.ToList();

        public Course GetId(Guid id) => _context.Courses.FirstOrDefault(x => x.Course_ID == id);

        public void Save() => _context.SaveChanges();   

    }
}

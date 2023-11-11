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
            if (_context.Courses.Contains(entity))
            {
                List<GroupStudent> courseGroups = _context.Groups
                    .Where(x => x.CourseId == entity.Course_ID)
                    .ToList();
                GroupRepository groupRepository = new GroupRepository(_context);
                foreach (var group in courseGroups)
                {
                    groupRepository.Delete(group);
                }
                _context.Courses.Remove(entity);
            }
            else
                return;
        }
        public void Update(Course entity) => _context.Courses.Update(entity);

        public List<Course> GetAll() => _context.Courses.ToList();

        public Course GetId(Guid id) => _context.Courses.FirstOrDefault(x => x.Course_ID == id);

        public void Save() => _context.SaveChanges();   

    }
}

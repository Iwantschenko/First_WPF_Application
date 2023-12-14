using DbContextClasses;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionRepository
{
    public class CourseRepository : IRepository<Course>
    {
        private readonly DataBaseContext _context;
        public CourseRepository(DataBaseContext context) 
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
                _context.Courses.Remove(_context.Courses.FirstOrDefault(x=> x.Course_ID == entity.Course_ID));
            }
        }
        public void Update(Course entity) 
        {
            var course = GetId(entity.Course_ID);
            if (course != null)
            {
                course.Course_Name = entity.Course_Name;
                course.Course_Description = entity.Course_Description;
            }
        }

        public List<Course> GetAll() => _context.Courses.ToList();

        public Course GetId(Guid id) => _context.Courses.FirstOrDefault(x => x.Course_ID == id);

        public async void Save() => await _context.SaveChangesAsync();   

    }
}

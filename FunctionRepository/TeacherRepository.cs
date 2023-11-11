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

        public void Add(Teacher entity) => _context.Teachers.Add(entity);

        public void AddRange(IEnumerable<Teacher> entity) => _context.Teachers.AddRange(entity);

        public void Delete(Teacher entity)
        {
            if (_context.Teachers.Contains(entity))
            {
                List<GroupStudent> teacherGroups = _context.Groups
                    .Where(x=> x.TeacherId == entity.Teacher_Id)
                    .ToList();
                GroupRepository groupRepository = new GroupRepository(_context);
                foreach (var group in teacherGroups)
                {
                    groupRepository.Delete(group);
                }
                _context.Teachers.Remove(entity);
            }
            else 
                return;
        }
        public void Update(Teacher entity)  => _context.Teachers.Update(entity);
        public List<Teacher> GetAll() => _context.Teachers.ToList();

        public Teacher GetId(Guid id) => _context.Teachers.FirstOrDefault(x => x.Teacher_Id == id);

        public void Save() => _context.SaveChanges();

        
    }
}

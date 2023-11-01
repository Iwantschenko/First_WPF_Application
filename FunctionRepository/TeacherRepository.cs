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
            throw new NotImplementedException();
        }
        public void Update(Teacher entity)
        {
            throw new NotImplementedException();
        }
        public List<Teacher> GetAll() => _context.Teachers.ToList();

        public Teacher GetId(Guid id) => _context.Teachers.FirstOrDefault(x => x.Teacher_Id == id);

        public void Save() => _context.SaveChanges();

        
    }
}

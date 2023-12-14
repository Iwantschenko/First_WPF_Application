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
        private readonly DataBaseContext _context;
        public StudentRepository(DataBaseContext context)
        {
            _context = context;
        }

        public void Add(Student entity) => _context.Students.Add(entity);      
        public void AddRange(IEnumerable<Student> entity) => _context.Students.AddRange(entity);
        public void Delete(Student entity) => _context.Students.Remove(entity);
        public void Update(Student entity) => _context.Students.Update(entity);
        public List<Student> GetAll() => _context.Students.ToList();
        public Student GetId(Guid id) => _context.Students.FirstOrDefault(x=> x.Student_Id == id);
        public void Save() => _context.SaveChanges();

    }
}

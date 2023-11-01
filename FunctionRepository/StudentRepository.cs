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

        public void Add(Student entity) => _context.Students.Add(entity);

        public void AddRange(IEnumerable<Student> entity) => _context.Students.AddRange(entity);

        public void Delete(Student entity)
        {
            throw new NotImplementedException();
        }
        public void Update(Student entity)
        {
            throw new NotImplementedException();
        }
        public List<Student> GetAll() => _context.Students.ToList();

        public Student GetId(Guid id) => _context.Students.FirstOrDefault(x=> x.Student_Id == id);

        public void Save() => _context.SaveChanges();

       
    }
}

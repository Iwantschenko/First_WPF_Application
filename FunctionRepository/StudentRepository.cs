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
        public void Delete(Student entity) 
        {
            if (_context.Students.Contains(entity))
            {
                _context.Students.Remove(GetId(entity.Student_Id));
            }
        }
        public void Update(Student entity) 
        {
            Student student = GetId(entity.Student_Id);
            if (student != null) 
            {
                student.First_Name = entity.First_Name;
                student.Last_Name = entity.Last_Name;
            }
        }
        public List<Student> GetAll() => _context.Students.ToList();
        public Student GetId(Guid id) => _context.Students.FirstOrDefault(x=> x.Student_Id == id);
        public void Save() => _context.SaveChanges();

    }
}

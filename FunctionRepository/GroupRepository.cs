using DbContextClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace FunctionRepository
{
    public class GroupRepository : IRepository<GroupStudent>
    {
        private readonly DataBaseContext _context;
        public GroupRepository(DataBaseContext context)
        {
            this._context = context;
        }

        public void Add(GroupStudent entity) => _context.Groups.Add(entity);

        public void AddRange(IEnumerable<GroupStudent> entity) => _context.Groups.AddRange(entity);

        public void Delete(GroupStudent entity)
        {
            if (_context.Groups.Contains(entity))
            {
                _context.Students.RemoveRange(_context.Students
                    .Where(x=> x.GroupId == entity.Group_Id)
                    .ToList());
                _context.Groups.Remove(entity);
            }
            else
                return;

        }
        public void Update(GroupStudent entity) => _context.Groups.Update(entity);
        public List<GroupStudent> GetAll() => _context.Groups.ToList();

        public GroupStudent GetId(Guid id) => _context.Groups.FirstOrDefault(x => x.Group_Id == id);

        public void Save() => _context.SaveChanges();

        
    }
}

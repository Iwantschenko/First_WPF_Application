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
        private readonly DataBaseContextClass _context;
        public GroupRepository(DataBaseContextClass context)
        {
            this._context = context;
        }

        public void Add(GroupStudent entity)
        {
            throw new NotImplementedException();
        }

        public void AddRange(IEnumerable<GroupStudent> entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(GroupStudent entity)
        {
            throw new NotImplementedException();
        }

        public List<GroupStudent> GetAll()
        {
            throw new NotImplementedException();
        }

        public GroupStudent GetId(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public void Update(GroupStudent entity)
        {
            throw new NotImplementedException();
        }
    }
}

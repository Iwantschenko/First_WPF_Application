using FunctionRepository;

namespace Services
{
    public class ServiceDb<T>
    {
        private readonly IRepository<T> _repository;
        public ServiceDb(IRepository<T> repository)
        {
            _repository = repository;
        }
        public void Add(T entity) => _repository.Add(entity);
        public void AddRange(IEnumerable<T> entities) => _repository.AddRange(entities);

        public void Remove(T entity) => _repository.Delete(entity);
        public void Update(T entity) => _repository.Update(entity);
        public List<T> GetAll() => _repository.GetAll();
        public T GetId(Guid id) => _repository.GetId(id);
        public void Save() => _repository.Save();
        
        
    }
}
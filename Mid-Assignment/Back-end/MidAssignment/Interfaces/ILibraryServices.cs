namespace MidAssignment.Interfaces
{
    public interface ILibraryServices<T>
    {
        List<T> GetAll();
        public T GetById(int id);

        public void Add(T item);

        public void Update(T item);

        public void Remove(T item);

        public bool IsValidForeignKey(int? id);

        public void Transaction(Action<T> action, T item);

    }
}
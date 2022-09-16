namespace library.parameters.mappers
{
    public class EmptyMapper<T> : IMapable<T, T>
    {
        public T Map(T entity)
        {
            return entity;
        }
    }
}

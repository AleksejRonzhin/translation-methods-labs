namespace library.parameters.mappers
{
    public interface IMapable<T, V>
    {
        V Map(T entity);
    }
}
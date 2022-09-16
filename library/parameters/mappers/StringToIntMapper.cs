using library.exceptions;

namespace library.parameters.mappers
{
    public class StringToIntMapper : IMapable<string, int>
    {
        public int Map(string entity)
        {
            try
            {
                return int.Parse(entity);
            }
            catch
            {
                throw new ValidationException($"{entity} не является целым числом.");
            }
        }
    }
}

using library.parameters.mappers;
using library.parameters.validators;

namespace library.parameters
{
    public abstract class Parameter<V>
    {
        private readonly string _arg;
        private readonly IMapable<string, V> _mapper;
        private readonly IValidateble<V> _validator;

        protected Parameter(string arg, IMapable<string, V> mapper, IValidateble<V> validator)
        {
            this._arg = arg;
            this._mapper = mapper;
            this._validator = validator;
        }

        virtual public V GetValue()
        {
            var value = _mapper.Map(_arg);
            _validator.Validate(value);
            return value;
        }
    }
}
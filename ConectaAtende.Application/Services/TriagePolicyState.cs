using ConectaAtende.Domain.Enum;

namespace ConectaAtende.Application.Services;

public sealed class TriagePolicyState
{
    private readonly object _lock = new();
    private TriagePolicyType _current = TriagePolicyType.Ordem;

    public TriagePolicyType Current
    {
        get
        {
            lock (_lock)
            {
                return _current;
            }
        }
        
        set
        {
            lock (_lock)
            {
                _current = value;
            }
        }
    }
}

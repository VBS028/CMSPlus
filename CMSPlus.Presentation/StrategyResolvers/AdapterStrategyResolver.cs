using CMSPlus.Domain.Interfaces;
using CMSPlus.Infrastructure.Enums;

namespace CMSPlus.Presentation.StrategyResolvers;

public class AdapterStrategyResolver:IStrategyResolver<IAdapter>
{
    private readonly IEnumerable<IAdapter> _adapters;

    public AdapterStrategyResolver(IEnumerable<IAdapter> adapters)
    {
        _adapters = adapters;
    }

    public IAdapter GetStrategy(EntityTypes type)
    {
        return _adapters.FirstOrDefault(x => x.Type == type);
    }
}
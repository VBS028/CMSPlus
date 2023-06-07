using CMSPlus.Infrastructure.Enums;

namespace CMSPlus.Domain.Interfaces;

public interface IStrategyResolver<T>
{
    T GetStrategy(EntityTypes type);
}
using CMSPlus.Infrastructure.Enums;

namespace CMSPlus.Domain.Interfaces;

public interface IAdapter
{
    public EntityTypes Type { get; }
    object GetData(object source);
}
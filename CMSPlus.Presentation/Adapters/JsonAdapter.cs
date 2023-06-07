using CMSPlus.Domain.Entities;
using CMSPlus.Domain.Interfaces;
using CMSPlus.Infrastructure.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace CMSPlus.Presentation.Adapters;

public class JsonAdapter : IAdapter
{
    public EntityTypes Type => EntityTypes.Json;

    public object GetData(object source)
    {
        if (!(source is BaseEntity baseEntity))
            throw new ArgumentException("Invalid source type. Expected BaseEntity.");

        var settings = new JsonSerializerSettings
        {
            ContractResolver = new DefaultContractResolver
            {
                NamingStrategy = new CamelCaseNamingStrategy()
            },
            Formatting = Formatting.Indented,
            NullValueHandling = NullValueHandling.Ignore
        };
        
        return JsonConvert.SerializeObject(baseEntity, settings);
    }
}
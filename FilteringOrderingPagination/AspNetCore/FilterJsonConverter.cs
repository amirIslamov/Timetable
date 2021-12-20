using System.Reflection;
using FilteringOrderingPagination.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace FilteringOrderingPagination.AspNetCore;

public class FilterJsonConverter: JsonConverter
{
    public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
    {
        throw new NotImplementedException();
    }

    public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
    {
        var jObject = JObject.Load(reader);

        var filter = Activator.CreateInstance(objectType);
        foreach (var patternProperty in EnumeratePatterns(objectType))
        {
            var pattern = Activator.CreateInstance(patternProperty.PropertyType);
            var patternName = patternProperty.Name;
            
            if (jObject[patternName] != null)
            {
                serializer.Populate(jObject[patternName].CreateReader(), pattern);
            }
            
            patternProperty.SetValue(existingValue, pattern);
        }

        return existingValue;
    }

    private IEnumerable<PropertyInfo> EnumeratePatterns(Type type)
    {
        foreach (var property in type.GetProperties())
        {
            if (HasGenericTypeBase(property.PropertyType, typeof(Pattern<>)))
            {
                yield return property;
            }
        }
    }

    public override bool CanConvert(Type objectType)
    {
        return HasGenericTypeBase(objectType, typeof(IFilter<>));
    }
    
    private bool HasGenericTypeBase(Type type, Type genericType)
    {
        while (type != typeof(object))
        {
            if (type.IsGenericType && type.GetGenericTypeDefinition() == genericType) return true;
            type = type.BaseType;
        }

        return false;
    }
}
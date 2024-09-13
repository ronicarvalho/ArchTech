using System.ComponentModel;
using System.Text;
using ArchTech.Custom.Interfaces;

namespace ArchTech.Custom.Extensions;

public static class Adapter
{
    public static T Adapt<T>(this IAdaptable adapter) where T : class, new()
    {
        var adaptee = new T();
        var destinyProps = typeof(T).GetProperties().ToDictionary(p => p.Name);
        var sourceProps = adapter.GetType().GetProperties().ToDictionary(p => p.Name);

        foreach (var (key, destinyProp) in destinyProps)
        {
            if (!sourceProps.TryGetValue(key, out var sourceProp)) 
                continue;
            
            var sourceValue = sourceProp.GetValue(adapter);
            var destinyValue = ChangeType(sourceValue, destinyProp.PropertyType);
            
            destinyProp.SetValue(adaptee, destinyValue);
        }

        return adaptee;
    }
    
    public static T Adapt<T>(this IAdaptable adapter, string name, string value) where T : class, new()
    {
        var adaptee = new T();
        var destinyProps = typeof(T).GetProperties().ToDictionary(p => p.Name);
        var sourceProps = adapter.GetType().GetProperties().ToDictionary(p => p.Name);

        foreach (var (key, destinyProp) in destinyProps)
        {
            if (!sourceProps.TryGetValue(key, out var sourceProp))
                if (key != name) continue;
            
            var sourceValue = (key == name) ? value : sourceProp?.GetValue(adapter);
            var destinyValue = ChangeType(sourceValue, destinyProp.PropertyType);
            
            destinyProp.SetValue(adaptee, destinyValue);
        }

        return adaptee;
    }

    private static object? ChangeType(object? value, Type type)
    {
        if (value == null) return null;

        var converter = TypeDescriptor.GetConverter(type);

        if (converter.CanConvertFrom(value.GetType()))
            return converter.ConvertFrom(value);

        if (type == typeof(byte[]) && value is string str)
            return Encoding.Unicode.GetBytes(str);

        return Convert.ChangeType(value, type);
    }
}

using System;
using System.Collections;
using System.Reflection;
using System.Text;
using Serializer.DataAccess.Repository.IRepository;

namespace Serializer.DataAccess.Repository;

public class JSONSerializer : ISerializer
{
    public string SerializeObject(object obj)
    {
        if (obj is null)
            return "null";
        
        Type type = obj.GetType();
        StringBuilder jsonString = new StringBuilder();
        jsonString.Append("{\n");
        
        PropertyInfo[] properties = type.GetProperties();
        for (int i = 0; i < properties.Length; i++)
        {
            PropertyInfo property = properties[i];
            object? value = property.GetValue(obj);
            string name = property.Name;

            jsonString.Append($"\"{name}\": {SerializeValues(value, null)}");
            
            if (i < properties.Length - 1)
                jsonString.Append(",\n");
        }

        jsonString.Append("\n}");
        return jsonString.ToString();
    }

    public string SerializeValues(object value, string? elementName)
    {
        if (value is null)
            return "null";
        
        Type t = value.GetType();

        if (t == typeof(string) || t == typeof(char) || t == typeof(DateTime))
            return $"\"{value}\"";
        
        if (t.IsPrimitive || value is decimal)
            return value.ToString();

        if (value is IEnumerable enumerable)
        {
            StringBuilder jsonString = new StringBuilder();
            jsonString.Append("[\n");

            IEnumerator enumerator = enumerable.GetEnumerator();
            bool first = true;

            while(enumerator.MoveNext())
            {
                if (!first)
                    jsonString.Append(",\n");

                jsonString.Append(SerializeValues(enumerator.Current, null));
                first = false;
            }
            
            jsonString.Append("\n]");
            return jsonString.ToString();
        }

        return SerializeObject(value);
    }
}

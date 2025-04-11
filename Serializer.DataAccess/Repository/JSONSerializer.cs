using System;
using System.Collections;
using System.Reflection;
using System.Text;
using Serializer.DataAccess.Repository.IRepository;

namespace Serializer.DataAccess.Repository;

public partial class JSONSerializer : ISerializer
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
}

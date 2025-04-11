using System;
using System.Collections;
using System.Reflection;
using System.Text;
using Serializer.DataAccess.Repository.IRepository;

namespace Serializer.DataAccess.Repository;

public partial class XMLSerializer : ISerializer
{
    public string SerializeObject(object obj)
    {
        Type t = obj.GetType();
            if (obj is null)
                return $"<{t.Name} />\n";

            StringBuilder xmlString = new StringBuilder();
            xmlString.Append($"<{t.Name}>\n");
            
            PropertyInfo[] properties = t.GetProperties();
            for (int i = 0; i < properties.Length; i++)
            {
                PropertyInfo property = properties[i];
                object? value = property.GetValue(obj);
                xmlString.Append($"{SerializeValues(value, property.Name)}");

                if (i < properties.Length-1)
                    xmlString.Append("\n");
            }

            xmlString.Append($"\n</{t.Name}>");
            return xmlString.ToString();
    }
}

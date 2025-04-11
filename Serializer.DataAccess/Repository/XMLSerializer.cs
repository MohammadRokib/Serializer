using System;
using System.Collections;
using System.Reflection;
using System.Text;
using Serializer.DataAccess.Repository.IRepository;

namespace Serializer.DataAccess.Repository;

public class XMLSerializer : ISerializer
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

    public string SerializeValues(object value, string? elementName)
    {
        if (value is null)
            return $"<{elementName} />\n";
        
        Type t = value.GetType();

        if (t == typeof(string) || t == typeof(char) || t == typeof(DateTime))
            return $"<{elementName}>\n{EscapeXml(value.ToString())}\n</{elementName}>";
        
        if (value is decimal || t.IsPrimitive)
            return $"<{elementName}>\n{value}\n</{elementName}>";
        
        if (value is IEnumerable enumearable)
        {
            StringBuilder xmlList = new StringBuilder();
            
            foreach (var item in enumearable)
                xmlList.Append(SerializeValues(item, elementName));
            
            return xmlList.ToString();
        }

        return SerializeObject(value);
    }
    private string EscapeXml(string? input)
    {
        if (input is null) return "";
        return input
            .Replace("&", "&amp;")
            .Replace("<", "&lt;")
            .Replace(">", "&gt;")
            .Replace("\"", "&quot;")
            .Replace("'", "&apos;");
    }
}

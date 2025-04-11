using System;
using System.Collections;
using System.Text;

namespace Serializer.DataAccess.Repository;

public partial class XMLSerializer
{
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

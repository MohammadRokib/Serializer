using System;
using System.Collections;
using System.Text;

namespace Serializer.DataAccess.Repository;

public partial class JSONSerializer
{
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

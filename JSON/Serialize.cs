using System.Collections;
using System.Reflection;
using System.Text;

namespace JSON;

public class Serialize
{
    public void Start(object obj)
    {
        try
        {
            string json = SerializeObject(obj);

            string folderPath = new DirectoryInfo(Directory.GetCurrentDirectory())
                .Parent.Parent.Parent.Parent.ToString();
            
            string filePath = Path.Combine(folderPath, "output.txt");
            File.WriteAllText(filePath, json);

            System.Console.WriteLine("File Write successfull ✅");
            System.Console.WriteLine(json);
        }
        catch (Exception ex)
        {
            System.Console.WriteLine(ex);
        }
    }
    private string SerializeObject(object? obj)
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

            jsonString.Append($"\"{name}\": {SerializeValue(value)}");
            
            if (i < properties.Length - 1)
                jsonString.Append(",\n");
        }

        jsonString.Append("\n}");
        return jsonString.ToString();
    }

    private string SerializeValue(object? value)
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

                jsonString.Append(SerializeValue(enumerator.Current));
                first = false;
            }
            
            jsonString.Append("\n]");
            return jsonString.ToString();
        }

        return SerializeObject(value);
    }
}

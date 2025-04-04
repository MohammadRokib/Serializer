using System.Reflection;
using JSON;
using XML;
using Newtonsoft.Json;

namespace SerializerAssignment;

class Program
{
    static void Main(string[] args)
    {
        DirectoryInfo directory = new DirectoryInfo(Directory.GetCurrentDirectory())
            .Parent.Parent.Parent;
        
        string config = File.ReadAllText(directory.GetFiles()
            .Where(x => x.Name.Contains("config"))
            .First()
            .FullName);
        
        dynamic configJson = JsonConvert.DeserializeObject(config);

        try
        {
            string typeName = configJson!.Serializer.ToString();
            Assembly assembly;

            if (typeName == "JSON")
            {
                typeName += ".Serialize";
                assembly = typeof(JSON.Serialize).Assembly;
            }
            else
            {
                typeName += ".Serialize";
                assembly = typeof(XML.Serialize).Assembly;
            }

            Type t = assembly.GetType(typeName);

            if (t == null)
            {
                System.Console.WriteLine($"Type: {typeName} couldn't be found");
                return;
            }
            
            ConstructorInfo constructor = t.GetConstructor(new Type[] {});
            object o = constructor.Invoke(new object[] {});

            MethodInfo method = t.GetMethod("Start", new Type[] {});
            method.Invoke(o, new object[] {});
        }
        catch (Exception ex)
        {
            System.Console.WriteLine(ex);
        }
    }
}

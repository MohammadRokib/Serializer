using System.Reflection;
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
            Type t = Assembly
                .GetExecutingAssembly()
                .GetTypes()
                .Where(x => x.Name == configJson.Serializer.ToString())
                .First();
            
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

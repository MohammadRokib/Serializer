using System.Reflection;
using JSON;
using XML;
using Newtonsoft.Json;
using ObjectRef;

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
            
            AdmissionTest test = new AdmissionTest
            {
                StartDateTime = DateTime.Now,
                EndDateTime = DateTime.Now,
                TestFees = 1000.00,
                Names = new List<string>
                {
                    "Mohammad",
                    "Rokib",
                    "Khan"
                }
            };
            
            Topic topicObj = new Topic
            {
                Title = "Reflection",
                Sessions = new List<Session>
                {
                    new Session
                    {
                        DurationInHour = 3,
                        LearningObjective = "NULL"
                    },
                    new Session
                    {
                        DurationInHour = 3,
                        LearningObjective = "NULL"
                    },
                    new Session
                    {
                        DurationInHour = 3,
                        LearningObjective = "NULL"
                    }
                },
                Description = "Practicing Reflection",
            };

            ConstructorInfo constructor = t.GetConstructor(new Type[] {});
            object o = constructor.Invoke(new object[] {});

            MethodInfo method = t.GetMethod("Start", new Type[] { typeof(object) });
            method.Invoke(o, new object[] { topicObj });
        }
        catch (Exception ex)
        {
            System.Console.WriteLine(ex);
        }
    }
}

using System.Reflection;
using Newtonsoft.Json;
using ObjectRef;
using Serializer.DataAccess.Repository.IRepository;
using Serializer.DataAccess.Repository;

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
            IUnitOfWork unitOfWork;

            if (typeName == "JSON")
                unitOfWork = new UnitOfWork(new JSONSerializer());
            else
                unitOfWork = new UnitOfWork(new XMLSerializer());

            ObjGenerator objGenerator = new ObjGenerator();
            object obj = objGenerator.Instructor();
            unitOfWork.Start(obj);
        }
        catch (Exception ex)
        {
            System.Console.WriteLine(ex);
        }
    }
}

using System;
using Serializer.DataAccess.Repository.IRepository;

namespace Serializer.DataAccess.Repository;

public class UnitOfWork : IUnitOfWork
{
    private readonly ISerializer _serializer;
    public UnitOfWork(ISerializer serializer)
    {
        _serializer = serializer;
    }
    public void Start(object obj)
    {
        try
        {
            string text = _serializer.SerializeObject(obj);

            string folderPath = new DirectoryInfo(Directory.GetCurrentDirectory())
                .Parent.Parent.Parent.Parent.ToString();

            string filePath = Path.Combine(folderPath, "output.txt");
            File.WriteAllText(filePath, text);

            System.Console.WriteLine("File Write successfull ✅");
            System.Console.WriteLine(text);
        }
        catch (Exception ex)
        {
            System.Console.WriteLine(ex);
        }
    }
}

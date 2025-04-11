using System;

namespace Serializer.DataAccess.Repository.IRepository;

public interface ISerializer
{
    public string SerializeObject(object obj);
    public string SerializeValues(object obj, string? elementName);
}

using System;

namespace Serializer.DataAccess.Repository.IRepository;

public interface IUnitOfWork
{
    public void Start(object obj);
}

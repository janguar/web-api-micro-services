﻿using System.Linq;
using System.Threading.Tasks;

namespace Microservice.Data;

public interface IDbContext
{
    IQueryable<T> GetData<T>(bool trackingChanges = false) where T : class;

    void Insert<T>(T entity) where T : class;

    void Delete<T>(T entity) where T : class;

    Task SaveAsync();
}
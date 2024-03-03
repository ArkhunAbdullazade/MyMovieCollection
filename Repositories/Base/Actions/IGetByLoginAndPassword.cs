using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyMovieCollection.Repositories.Base.Actions;
public interface IGetByLoginAndPassword<T>
{
    public Task<T?> GetByLoginAndPassword(string? login, string? password);
}
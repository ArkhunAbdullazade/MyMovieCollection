using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyMovieCollection.Repositories.Base.Actions;
public interface IGetByDto<T, TResult>
{
    public Task<TResult?> IGetByDto(T dto);
}
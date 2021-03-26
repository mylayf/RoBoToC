using Microsoft.AspNetCore.Mvc;
using RoBoToC.Core.Utilities.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RoBoToC.WebUI.ApiProcess.Abstract
{
    public interface IGenericApiService<Entity>
    {
        Task<IDataResult<Entity>> Add(Entity entity, CancellationToken cancellationToken);
        Task<IDataResult<Entity>> Update(Entity entity, CancellationToken cancellationToken);
        Task<IDataResult<int>> Delete(int Id, CancellationToken cancellationToken);
        Task<IDataResult<Entity[]>> GetAll(CancellationToken cancellationToken);
        Task<IDataResult<Entity>> GetById(int Id, CancellationToken cancellationToken);
    }
}

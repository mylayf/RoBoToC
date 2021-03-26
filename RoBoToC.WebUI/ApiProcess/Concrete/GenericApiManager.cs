using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RoBoToC.Core.Utilities.Results;
using RoBoToC.WebUI.ApiProcess.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RoBoToC.WebUI.ApiProcess.Concrete
{
    public class GenericApiManager<Entity> : IGenericApiService<Entity>
    {



        public async Task<IDataResult<Entity>> Add(Entity entity, CancellationToken cancellationToken)
        {
            var content = ApiHelper.SerializeContent(entity);
            var response = await ApiHelper.Post(content, cancellationToken);
            var responseContent = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<IDataResult<Entity>>(responseContent);
        }

        public async Task<IDataResult<int>> Delete(int Id, CancellationToken cancellationToken)
        {
            var response = await ApiHelper.Delete(Id, cancellationToken);
            var responseContent = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<IDataResult<int>>(responseContent);
        }

        public async Task<IDataResult<Entity[]>> GetAll(CancellationToken cancellationToken)
        {
            var response = await ApiHelper.GetAll(cancellationToken);
            var responseContent = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<IDataResult<Entity[]>>(responseContent);
        }

        public async Task<IDataResult<Entity>> GetById(int Id, CancellationToken cancellationToken)
        {
            var response = await ApiHelper.GetById(Id, cancellationToken);
            var responseContent = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<IDataResult<Entity>>(responseContent);
        }

        public async Task<IDataResult<Entity>> Update(Entity entity, CancellationToken cancellationToken)
        {
            var content = ApiHelper.SerializeContent(entity);
            var response = await ApiHelper.Put(content, cancellationToken);
            var responseContent = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<IDataResult<Entity>>(responseContent);
        }
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RoBoToC.Business.Abstract;
using RoBoToC.Core.Extensions;
using RoBoToC.Entity.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace RoBoToC.WebApi.Controllers
{
    public class BaseController<Entity, Model> : Controller where Entity : class, IEntity, new()
    {
        private IGenericService<Entity> genericService;

        public BaseController(IGenericService<Entity> genericService)
        {
            this.genericService = genericService;
        }
        [HttpGet("Index")]
        public virtual async Task<IActionResult> Index(CancellationToken cancellationToken)
        {
            var entities = await genericService.GetAll(cancellationToken);
            return Ok(entities);
        }
        [HttpGet("Edit")]
        public virtual async Task<IActionResult> Edit(CancellationToken cancellationToken, int Id)
        {
            var entity = await genericService.Get(cancellationToken, x => x.Id == Id);
            return Ok(entity);
        }
        [HttpPost("Create")]
        public virtual async Task<IActionResult> Create(CancellationToken cancellationToken, Entity Entity)
        {
            await FillRelationships(Entity);
            var entity = await genericService.Add(Entity, cancellationToken);
            return Ok(entity);
        }
        [HttpPost("Edit")]
        public virtual async Task<IActionResult> Edit(CancellationToken cancellationToken, Entity Entity)
        {
            var entity = await genericService.Update(Entity, cancellationToken);
            return Ok(entity);
        }
        [HttpPost("Delete")]
        public virtual async Task<IActionResult> Delete(CancellationToken cancellationToken, Entity Entity)
        {
            var result = await genericService.Remove(Entity, cancellationToken);
            return Ok(result);
        }
        protected virtual Task FillRelationships(IEntity Entity)
        {
            if (typeof(IUserEntity).IsAssignableFrom(Entity.GetType()))
            {
                ((IUserEntity)Entity).UserId = HttpContext.User.UserId();
            }
            return Task.CompletedTask;
        }
    }
}

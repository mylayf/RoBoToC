using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RoBoToC.Business.Abstract;
using RoBoToC.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RoBoToC.WebApi.Controllers
{
    [Route("CurrentProcess")]
    [Authorize]
    public class CurrentProcessController : BaseController<CurrentProcess, bool>
    {
        private ICurrentProcessService currentProcessService;
        public CurrentProcessController(ICurrentProcessService currentProcessService) : base(currentProcessService)
        {
            this.currentProcessService = currentProcessService;
        }
        [NonAction]
        public override Task<IActionResult> Edit(CancellationToken cancellationToken, CurrentProcess Entity)
        {
            return base.Edit(cancellationToken, Entity);
        }
        [NonAction]
        public override Task<IActionResult> Edit(CancellationToken cancellationToken, int Id)
        {
            return base.Edit(cancellationToken, Id);
        }
        [NonAction]
        public override Task<IActionResult> Create(CancellationToken cancellationToken, CurrentProcess Entity)
        {
            return base.Create(cancellationToken, Entity);
        }
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RoBoToC.Business.Abstract;
using RoBoToC.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoBoToC.WebApi.Controllers
{
    [Route("CompletedProcess")]
    [Authorize]
    public class CompletedProcessController : BaseController<CompletedProcess,bool>
    {
        private ICompletedProcessService completedProcessService;
        public CompletedProcessController(ICompletedProcessService completedProcessService) : base(completedProcessService)
        {
            this.completedProcessService = completedProcessService;
        }
    }
}

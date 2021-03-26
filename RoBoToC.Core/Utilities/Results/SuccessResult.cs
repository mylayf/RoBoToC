using System;
using System.Collections.Generic;
using System.Text;

namespace RoBoToC.Core.Utilities.Results
{
    public class SuccessResult : Result,IResult
    {
        public SuccessResult() : base(true)
        {
        }
        public SuccessResult(string message) : base(true, message)
        {
        }
    }
}

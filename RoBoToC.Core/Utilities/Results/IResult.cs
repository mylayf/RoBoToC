using System;
using System.Collections.Generic;
using System.Text;

namespace RoBoToC.Core.Utilities.Results
{
    public interface IResult
    {
        bool Success { get; set; }
        string Message { get; set; }
    }
}

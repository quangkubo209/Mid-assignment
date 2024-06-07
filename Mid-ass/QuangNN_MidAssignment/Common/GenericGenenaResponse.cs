using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class GeneralResponse<T> : GeneralResponse
    {
        public T? Content { get; set; }
    }

    public class SuccessGeneralResponse<T> : GeneralResponse<T>
    {
        public override bool Success { get; set; } = true;
    }
}

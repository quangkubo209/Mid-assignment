using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class GeneralValidationResult
    {
        public bool IsValid { get; set; } = false;

        public Dictionary<string, string> Errors { get; set; } = [];

        public string Message => string.Join(", ", Errors.Select(e => $"{e.Key}: {e.Value}"));
    }

    public class ModelValidationResult<T> : GeneralValidationResult where T : class
    {
        public T? Content { get; set; }
    }
}

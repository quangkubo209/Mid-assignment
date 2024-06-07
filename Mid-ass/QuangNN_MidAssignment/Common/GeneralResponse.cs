

using System.Net;

namespace Common
{
    public class GeneralResponse
    {
        public virtual bool Success { get; set; } = false;

        public HttpStatusCode StatusCode { get; set; }

        public string? Message { get; set; }

        public GeneralValidationResult ValidationResult { get; set; } = new GeneralValidationResult();
    }

    public class SuccessGeneralResponse : GeneralResponse
    {
        public override bool Success { get; set; } = true;
    }
}



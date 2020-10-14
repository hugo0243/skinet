using System.Collections.Generic;

namespace API.Errors
{
    // Class that handles errors of validations, blank forms, or the validations handle by ApiController
    public class ApiValidationErrorResponse : ApiResponse
    {
        public IEnumerable<string> Errors { get; set; }
        public ApiValidationErrorResponse() : base(400)
        {

        }
    }
}
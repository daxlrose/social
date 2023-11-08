using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Social.Api.Contracts.Common;

namespace Social.Api.Filters
{
    public class ValidateGuidAttribute : ActionFilterAttribute
    {
        private readonly string _key;
        public ValidateGuidAttribute(string key)
        {
            _key = key;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ActionArguments.TryGetValue(_key, out var value))
            {
                if (!Guid.TryParse(value.ToString(), out var guid))
                {
                    var apiError = new ErrorResponse();
                    apiError.StatusCode = 400;
                    apiError.StatusPhrase = "Bad Request";
                    apiError.Timestamp = DateTime.Now;
                    apiError.Errors.Add($"The identifier for {_key} is not a correct GUID format");
                    context.Result = new ObjectResult(apiError);
                }
            }
        }
    }
}

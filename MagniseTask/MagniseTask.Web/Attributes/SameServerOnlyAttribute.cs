using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;
using MagniseTask.Domain.Exceptions;

public class SameServerOnlyAttribute : Attribute, IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var request = context.HttpContext.Request;
        var referer = request.Host.ToString();
        var host = request.Host.ToString();

        if (string.IsNullOrEmpty(referer) || !referer.Contains(host))
        {
            var errorResponse = new ErrorResponse
            {
                StatusCode = (int)HttpStatusCode.Forbidden,
                Message = "Forbidden: Request must come from the same server.",
                Errors = null
            };

            context.Result = new JsonResult(errorResponse)
            {
                StatusCode = (int)HttpStatusCode.Forbidden
            };
        }
    }
}
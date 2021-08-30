using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;

namespace application_programming_interface.Atributes
{
    public class AuthenticationAttribute : AuthorizeAttribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            throw new System.NotImplementedException();
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            throw new System.NotImplementedException();
        }
    }
}

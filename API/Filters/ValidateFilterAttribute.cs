using Core.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace API.Filters
{
    public class ValidateFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext actionContext)
        {
            if(!actionContext.ModelState.IsValid)
            {
                var errors= actionContext.ModelState.Values.SelectMany(x => x.Errors).Select(x=>x.ErrorMessage).ToList();
                actionContext.Result = new BadRequestObjectResult(CustomResponseDto<NoContentDto>.Fail(400,errors));
            }
        }
    }
}

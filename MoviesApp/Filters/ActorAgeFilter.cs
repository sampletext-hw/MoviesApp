using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MoviesApp.Filters
{
    public class ActorAgeFilter : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            base.OnActionExecuted(context);
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var birthdateDatetimeString = context.HttpContext.Request.Form["Birthdate"];
            var birthdateDatetime = DateTime.Parse(birthdateDatetimeString);
            var nowDate = DateTime.Now.Date;
            var deltaYear = Math.Abs(birthdateDatetime.Year - nowDate.Year);
            if (deltaYear < 7 || deltaYear > 99)
            {
                context.Result = new BadRequestResult();
            }
            base.OnActionExecuting(context);
        }
    }
}
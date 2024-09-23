using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace StocksApp.Filters
{
    public class BuyOrderAndSellOrderActionFilter : ActionFilterAttribute
    {

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
                context.Result = new RedirectToRouteResult(
                    new RouteValueDictionary(new { controller = "Trade", action = "Index" })
                );
        }
    }
}

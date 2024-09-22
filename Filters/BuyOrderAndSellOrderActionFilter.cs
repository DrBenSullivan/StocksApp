using Microsoft.AspNetCore.Mvc.Filters;

namespace StocksApp.Filters
{
    public class BuyOrderAndSellOrderActionFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            throw new NotImplementedException();
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            throw new NotImplementedException();
        }
    }
}

using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ActionConstraints;

namespace Bookify.Filters
{
	public class AjaxOnly : ActionMethodSelectorAttribute
	{
		public override bool IsValidForRequest(RouteContext routeContext, ActionDescriptor action)
		{
			var request = routeContext.HttpContext.Request; //Store Request Data
			var isAjax = request.Headers["X-Requested-With"] == "XMLHttpRequest"; //if the request has this header then it came throw Ajax

			return isAjax;
		}
	}
}

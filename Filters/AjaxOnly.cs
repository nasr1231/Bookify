using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ActionConstraints;

namespace Bookify.Filters
{
	public class AjaxOnly : ActionMethodSelectorAttribute
	{
		public override bool IsValidForRequest(RouteContext routeContext, ActionDescriptor action)
		{
			var request = routeContext.HttpContext.Request;
			var isAjax = request.Headers["X-Requested-With"] == "XMLHttpRequest";

			return isAjax;
		}
	}
}


using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

public class IpRequiredAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext filterContext)
    {
        var sessionIp = filterContext.HttpContext.Session.GetString("DbServerIp");

        // Si no hay IP en sesión y no estamos ya en la página de configuración, redirigir
        if (string.IsNullOrEmpty(sessionIp) &&
            filterContext.RouteData.Values["controller"]?.ToString() != "NetworkConfig")
        {
            filterContext.Result = new RedirectToActionResult("SetupIP", "NetworkConfig", null);
        }

        base.OnActionExecuting(filterContext);
    }
}
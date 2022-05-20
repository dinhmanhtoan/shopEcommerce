using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Routing;

namespace Model.Infrastructure;
public class RazorViewRenderer : IRazorViewRenderer
{
    private readonly IRazorViewEngine _viewEngine;
    private readonly IServiceProvider _serviceProvider;
    private readonly ITempDataProvider _temDataProvider;
    private readonly IHttpContextAccessor _contextAccessor;

    public RazorViewRenderer(IRazorViewEngine viewEngine, IServiceProvider serviceProvider, ITempDataProvider temDataProvider, IHttpContextAccessor contextAccessor)
    {
        _viewEngine = viewEngine;
        _serviceProvider = serviceProvider;
        _temDataProvider = temDataProvider;
        _contextAccessor = contextAccessor;
    }


    public async Task<string> RenderViewToStringAsync<TModel>(string viewName,TModel model)
    {
       // var actionContext = GetActionContext();
        var actionContext = new ActionContext(_contextAccessor.HttpContext, _contextAccessor.HttpContext.GetRouteData(), new ActionDescriptor());

        var view = FindView(actionContext, viewName);
        var viewData = new  ViewDataDictionary<TModel>(new EmptyModelMetadataProvider(), new ModelStateDictionary())
            {
                Model = model
            };
        using (var output = new StringWriter())
        {
            var viewContext = new ViewContext(
                    actionContext,
                    view,
                    viewData,
                    new TempDataDictionary(actionContext.HttpContext, _temDataProvider),
                    output,
                    new HtmlHelperOptions()
                );

            await view.RenderAsync(viewContext);
            return output.ToString();
        };

    }
    private IView FindView(ActionContext actionContext, string viewName)
    {
        var getViewResult = _viewEngine.GetView(executingFilePath: null, viewPath: viewName, isMainPage: true);
        if (getViewResult.Success)
        {
            return getViewResult.View;
        }
        var findViewResult = _viewEngine.FindView(actionContext, viewName, isMainPage: true);
        if (getViewResult.Success)
        {
            return getViewResult.View;
        }
        var searchedLocations = getViewResult.SearchedLocations.Concat(findViewResult.SearchedLocations);
        var errorMessage = String.Join(
            Environment.NewLine,
            new[] { $"Unable to find view '{viewName}'. The following locations were searched:" }.Concat(searchedLocations));
        throw new InvalidOperationException(errorMessage);
    }
    private ActionContext GetActionContext()
    {
        var httpContext = new DefaultHttpContext
        {
            RequestServices = _serviceProvider
        };
        return new ActionContext(httpContext, new RouteData(), new ActionDescriptor());
    }
}

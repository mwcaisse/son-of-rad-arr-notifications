using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace SonOfRadArrNotifications.Common;

public static class EmailBuilderUtils
{
    public static  Task<string> RenderTemplate<T>(HtmlRenderer renderer, Dictionary<string, object> parameters) where T : IComponent
    {
        return renderer.Dispatcher.InvokeAsync(async () =>
        {
            var output = await renderer.RenderComponentAsync<T>(ParameterView.FromDictionary(parameters!));
            return output.ToHtmlString();
        });
    }
}
using System.Globalization;

namespace MyRecipeBook.Api.Middleware;

public class CultureMiddleware(RequestDelegate next)
{
    private static readonly CultureInfo[] SupportedCulture = CultureInfo.GetCultures(CultureTypes.AllCultures);

    public async Task Invoke(HttpContext context)
    {
        try
        {
            var requestedCulture = context.Request.Headers.AcceptLanguage.FirstOrDefault();
            var cultureInfo = new CultureInfo("en");

            if (!string.IsNullOrEmpty(requestedCulture) &&
                SupportedCulture.Any(c => c.Name == requestedCulture))
                cultureInfo = new CultureInfo(requestedCulture);

            CultureInfo.CurrentCulture = cultureInfo;
            CultureInfo.CurrentUICulture = cultureInfo;
        }
        catch (Exception)
        {
            // ignored
        }

        await next(context);
    }
}
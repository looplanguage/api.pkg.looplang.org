using lpr.Common.Models;
using System.Text.Json;

namespace lpr.WebAPI.Middleware
{
public class GlobalErrorHandler
{
    private readonly RequestDelegate _next;

    public GlobalErrorHandler(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        } catch (ApiException ex)
        {
            HttpResponse response = context.Response;
            response.ContentType = "application/json";

            response.StatusCode = ex.ErrorCode;
            if(ex.ErrorCode == 401 || ex.ErrorCode == 402 || ex.ErrorCode == 404 || ex.ErrorCode == 500)
            {
                await response.WriteAsync(JsonSerializer.Serialize(ex.ErrorMessage));
                return;
            }
            await response.WriteAsync("");
        }
    }
}
}

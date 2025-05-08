namespace LibraryManagementSystem.Api.Middlewares
{
    //public class ApiKeyMiddleware
    //{
    //    private readonly RequestDelegate _next;
    //    private readonly string _apiKeyHeaderName = "x-api-key";
    //    private readonly string _expectedApiKey;

    //    public ApiKeyMiddleware(RequestDelegate next, IConfiguration configuration)
    //    {
    //        _next = next;
    //        _expectedApiKey = configuration["ApiKeySettings:Key"] ?? string.Empty;
    //    }

    //    public async Task InvokeAsync(HttpContext context)
    //    {
    //        if (!context.Request.Headers.TryGetValue(_apiKeyHeaderName, out var providedApiKey))
    //        {
    //            context.Response.StatusCode = 401;
    //            await context.Response.WriteAsync("API key is missing.");
    //            return;
    //        }

    //        if (!string.Equals(providedApiKey, _expectedApiKey))
    //        {
    //            context.Response.StatusCode = 401;
    //            await context.Response.WriteAsync("Invalid API key.");
    //            return;
    //        }


    //        await _next(context);
    //    }
    //}

    public class ApiKeyMiddleware
    {
        private readonly RequestDelegate _next;
        private const string ApiKeyHeaderName = "x-api-key";
        private readonly string _expectedApiKey;

        public ApiKeyMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            _expectedApiKey = configuration["ApiKeySettings:Key"] ?? string.Empty;
        }

        public async Task InvokeAsync(HttpContext context)
        {


            if (!context.Request.Headers.TryGetValue(ApiKeyHeaderName, out var providedApiKey))
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("API key is missing.");
                return;
            }

            //if (!context.Request.Headers.TryGetValue(ApiKeyHeaderName, out var providedApiKey))
            //{
            //    context.Response.StatusCode = 401;
            //    await context.Response.WriteAsync("API key is missing.");
            //    return;
            //}

            if (!string.Equals(providedApiKey, _expectedApiKey))
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Invalid API key.");
                return;
            }

            await _next(context);
        }
    }

}

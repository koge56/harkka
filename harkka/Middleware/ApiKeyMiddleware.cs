﻿namespace harkka.Middleware
{
    public class ApiKeyMiddleware
    {
        private readonly RequestDelegate _next;
        private const string APIKEYNAME = "ApiKey";
        public ApiKeyMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            if (!context.Request.Headers.TryGetValue(APIKEYNAME, out var extractedApiKey))
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("ApiKey Missing");
                return;
            }
            var appSettings = context.RequestServices.GetRequiredService<IConfiguration>();
            var ApiKey = appSettings.GetValue<string>(APIKEYNAME);
            if (!ApiKey.Equals(extractedApiKey))
            {
                context.Response.StatusCode = 403;
                await context.Response.WriteAsync("Unauthorized client");
                return;
            }
            await _next(context);

        }
    }
}

using Microsoft.AspNetCore.OutputCaching;

namespace Gimnasio.Server.Services.Cache
{
    public sealed class AuthOutputCachePolicy : IOutputCachePolicy
    {
        public ValueTask CacheRequestAsync(OutputCacheContext context, CancellationToken cancellationToken)
        {
            var request = context.HttpContext.Request;

            // Solo cachear GET y HEAD
            if (!HttpMethods.IsGet(request.Method) && !HttpMethods.IsHead(request.Method))
            {
                context.AllowCacheLookup = false;
                context.AllowCacheStorage = false;
                return ValueTask.CompletedTask;
            }

            context.EnableOutputCaching = true;
            context.AllowCacheLookup = true;
            context.AllowCacheStorage = true;
            context.AllowLocking = true;

            // Variar por Authorization header
            if (request.Headers.TryGetValue("Authorization", out var authHeader))
            {
                context.CacheVaryByRules.VaryByValues["Authorization"] = authHeader.ToString();
            }

            return ValueTask.CompletedTask;
        }

        public ValueTask ServeFromCacheAsync(OutputCacheContext context, CancellationToken cancellationToken)
        {
            return ValueTask.CompletedTask;
        }

        public ValueTask ServeResponseAsync(OutputCacheContext context, CancellationToken cancellationToken)
        {
            var response = context.HttpContext.Response;

            // No cachear si hay cookies o no es 200 OK
            if (response.Headers.ContainsKey("Set-Cookie") || response.StatusCode != 200)
            {
                context.AllowCacheStorage = false;
            }

            return ValueTask.CompletedTask;
        }
    }
}
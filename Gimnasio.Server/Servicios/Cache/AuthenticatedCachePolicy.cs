using Microsoft.AspNetCore.OutputCaching;
using Microsoft.Extensions.Primitives;

namespace Gimnasio.Server.Services.Cache
{
    public class AuthenticatedCachePolicy : IOutputCachePolicy
    {
        public static readonly AuthenticatedCachePolicy Instance = new();

        private AuthenticatedCachePolicy() { }

        ValueTask IOutputCachePolicy.CacheRequestAsync(OutputCacheContext context, CancellationToken cancellationToken)
        {
            var attemptOutputCaching = AttemptOutputCaching(context);
            context.EnableOutputCaching = true;
            context.AllowCacheLookup = attemptOutputCaching;
            context.AllowCacheStorage = attemptOutputCaching;
            context.AllowLocking = true;

            // Variar el caché por usuario autenticado
            context.CacheVaryByRules.VaryByValues.Add("Authorization",
                context.HttpContext.Request.Headers.Authorization.ToString());

            return ValueTask.CompletedTask;
        }

        ValueTask IOutputCachePolicy.ServeFromCacheAsync(OutputCacheContext context, CancellationToken cancellationToken)
        {
            return ValueTask.CompletedTask;
        }

        ValueTask IOutputCachePolicy.ServeResponseAsync(OutputCacheContext context, CancellationToken cancellationToken)
        {
            var response = context.HttpContext.Response;

            if (!StringValues.IsNullOrEmpty(response.Headers.SetCookie))
            {
                context.AllowCacheStorage = false;
                return ValueTask.CompletedTask;
            }

            if (response.StatusCode != StatusCodes.Status200OK)
            {
                context.AllowCacheStorage = false;
                return ValueTask.CompletedTask;
            }

            return ValueTask.CompletedTask;
        }

        private static bool AttemptOutputCaching(OutputCacheContext context)
        {
            var request = context.HttpContext.Request;

            if (!HttpMethods.IsGet(request.Method) && !HttpMethods.IsHead(request.Method))
            {
                return false;
            }

            return true;
        }
    }
}
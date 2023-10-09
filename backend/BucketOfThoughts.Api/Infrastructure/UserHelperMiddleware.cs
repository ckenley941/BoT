using BucketOfThoughts.Api.Services;
using System.Globalization;

namespace EnsenaMe.Infrastructure
{
    public class UserHelperMiddleware
    {
        /// <summary>
        /// Testing out middleware logic - eventually might need something for users
        /// </summary>
        private readonly RequestDelegate _next;

        public UserHelperMiddleware(RequestDelegate next) 
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context, IUserService userService)
        {
            userService.GetUserInfo();
            var endpoint = context.GetEndpoint();
            var cultureQuery = context.Request.Query["culture"];
            if (!string.IsNullOrWhiteSpace(cultureQuery))
            {
                var culture = new CultureInfo(cultureQuery);

                CultureInfo.CurrentCulture = culture;
                CultureInfo.CurrentUICulture = culture;
            }

            // Call the next delegate/middleware in the pipeline.
            await _next(context);
        }
    }
}

using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Client.Middleware
{
    public class RedirectMiddleware
    {
        private readonly RequestDelegate next;

        public RedirectMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Path == "/Account/Login")
            {
                if (context.User.Identity.IsAuthenticated)
                {
                    context.Response.Redirect("/dashboard");
                }
            }
            await next.Invoke(context);
        }

    }
}

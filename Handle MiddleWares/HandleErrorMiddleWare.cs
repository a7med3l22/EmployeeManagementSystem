
using Microsoft.Extensions.Caching.Memory;
using System.Collections.Concurrent;
using System.Text.Json;
using System.Threading;

namespace Full_E_Commerce_Project.Handle_MiddleWares
{
	public class HandleErrorMiddleWare : IMiddleware
	{
		private readonly IWebHostEnvironment environment;
		private readonly IMemoryCache memoryCache;
		private readonly TimeSpan _rateLimitWindow = TimeSpan.FromSeconds(30);
	
		public HandleErrorMiddleWare(IWebHostEnvironment environment, IMemoryCache memoryCache)
		{
			this.environment = environment;
			this.memoryCache = memoryCache;
		}
		public async Task InvokeAsync(HttpContext context, RequestDelegate next)
		{
			try
			{
			
				ApplySecurityHeaders(context);
				await next(context);
			}
            catch (Exception ex)
            {
                context.Response.Clear();
                context.Response.StatusCode = 500;

                context.Response.Redirect("/Error?msg=" + Uri.EscapeDataString(ex.Message));

                //context.Response.Redirect("/Error");
                return;
            }
        }



	private void ApplySecurityHeaders(HttpContext context)
		{
            //  يمنع تخمين نوع الملفات (MIME sniffing) // يعني image.jpg يقراه كده حتي لو جواه js ميقراهوش ك js
            context.Response.Headers["X-Content-Type-Options"] = "nosniff";

			//  يمنع وضع موقعك في iframe (حماية من Clickjacking)
			context.Response.Headers["X-Frame-Options"] = "DENY";

            // لتقليل مخاطر XSS. هجوم بيخلي حد يحط JavaScript خبيث في موقعك.
            context.Response.Headers["Content-Security-Policy"] =
   "default-src 'self'; " +
   "script-src 'self' 'unsafe-inline'; " +
   "img-src 'self' data: blob:; " +
   "object-src 'none'; " +
   "frame-ancestors 'none'";

            context.Response.Headers["Referrer-Policy"] = "strict-origin-when-cross-origin";

			// HSTS → بيجبر المتصفح يستخدم HTTPS (لو موقعك https)
			context.Response.Headers["Strict-Transport-Security"] =
				"max-age=31536000; includeSubDomains; preload";

		}

	}
}

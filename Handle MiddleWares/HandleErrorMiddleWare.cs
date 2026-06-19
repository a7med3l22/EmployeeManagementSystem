
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
		private static readonly ConcurrentDictionary<string, SemaphoreSlim> _locks
		= new ConcurrentDictionary<string, SemaphoreSlim>();

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

                context.Response.Redirect("/Error");
                return;
            }
        }



	private void ApplySecurityHeaders(HttpContext context)
		{
			// ✅ يمنع تخمين نوع الملفات (MIME sniffing)
			context.Response.Headers["X-Content-Type-Options"] = "nosniff";

			// ✅ يمنع وضع موقعك في iframe (حماية من Clickjacking)
			context.Response.Headers["X-Frame-Options"] = "DENY";

            // Temporarily allowing inline scripts for development/testing purposes.
            // In production, remove 'unsafe-inline' and move all scripts to external JS files for better security (CSP best practice).
            context.Response.Headers["Content-Security-Policy"] =
   "default-src 'self'; " +
   "script-src 'self' 'unsafe-inline'; " +
   "img-src 'self' data: blob:; " +
   "object-src 'none'; " +
   "frame-ancestors 'none'";

            // ✅ سياسة الـ Referrer
            context.Response.Headers["Referrer-Policy"] = "strict-origin-when-cross-origin";

			// ✅ HSTS → بيجبر المتصفح يستخدم HTTPS (لو موقعك https)
			context.Response.Headers["Strict-Transport-Security"] =
				"max-age=31536000; includeSubDomains; preload";

		}

	}
}

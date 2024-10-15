namespace MiddlewareExample.CustomMiddleware
{
    public class HelloCustomMiddleware
    {
        private readonly RequestDelegate _next;

        public HelloCustomMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            // Kiểm tra nếu có "firstname" và "lastname" trong query string
            if (httpContext.Request.Query.ContainsKey("firstname") && httpContext.Request.Query.ContainsKey("lastname"))
            {
                // Tạo chuỗi tên đầy đủ
                string fullName = httpContext.Request.Query["firstname"] + " " + httpContext.Request.Query["lastname"];
            
                // Ghi thông tin tên đầy đủ vào response
                await httpContext.Response.WriteAsync(fullName);
            }

            // Chuyển tiếp xử lý đến middleware tiếp theo trong pipeline
            await _next(httpContext);
        }
    }

    // Extension method for easy registration of the middleware
    public static class HelloCustomModdleExtensions
    {
        public static IApplicationBuilder UseHelloCustomMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<HelloCustomMiddleware>();
        }
    }
}
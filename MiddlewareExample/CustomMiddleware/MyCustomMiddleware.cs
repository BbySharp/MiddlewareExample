namespace MiddlewareExample.CustomMiddleware
{
    // Custom Middleware: Tạo middleware tùy chỉnh triển khai từ IMiddleware
    // - Middleware này sẽ được thêm vào chuỗi middleware.
    // - Nó ghi "My custom middleware - Starts" trước khi gọi next() và "My custom middleware - Ends" sau khi next() hoàn thành.
    public class MyCustomMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            await context.Response.WriteAsync("My custom middleware - Starts");  // Thông báo bắt đầu của middleware tùy chỉnh
            await next(context);  // Chuyển tiếp quyền xử lý cho middleware tiếp theo
            await context.Response.WriteAsync("My custom middleware - Ends");  // Thông báo kết thúc của middleware tùy chỉnh
        }
    }
}
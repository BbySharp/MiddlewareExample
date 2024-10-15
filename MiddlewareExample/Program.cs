using MiddlewareExample.CustomMiddleware;

var builder = WebApplication.CreateBuilder(args);

// Đăng ký HelloCustomMiddleware vào dịch vụ với AddTransient (tạo middleware mỗi lần cần)
// - HelloCustomMiddleware được thêm vào DI container để sử dụng trong chuỗi middleware.
builder.Services.AddTransient<HelloCustomMiddleware>();

var app = builder.Build();

// Middleware 1: Middleware đầu tiên trong chuỗi.
// - Ghi "Hello middleware!" vào response và chuyển quyền cho middleware tiếp theo.
app.Use(async (HttpContext context, RequestDelegate next) =>
{
    await context.Response.WriteAsync("Hello middleware! \n");  // Ghi thông điệp đầu tiên vào response
    await next(context);  // Chuyển quyền cho middleware tiếp theo
});

// Middleware 2: Middleware thứ hai trong chuỗi.
// - Ghi "Hello middleware 2!" vào response và chuyển quyền cho middleware tiếp theo.
app.Use(async (HttpContext context, RequestDelegate next) =>
{
    await context.Response.WriteAsync("Hello middleware 2! \n");  // Ghi thông điệp thứ hai vào response
    await next(context);  // Chuyển quyền cho middleware tiếp theo
});

// Thêm HelloCustomMiddleware vào chuỗi middleware thông qua extension method UseHelloCustomMiddleware.
// - Đây là middleware tùy chỉnh được thêm vào, nó ghi thông điệp nếu query string có chứa firstname và lastname.
app.UseHelloCustomMiddleware();

// Middleware cuối cùng trong chuỗi: app.Run không gọi next() và kết thúc pipeline.
// - Ghi "Hello middleware 3!" vào response, là thông điệp cuối cùng trước khi response kết thúc.
app.Run(async (HttpContext context) =>
{
    await context.Response.WriteAsync("Hello middleware 3! \n");  // Ghi thông điệp cuối cùng vào response
});

app.Run();  // Khởi chạy ứng dụng
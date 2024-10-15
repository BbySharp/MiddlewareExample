var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// Middleware 1: Đây là middleware đầu tiên trong chuỗi (pipeline).
// - Nó sẽ ghi "Hello World!" vào response.
// - Sau đó gọi next(context) để chuyển tiếp quyền xử lý đến middleware tiếp theo.
app.Use(async (HttpContext context, RequestDelegate next) =>
{
    await context.Response.WriteAsync("Hello middleware!");  // Ghi thông điệp đầu tiên vào response
    await next(context);  // Chuyển quyền cho middleware tiếp theo
}); 

// Middleware 2: Middleware này chạy sau middleware đầu tiên.
// - Nó sẽ thêm "Hello World!!" vào response và chuyển tiếp đến middleware tiếp theo.
app.Use(async (HttpContext context, RequestDelegate next) =>
{
    await context.Response.WriteAsync("Hello middleware 2!");  // Thêm thông điệp thứ hai vào response
    await next(context);  // Chuyển quyền cho middleware tiếp theo
}); 

// Middleware cuối cùng: app.Run() không gọi next(context) và đóng vai trò như điểm kết thúc của pipeline.
// - Nó sẽ ghi "Hello World!!!" vào response, và đây là middleware cuối cùng được thực thi.
app.Run(async (HttpContext context) =>
{
    await context.Response.WriteAsync("Hello middleware 3!");  // Ghi thông điệp cuối cùng
}); 

app.Run();  // Khởi chạy ứng dụng
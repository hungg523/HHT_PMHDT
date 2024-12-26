using NhaThuoc.Application.DependencyInjection.Extensions;
using NhaThuoc.Data.DependencyInjection.Extensions;
using NhaThuoc.Share.Service;
using NhaThuoc.WebApi;

var builder = WebApplication.CreateBuilder(args);
HttpClientHandler handler = new HttpClientHandler
{
    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
};

HttpClient client = new HttpClient(handler);
// Add services to the container.
builder.Services.AddData(builder.Configuration);
builder.Services.AddApplication();
builder.Services.AddSignalR();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        policy => policy.WithOrigins("http://127.0.0.1:5500")
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials());
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();
app.MapHub<ChatHub>("/chat");
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Áp d?ng CORS tr??c các middleware khác
app.UseCors("AllowSpecificOrigin");

app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseHttpsRedirection();
app.UseSession();
app.UseAuthorization();
app.MapControllers();
app.UseStaticFiles();
app.Run();

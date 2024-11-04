using NhaThuoc.Application.Extensions;
using NhaThuoc.Data.Extensions;
using NhaThuoc.WebAdmin;

var builder = WebApplication.CreateBuilder(args);



// Add services to the container.
builder.Services.AddData(builder.Configuration);
builder.Services.AddApplication();
builder.Services.AddControllers();

// Cấu hình CORS để cho phép truy cập từ http://127.0.0.1:5500
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        policy => policy.WithOrigins("http://127.0.0.1:5500")
                        .AllowAnyHeader()
                        .AllowAnyMethod());
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Áp dụng CORS trước các middleware khác
app.UseCors("AllowSpecificOrigin");

app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();

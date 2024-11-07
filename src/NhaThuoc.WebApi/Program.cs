using NhaThuoc.Application.DependencyInjection.Extensions;
using NhaThuoc.Data.DependencyInjection.Extensions;
using NhaThuoc.WebApi;

var builder = WebApplication.CreateBuilder(args);



// Add services to the container.
builder.Services.AddData(builder.Configuration);
builder.Services.AddApplication();
builder.Services.AddControllers();

// C?u hình CORS ?? cho phép truy c?p t? http://127.0.0.1:5500
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

// Áp d?ng CORS tr??c các middleware khác
app.UseCors("AllowSpecificOrigin");

app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.UseStaticFiles();
app.Run();

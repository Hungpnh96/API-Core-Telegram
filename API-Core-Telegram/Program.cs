using API;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Thêm dịch vụ CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", builder =>
    {
        builder.WithOrigins("https://your-github-username.github.io") // Thay đổi với URL của GitHub Pages của bạn
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});


builder.Services.AddControllers()
    .AddApplicationPart(Assembly.Load(new AssemblyName("API")));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAPI();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseCors("AllowAllOrigins"); // Áp dụng chính sách CORS
app.UseCors("AllowSpecificOrigin"); // Áp dụng chính sách CORS

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

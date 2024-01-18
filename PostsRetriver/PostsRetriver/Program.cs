using Microsoft.OpenApi.Models;
using PostsRetriver.Communication;
using PostsRetriver.Exceptions;
using PostsRetriver.Sevice;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IPostCommunicator, PostCommunicator>();
builder.Services.AddScoped<IPostService, PostService>();
var  AllowLocalOrigins = "_myAllowSpecificOrigins";
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "PostRetriever",
        Description = "This API Has the objective to implement a Post Retriever Service",
        Contact = new OpenApiContact() { Name = "José Lourenço Lemos Netto", Email = "jllnetto@gmail.com" },
        License = new OpenApiLicense() { Name = "MIT", Url = new Uri("https://opensource.org/licenses/MIT") }
    });
});
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: AllowLocalOrigins,
        policy  =>
        {
            policy.AllowAnyOrigin();
        });
});


var app = builder.Build();
app.UseMiddleware<ExceptionMiddleware>();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    
}
app.UseCors(AllowLocalOrigins);
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
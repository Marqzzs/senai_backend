using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

//Adiciona o servi�o de controllers
builder.Services.AddControllers();
//Adiciona o servi�o de Swagger
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "API Filmes",
        Description = "API para gerenciamento de filmes - Introdu��o da sprint2 back-end API",
        Contact = new OpenApiContact
        {
            Name = "Marqzzs",
            Url = new Uri("https://github.com/Marqzzs/senai_backend_2semestrre/tree/main/API")
        },
    });
    // Configure o Swagger para usar o arquivo XML gerado com as instru��es anteriores;
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

var app = builder.Build();

//Come�a a configura��o do Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = string.Empty;
});
//Finaliza o processo do Swagger

//Adiciona o mapeamento dos controllers
app.MapControllers();

app.UseHttpsRedirection();

app.Run();



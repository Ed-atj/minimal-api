using MinimalApi.Endpoints;
using MinimalApi.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddAppServices(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseGlobalExceptionHandler();
app.UseHttpsRedirection();
app.UseCors();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();


app.MapVeiculoEndpoints();
app.MapAdministradorEndpoints();

app.Run();

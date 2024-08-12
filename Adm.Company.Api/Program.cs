using Adm.Company.Api.Configurations;
using Adm.Company.Application.ViewModel.Jwt;
using Adm.Company.IoC.Application;
using Adm.Company.IoC.Context;
using Adm.Company.IoC.HttpClient;
using Adm.Company.IoC.Repositories;
using dotenv.net;

var builder = WebApplication.CreateBuilder(args);

DotEnv.Load();

var apiKeyWhats = VariaveisDeAmbiente.GetVariavel("WHATS_API_KEY");
var urlWhats = VariaveisDeAmbiente.GetVariavel("URL_API_WHATSS");
var origin = VariaveisDeAmbiente.GetVariavel("ORIGIN");
var connectionStringDb = VariaveisDeAmbiente.GetVariavel("CONNECTION_STRING");
var keyJwt = VariaveisDeAmbiente.GetVariavel("JWT_KEY");
var issue = VariaveisDeAmbiente.GetVariavel("JWT_ISSUE");
var audience = VariaveisDeAmbiente.GetVariavel("JWT_AUDIENCE");
var expirate = int.Parse(VariaveisDeAmbiente.GetVariavel("JWT_EXPIRATION"));

ConfiguracaoJwt.Configure(keyJwt, issue, audience, expirate);

builder.Services
    .ConfigureController()
    .ConfigureSwagger()
    .InjectServices()
    .InjectRepositories()
    .InjectCors(origin)
    .InjectWhatsHttp(urlWhats, apiKeyWhats)
    .InjectContext(connectionStringDb)
    .InjectJwt(keyJwt, issue, audience)
    .AddSignalR();

var app = builder.Build();

var basePath = "/api";
app.UsePathBase(new PathString(basePath));

app.UseRouting();

if (VariaveisDeAmbiente.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    app.Urls.Add("http://192.168.1.124:5022");
}

app.UseCors("base");

app.AddHubs();

app.UseHttpsRedirection();

app.UseAuthorization();

app.AddMiddlewaresApi();

app.MapControllers();

app.Run();

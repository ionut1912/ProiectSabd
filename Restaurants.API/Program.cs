

using Restaurants.API.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();
builder.Services.AddSingleton<IRestaurantRepository>(InitializeCosmosClientInstanceAsync(builder.Configuration.GetSection("CosmosDb")).GetAwaiter().GetResult());
//builder.Services.AddHttpClient<RestaurantRepository>(c=>c.BaseAddress = new Uri(builder.Configuration["ApiConfigs:Cities:Uri"]));
builder.Services.AddHttpClient("Cities", httpClient =>
{
    httpClient.BaseAddress = new Uri(builder.Configuration["ApiConfigs:Cities:Uri"]);

    // using Microsoft.Net.Http.Headers;
    // The GitHub API requires two headers.
    //httpClient.DefaultRequestHeaders.Add(
    //    HeaderNames.Accept, "application/vnd.github.v3+json");
    //httpClient.DefaultRequestHeaders.Add(
    //    HeaderNames.UserAgent, "HttpRequestsSample");
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
static async Task<RestaurantRepository> InitializeCosmosClientInstanceAsync(IConfigurationSection configurationSection)
{
    var databaseName = configurationSection["DatabaseName"];
    var containerName = configurationSection["ContainerName"];
    var account = configurationSection["Account"];
    var key = configurationSection["Key"];
    var client = new Microsoft.Azure.Cosmos.CosmosClient(account, key);
    var database = await client.CreateDatabaseIfNotExistsAsync(databaseName);
    await database.Database.CreateContainerIfNotExistsAsync(containerName, "/id");
    var cosmosDbService = new RestaurantRepository(client, databaseName, containerName);
    return cosmosDbService;
}

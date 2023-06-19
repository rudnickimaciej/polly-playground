using Polly;
using RequestService.Policies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddHttpClient("github",
c=>
{
    c.BaseAddress= new Uri("https://localhost:7057/api/Response/25");
})
.AddPolicyHandler(
    request => new ClientPolicy().ImmediateHttpRetry);

builder.Services.AddHttpClient("github2",c=>{});

builder.Services.AddSingleton<ClientPolicy>(new ClientPolicy());
builder.Services.AddControllers();
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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

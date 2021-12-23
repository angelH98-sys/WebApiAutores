using WebApiAutores;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var starup = new Startup(builder.Configuration);

starup.ConfigureServices(builder.Services);

var app = builder.Build();

starup.Configure(app, app.Environment);

app.Run();

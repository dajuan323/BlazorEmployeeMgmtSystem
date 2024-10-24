using Server.Extensions;

var builder = WebApplication.CreateBuilder(args);
// Registrar
builder.RegisterServices(typeof(Program));

var app = builder.Build();

app.RegisterPipelineComponents(typeof(Program));

app.Run();

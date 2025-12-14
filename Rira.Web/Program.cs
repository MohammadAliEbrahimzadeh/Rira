using Rira.Application.GrpcImplementations;
using Rira.Web;

var builder = WebApplication.CreateBuilder(args);

builder.
    Services
    .InjectContext(builder.Configuration)
    .InjectUnitOfWork()
    .InjectServices()
    .InjectMapster()
    .InjectGrpc();

var app = builder.Build();

app.MapGrpcService<UserServiceGrpc>();

app.UseHttpsRedirection();

app.Run();

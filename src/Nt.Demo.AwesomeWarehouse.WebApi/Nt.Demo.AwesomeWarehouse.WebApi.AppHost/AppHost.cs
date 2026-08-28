var builder = DistributedApplication.CreateBuilder(args);


var password = builder.AddParameter("password", "Password1"); //ofc. use secret

var sql = builder.AddSqlServer("sqldb", password)
    .WithDataVolume()
    .WithLifetime(ContainerLifetime.Persistent);

var db = sql.AddDatabase("warehouse");

var apiService = builder.AddProject<Projects.Nt_Demo_AwesomeWarehouse_WebApi_ApiService>("apiservice")
    .WithHttpHealthCheck("/health")
    .WithReference(db)
    .WaitFor(db);

builder.Build().Run();

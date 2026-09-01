using System;

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

// Add JavaScript app using Aspire's built-in integration. This will run the
// provided npm script during local development and integrate the app with
// the AppHost resource graph. Register an HTTP endpoint so the Aspire
// dashboard shows a link to the running UI.
var uiApp = builder.AddJavaScriptApp("ui", "../../Nt.Demo.AwesomeWarehouse.Ui", "start:local")
    .WithReference(apiService)
    .WithHttpEndpoint(port: 4200, env: "PORT");

var app = builder.Build();
app.Run();

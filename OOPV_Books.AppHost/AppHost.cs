var builder = DistributedApplication.CreateBuilder(args);

var apiService = builder.AddProject<Projects.OOPV_Books_ApiService>("apiservice")
    .WithHttpHealthCheck("/health");

builder.AddProject<Projects.OOPV_Books_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithHttpHealthCheck("/health")
    .WithReference(apiService)
    .WaitFor(apiService);

builder.Build().Run();

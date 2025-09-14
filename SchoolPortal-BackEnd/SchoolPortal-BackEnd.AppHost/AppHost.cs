var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.SchoolPortal_API>("schoolportal-api");

builder.Build().Run();

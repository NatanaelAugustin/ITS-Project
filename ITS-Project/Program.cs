using ITS_Project.Services;

StatusService statusService = new();
await statusService.CreateStatusTypesAsync();
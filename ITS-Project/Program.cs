using ITS_Project.Services;

StatusService statusService = new();
UIService menuService = new();

await statusService.InitializeAsync();
await menuService.TicketsOverview();


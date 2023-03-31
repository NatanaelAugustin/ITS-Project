using ITS_Project.Services;

StatusService statusService = new();
MenuService menuService = new();

await statusService.InitializeAsync();
await menuService.MainMenu();


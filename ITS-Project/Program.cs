using ITS_Project.Services;

StatusService statusService = new();
MenuService menuService = new();

await statusService.CreateStatusTypesAsync();

while (true)
{
    Console.Clear();
    await menuService.MainMenu();
    Console.ReadKey();
}
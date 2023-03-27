using ITS_Project.Services;

StatusService statusService = new();
MenuService menuService = new();

await statusService.InitAsync();

while (true)
{
    Console.Clear();
    await menuService.MainMenu();
    Console.ReadKey();
}
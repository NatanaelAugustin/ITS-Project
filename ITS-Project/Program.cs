using ITS_Project.Services;

var start = new MenuService();


await start.GetdataAsync();
await start.MainMenu();


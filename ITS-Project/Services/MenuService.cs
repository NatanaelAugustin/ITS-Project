using ITS_Project.Models.Entities;

namespace ITS_Project.Services;

internal class MenuService
{
    private readonly UserService _userService = new();
    private readonly CaseService _caseService = new();

    public async Task<UserEntity> CreateUserAsync()
    {
        var _entity = new UserEntity();
        Console.Clear();
        Console.WriteLine("################## New Handler ###############");
        Console.Write("Enter Firstname:  ");
        _entity.FirstName = Console.ReadLine() ?? "";
        Console.WriteLine("Enter Lastname:  ");
        _entity.LastName = Console.ReadLine() ?? "";
        Console.WriteLine("Enter Email:  ");
        _entity.Email = Console.ReadLine() ?? "";

        return await _userService.CreateAsync(_entity);
    }

    public async Task MainMenu(int userId)
    {
        Console.Clear();
        Console.WriteLine("################## Main Menu ###############");
        Console.WriteLine("1. Show all active issues");
        Console.WriteLine("2. Show all handlers");
        Console.WriteLine("3. Create new issue");
        Console.Write("choose one of the options above:  ");
        var option = Console.ReadLine();

        switch (option)
        {
            case "1":
                await ActiveCasesAsync();
                break;


            case "2":
                await HandlersAsync();
                break;


            case "3":
                await NewCaseAsync(userId);
                break;

            default:
                Console.Clear();
                Console.Write("please enter a valid input (1 - 3)");
                break;

        }
    }

    private async Task ActiveCasesAsync()
    {
        Console.Clear();
        Console.WriteLine("################## Active Issues ###############");
        foreach (var _case in await _caseService.GetActiveAsync())
        {
            Console.WriteLine($"Case ID: {_case.Id}");
            Console.WriteLine($"Created at: {_case.Created}");
            Console.WriteLine($"Modified at: {_case.Modified} ");
            Console.WriteLine($"Status {_case.Status.StatusName} ");
            Console.WriteLine();
        }
    }

    private async Task HandlersAsync()
    {
        Console.Clear();
        Console.WriteLine("################## Handler ###############");
        foreach (var _user in await _userService.GetAllAsync())
        {
            Console.WriteLine($"Handler ID :{_user.Id}");
            Console.WriteLine($"Name: {_user.FirstName}, {_user.LastName}");
            Console.WriteLine($"Email: {_user.Email} ");
            Console.WriteLine("");
        }
    }
    private async Task NewCaseAsync(int userId)
    {
        var _entity = new CaseEntity { UserId = userId };
        Console.Clear();
        Console.WriteLine("################## New Issue ###############");
        Console.Write("Enter the clients name:  ");
        _entity.CustomerName = Console.ReadLine() ?? "";
        Console.WriteLine("Enter the clients Email:  ");
        _entity.CustomerEmail = Console.ReadLine() ?? "";
        Console.WriteLine("Enter the clients phonenumber:  ");
        _entity.CustomerPhoneNumber = Console.ReadLine() ?? "";
        Console.WriteLine("Describe the issue:  ");
        _entity.Description = Console.ReadLine() ?? "";

        await _caseService.CreateAsync(_entity);
        await ActiveCasesAsync();
    }
}





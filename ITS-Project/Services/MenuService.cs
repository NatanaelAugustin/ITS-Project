using ITS_Project.Models.Entities;

namespace ITS_Project.Services;

internal class MenuService
{
    private readonly UserService _userService = new();
    private readonly CaseService _caseService = new();

    public async Task MainMenu()
    {
        Console.Clear();
        Console.WriteLine("################## Welcome to Natanaels Issues tracking system ###############");
        Console.WriteLine("1. Show all issues");
        Console.WriteLine("2. Search for specfific case");
        Console.WriteLine("3. Create new issue");
        Console.WriteLine("5. Close program");
        Console.Write("choose one of the options above:  ");
        var option = Console.ReadLine();

        switch (option)
        {
            case "1":
                await AllCasesAsync();
                break;

            /*    case "2":
                    await SearchCaseAsync();
                    break;
            */

            case "3":
                await NewCaseAsync();
                break;


            case "4":
                Environment.Exit(1);
                break;



            default:
                Console.Clear();
                Console.Write("please enter a valid input (1 - 4)");
                break;

        }
    }

    private async Task AllCasesAsync()
    {
        Console.Clear();
        Console.WriteLine("################## GetAllCasesAsync ###############");
        foreach (var _case in await _caseService.GetAllCasesAsync())
        {
            Console.WriteLine($"Status {_case.Status.StatusType} ");
            Console.WriteLine($"Created at: {_case.Created}");
            Console.WriteLine($"Case ID: {_case.Id}");
            Console.WriteLine($"Creator: {_case.User} ");
            Console.WriteLine($"Descriptiom: {_case.Description}");
            Console.WriteLine($"Comments: {_case.Comments}");
            Console.WriteLine("");
        }
    }

    /* private async Task SearchCaseAsync()
     {
         Console.Clear();
         Console.WriteLine("################## Search Specific Issue ###############");
         Console.Write("Enter a case ID: ");
         var caseId = Convert.ToInt32(Console.ReadLine());

         var _case = await _caseService.GetAsync(caseId);
         if (_case != null)
         {
             Console.WriteLine($"Status: {_case.Status.StatusType}");
             Console.WriteLine($"Created at: {_case.Created}");
             Console.WriteLine($"Case ID: {_case.Id}");
             Console.WriteLine($"Creator: {_case.User}");
             Console.WriteLine($"Description: {_case.Description}");
             Console.WriteLine($"Modified at: {_case.Modified}");
             Console.WriteLine($"Comments:");

             foreach (var comment in _case.Comments)
             {
                 Console.WriteLine($"{comment.Author} ({comment.Created}): {comment.Comment}");
             }
         }
         else
         {
             Console.Clear();
             Console.WriteLine($"There's no case with ID {caseId}");
         }
     }
    */

    private async Task NewCaseAsync()
    {
        Console.Clear();
        var _caseEntity = new CaseEntity();
        var _userEntity = new UserEntity();

        Console.WriteLine("################## New Issue ###############");
        Console.Write("Enter the clients name:  ");
        _userEntity.FirstName = Console.ReadLine() ?? "";
        Console.Write("Enter the clients name:  ");
        _userEntity.LastName = Console.ReadLine() ?? "";
        Console.WriteLine("Enter the clients Email:  ");
        _userEntity.Email = Console.ReadLine() ?? "";
        Console.WriteLine("Enter the clients phonenumber:  ");
        _userEntity.PhoneNumber = Console.ReadLine() ?? "";
        Console.WriteLine("Describe the issue:  ");
        _caseEntity.Description = Console.ReadLine() ?? "";

        await _userService.CreateAsync(_userEntity);
        await _caseService.CreateAsync(_caseEntity);
        await AllCasesAsync();
    }

}






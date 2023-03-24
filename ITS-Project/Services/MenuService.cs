namespace ITS_Project.Services;

internal class MenuService
{
    private readonly UserService _userService = new();
    private readonly CaseService _caseService = new();

    public async Task MainMenu()
    {
        Console.Clear();
        Console.WriteLine("################## HuvudMeny ###############");
        Console.WriteLine("1. Visa all aktiva arenden");
        Console.WriteLine("2. Visa alla handlaggare");
        Console.WriteLine("3. Skapa nytt arende");
        Console.Write("valj ett av ovanstande alternativ");
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
                await NewCaseAsync();
                break;

            default:
                break;

        }
    }

    private async Task ActiveCasesAsync()
    {

    }

    private async Task HandlersAsync()
    {

    }

    private async Task NewCaseAsync()
    {

    }
}
}
}

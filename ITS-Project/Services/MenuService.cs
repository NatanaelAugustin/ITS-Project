using ITS_Project.Contexts;
using ITS_Project.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace ITS_Project.Services;

internal class MenuService
{
    private readonly DataContext _context = new();
    private readonly CommentService _commentService = new();
    private readonly StatusService _statusService = new();
    private readonly UserService _userService = new();
    private readonly CaseService _caseService = new();


    private static string CheckIfValid(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            var input = Console.ReadLine();

            if (input == null || input == "")
            {
                Console.WriteLine("! Please provide a value");
                continue;
            }

            return input.Trim();
        }
    }

    public async Task CreateCaseMenu()
    {
        Console.Clear();
        Console.WriteLine("######## User details ############");
        var email = CheckIfValid("Email :");
        var user = await _userService.GetByEmailAsync(email);

        if (user == null)
        {
            var firstName = CheckIfValid("First name: ");
            var lastName = CheckIfValid("Last name: ");
            var phoneNumber = CheckIfValid("Phonenumber: ");

            user = await _userService.CreateAsync(new CreateUser(firstName, lastName, email, phoneNumber));
        }

        Console.WriteLine("=====================================");
        Console.WriteLine($"Welcome {user.FirstName}!");
        Console.WriteLine();
        Console.WriteLine("########### Case details ########");

        var subject = CheckIfValid("Case subject: ");
        var description = CheckIfValid("Case description: ");

        var oneCase = await _caseService.CreateAsync(new CreateCase(subject, description, user.Id));

        if (oneCase != null)
        {
            Console.WriteLine();
            Console.WriteLine($" You succesfullfy generated a case witn case number : {oneCase.Id} ");
            Console.WriteLine("Press any key to return to the main menu");
        }
    }

    public async Task UpdateCaseStatus(Guid caseId, string args)
    {
        if (args.IsNullOrEmpty())
        {
            Console.WriteLine("Please enter a new status ID ");
            Console.ReadKey();
            return;
        }

        if (!int.TryParse(args.Trim(), out int statusId))
        {
            Console.WriteLine("! Could not parse status ID ");
            Console.ReadKey();
            return;
        }

        if (await _statusService.GetAsync(statusId) == null)
        {
            Console.WriteLine($"! Status ID: {statusId} does not exist");
            Console.ReadKey();
            return;
        }
        if (await _caseService.UpdateCaseAsync(caseId, statusId) == null)
        {
            Console.WriteLine(" status update failed");
            Console.ReadKey();
            return;
        }
    }

    public async Task AddComment(Guid caseId, string args)
    {
        if (args.IsNullOrEmpty())
        {
            Console.WriteLine(" Enter a comment");
            Console.ReadKey();
            return;
        }

        if (await _commentService.CreateAsync(new CreateComment(args.Trim(), caseId)) == null)
        {
            Console.WriteLine(" Could not add the comment");
            Console.ReadKey();
            return;
        }

    }

    public async Task<bool> DeleteCase(Guid caseId)
    {
        Console.Write("Would you like to delete this case? YES or NO");
        while (true)
        {
            var result = Console.ReadLine()?.Trim().ToLower();
            if (result == "y" || result == "yes")
            {
                var deletedCase = await _caseService.DeleteCaseAsync(caseId);
                Console.WriteLine($" You deleted case {deletedCase.Id}");
                Console.ReadKey();
                return true;
            }

            Console.WriteLine("Deletion cancelled");
            Console.ReadKey();
            return false;

        }
    }
    public async Task<List<Guid>> ShowCasesAsync()
    {
        var cases = await _context.Cases.ToListAsync();
        Console.WriteLine("## Existing Cases ##");
        Console.WriteLine("{0,-5}{1,-30}{2,-20}{3,-20}{4,-20}", "No.", "Subject", "Status", "User", "Created");

        int number = 1;
        List<Guid> listId = new List<Guid>();
        foreach (var oneCase in cases)
        {
            var status = await _context.Statuses.FindAsync(oneCase.StatusId);
            var user = await _context.Users.FindAsync(oneCase.UserId);
            Console.WriteLine("{0,-5}{1,-30}{2,-20}{3,-20}{4,-20}",
                number,
                oneCase.Subject,
                status.StatusType,
                user.FirstName,
                oneCase.Created.ToString("yyyy-MM-dd HH:mm"));
            listId.Add(oneCase.Id);
            number++;
        }
        return listId;
    }

    public async Task MainMenu()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("###########   All Cases   ###################");
            var cases = await _caseService.GetAllCasesAsync();
            List<Guid> idList = new();
            if (cases.IsNullOrEmpty())
            {
                Console.WriteLine("no cases added yet");
                Console.WriteLine();
            }
            else
            {
                idList = await ShowCasesAsync();
            }
            Console.WriteLine("###########  Commands  ##################");
            if (!cases.IsNullOrEmpty())
            {
                Console.WriteLine("type <open> + <nr> to show case details");

            }
            Console.WriteLine("type <new> to add a new case");
            Console.WriteLine("type <exit> to close program");
            Console.WriteLine();
            Console.Write(">");

            var result = Console.ReadLine();
            if (result == null || result == "")
                continue;
            var command = result.Split(' ')[0].Trim().ToLower();
            var args = string.Join(" ", result.Split().Skip(1));

            switch (command)
            {
                case "open":
                    if (cases.IsNullOrEmpty())
                        break;
                    Console.Write("Enter case number: ");
                    var enteredNumber = Console.ReadLine();
                    if (!int.TryParse(enteredNumber, out int caseNumber))
                    {
                        Console.WriteLine("Invalid input");
                        Console.ReadKey();
                        break;
                    }
                    await CaseDetails(idList, enteredNumber, caseNumber);
                    break;

                case "new":
                    await CreateCaseMenu();
                    break;

                case "exit":
                    return;
                default:
                    Console.WriteLine("Unknown command");
                    break;
            }

            Console.WriteLine();
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }


    public async Task CaseDetails(List<Guid> listId, string enteredNumber, int caseNumber)
    {
        if (enteredNumber.IsNullOrEmpty())
        {
            Console.WriteLine(" Please enter a valid case number ");
            Console.ReadKey();
            return;
        }

        if (!int.TryParse(enteredNumber.Trim(), out int parsedNumber))
        {
            Console.WriteLine(" could not read the given input");
            Console.ReadKey();
            return;

        }

        Guid caseId;
        try
        {
            caseId = listId.ElementAt(parsedNumber - 1);
        }
        catch
        {
            Console.WriteLine($"! Case number is out of range");
            Console.ReadKey();
            return;
        }

        while (true)
        {
            var onecase = await _caseService.GetAsync(caseId);

            if (onecase == null)
            {
                Console.WriteLine($"! Failed to fetch the case");
                Console.ReadKey();
                return;

            }

            Console.Clear();
            Console.WriteLine("############# Case details #############");
            Console.WriteLine("ID :" + $"{caseId}");
            Console.WriteLine("Tennant :" + $"{onecase.User.FirstName}" + " " + $"{onecase.User.LastName}" + " " + $"{onecase.User.Email}" + " " + $"{onecase.User.PhoneNumber}");
            Console.WriteLine("Created :" + $"{onecase.Created}");
            Console.WriteLine("Status :" + $"{onecase.Status.StatusType}");
            Console.WriteLine("Subject :" + $"{onecase.Subject}");
            Console.WriteLine("Descripton :" + $"{onecase.Description}");
            Console.WriteLine();
            Console.WriteLine("############### Comments #################");

            var comments = await _commentService.GetByCaseId(caseId);
            if (!comments.Any())
            {
                Console.WriteLine("no comments added yet");
                Console.WriteLine();
            }
            foreach (var oneComment in comments)
            {
                Console.WriteLine($"{oneComment.CommentText}");
                Console.WriteLine(oneComment.Created);
                Console.WriteLine("============================");
            }

            Console.WriteLine("#Commands#");
            Console.Write("Status <nr> to change the case-status  ");
            foreach (var status in await _statusService.GetAllAsync())
            {
                Console.Write($" {status.Id} {status.StatusType}");
            }

            Console.WriteLine();
            Console.WriteLine("type <comment> to add comment");
            Console.WriteLine("type <delete> to remove case");
            Console.WriteLine("type <exit> to navigate to mainscreen");
            Console.WriteLine();

            Console.Write("Type here >");

            var input = Console.ReadLine();
            if (input == null || input == "")
                continue;
            var command = input.Split(' ')[0].Trim().ToLower();
            var args = string.Join(' ', input.Split().Skip(1));

            switch (command)
            {
                case "status":
                    await UpdateCaseStatus(onecase.Id, args);
                    break;

                case "comment":
                    await AddComment(caseId, args);
                    break;

                case "delete":
                    if (await DeleteCase(caseId)) ;
                    return;

                case "exit":
                    return;

                default:
                    Console.WriteLine("Invalid input");
                    Console.ReadKey();
                    break;
            }

        }

    }

}






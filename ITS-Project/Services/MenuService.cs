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

    public async Task Getdata()
    {
        Console.WriteLine("Waiting for database...");

        if (!await _context.Statuses.AnyAsync())
        {
            Console.WriteLine("Starting up program...");
            await _statusService.StartAsync();
        }
    }

    private static void PrintLine()
    {
        Console.WriteLine("==================================================================================================================");
    }


    private static string CheckIfValid(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            var input = Console.ReadLine();

            if (input == null || input == "")
            {
                Console.WriteLine(" Submit a value please");
                continue;
            }

            return input.Trim();
        }
    }

    public async Task CreateCaseMenu()
    {
        Console.Clear();
        Console.WriteLine("###### User details ######");
        PrintLine();
        var email = CheckIfValid("Email: ");
        var user = await _userService.GetByEmailAsync(email);

        if (user == null)
        {
            var firstName = CheckIfValid("First name: ");
            var lastName = CheckIfValid("Last name: ");
            var phoneNumber = CheckIfValid("Phone-number: ");

            user = await _userService.CreateAsync(new CreateUser(firstName, lastName, email, phoneNumber));
        }

        PrintLine();
        Console.WriteLine($"Logged in as {user.FirstName}");
        Console.WriteLine();
        Console.WriteLine("###### Case details ######");
        PrintLine();

        var subject = CheckIfValid("Case subject: ");
        var description = CheckIfValid("Case description: ");


        var oneCase = await _caseService.CreateAsync(new CreateCase(subject, description, user.Id));

        if (oneCase != null)
        {
            Console.WriteLine();
            Console.WriteLine($"You created a new case with number: {oneCase.Id} ");
        }
    }

    public async Task UpdateCaseStatus(Guid caseId, string args)
    {
        if (args.IsNullOrEmpty())
        {
            Console.WriteLine("Options ");
            var statuses = await _statusService.GetAllAsync();
            foreach (var newStatus in statuses)
            {
                Console.WriteLine($" {newStatus.Id} = {newStatus.StatusType}");
            }
            Console.WriteLine("Please enter a new status ID ");
            Console.Write("> ");
            args = Console.ReadLine()?.Trim();
        }

        if (!int.TryParse(args, out int statusId))
        {
            Console.WriteLine("Error: Could not find that status ID ");
            Console.ReadKey();
            return;
        }

        var status = await _statusService.GetAsync(statusId);
        if (status == null)
        {
            Console.WriteLine($"Error: Cant find a status connected to that {statusId} ");
            Console.ReadKey();
            return;
        }

        var updatedCase = await _caseService.UpdateCaseAsync(caseId, statusId);
        if (updatedCase == null)
        {
            Console.WriteLine("Error: Could not update that specific case ");
            Console.ReadKey();
            return;
        }

        Console.WriteLine($" Case {updatedCase.Id} updated with status '{status.StatusType}'");
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }
    public async Task WriteComment(Guid caseId)
    {
        Console.WriteLine("Enter your comment");
        string comment = Console.ReadLine();

        if (string.IsNullOrEmpty(comment))
        {
            Console.WriteLine("Error: Comment cannot be empty");
            return;
        }

        var newComment = new CreateComment(comment.Trim(), caseId);
        var addedComment = await _commentService.CreateAsync(newComment);

        if (addedComment == null)
        {
            Console.WriteLine("Error: Could not add the comment");
        }
        else
        {
            Console.WriteLine("Comment added successfully");
        }
    }

    public async Task<bool> DeleteCase(Guid caseId)
    {
        Console.WriteLine();
        Console.WriteLine("Would you like to delete this case? (Y/N) ");
        Console.Write("> ");
        string input = Console.ReadLine()?.Trim().ToLower();

        if (input == "y" || input == "yes")
        {
            var deletedCase = await _caseService.DeleteCaseAsync(caseId);
            Console.WriteLine($"Case {deletedCase.Id} has been removed");
            Console.ReadKey();
            return true;

        }
        else if (input == "n" || input == "no")
        {
            Console.WriteLine("Deletion aborted");
            Console.ReadKey();
            return false;
        }
        else
        {
            Console.WriteLine("Invalid input. Please enter Y or N");
            Console.ReadKey();
            return false;
        }
    }
    public async Task<List<Guid>> DisplayCasesAsync()
    {
        var cases = await _context.Cases.ToListAsync();

        Console.WriteLine("{0,-5}{1,-30}{2,-20}{3,-20}{4,-20}", "No.", "Subject", "Status", "User", "Created");

        int number = 1;
        List<Guid> listId = new();
        foreach (var oneCase in cases)
        {
            var status = await _context.Statuses.FindAsync(oneCase.StatusId);
            var user = await _context.Users.FindAsync(oneCase.UserId);
            Console.WriteLine("{0,-5}{1,-30}{2,-20}{3,-20}{4,-20}",
                number,
                oneCase.Subject,
                status?.StatusType,
                user?.FirstName,
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
            Console.WriteLine("######   All Cases   ######");
            PrintLine();
            var cases = await _caseService.GetAllCasesAsync();
            List<Guid> idList = new();
            if (cases.IsNullOrEmpty())
            {
                Console.WriteLine("no cases added yet");
                Console.WriteLine();
            }
            else
            {
                idList = await DisplayCasesAsync();
                PrintLine();
            }
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("###### Options  ######");
            PrintLine();
            if (!cases.IsNullOrEmpty())
            {
                Console.WriteLine("Type <open> to show case details");

            }
            Console.WriteLine("Type <new> to add a new case");
            Console.WriteLine("Type <exit> to close program");
            PrintLine();
            Console.WriteLine();
            Console.Write("> ");

            var result = Console.ReadLine();
            if (result == null || result == "")
                continue;
            var options = result.Split(' ')[0].Trim().ToLower();
            var args = string.Join(" ", result.Split().Skip(1));

            switch (options)
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
            Console.WriteLine("Enter a valid case number ");
            Console.ReadKey();
            return;
        }

        if (!int.TryParse(enteredNumber.Trim(), out int parsedNumber))
        {
            Console.WriteLine("Error: Could not read the given input");
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
            Console.WriteLine($"Error: Cant find that casenumber");
            Console.ReadKey();
            return;
        }

        while (true)
        {
            var onecase = await _caseService.GetAsync(caseId);

            if (onecase == null)
            {
                Console.WriteLine($"Error: Failed to fetch the case");
                Console.ReadKey();
                return;

            }

            Console.Clear();
            Console.WriteLine("###### Case details ######");
            PrintLine();
            Console.WriteLine("ID: " + $"{caseId}");
            Console.WriteLine("Written by: " + $"{onecase.User.FirstName}" + " " + $"{onecase.User.LastName}" + " | " + $"{onecase.User.Email}" + " | " + $"{onecase.User.PhoneNumber} ");
            Console.WriteLine("Created: " + $"{onecase.Created}");
            Console.WriteLine("Status: " + $"{onecase.Status.StatusType}");
            Console.WriteLine("Subject: " + $"{onecase.Subject}");
            Console.WriteLine("Descripton: " + $"{onecase.Description}");
            Console.WriteLine();
            Console.WriteLine("###### Comments ######");
            PrintLine();
            var comments = await _commentService.GetByCaseId(caseId);
            if (!comments.Any())
            {
                Console.WriteLine("This case has not been commented on yet");
                Console.WriteLine();
            }
            foreach (var oneComment in comments)
            {
                Console.WriteLine($"{oneComment.CommentText}");
                Console.WriteLine(oneComment.Created);
                PrintLine();
            }
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("###### Options ######");
            PrintLine();
            Console.Write("Type <status> to change the case-status  ");
            Console.WriteLine();
            Console.WriteLine("Type <comment> to write a comment");
            Console.WriteLine("Type <remove> to remove case");
            Console.WriteLine("Type <back> to navigate to mainscreen");
            Console.WriteLine();
            Console.Write("> ");

            var input = Console.ReadLine();
            if (input == null || input == "")
                continue;
            var option = input.Split(' ')[0].Trim().ToLower();
            var args = string.Join(' ', input.Split().Skip(1));

            switch (option)
            {
                case "status":
                    await UpdateCaseStatus(onecase.Id, args);
                    break;

                case "comment":
                    await WriteComment(caseId);
                    break;

                case "remove":
                    if (await DeleteCase(caseId)) ;
                    return;

                case "back":
                    return;

                default:
                    Console.WriteLine("Invalid input");
                    Console.ReadKey();
                    break;
            }

        }

    }

}

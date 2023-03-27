﻿using ITS_Project.Models.Entities;

namespace ITS_Project.Services;

internal class MenuService
{
    private readonly UserService _userService = new();
    private readonly CaseService _caseService = new();

    public async Task MainMenu()
    {
        Console.Clear();
        Console.WriteLine("################## Welcome to Natanaels Issues tracking system ###############");
        Console.WriteLine("1. Show all active issues");
        Console.WriteLine("2. Show all issues");
        Console.WriteLine("3. Create new issue");
        Console.Write("choose one of the options above:  ");
        var option = Console.ReadLine();

        switch (option)
        {
            case "1":
                await AllActiveCasesAsync();
                break;


            case "2":
                await AllCasesAsync();
                break;

            /*  case "3":
                  await SearchCaseAsync();
                  break;
            */


            case "4":
                await NewCaseAsync();
                break;


            default:
                Console.Clear();
                Console.Write("please enter a valid input (1 - 5)");
                break;

        }
    }

    private async Task AllActiveCasesAsync()
    {
        Console.Clear();
        Console.WriteLine("################## Active Issues ###############");
        foreach (var _case in await _caseService.GetAllActiveCasesAsync())
        {
            Console.WriteLine($"Status {_case.Status.StatusType} ");
            Console.WriteLine($"Created at: {_case.Created}");
            Console.WriteLine($"Case ID: {_case.Id}");
            Console.WriteLine($"Creator: {_case.User} ");
            Console.WriteLine($"Descriptiom: {_case.Description}");
            Console.WriteLine($"Modified at: {_case.Modified} ");
            Console.WriteLine($"Comments: {_case.Comments}");
            Console.WriteLine("");
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
            Console.WriteLine($"Modified at: {_case.Modified} ");
            Console.WriteLine($"Comments: {_case.Comments}");
            Console.WriteLine("");
        }
    }

    /*
    private async Task SearchCaseAsync()
    {
        Console.Write("Enter a case ID: ");
        var userId = Convert.ToInt32(Console.ReadLine());

        if (userId != null)
        {
            var cases = await _caseService.GetAsync();
            if (cases != null)
            {
                Console.WriteLine($"User ID: {cases}");
                Console.WriteLine($"User name: {_case}");
                Console.WriteLine($"Status: {_case.Status}");
                Console.WriteLine($"case number: {_case.Id}");


            }
            else
            {
                Console.Clear();
                Console.WriteLine($"There's one with this user ID {userId}");

            }
        }
    } */
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
        await AllActiveCasesAsync();
    }

}






﻿namespace ITS_Project.Models;

internal class CreateUser
{
    public CreateUser(string firstName, string lastName, string email, string phoneNumber)
    {
        FirstName = firstName;

        LastName = lastName;

        Email = email;

        PhoneNumber = phoneNumber;


    }

    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
}

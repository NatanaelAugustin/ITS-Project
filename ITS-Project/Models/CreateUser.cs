namespace ITS_Project.Models;

internal class CreateUser
{
    public CreateUser(string firstName, string lastName, string phoneNumber, string email)
    {
        FirstName = firstName;

        LastName = lastName;

        PhoneNumber = phoneNumber;

        Email = email;


    }

    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
}

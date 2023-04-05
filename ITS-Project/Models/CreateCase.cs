namespace ITS_Project.Models;

internal class CreateCase
{
    public CreateCase(string subject, string description, Guid userId)
    {
        Subject = subject;

        Description = description;

        UserId = userId;

    }

    public string Subject { get; set; }
    public string Description { get; set; }
    public Guid UserId { get; set; }
}

namespace ITS_Project.Models.Entities;

internal class UserEntity
{
    public int Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public ICollection<CaseEntity> Cases { get; set; } = new List<CaseEntity>();
}



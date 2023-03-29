using Microsoft.EntityFrameworkCore;

namespace ITS_Project.Models.Entities;

[Index(nameof(Email), IsUnique = true)]
internal class UserEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public ICollection<CaseEntity> Cases { get; set; } = new List<CaseEntity>();
}



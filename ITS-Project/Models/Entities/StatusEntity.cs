namespace ITS_Project.Models.Entities;

internal class StatusEntity
{
    public int Id { get; set; }

    public string StatusType { get; set; } = null!;

    public ICollection<CaseEntity> Cases { get; set; } = new List<CaseEntity>();


}

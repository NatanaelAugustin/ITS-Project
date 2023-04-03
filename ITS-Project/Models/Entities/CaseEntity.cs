namespace ITS_Project.Models.Entities;

internal class CaseEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Description { get; set; } = null!;
    public DateTime Created { get; set; } = DateTime.Now;
    public string Subject { get; set; } = null!;


    public int StatusId { get; set; } = 1;
    public StatusEntity Status { get; set; } = null!;

    public Guid UserId { get; set; }
    public UserEntity User { get; set; } = null!;

    public ICollection<CommentEntity> Comments { get; set; } = new HashSet<CommentEntity>();

}
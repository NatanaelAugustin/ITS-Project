namespace ITS_Project.Models.Entities;
internal class CommentEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string CommentText { get; set; } = null!;
    public DateTime Created { get; set; } = DateTime.Now;


    public Guid CaseId { get; set; }
    public CaseEntity Case { get; set; } = null!;

}

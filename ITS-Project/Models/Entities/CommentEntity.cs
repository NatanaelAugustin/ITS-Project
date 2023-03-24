namespace ITS_Project.Models.Entities;
internal class CommentEntity
{
    public int Id { get; set; }
    public string Comment { get; set; } = null!;
    public DateTime Created { get; set; } = DateTime.Now;
    public string Author { get; set; } = null!;


    public int CaseId { get; set; }
    public CaseEntity Case { get; set; } = null!;





}

namespace ITS_Project.Models;

internal class CreateComment
{
    public CreateComment(string commentText, Guid caseId)
    {
        CommentText = commentText;

        CaseId = caseId;

    }

    public string CommentText { get; set; }
    public Guid CaseId { get; set; }
}

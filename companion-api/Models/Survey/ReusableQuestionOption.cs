public class ReusableQuestionOption
{
    public int Id { get; set; }
    public required QuestionOption OriginalQuestionOption { get; set; }
    public required List<QuestionOption> Clones { get; set; } = [];
    public required List<string> Tags { get; set; }

    //public EmissionsSource? originalEmissionsSource { get; set; }
}
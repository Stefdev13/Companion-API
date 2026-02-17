public class ReusableQuestionOption
{
    public int id { get; set; }
    public required QuestionOption originalQuestionOption { get; set; }
    public required List<QuestionOption> clones { get; set; } = [];
    public required List<string> tags { get; set; }

    //public EmissionsSource? originalEmissionsSource { get; set; }
}
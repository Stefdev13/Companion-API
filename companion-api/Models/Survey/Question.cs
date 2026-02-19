public class Question
{
    public int? Id { get; set; }
    public required string QuestionName { get; set; }
    public required string Description { get; set; }
    public required List<string> Tips { get; set; }
    public required List<QuestionOption> QuestionOptions { get; set; } = [];
    public QuestionOption? OptionTemplate { get; set; }
    public bool AllowCustomOptions { get; set; } = false;
    public string NewCustomOptionPrompt { get; set; } = "";
    public bool AllowReusableQuestionOptions { get; set; } = false;
    public required List<string> ReusableQuestionOptionsTags { get; set; }
    public List<string>? DynamicQuestionOptionParams { get; set; }
}
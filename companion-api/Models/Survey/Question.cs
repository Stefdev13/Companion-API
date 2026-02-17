public class Question
{
    public int id { get; set; }
    public required string question { get; set; }
    public required string description { get; set; }
    public required List<string> tips { get; set; }
    public required List<QuestionOption> questionOptions { get; set; } = [];
    public QuestionOption? optionTemplate { get; set; }
    public bool allowCustomOptions { get; set; } = false;
    public string newCustomOptionPrompt { get; set; } = "";
    public bool allowReusableQuestionOptions { get; set; } = false;
    public required List<string> reusableQuestionOptionsTags { get; set; }
    public List<string>? dynamicQuestionOptionParams { get; set; }
}
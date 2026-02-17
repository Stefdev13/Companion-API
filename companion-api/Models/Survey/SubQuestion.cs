public class SubQuestion
{
    public int id { get; set; }
    public required string question { get; set; }
    public required string description { get; set; }
    public required QuestionType questionType { get; set; }
    public required List<string> answerOptions { get; set; } = [];
    public required string answer { get; set; } = "";
    public required List<string> unitOptions { get; set; }
    public string? selectedUnit { get; set; }
    public required string defaultMetricUnit { get; set; }
    public required string defaultImperialUnit { get; set; }
    public List<SubQuestion>? averageValueParams { get; set; }
    public List<DisplayRule>? displayRules { get; set; }
    public bool markedForCheckingWithClones { get; set; } = false;
    public string? usersValueOverriddenMessage { get; set; }
    public required string subQuestionKey { get; set; }
}
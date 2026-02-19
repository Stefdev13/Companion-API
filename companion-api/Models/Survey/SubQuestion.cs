public class SubQuestion
{
    public int? Id { get; set; }
    public required string Question { get; set; }
    public required string Description { get; set; }
    public required QuestionType QuestionType { get; set; }
    public List<string>? AnswerOptions { get; set; } = [];
    public required string Answer { get; set; } = "";
    public List<string>? UnitOptions { get; set; }
    public string? SelectedUnit { get; set; }
    public string? DefaultMetricUnit { get; set; }
    public string? DefaultImperialUnit { get; set; }
    public List<SubQuestion>? AverageValueParams { get; set; }
    public List<DisplayRule>? DisplayRules { get; set; }
    public bool MarkedForCheckingWithClones { get; set; } = false;
    public string? UsersValueOverriddenMessage { get; set; }
    public required string SubQuestionKey { get; set; }
}
public class SurveyModel
{
    public int Id { get; set; }
    public required string Version { get; set; }
    public required List<SurveyCategory> SurveyCategories { get; set; } = [];
    public required List<ReusableQuestionOption> ReusableQuestionOptions { get; set; } = [];
}
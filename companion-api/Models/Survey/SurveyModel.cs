public class SurveyModel
{
    public int id { get; set; }
    public required string version { get; set; }
    public required List<SurveyCategory> surveyCategories { get; set; } = [];
    public required List<ReusableQuestionOption> reusableQuestionOptions { get; set; } = [];
}
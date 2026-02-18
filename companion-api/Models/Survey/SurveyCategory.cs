public class SurveyCategory
{
    public int? Id { get; set; }
    public required string CategoryName { get; set; }
    public required string Description { get; set; }
    public required List<Question> Questions { get; set; } = [];
}
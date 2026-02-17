public class SurveyCategory
{
    public int id { get; set; }
    public required string categoryName { get; set; }
    public required string description { get; set; }
    public required List<Question> questions { get; set; } = [];
}
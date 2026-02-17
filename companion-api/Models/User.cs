public class User
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public SurveyModel? SurveyModel { get; set; }
}
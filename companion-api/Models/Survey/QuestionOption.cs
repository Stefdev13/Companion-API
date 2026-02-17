public class QuestionOption
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public bool IsSelected { get; set; }
    public required List<SubQuestion> DisplaySubQuestions { get; set; } = [];
    public required List<string> Tags { get; set; }
}
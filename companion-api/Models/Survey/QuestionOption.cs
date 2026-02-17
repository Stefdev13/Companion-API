public class QuestionOption
{
    public int id { get; set; }
    public required string name { get; set; }
    public bool isSelected { get; set; }
    public required List<SubQuestion> displaySubQuestions { get; set; } = [];
    public required List<string> tags { get; set; }
}
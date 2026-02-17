public class FJLeg
{
    public int id { get; set; }
    public required string motName { get; set; }
    public required ReusableQuestionOption reusableQuestionOption { get; set; }
    public required SubQuestion distanceQuestion { get; set; }
    public required SubQuestion motSubQuestion { get; set; }
}
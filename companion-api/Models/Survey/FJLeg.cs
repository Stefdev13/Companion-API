public class FJLeg
{
    public int Id { get; set; }
    public required string MotName { get; set; }
    public required ReusableQuestionOption ReusableQuestionOption { get; set; }
    public required SubQuestion DistanceQuestion { get; set; }
    public required SubQuestion MotSubQuestion { get; set; }
}
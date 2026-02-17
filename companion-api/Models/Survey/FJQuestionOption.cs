public class FJQuestionOption : QuestionOption
{
    public required SubQuestion roundTripSubQuestion { get; set; }
    public required SubQuestion frequencySubQuestion { get; set; }
    public required SubQuestion startDateSubQuestion { get; set; }
    public required SubQuestion endDateSubQuestion { get; set; }
    public required List<FJLeg> legs { get; set; } = [];
    public ReusableQuestionOption? reusableQuestionOption { get; set; }
}
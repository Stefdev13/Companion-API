public class FJQuestionOption : QuestionOption
{
    public required SubQuestion RoundTripSubQuestion { get; set; }
    public required SubQuestion FrequencySubQuestion { get; set; }
    public required SubQuestion StartDateSubQuestion { get; set; }
    public required SubQuestion EndDateSubQuestion { get; set; }
    public required List<FJLeg> Legs { get; set; } = [];
    public ReusableQuestionOption? ReusableQuestionOption { get; set; }
}
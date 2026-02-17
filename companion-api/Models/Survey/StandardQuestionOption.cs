public class StandardQuestionOption : QuestionOption
{
    public required List<SubQuestion> subQuestions { get; set; } = [];
    public ReusableQuestionOption? reusableQuestionOption { get; set; }
    public bool usedInOtherSurveyQuestions { get; set; }
    public bool cloneOfOtherQuestionOption { get; set; }
    public bool shouldInfluenceOriginalQuestionOption { get; set; }
}
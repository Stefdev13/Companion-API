public class StandardQuestionOption : QuestionOption
{
    public required List<SubQuestion> SubQuestions { get; set; } = [];
    public ReusableQuestionOption? ReusableQuestionOption { get; set; }
    public bool UsedInOtherSurveyQuestions { get; set; }
    public bool CloneOfOtherQuestionOption { get; set; }
    public bool ShouldInfluenceOriginalQuestionOption { get; set; }
}
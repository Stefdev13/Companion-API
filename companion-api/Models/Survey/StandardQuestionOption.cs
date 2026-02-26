public class StandardQuestionOption : QuestionOption
{
    public required List<SubQuestion> SubQuestions { get; set; } = [];
    public ReusableQuestionOption? ReusableQuestionOption { get; set; }
    public bool UsedInOtherSurveyQuestions { get; set; } = false;
    public bool CloneOfOtherQuestionOption { get; set; } = false;
    public bool ShouldInfluenceOriginalQuestionOption { get; set; } = false;
}
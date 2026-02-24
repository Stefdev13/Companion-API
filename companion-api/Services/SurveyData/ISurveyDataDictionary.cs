public interface ISurveyDataDictionary
{
    public Dictionary<string, object> SurveyData { get; set; }

    public Dictionary<string, object> getSurveyDataPoint(string route, string subQuestionKey, string dynamicParams, string country, string? region);
    public List<string> getSurveyDataKeys(string route, string subQuestionKey, string dynamicParams, string country, string? region);
    public List<QuestionOption> generateDynamicQuestionOptions(List<string> dynamicParams, string country, string? region);
}
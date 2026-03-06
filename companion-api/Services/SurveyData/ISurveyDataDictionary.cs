public interface ISurveyDataDictionary
{
    public static Dictionary<string, object> SurveyData { get; set; }

    public Dictionary<string, string>? getSurveyDataPoint(string route, string subQuestionKey, List<string>? dynamicParams, string? country, string? region);
    public List<string> getSurveyDataKeys(string route, string subQuestionKey, string dynamicParams, string country, string? region);
    public List<string> generateDynamicQuestionOptions(List<string> dynamicParams, string country, string? region);
}
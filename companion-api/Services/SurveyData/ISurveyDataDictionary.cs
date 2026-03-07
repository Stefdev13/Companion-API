public interface ISurveyDataDictionary
{
    public static Dictionary<string, object> SurveyData { get; set; } = new Dictionary<string, object>();

    public Dictionary<string, string>? getSurveyDataPoint(string route, string subQuestionKey, List<string>? dynamicParams, string? country, string? region);
    public List<string> generateDynamicQuestionOptions(List<string> dynamicParams, string country, string? region);
}
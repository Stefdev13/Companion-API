
public class V1SurveyDataDictionary : ISurveyDataDictionary
{
    public Dictionary<string, object> SurveyData { get; set; }

    public List<string> getSurveyDataKeys(string route, string subQuestionKey, string dynamicParams, string country, string? region)
    {
        throw new NotImplementedException();
    }

    public Dictionary<string, object> getSurveyDataPoint(string route, string subQuestionKey, string dynamicParams, string country, string? region)
    {
        throw new NotImplementedException();
    }
}

public class V1SurveyDataDictionary : ISurveyDataDictionary
{
    public Dictionary<string, object> SurveyData { get; set; }

    public Dictionary<string, object> getSurveyDataPoint(string route, string subQuestionKey, string dynamicParams)
    {
        throw new NotImplementedException();
    }
}
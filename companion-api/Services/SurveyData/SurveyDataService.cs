public class SurveyDataService
{
    private readonly ISurveyDataDictionary _surveyDataDictionary;

    public SurveyDataService(string version)
    {
        switch (version)
        {
            case "1":
                _surveyDataDictionary = new V1SurveyDataDictionary();
                break;
            default:
                _surveyDataDictionary = new V1SurveyDataDictionary();
                break;
        }
    }

    public Dictionary<string, object> getSurveyDataPoint(string route, string subQuestionKey, string dynamicParams, string country, string? region)
    {
        return _surveyDataDictionary.getSurveyDataPoint(route, subQuestionKey, dynamicParams, country, region);
    }
}
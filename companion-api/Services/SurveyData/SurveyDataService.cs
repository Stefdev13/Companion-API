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

    public Dictionary<string, object> getSurveyDataPoint(string route, string subQuestionKey, List<string>? dynamicParams, string? country, string? region)
    {
        return _surveyDataDictionary.getSurveyDataPoint(route, subQuestionKey, dynamicParams, country, region);
    }

    public List<QuestionOption> generateDynamicQuestionOptions(string route, List<string>? dynamicParams, string? country, string? region)
    {
        throw new NotImplementedException();
    }
}
public class SurveyModelTemplateGenerator
{
    private readonly ISurveyModelGenerator _surveyModelGenerator;

    public SurveyModelTemplateGenerator(string version)
    {
        switch (version)
        {
            case "1":
                _surveyModelGenerator = new V1SurveyModelGenerator();
                break;
            default:
                _surveyModelGenerator = new V1SurveyModelGenerator();
                break;
        }
    }

    public SurveyModel generateSurveyModel(string country, string? region)
    {
        return _surveyModelGenerator.GenerateSurveyModel(country, region);
    }
}
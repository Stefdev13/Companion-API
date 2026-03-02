using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class SurveyController : ControllerBase
{
    [HttpPost("generate/{version}")]
    public IActionResult GenerateSurveyModel([FromBody] GenerateSurveyModelRequest request)
    {
        SurveyModelTemplateGenerator templateGenerator = new SurveyModelTemplateGenerator(request.version);

        try
        {
            SurveyModel surveyModel = templateGenerator.generateSurveyModel(request.country, request.region);
            return Ok(surveyModel);
        }
        catch
        {
            return Problem();
        }
    }

    [HttpGet("averagevalue/{version}")]
    public IActionResult GetAverageValue([FromBody] GetAverageValueDTO request)
    {
        SurveyDataService dataService = new SurveyDataService(request.version);

        try
        {
            Dictionary<string, object> dataPoint = dataService.getSurveyDataPoint(request.route, request.subQuestionKey, request.dynamicParams, request.country, request.region);
            return Ok(dataPoint);
        }
        catch
        {
            return Problem();
        }
    }

    [HttpGet("dynamicquestionoptions/{version}")]
    public IActionResult GetDynamicQuestionOptions([FromBody] GetDynamicQuestionOptionsDTO request)
    {
        SurveyDataService dataService = new SurveyDataService(request.version);

        try
        {
            List<QuestionOption> questionOptions = dataService.generateDynamicQuestionOptions(request.route, request.dynamicParams, request.country, request.region);
            return Ok(questionOptions);
        }
        catch
        {
            return Problem();
        }
    }
}
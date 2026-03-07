using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class SurveyController : ControllerBase
{
    [HttpPost("generate")]
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

    [HttpGet("averagevalue")]
    public IActionResult GetAverageValue([FromQuery] GetAverageValueDTO request)
    {
        SurveyDataService dataService = new SurveyDataService(request.version);

        try
        {
            Dictionary<string, string>? dataPoint = dataService.getSurveyDataPoint(request.route, request.subQuestionKey, request.dynamicParams, request.country, request.region);
            return dataPoint != null ? Ok(dataPoint) : Problem();
        }
        catch
        {
            return Problem();
        }
    }

    [HttpGet("dynamicquestionoptions/")]
    public IActionResult GetDynamicQuestionOptions([FromQuery] GetDynamicQuestionOptionsDTO request)
    {
        SurveyDataService dataService = new SurveyDataService(request.version);

        try
        {
            List<string> questionOptions = dataService.generateDynamicQuestionOptions(request.dynamicParams, request.country, request.region);
            return Ok(questionOptions);
        }
        catch
        {
            return Problem();
        }
    }
}
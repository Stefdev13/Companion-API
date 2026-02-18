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
}
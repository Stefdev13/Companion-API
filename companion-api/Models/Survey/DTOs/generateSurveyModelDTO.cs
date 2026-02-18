public class GenerateSurveyModelRequest
{
    public required string version { get; set; }
    public required string country { get; set; }
    public string? region { get; set; }
}
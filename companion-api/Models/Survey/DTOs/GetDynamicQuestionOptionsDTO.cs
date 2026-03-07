public class GetDynamicQuestionOptionsDTO
{
    public required string version { get; set; }
    public required List<string> dynamicParams { get; set; }
    public required string country { get; set; }
    public string? region { get; set; }
}
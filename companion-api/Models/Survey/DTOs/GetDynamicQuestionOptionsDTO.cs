public class GetDynamicQuestionOptionsDTO
{
    public required string version { get; set; }
    public required string route { get; set; }
    public List<string>? dynamicParams { get; set; }
    public required string? country { get; set; }
    public string? region { get; set; }
}
public class GetAverageValueDTO
{
    public required string version { get; set; }
    public required string route { get; set; }
    public required string subQuestionKey { get; set; }
    public List<string>? dynamicParams { get; set; }
    public required string? country { get; set; }
    public string? region { get; set; }
}
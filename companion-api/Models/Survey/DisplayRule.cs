public class DisplayRule
{
    public int? Id { get; set; }
    public required SubQuestion SubQuestion { get; set; }
    public required List<string> ValidValues { get; set; }
}
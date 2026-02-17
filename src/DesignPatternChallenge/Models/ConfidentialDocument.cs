namespace DesignPatternChallenge.Models;

public class ConfidentialDocument(string id, string title, string content, int securityLevel)
{
    public string Id { get; set; } = id;
    public string Title { get; set; } = title;
    public string Content { get; set; } = content;
    public int SecurityLevel { get; set; } = securityLevel;
    public long SizeInBytes { get; set; } = content.Length * 2;
}
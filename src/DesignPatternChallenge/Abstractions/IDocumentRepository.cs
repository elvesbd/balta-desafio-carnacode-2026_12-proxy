using DesignPatternChallenge.Models;

namespace DesignPatternChallenge.Abstractions;

public interface IDocumentRepository
{
    ConfidentialDocument? GetDocument(string documentId);
    void UpdateDocument(string documentId, string newContent);
}
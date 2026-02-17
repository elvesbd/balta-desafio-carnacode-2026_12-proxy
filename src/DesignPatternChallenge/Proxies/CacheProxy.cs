using DesignPatternChallenge.Models;
using DesignPatternChallenge.Abstractions;

namespace DesignPatternChallenge.Proxies;

public class CacheProxy(IDocumentRepository repository) : IDocumentRepository
{
    private readonly IDocumentRepository _repository = repository;
    private readonly Dictionary<string, ConfidentialDocument> _cache = [];
    
    public ConfidentialDocument? GetDocument(string documentId)
    {
        if (_cache.TryGetValue(documentId, out var cached))
        {
            Console.WriteLine($"[Cache] Documento {documentId} encontrado no cache");
            return cached;
        }

        var doc = _repository.GetDocument(documentId);

        if (doc is not null)
            _cache[documentId] = doc;

        return doc;
    }

    public void UpdateDocument(string documentId, string newContent)
    {
        _repository.UpdateDocument(documentId, newContent);
        
        _cache.Remove(documentId);
        
        Console.WriteLine($"[Cache] Cache do documento {documentId} invalidado");
    }
}
using DesignPatternChallenge.Models;
using DesignPatternChallenge.Abstractions;

namespace DesignPatternChallenge.Proxies;

public class AccessControlProxy(IDocumentRepository repository, User user) : IDocumentRepository
{
    private readonly User _user = user;
    private readonly IDocumentRepository _repository = repository;
    
    public ConfidentialDocument? GetDocument(string documentId)
    {
        var doc = _repository.GetDocument(documentId);

        if (doc is null) return null;

        if (_user.ClearanceLevel < doc.SecurityLevel)
        {
            Console.WriteLine($"Acesso negado! Nível {_user.ClearanceLevel} < Requerido {doc.SecurityLevel}");
            return null;
        }

        Console.WriteLine($"Acesso permitido ao documento: {doc.Title}");
        return doc;
    }

    public void UpdateDocument(string documentId, string newContent)
    {
        var doc = _repository.GetDocument(documentId);

        if (doc is null || _user.ClearanceLevel < doc.SecurityLevel)
        {
            Console.WriteLine("Operação não autorizada");
            return;
        }

        _repository.UpdateDocument(documentId, newContent);
    }
}
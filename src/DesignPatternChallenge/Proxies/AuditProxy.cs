using DesignPatternChallenge.Models;
using DesignPatternChallenge.Abstractions;

namespace DesignPatternChallenge.Proxies;

public class AuditProxy(IDocumentRepository repository) : IDocumentRepository
{
    private readonly List<string> _auditLog = [];
    private readonly IDocumentRepository _repository = repository;

    public ConfidentialDocument? GetDocument(string documentId)
    {
        Log($"Solicitação de leitura do documento {documentId}");
        
        var doc = _repository.GetDocument(documentId);
        
        Log(doc is not null
            ? $"Documento {documentId} retornado com sucesso"
            : $"Documento {documentId} não encontrado");
        
        return doc;
    }

    public void UpdateDocument(string documentId, string newContent)
    {
        Log($"Solicitação de edição do documento {documentId}");
        
        _repository.UpdateDocument(documentId, newContent);
        
        Log($"Documento {documentId} atualizado");
    }
    
    public void ShowAuditLog()
    {
        Console.WriteLine("\n=== Log de Auditoria ===");
        
        foreach (var entry in _auditLog)
            Console.WriteLine(entry);
    }
    
    private void Log(string message)
    {
        var entry = $"[{DateTime.Now:HH:mm:ss}] {message}";
        _auditLog.Add(entry);
        Console.WriteLine($"[Audit] {entry}");
    }
}
using DesignPatternChallenge.Models;
using DesignPatternChallenge.Abstractions;

namespace DesignPatternChallenge.Repository;

public class DocumentRepository : IDocumentRepository
{
    private readonly Dictionary<string, ConfidentialDocument> _database;
    
    public DocumentRepository()
    {
        Console.WriteLine("[Repository] Inicializando conexão com banco de dados...");
        Thread.Sleep(1000);

        _database = new Dictionary<string, ConfidentialDocument>
        {
            ["DOC001"] = new("DOC001", "Relatório Financeiro Q4",
                "Conteúdo confidencial do relatório financeiro... (10 MB)", 3),
            ["DOC002"] = new("DOC002", "Estratégia de Mercado 2025",
                "Planos estratégicos confidenciais... (50 MB)", 5),
            ["DOC003"] = new("DOC003", "Manual do Funcionário",
                "Políticas e procedimentos... (2 MB)", 1)
        };

        Console.WriteLine("[Repository] Conexão estabelecida!\n");
    }

    public ConfidentialDocument? GetDocument(string documentId)
    {
        Console.WriteLine($"[Repository] Carregando documento {documentId} do banco...");
        Thread.Sleep(500);

        if (!_database.TryGetValue(documentId, out var doc)) return null;
        
        Console.WriteLine($"[Repository] Documento carregado: {doc.Title}");
        return doc;
    }

    public void UpdateDocument(string documentId, string newContent)
    {
        Console.WriteLine($"[Repository] Atualizando documento {documentId}...");
        Thread.Sleep(300);

        if (!_database.TryGetValue(documentId, out var value)) return;
        
        value.Content = newContent;
        Console.WriteLine("[Repository] Documento atualizado no banco");
    }
}
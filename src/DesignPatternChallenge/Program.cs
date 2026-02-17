using DesignPatternChallenge.Models;
using DesignPatternChallenge.Repository;
using DesignPatternChallenge.Proxies;

Console.WriteLine("=== Sistema de Documentos Confidenciais - Proxy ===\n");

// Monta a cadeia de proxies: Audit → AccessControl → Cache → Repository
var repository = new DocumentRepository();
var cache = new CacheProxy(repository);

var manager = new User("joao.silva", 5);
var employee = new User("maria.santos", 2);

// Cada usuário tem sua própria cadeia com AccessControl personalizado
var managerAccess = new AuditProxy(new AccessControlProxy(cache, manager));
var employeeAccess = new AuditProxy(new AccessControlProxy(cache, employee));

Console.WriteLine("--- Gerente acessando documento de alto nível ---");
var doc1 = managerAccess.GetDocument("DOC002");

Console.WriteLine("\n--- Funcionário tentando acessar mesmo documento ---");
var doc2 = employeeAccess.GetDocument("DOC002");

Console.WriteLine("\n--- Gerente acessando novamente (cache!) ---");
var doc3 = managerAccess.GetDocument("DOC002");

Console.WriteLine("\n--- Funcionário acessando documento permitido ---");
var doc4 = employeeAccess.GetDocument("DOC003");

Console.WriteLine("\n--- Funcionário acessando mesmo documento (cache!) ---");
var doc5 = employeeAccess.GetDocument("DOC003");

// Audit log do gerente
((AuditProxy)managerAccess).ShowAuditLog();

// Audit log do funcionário
((AuditProxy)employeeAccess).ShowAuditLog();
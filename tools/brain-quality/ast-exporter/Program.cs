// #EJC20261027_BRAIN_AST: source syntax facts, never runtime reachability claims.
using System.Security.Cryptography;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.VisualBasic;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;

if (args.Length > 0 && args[0] == "--sql") return SqlExport.Run(args.Skip(1).ToArray());

if (args.Length < 3)
{
    Console.Error.WriteLine("Usage: AstExporter <repo-root> <output-json> <project.vbproj>...");
    return 2;
}
var root = Path.GetFullPath(args[0]);
var files = new Dictionary<string, HashSet<string>>(StringComparer.OrdinalIgnoreCase);
var issues = new List<object>();
var projectFacts = new List<object>();
foreach (var project in args.Skip(2))
{
    var projectPath = Path.GetFullPath(Path.Combine(root, project));
    var document = XDocument.Load(projectPath);
    projectFacts.Add(new { path = project.Replace('\\', '/'), sha256 = Convert.ToHexStringLower(SHA256.HashData(File.ReadAllBytes(projectPath))) });
    foreach (var compile in document.Descendants().Where(e => e.Name.LocalName == "Compile"))
    {
        var include = compile.Attribute("Include")?.Value;
        if (include is null) continue;
        if (include.Contains('*') || include.Contains('?'))
        {
            issues.Add(new { project, issue = "glob-not-expanded", include });
            continue;
        }
        var path = Path.GetFullPath(Path.Combine(Path.GetDirectoryName(projectPath)!, include));
        var relative = Path.GetRelativePath(root, path);
        if (relative.StartsWith("..") || Path.IsPathRooted(relative))
        {
            issues.Add(new { project, issue = "include-outside-repo" });
            continue;
        }
        if (!path.EndsWith(".vb", StringComparison.OrdinalIgnoreCase) || Path.GetFileName(path).Equals("Reference.vb", StringComparison.OrdinalIgnoreCase)) continue;
        if (!File.Exists(path))
        {
            issues.Add(new { project, issue = "missing-compile-include", path = relative });
            continue;
        }
        if (!files.TryGetValue(path, out var owners)) files[path] = owners = new();
        owners.Add(project);
    }
}
var sourceFiles = new List<object>();
var methods = new List<object>();
foreach (var (path, owners) in files.OrderBy(p => p.Key, StringComparer.OrdinalIgnoreCase))
{
    var relative = Path.GetRelativePath(root, path).Replace('\\', '/');
    var bytes = File.ReadAllBytes(path);
    var sha256 = Convert.ToHexStringLower(SHA256.HashData(bytes));
    using var reader = new StreamReader(new MemoryStream(bytes));
    var tree = VisualBasicSyntaxTree.ParseText(reader.ReadToEnd(), path: relative);
    var syntax = tree.GetRoot();
    var errors = tree.GetDiagnostics().Count(d => d.Severity == DiagnosticSeverity.Error);
    sourceFiles.Add(new { path = relative, sha256, projects = owners.Order().ToArray(), syntax_errors = errors });
    foreach (var method in syntax.DescendantNodes().OfType<MethodBlockSyntax>())
    {
        var statement = method.SubOrFunctionStatement;
        var type = method.Ancestors().Select(n => n switch
        {
            ClassBlockSyntax c => c.ClassStatement.Identifier.ValueText,
            ModuleBlockSyntax m => m.ModuleStatement.Identifier.ValueText,
            _ => null
        }).FirstOrDefault(n => n is not null) ?? "<unknown>";
        var name = statement.Identifier.ValueText;
        var lines = method.GetLocation().GetLineSpan();
        var calls = new List<object>();
        foreach (var invocation in method.DescendantNodes().OfType<InvocationExpressionSyntax>())
        {
            string? target = null, qualifier = null;
            if (invocation.Expression is MemberAccessExpressionSyntax access)
            {
                target = access.Name.Identifier.ValueText;
                qualifier = access.Expression?.ToString();
            }
            else if (invocation.Expression is SimpleNameSyntax simple) target = simple.Identifier.ValueText;
            if (target is null) continue;
            calls.Add(new { name = target, qualifier, argument_count = invocation.ArgumentList?.Arguments.Count ?? 0,
                            line = invocation.GetLocation().GetLineSpan().StartLinePosition.Line + 1,
                            classification = "invocation-syntax; may-be-array-or-default-property" });
        }
        var sqlReferences = new List<object>();
        foreach (var literal in method.DescendantNodes().OfType<LiteralExpressionSyntax>().Where(n => n.IsKind(SyntaxKind.StringLiteralExpression)))
        {
            var value = literal.Token.ValueText;
            foreach (Match match in Regex.Matches(value, @"\b(FROM|JOIN|INSERT\s+INTO|UPDATE|DELETE\s+FROM|EXEC(?:UTE)?)\s+(?:\[?dbo\]?\.)?\[?([A-Za-z_][\w]*)\]?", RegexOptions.IgnoreCase))
                sqlReferences.Add(new { operation = match.Groups[1].Value.ToUpperInvariant(), name = match.Groups[2].Value,
                                        line = literal.GetLocation().GetLineSpan().StartLinePosition.Line + 1, confidence = "sql-literal-candidate" });
            if (Regex.IsMatch(value, @"^dbo\.[A-Za-z_]\w*$"))
                sqlReferences.Add(new { operation = "OBJECT_LITERAL", name = value,
                                        line = literal.GetLocation().GetLineSpan().StartLinePosition.Line + 1, confidence = "sql-object-literal-candidate" });
        }
        methods.Add(new { id = $"{relative}:{lines.StartLinePosition.Line + 1}:{name}", path = relative, sha256,
                          type, name, parameter_count = statement.ParameterList?.Parameters.Count ?? 0,
                          web_method = statement.AttributeLists.SelectMany(a => a.Attributes).Any(a =>
                              a.Name.ToString().EndsWith("WebMethod", StringComparison.OrdinalIgnoreCase) ||
                              a.Name.ToString().EndsWith("WebMethodAttribute", StringComparison.OrdinalIgnoreCase)),
                          line_start = lines.StartLinePosition.Line + 1, line_end = lines.EndLinePosition.Line + 1,
                          syntax_errors = errors, calls, sql_references = sqlReferences });
    }
}
var result = new { version = 1, repo_root = root, scope = "Declared Compile items; MSBuild/preprocessor conditions not evaluated; syntax facts only, no runtime execution", configuration_sources = projectFacts, files = sourceFiles, methods, issues };
Directory.CreateDirectory(Path.GetDirectoryName(Path.GetFullPath(args[1]))!);
File.WriteAllText(args[1], JsonSerializer.Serialize(result, new JsonSerializerOptions { WriteIndented = true }));
Console.WriteLine(JsonSerializer.Serialize(new { files = sourceFiles.Count, methods = methods.Count, issues = issues.Count }));
return 0;

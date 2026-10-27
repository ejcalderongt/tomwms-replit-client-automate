// #EJC20261027_BRAIN_SQL_AST: parse repository SQL; never connect or execute.
using System.Security.Cryptography;
using System.Text.Json;
using Microsoft.SqlServer.TransactSql.ScriptDom;

public static class SqlExport
{
    public static int Run(string[] args)
    {
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: AstExporter --sql <repo-root> <output-json> <file.sql>...");
            return 2;
        }
        var root = Path.GetFullPath(args[0]);
        var files = new List<object>();
        var inputs = new List<string>();
        object? inputManifest = null;
        string? inventoryPolicy = null;
        foreach (var argument in args.Skip(2))
        {
            if (!argument.StartsWith('@')) { inputs.Add(argument); continue; }
            var manifestPath = Path.GetFullPath(argument[1..]);
            var manifestBytes = File.ReadAllBytes(manifestPath);
            using var manifest = JsonDocument.Parse(manifestBytes);
            inputs.AddRange(manifest.RootElement.GetProperty("files").EnumerateArray().Select(e => e.GetString()!));
            inventoryPolicy = manifest.RootElement.GetProperty("policy").GetString();
            inputManifest = new { path = manifestPath, sha256 = Convert.ToHexStringLower(SHA256.HashData(manifestBytes)) };
        }
        foreach (var relative in inputs.Distinct(OperatingSystem.IsWindows() ? StringComparer.OrdinalIgnoreCase : StringComparer.Ordinal))
        {
            var path = Path.GetFullPath(Path.Combine(root, relative));
            if (Path.GetRelativePath(root, path).StartsWith("..")) throw new ArgumentException("SQL path outside repository");
            var parser = new TSql160Parser(true);
            var bytes = File.ReadAllBytes(path);
            using var reader = new StreamReader(new MemoryStream(bytes));
            var fragment = parser.Parse(new StringReader(reader.ReadToEnd()), out var errors);
            var visitor = new SqlFactsVisitor();
            // Malformed legacy scripts can return a null/partial tree. Preserve
            // their diagnostics but never traverse or promote that partial AST.
            if (fragment is not null && errors.Count == 0) fragment.Accept(visitor);
            files.Add(new { path = relative.Replace('\\', '/'), sha256 = Convert.ToHexStringLower(SHA256.HashData(bytes)),
                            syntax_errors = errors.Select(e => new { code = e.Number, line = e.Line }).ToArray(),
                            definitions = visitor.Definitions, references = visitor.References });
        }
        var result = new { version = 1, repo_root = root, scope = "Repository script syntax; no proof of deployment or runtime execution", inventory_policy = inventoryPolicy, input_manifest = inputManifest, files };
        Directory.CreateDirectory(Path.GetDirectoryName(Path.GetFullPath(args[1]))!);
        File.WriteAllText(args[1], JsonSerializer.Serialize(result, new JsonSerializerOptions { WriteIndented = true }));
        Console.WriteLine(JsonSerializer.Serialize(new { files = files.Count }));
        return 0;
    }
}

public sealed class SqlFactsVisitor : TSqlFragmentVisitor
{
    public List<object> Definitions { get; } = new();
    public List<object> References { get; } = new();

    private static string Name(SchemaObjectName name) => string.Join(".", name.Identifiers.Select(i => i.Value));
    private void Definition(string kind, SchemaObjectName name, TSqlFragment node) => Definitions.Add(new
    {
        kind, name = Name(name), line_start = node.StartLine,
        offset = node.StartOffset, length = node.FragmentLength
    });
    private void Reference(string relation, NamedTableReference table, TSqlFragment node) => References.Add(new
    {
        relation, name = Name(table.SchemaObject), line = node.StartLine,
        offset = node.StartOffset, length = node.FragmentLength,
        temporary = table.SchemaObject.BaseIdentifier.Value.StartsWith('#')
    });

    public override void ExplicitVisit(CreateProcedureStatement node)
    { Definition("procedure", node.ProcedureReference.Name, node); base.ExplicitVisit(node); }
    public override void ExplicitVisit(AlterProcedureStatement node)
    { Definition("procedure", node.ProcedureReference.Name, node); base.ExplicitVisit(node); }
    public override void ExplicitVisit(CreateOrAlterProcedureStatement node)
    { Definition("procedure", node.ProcedureReference.Name, node); base.ExplicitVisit(node); }
    public override void ExplicitVisit(CreateViewStatement node)
    { Definition("view", node.SchemaObjectName, node); base.ExplicitVisit(node); }
    public override void ExplicitVisit(AlterViewStatement node)
    { Definition("view", node.SchemaObjectName, node); base.ExplicitVisit(node); }
    public override void ExplicitVisit(CreateOrAlterViewStatement node)
    { Definition("view", node.SchemaObjectName, node); base.ExplicitVisit(node); }
    public override void ExplicitVisit(CreateTableStatement node)
    { Definition("table", node.SchemaObjectName, node); base.ExplicitVisit(node); }
    public override void ExplicitVisit(AlterTableAddTableElementStatement node)
    { Definition("table-alter-target", node.SchemaObjectName, node); base.ExplicitVisit(node); }
    public override void ExplicitVisit(AlterTableAlterColumnStatement node)
    { Definition("table-alter-target", node.SchemaObjectName, node); base.ExplicitVisit(node); }
    public override void ExplicitVisit(NamedTableReference node)
    { Reference("references", node, node); base.ExplicitVisit(node); }
    public override void ExplicitVisit(InsertSpecification node)
    { if (node.Target is NamedTableReference target) Reference("writes", target, node); base.ExplicitVisit(node); }
    public override void ExplicitVisit(UpdateSpecification node)
    { if (node.Target is NamedTableReference target) Reference("writes-or-alias", target, node); base.ExplicitVisit(node); }
    public override void ExplicitVisit(DeleteSpecification node)
    { if (node.Target is NamedTableReference target) Reference("writes-or-alias", target, node); base.ExplicitVisit(node); }
    public override void ExplicitVisit(MergeSpecification node)
    { if (node.Target is NamedTableReference target) Reference("writes-or-alias", target, node); base.ExplicitVisit(node); }
}

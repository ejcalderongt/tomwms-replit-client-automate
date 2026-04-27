Describe 'Test-WmsBrainSqlIsReadOnly' {
    BeforeAll {
        $modulePath = (Resolve-Path "$PSScriptRoot/../../src/WmsBrainClient.psd1").Path
        Import-Module $modulePath -Force
        # Forzar carga de los privados via dot-source en el scope de los tests
        $priv = Resolve-Path "$PSScriptRoot/../../src/Private/_SqlSafety.ps1"
        . $priv.Path
    }

    Context 'Read-only queries' {
        It 'permite SELECT simple' {
            Test-WmsBrainSqlIsReadOnly -Sql 'SELECT * FROM dbo.Foo' | Should -BeNullOrEmpty
        }
        It 'permite SELECT con WITH (CTE)' {
            $sql = 'WITH x AS (SELECT 1 a) SELECT a FROM x'
            Test-WmsBrainSqlIsReadOnly -Sql $sql | Should -BeNullOrEmpty
        }
        It 'permite SELECT con joins y comentarios' {
            $sql = @"
-- comentario inocuo: INSERT INTO foo
/* tambien block: DELETE FROM bar */
SELECT a.id, b.name
FROM dbo.A a
INNER JOIN dbo.B b ON b.id = a.b_id
WHERE a.fec_agr >= '2026-01-01'
"@
            Test-WmsBrainSqlIsReadOnly -Sql $sql | Should -BeNullOrEmpty
        }
        It 'permite SELECT con literal que contiene la palabra DELETE' {
            $sql = "SELECT 'DELETE FROM nada' AS msg"
            Test-WmsBrainSqlIsReadOnly -Sql $sql | Should -BeNullOrEmpty
        }
    }

    Context 'DML/DDL detection' {
        $cases = @(
            @{ Sql = 'INSERT INTO dbo.Foo VALUES (1)';     Hit = 'INSERT' }
            @{ Sql = 'UPDATE dbo.Foo SET a=1';             Hit = 'UPDATE' }
            @{ Sql = 'DELETE FROM dbo.Foo';                Hit = 'DELETE' }
            @{ Sql = 'MERGE dbo.Foo AS t USING dbo.Bar';   Hit = 'MERGE' }
            @{ Sql = 'TRUNCATE TABLE dbo.Foo';             Hit = 'TRUNCATE' }
            @{ Sql = 'DROP TABLE dbo.Foo';                 Hit = 'DROP' }
            @{ Sql = 'ALTER TABLE dbo.Foo ADD col INT';    Hit = 'ALTER' }
            @{ Sql = 'CREATE TABLE dbo.Foo (id INT)';      Hit = 'CREATE' }
            @{ Sql = 'GRANT SELECT ON dbo.Foo TO bob';     Hit = 'GRANT' }
            @{ Sql = 'REVOKE SELECT ON dbo.Foo FROM bob';  Hit = 'REVOKE' }
            @{ Sql = 'EXEC dbo.usp_Foo';                   Hit = 'EXEC' }
            @{ Sql = 'EXECUTE dbo.usp_Foo';                Hit = 'EXECUTE' }
            @{ Sql = 'sp_who2';                            Hit = 'sp_' }
            @{ Sql = 'xp_cmdshell ''dir''';                Hit = 'xp_' }
        )
        It 'detecta <Hit>' -TestCases $cases {
            param($Sql, $Hit)
            (Test-WmsBrainSqlIsReadOnly -Sql $Sql) | Should -Be $Hit
        }
    }

    Context 'Assert-WmsBrainSqlIsReadOnly' {
        It 'no tira en SELECT' {
            { Assert-WmsBrainSqlIsReadOnly -Sql 'SELECT 1' } | Should -Not -Throw
        }
        It 'tira en DML' {
            { Assert-WmsBrainSqlIsReadOnly -Sql 'DELETE FROM foo' } | Should -Throw
        }
    }
}

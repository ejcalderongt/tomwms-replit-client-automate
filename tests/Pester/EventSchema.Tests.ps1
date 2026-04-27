Describe 'Test-WmsBrainEventShape (schema v1)' {
    BeforeAll {
        $modulePath = (Resolve-Path "$PSScriptRoot/../../src/WmsBrainClient.psd1").Path
        Import-Module $modulePath -Force
        . (Resolve-Path "$PSScriptRoot/../../src/Private/_BrainEventSchema.ps1").Path
    }

    It 'evento minimo valido no tira errores' {
        $ev = [PSCustomObject]@{
            id             = '20260427-1530-EJC'
            schema_version = '1'
            created_at     = '2026-04-27T15:30:00-03:00'
            type           = 'apply_succeeded'
            source         = 'apply_bundle'
            ref            = [PSCustomObject]@{ bundle = 'v23' }
            context        = [PSCustomObject]@{ message = 'ok' }
            status         = 'pending'
            history        = @(@{ at = '2026-04-27T15:30:00-03:00'; action = 'notify'; by = 'EJC' })
        }
        $errors = Test-WmsBrainEventShape -Event $ev -SchemaVersion '1'
        $errors.Count | Should -Be 0
    }

    It 'flagea type invalido en v1' {
        $ev = [PSCustomObject]@{
            id             = '20260427-1530-EJC'
            schema_version = '1'
            created_at     = '2026-04-27T15:30:00-03:00'
            type           = 'question_request'   # invalido en v1
            source         = 'openclaw'
            ref            = [PSCustomObject]@{}
            context        = [PSCustomObject]@{}
            status         = 'pending'
            history        = @()
        }
        $errors = Test-WmsBrainEventShape -Event $ev -SchemaVersion '1'
        $errors.Count | Should -BeGreaterThan 0
        ($errors -join ';') | Should -Match 'type invalido'
    }

    It 'flagea status invalido en v1' {
        $ev = [PSCustomObject]@{
            id             = '20260427-1530-EJC'
            schema_version = '1'
            created_at     = '2026-04-27T15:30:00-03:00'
            type           = 'directive'
            source         = 'openclaw'
            ref            = [PSCustomObject]@{}
            context        = [PSCustomObject]@{}
            status         = 'answered'    # invalido en v1
            history        = @()
        }
        $errors = Test-WmsBrainEventShape -Event $ev -SchemaVersion '1'
        ($errors -join ';') | Should -Match 'status invalido'
    }

    It 'flagea id mal formado' {
        $ev = [PSCustomObject]@{
            id             = 'no-respeta-formato'
            schema_version = '1'
            created_at     = '2026-04-27T15:30:00-03:00'
            type           = 'directive'
            source         = 'openclaw'
            ref            = [PSCustomObject]@{}
            context        = [PSCustomObject]@{}
            status         = 'pending'
            history        = @()
        }
        $errors = Test-WmsBrainEventShape -Event $ev -SchemaVersion '1'
        ($errors -join ';') | Should -Match 'id no respeta formato'
    }

    It 'acepta question_request en v2' {
        $ev = [PSCustomObject]@{
            id             = '20260427-1530-EJC'
            schema_version = '2'
            created_at     = '2026-04-27T15:30:00-03:00'
            type           = 'question_request'
            source         = 'openclaw'
            ref            = [PSCustomObject]@{}
            context        = [PSCustomObject]@{}
            status         = 'pending'
            history        = @()
        }
        $errors = Test-WmsBrainEventShape -Event $ev -SchemaVersion '2'
        $errors.Count | Should -Be 0
    }
}

Describe 'Get-WmsBrainBridgeSchemaVersion' {
    BeforeAll {
        $modulePath = (Resolve-Path "$PSScriptRoot/../../src/WmsBrainClient.psd1").Path
        Import-Module $modulePath -Force
        . (Resolve-Path "$PSScriptRoot/../../src/Private/_BrainEventSchema.ps1").Path
    }
    It 'lee la constante SCHEMA_VERSION del .mjs' {
        $tmp = New-TemporaryFile
        try {
            $content = @"
// fake brain_bridge.mjs
import x from 'y';
const SCHEMA_VERSION = "1";
const VALID_TYPES = ["apply_succeeded"];
"@
            Set-Content -LiteralPath $tmp -Value $content
            (Get-WmsBrainBridgeSchemaVersion -BrainBridgeMjsPath $tmp) | Should -Be '1'
        } finally {
            Remove-Item -LiteralPath $tmp -Force
        }
    }
    It 'tira si no encuentra la constante' {
        $tmp = New-TemporaryFile
        try {
            Set-Content -LiteralPath $tmp -Value 'sin schema_version aca'
            { Get-WmsBrainBridgeSchemaVersion -BrainBridgeMjsPath $tmp } | Should -Throw
        } finally {
            Remove-Item -LiteralPath $tmp -Force
        }
    }
}

Describe 'Test-WmsBrainEventShape — schema_version value enforcement' {
    BeforeAll {
        . (Resolve-Path "$PSScriptRoot/../../src/Private/_BrainEventSchema.ps1").Path
    }

    It 'rechaza evento con schema_version=2 cuando se pide v1' {
        $e = [PSCustomObject]@{
            id             = '20260427-1845-EJC'
            schema_version = '2'
            created_at     = '2026-04-27T18:45:00Z'
            type           = 'apply_succeeded'
            source         = 'apply_bundle'
            ref            = [PSCustomObject]@{}
            context        = [PSCustomObject]@{}
            status         = 'pending'
            history        = @()
        }
        $errs = @(Test-WmsBrainEventShape -Event $e -SchemaVersion '1')
        ($errs -join '|') | Should -Match "schema_version='2' no coincide con esperado '1'"
    }

    It 'acepta evento con schema_version=1 cuando se pide v1' {
        $e = [PSCustomObject]@{
            id             = '20260427-1845-EJC'
            schema_version = '1'
            created_at     = '2026-04-27T18:45:00Z'
            type           = 'apply_succeeded'
            source         = 'apply_bundle'
            ref            = [PSCustomObject]@{}
            context        = [PSCustomObject]@{}
            status         = 'pending'
            history        = @()
        }
        $errs = @(Test-WmsBrainEventShape -Event $e -SchemaVersion '1')
        ($errs | Where-Object { $_ -match 'schema_version' }).Count | Should -Be 0
    }

    It 'AllowDraft tolera schema_version ausente pero rechaza valor incorrecto si esta presente' {
        $bad = [PSCustomObject]@{
            schema_version = '99'
            type    = 'apply_succeeded'
            source  = 'apply_bundle'
            status  = 'draft'
            ref     = [PSCustomObject]@{}
            context = [PSCustomObject]@{}
        }
        $errs = @(Test-WmsBrainEventShape -Event $bad -SchemaVersion '1' -AllowDraft)
        ($errs -join '|') | Should -Match "schema_version='99'"

        $okDraft = [PSCustomObject]@{
            type    = 'apply_succeeded'
            source  = 'apply_bundle'
            status  = 'draft'
            ref     = [PSCustomObject]@{}
            context = [PSCustomObject]@{}
        }
        $errs2 = @(Test-WmsBrainEventShape -Event $okDraft -SchemaVersion '1' -AllowDraft)
        ($errs2 | Where-Object { $_ -match 'schema_version' }).Count | Should -Be 0
    }
}

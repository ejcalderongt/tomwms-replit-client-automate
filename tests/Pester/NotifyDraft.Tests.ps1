Describe 'Test-WmsBrainEventShape -AllowDraft (apply_bundle.mjs draft compatibility)' {
    BeforeAll {
        $modulePath = (Resolve-Path "$PSScriptRoot/../../src/WmsBrainClient.psd1").Path
        Import-Module $modulePath -Force
        . (Resolve-Path "$PSScriptRoot/../../src/Private/_BrainEventSchema.ps1").Path
    }

    It 'rechaza un draft sin -AllowDraft (faltan id/status/created_at/history)' {
        $draft = [PSCustomObject]@{
            schema_version = '1'
            type   = 'apply_succeeded'
            source = 'apply_bundle'
            status = 'draft'
            ref    = [PSCustomObject]@{ bundle = 'v23'; commit_sha = 'abc1234'; modules_touched = @(); files_changed = @(); pr_or_branch = $null }
            context = [PSCustomObject]@{ message = 'v23 OK'; tags = @(); modules_touched = @() }
        }
        $errs = @(Test-WmsBrainEventShape -Event $draft -SchemaVersion '1')
        $errs.Count | Should -BeGreaterThan 0
        ($errs -join '|') | Should -Match 'falta campo obligatorio: id'
    }

    It 'acepta el mismo draft con -AllowDraft (status=draft, sin id, sin history)' {
        $draft = [PSCustomObject]@{
            schema_version = '1'
            type   = 'apply_succeeded'
            source = 'apply_bundle'
            status = 'draft'
            ref    = [PSCustomObject]@{ bundle = 'v23'; commit_sha = 'abc1234'; modules_touched = @(); files_changed = @(); pr_or_branch = $null }
            context = [PSCustomObject]@{ message = 'v23 OK'; tags = @(); modules_touched = @() }
        }
        $errs = @(Test-WmsBrainEventShape -Event $draft -SchemaVersion '1' -AllowDraft)
        $errs.Count | Should -Be 0
    }

    It 'AllowDraft sigue rechazando type invalido' {
        $bad = [PSCustomObject]@{
            type = 'frankenstein'
            source = 'apply_bundle'
            status = 'draft'
            ref = [PSCustomObject]@{}
            context = [PSCustomObject]@{}
        }
        $errs = @(Test-WmsBrainEventShape -Event $bad -SchemaVersion '1' -AllowDraft)
        ($errs -join '|') | Should -Match "type invalido 'frankenstein'"
    }

    It 'Test-WmsBrainEventIsDraft detecta status=draft' {
        $e = [PSCustomObject]@{ id = '20260427-1845-EJC'; status = 'draft' }
        Test-WmsBrainEventIsDraft -Event $e | Should -BeTrue
    }

    It 'Test-WmsBrainEventIsDraft detecta id vacio aunque status sea valido' {
        $e = [PSCustomObject]@{ id = ''; status = 'pending' }
        Test-WmsBrainEventIsDraft -Event $e | Should -BeTrue
    }

    It 'Test-WmsBrainEventIsDraft devuelve false para evento completo' {
        $e = [PSCustomObject]@{ id = '20260427-1845-EJC'; status = 'pending' }
        Test-WmsBrainEventIsDraft -Event $e | Should -BeFalse
    }
}

Describe 'Invoke-WmsBrainNotify validation gate (sin invocar node)' {
    BeforeAll {
        $modulePath = (Resolve-Path "$PSScriptRoot/../../src/WmsBrainClient.psd1").Path
        Import-Module $modulePath -Force
        $script:tmp = Join-Path ([System.IO.Path]::GetTempPath()) ("WmsNotify-" + [Guid]::NewGuid().ToString('N').Substring(0,8))
        New-Item -ItemType Directory -Path $script:tmp -Force | Out-Null
    }
    AfterAll {
        if (Test-Path -LiteralPath $script:tmp) { Remove-Item -LiteralPath $script:tmp -Recurse -Force -ErrorAction SilentlyContinue }
    }

    It 'declara -SupportsShouldProcess y -ConfirmImpact High' {
        $cmd = Get-Command -Module WmsBrainClient -Name Invoke-WmsBrainNotify
        $cmd.Parameters.Keys | Should -Contain 'WhatIf'
        $cmd.Parameters.Keys | Should -Contain 'Confirm'
    }

    It 'tira [2] si el FromEventFile no existe (antes de cualquier validacion de schema)' {
        { Invoke-WmsBrainNotify -ExchangeRepo $script:tmp -FromEventFile (Join-Path $script:tmp 'no-existe.json') -NoPush -Confirm:$false } |
            Should -Throw '*FromEventFile no existe*'
    }
}

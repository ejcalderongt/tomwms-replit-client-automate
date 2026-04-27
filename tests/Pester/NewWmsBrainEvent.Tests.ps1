Describe 'New-WmsBrainEvent' {
    BeforeAll {
        $modulePath = (Resolve-Path "$PSScriptRoot/../../src/WmsBrainClient.psd1").Path
        Import-Module $modulePath -Force
        $script:tempDir = Join-Path ([System.IO.Path]::GetTempPath()) ("WmsBrainTests-" + [Guid]::NewGuid().ToString('N').Substring(0,8))
        New-Item -ItemType Directory -Path $script:tempDir -Force | Out-Null
    }
    AfterAll {
        if (Test-Path -LiteralPath $script:tempDir) {
            Remove-Item -LiteralPath $script:tempDir -Recurse -Force -ErrorAction SilentlyContinue
        }
    }

    It 'genera evento apply_succeeded valido y lo escribe a disco' {
        $r = New-WmsBrainEvent -Type apply_succeeded -Source apply_bundle `
            -Message 'test ok' -Bundle 'v99' -CommitSha 'deadbeef' `
            -RamaDestino 'dev_test' -Tags @('test') -Modules @('mod1') `
            -Author 'TST' -OutputDir $script:tempDir -Confirm:$false
        $r.Id | Should -Match '^\d{8}-\d{4}-TST$'
        $r.Path | Should -Exist
        $json = Get-Content -LiteralPath $r.Path -Raw | ConvertFrom-Json
        $json.schema_version | Should -Be '1'
        $json.type           | Should -Be 'apply_succeeded'
        $json.source         | Should -Be 'apply_bundle'
        $json.status         | Should -Be 'pending'
        $json.ref.bundle     | Should -Be 'v99'
        $json.ref.commit_sha | Should -Be 'deadbeef'
        $json.ref.rama_destino | Should -Be 'dev_test'
        $json.context.message | Should -Be 'test ok'
        ($json.context.tags -join ',') | Should -Be 'test'
        ($json.context.modules_touched -join ',') | Should -Be 'mod1'
        $json.history[0].action | Should -Be 'notify'
        $json.history[0].by     | Should -Be 'TST'
    }

    It 'evita colision regenerando id con +1 minuto' {
        $existingId = ('{0:yyyyMMdd-HHmm}-TST' -f (Get-Date))
        New-Item -ItemType File -Path (Join-Path $script:tempDir "$existingId.json") -Force | Out-Null
        $r = New-WmsBrainEvent -Type directive -Source openclaw `
            -Message 'collide' -Author 'TST' -OutputDir $script:tempDir -Confirm:$false
        $r.Id | Should -Not -Be $existingId
    }

    It 'rechaza tipo invalido (validacion del param)' {
        { New-WmsBrainEvent -Type 'no_existe' -Source openclaw -Message 'x' -OutputDir $script:tempDir -Confirm:$false } | Should -Throw
    }

    It 'incluye host del equipo' {
        $r = New-WmsBrainEvent -Type directive -Source manual -Message 'host check' `
            -Author 'TST' -OutputDir $script:tempDir -Confirm:$false
        $json = Get-Content -LiteralPath $r.Path -Raw | ConvertFrom-Json
        $json.host | Should -Be ([Environment]::MachineName)
    }
}

Describe 'New-WmsBrainAnswerEvent' {
    BeforeAll {
        $modulePath = (Resolve-Path "$PSScriptRoot/../../src/WmsBrainClient.psd1").Path
        Import-Module $modulePath -Force
        $script:tmp = Join-Path ([System.IO.Path]::GetTempPath()) ("WmsAns-" + [Guid]::NewGuid().ToString('N').Substring(0,8))
        New-Item -ItemType Directory -Path $script:tmp -Force | Out-Null
    }
    AfterAll {
        if (Test-Path -LiteralPath $script:tmp) { Remove-Item -LiteralPath $script:tmp -Recurse -Force -ErrorAction SilentlyContinue }
    }

    It 'rechaza AnswerId mal formado' {
        { New-WmsBrainAnswerEvent -QuestionId 'Q-001' -AnswerId 'X-1' -AnswerFile 'A-001-q-001.md' `
            -Verdict confirmed -Confidence high -OutputDir $script:tmp -Confirm:$false } | Should -Throw '*AnswerId invalido*'
    }

    It 'rechaza QuestionId mal formado' {
        { New-WmsBrainAnswerEvent -QuestionId 'Q1' -AnswerId 'A-001' -AnswerFile 'A-001-q-001.md' `
            -Verdict confirmed -Confidence high -OutputDir $script:tmp -Confirm:$false } | Should -Throw '*QuestionId invalido*'
    }

    It 'emite evento directive con tags answer/A-NNN/Q-NNN y FilesChanged correcto' {
        $r = New-WmsBrainAnswerEvent -QuestionId 'Q-003' -AnswerId 'A-007' `
            -AnswerFile 'A-007-q-003.md' -Verdict partial -Confidence medium `
            -Author 'TST' -OutputDir $script:tmp -Confirm:$false -ExtraTags @('BB')
        $r.AnswerId   | Should -Be 'A-007'
        $r.QuestionId | Should -Be 'Q-003'
        $r.Path       | Should -Exist
        $json = Get-Content -LiteralPath $r.Path -Raw | ConvertFrom-Json
        $json.type   | Should -Be 'directive'
        $json.source | Should -Be 'openclaw'
        ($json.context.tags | Sort-Object) -join ',' | Should -Be (@('A-007','BB','Q-003','answer') -join ',')
        ($json.ref.files_changed -join ',') | Should -Be 'wms-brain-client/answers/A-007-q-003.md'
        $json.context.message | Should -Match 'Q-003'
        $json.context.message | Should -Match 'partial'
        $json.context.message | Should -Match 'medium'
    }
}

Describe 'New-WmsBrainLearningEvent' {
    BeforeAll {
        $modulePath = (Resolve-Path "$PSScriptRoot/../../src/WmsBrainClient.psd1").Path
        Import-Module $modulePath -Force
        $script:tmp2 = Join-Path ([System.IO.Path]::GetTempPath()) ("WmsLrn-" + [Guid]::NewGuid().ToString('N').Substring(0,8))
        New-Item -ItemType Directory -Path $script:tmp2 -Force | Out-Null
    }
    AfterAll {
        if (Test-Path -LiteralPath $script:tmp2) { Remove-Item -LiteralPath $script:tmp2 -Recurse -Force -ErrorAction SilentlyContinue }
    }

    It 'requiere -Title si no hay -LearningCardPath' {
        { New-WmsBrainLearningEvent -Scope BB -OutputDir $script:tmp2 -Confirm:$false } | Should -Throw '*Title*obligatorio*'
    }

    It 'genera LearningId default L-yyyyMMdd-HHmm si no se pasa' {
        $r = New-WmsBrainLearningEvent -Title 'algo' -Scope OPS `
            -Author 'TST' -OutputDir $script:tmp2 -Confirm:$false
        $r.LearningId | Should -Match '^L-\d{8}-\d{4}$'
    }

    It 'rechaza SourceQuestionId mal formado' {
        { New-WmsBrainLearningEvent -Title 'x' -Scope BB -SourceQuestionId 'foo' `
            -OutputDir $script:tmp2 -Confirm:$false } | Should -Throw '*SourceQuestionId invalido*'
    }

    It 'tags incluyen learning/L-NNN/scope y modules_touched cuando scope es codename' {
        $r = New-WmsBrainLearningEvent -LearningId 'L-TEST-1' -Title 'navsync 2min' `
            -Scope BB -SourceQuestionId 'Q-001' -Author 'TST' -OutputDir $script:tmp2 -Confirm:$false
        $r.LearningId | Should -Be 'L-TEST-1'
        $json = Get-Content -LiteralPath $r.Path -Raw | ConvertFrom-Json
        $json.type | Should -Be 'directive'
        ($json.context.tags | Sort-Object) -join ',' | Should -Be (@('BB','L-TEST-1','Q-001','learning') -join ',')
        ($json.context.modules_touched -join ',') | Should -Be 'BB'
        $json.context.message | Should -Match 'L-TEST-1'
        $json.context.message | Should -Match 'Q-001'
    }

    It 'no setea modules_touched si scope es ALL/OPS/BRIDGE' {
        $r = New-WmsBrainLearningEvent -LearningId 'L-TEST-2' -Title 'ops only' `
            -Scope OPS -Author 'TST' -OutputDir $script:tmp2 -Confirm:$false
        $json = Get-Content -LiteralPath $r.Path -Raw | ConvertFrom-Json
        @($json.context.modules_touched).Count | Should -Be 0
    }
}

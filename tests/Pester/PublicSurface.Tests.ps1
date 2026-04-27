Describe 'Public surface: cmdlets exportados y SupportsShouldProcess' {
    BeforeAll {
        $modulePath = (Resolve-Path "$PSScriptRoot/../../src/WmsBrainClient.psd1").Path
        Import-Module $modulePath -Force
    }

    $expected = @(
        'Invoke-WmsBrainHello',
        'Invoke-WmsBrainBootstrap',
        'Test-WmsBrainEnvironment',
        'Invoke-WmsBrainApplyBundle',
        'Get-WmsBrainBundleHistory',
        'New-WmsBrainEvent',
        'New-WmsBrainQuestionEvent',
        'New-WmsBrainAnswerEvent',
        'New-WmsBrainLearningEvent',
        'Invoke-WmsBrainNotify',
        'Get-WmsBrainEventQueue',
        'Get-WmsBrainEvent',
        'Get-WmsBrainConnectionString',
        'Invoke-WmsBrainQuery',
        'Test-WmsBrainConnection',
        'Get-WmsBrainSuiteList',
        'Invoke-WmsBrainSuite',
        'Invoke-WmsBrainScenario',
        'Get-WmsBrainQuestion',
        'Show-WmsBrainQuestion',
        'Invoke-WmsBrainQuestion',
        'Submit-WmsBrainAnswer',
        'Show-WmsBrainStatus'
    )

    It 'exporta los 23 cmdlets esperados' {
        $exported = (Get-Command -Module WmsBrainClient | Where-Object { $_.CommandType -eq 'Function' } | Sort-Object Name).Name
        $exported.Count | Should -Be $expected.Count
        foreach ($e in $expected) {
            $exported | Should -Contain $e
        }
    }

    It 'cmdlets de escritura declaran SupportsShouldProcess' {
        $writers = @(
            'New-WmsBrainEvent',
            'New-WmsBrainQuestionEvent',
            'New-WmsBrainAnswerEvent',
            'New-WmsBrainLearningEvent',
            'Invoke-WmsBrainNotify',
            'Invoke-WmsBrainApplyBundle',
            'Submit-WmsBrainAnswer'
        )
        foreach ($w in $writers) {
            $cmd = Get-Command -Module WmsBrainClient -Name $w
            $cmd | Should -Not -BeNullOrEmpty
            $cmd.Parameters.Keys | Should -Contain 'WhatIf'
            $cmd.Parameters.Keys | Should -Contain 'Confirm'
        }
    }

    It 'cmdlets de lectura tienen Get-Help no vacio' {
        $readers = @(
            'Get-WmsBrainEventQueue',
            'Get-WmsBrainEvent',
            'Get-WmsBrainBundleHistory',
            'Get-WmsBrainConnectionString',
            'Get-WmsBrainQuestion',
            'Get-WmsBrainSuiteList',
            'Show-WmsBrainStatus',
            'Test-WmsBrainEnvironment'
        )
        foreach ($r in $readers) {
            $h = Get-Help $r -ErrorAction SilentlyContinue
            $h | Should -Not -BeNullOrEmpty
            $h.Synopsis | Should -Not -BeNullOrEmpty
        }
    }

    It 'alias wmsbc apunta a Show-WmsBrainStatus' {
        $a = Get-Alias -Name 'wmsbc' -ErrorAction SilentlyContinue
        $a | Should -Not -BeNullOrEmpty
        $a.Definition | Should -Be 'Show-WmsBrainStatus'
    }
}

Describe 'WmsBrainClient module load' {
    BeforeAll {
        $script:modulePath = (Resolve-Path "$PSScriptRoot/../../src/WmsBrainClient.psd1").Path
    }

    It 'manifest valida con Test-ModuleManifest' {
        $m = Test-ModuleManifest -Path $script:modulePath
        $m.Name    | Should -Be 'WmsBrainClient'
        $m.Version | Should -Be '0.2.0'
    }

    It 'importa sin errores' {
        Import-Module $script:modulePath -Force
        Get-Module WmsBrainClient | Should -Not -BeNullOrEmpty
    }

    It 'exporta los 23 cmdlets esperados' {
        Import-Module $script:modulePath -Force
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
        $actual = (Get-Command -Module WmsBrainClient -CommandType Function).Name
        foreach ($e in $expected) {
            $actual | Should -Contain $e
        }
        $actual.Count | Should -Be $expected.Count
    }

    It 'cada cmdlet tiene comment-based help' {
        Import-Module $script:modulePath -Force
        $cmds = Get-Command -Module WmsBrainClient -CommandType Function
        foreach ($c in $cmds) {
            $h = Get-Help $c.Name -ErrorAction SilentlyContinue
            $h.Synopsis | Should -Not -BeNullOrEmpty -Because "el cmdlet $($c.Name) deberia tener .SYNOPSIS"
        }
    }
}

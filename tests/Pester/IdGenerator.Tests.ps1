Describe 'New-WmsBrainEventId' {
    BeforeAll {
        $modulePath = (Resolve-Path "$PSScriptRoot/../../src/WmsBrainClient.psd1").Path
        Import-Module $modulePath -Force
        . (Resolve-Path "$PSScriptRoot/../../src/Private/_IdGenerator.ps1").Path
    }

    It 'genera id formato YYYYMMDD-HHMM-INIT' {
        $at = Get-Date -Year 2026 -Month 4 -Day 27 -Hour 15 -Minute 30 -Second 0
        $id = New-WmsBrainEventId -Initials 'EJC' -At $at
        $id | Should -Be '20260427-1530-EJC'
    }

    It 'fuerza upper-case y limita a 4 chars' {
        $at = Get-Date -Year 2026 -Month 4 -Day 27 -Hour 15 -Minute 30 -Second 0
        $id = New-WmsBrainEventId -Initials 'erikxyz' -At $at
        $id | Should -Match '^20260427-1530-[A-Z]{1,4}$'
    }

    It 'desplaza +1 minuto si colisiona' {
        $at = Get-Date -Year 2026 -Month 4 -Day 27 -Hour 15 -Minute 30 -Second 0
        $id = New-WmsBrainEventId -Initials 'EJC' -ExistingIds @('20260427-1530-EJC') -At $at
        $id | Should -Be '20260427-1531-EJC'
    }

    It 'cae al default EJC si no se provee Initials ni env var' {
        $prev = $env:WMS_BRAIN_AUTHOR_INIT
        try {
            $env:WMS_BRAIN_AUTHOR_INIT = $null
            $at = Get-Date -Year 2026 -Month 1 -Day 1 -Hour 0 -Minute 0 -Second 0
            $id = New-WmsBrainEventId -At $at
            $id | Should -Be '20260101-0000-EJC'
        } finally {
            $env:WMS_BRAIN_AUTHOR_INIT = $prev
        }
    }
}

Describe 'Get-WmsBrainIsoLocal' {
    BeforeAll {
        $modulePath = (Resolve-Path "$PSScriptRoot/../../src/WmsBrainClient.psd1").Path
        Import-Module $modulePath -Force
        . (Resolve-Path "$PSScriptRoot/../../src/Private/_IdGenerator.ps1").Path
    }
    It 'devuelve ISO con offset de zona' {
        $at = Get-Date -Year 2026 -Month 4 -Day 27 -Hour 15 -Minute 30 -Second 0
        $iso = Get-WmsBrainIsoLocal -At $at
        $iso | Should -Match '^2026-04-27T15:30:00[+\-]\d{2}:\d{2}$'
    }
}

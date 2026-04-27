Describe 'Get-WmsBrainEventQueue (parsing JSON del bridge)' {
    BeforeAll {
        $modulePath = (Resolve-Path "$PSScriptRoot/../../src/WmsBrainClient.psd1").Path
        Import-Module $modulePath -Force
    }

    It 'el cmdlet existe y declara los parametros documentados' {
        $cmd = Get-Command -Module WmsBrainClient -Name Get-WmsBrainEventQueue
        $cmd | Should -Not -BeNullOrEmpty
        $cmd.Parameters.Keys | Should -Contain 'ExchangeRepo'
        $cmd.Parameters.Keys | Should -Contain 'Status'
        $cmd.Parameters.Keys | Should -Contain 'Tag'
        $cmd.Parameters.Keys | Should -Contain 'Type'
    }

    It 'Status valida solo los enums permitidos' {
        $cmd = Get-Command -Module WmsBrainClient -Name Get-WmsBrainEventQueue
        $statusParam = $cmd.Parameters['Status']
        $vSet = $statusParam.Attributes | Where-Object { $_.GetType().Name -eq 'ValidateSetAttribute' } | Select-Object -First 1
        $vSet | Should -Not -BeNullOrEmpty
        $vSet.ValidValues | Should -Contain 'pending'
        $vSet.ValidValues | Should -Contain 'analyzed'
        $vSet.ValidValues | Should -Contain 'proposed'
        $vSet.ValidValues | Should -Contain 'applied'
        $vSet.ValidValues | Should -Contain 'skipped'
    }
}

Describe 'Get-WmsBrainEvent (lookup por id)' {
    BeforeAll {
        $modulePath = (Resolve-Path "$PSScriptRoot/../../src/WmsBrainClient.psd1").Path
        Import-Module $modulePath -Force
    }

    It 'declara -Id como mandatory' {
        $cmd = Get-Command -Module WmsBrainClient -Name Get-WmsBrainEvent
        $cmd | Should -Not -BeNullOrEmpty
        $idParam = $cmd.Parameters['Id']
        $mAttr = $idParam.Attributes | Where-Object { $_.GetType().Name -eq 'ParameterAttribute' } | Select-Object -First 1
        $mAttr.Mandatory | Should -Be $true
    }
}

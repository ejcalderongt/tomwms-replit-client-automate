Describe 'Get-WmsBrainKnownProfile' {
    BeforeAll {
        $modulePath = (Resolve-Path "$PSScriptRoot/../../src/WmsBrainClient.psd1").Path
        Import-Module $modulePath -Force
        . (Resolve-Path "$PSScriptRoot/../../src/Private/_ProfileResolver.ps1").Path
    }

    It 'devuelve los 4 perfiles validos' {
        (Get-WmsBrainProfileNames | Sort-Object) -join ',' | Should -Be 'BB-PRD,C9-QAS,K7-PRD,LOCAL_DEV'
    }

    It 'K7-PRD apunta a server productivo correcto' {
        $p = Get-WmsBrainKnownProfile -Name 'K7-PRD'
        $p.Server      | Should -Be '52.41.114.122,1437'
        $p.Database    | Should -Be 'TOMWMS_KILLIOS_PRD'
        $p.UserName    | Should -Be 'wmsuser'
        $p.PasswordVar | Should -Be 'WMS_KILLIOS_DB_PASSWORD'
        $p.Trusted     | Should -BeFalse
    }
    It 'BB-PRD apunta a IMS4MB_BYB_PRD' {
        (Get-WmsBrainKnownProfile -Name 'BB-PRD').Database | Should -Be 'IMS4MB_BYB_PRD'
    }
    It 'C9-QAS apunta a IMS4MB_CEALSA_QAS' {
        (Get-WmsBrainKnownProfile -Name 'C9-QAS').Database | Should -Be 'IMS4MB_CEALSA_QAS'
    }
    It 'LOCAL_DEV es trusted' {
        (Get-WmsBrainKnownProfile -Name 'LOCAL_DEV').Trusted | Should -BeTrue
    }

    It 'tira si el perfil es desconocido' {
        { Get-WmsBrainKnownProfile -Name 'NO_EXISTE' } | Should -Throw
    }
}

Describe 'Resolve-WmsBrainSqlcmdParams' {
    BeforeAll {
        $modulePath = (Resolve-Path "$PSScriptRoot/../../src/WmsBrainClient.psd1").Path
        Import-Module $modulePath -Force
        . (Resolve-Path "$PSScriptRoot/../../src/Private/_ProfileResolver.ps1").Path
    }
    It 'tira si la var de password no esta seteada' {
        $prev = $env:WMS_KILLIOS_DB_PASSWORD
        try {
            $env:WMS_KILLIOS_DB_PASSWORD = $null
            $p = Get-WmsBrainKnownProfile -Name 'K7-PRD'
            { Resolve-WmsBrainSqlcmdParams -Profile $p } | Should -Throw
        } finally {
            $env:WMS_KILLIOS_DB_PASSWORD = $prev
        }
    }
    It 'arma hash splat-eable cuando la pass existe' {
        $prev = $env:WMS_KILLIOS_DB_PASSWORD
        try {
            $env:WMS_KILLIOS_DB_PASSWORD = 'fakepass-NOTREAL'
            $p = Get-WmsBrainKnownProfile -Name 'BB-PRD'
            $h = Resolve-WmsBrainSqlcmdParams -Profile $p -Query 'SELECT 1'
            $h['ServerInstance'] | Should -Be '52.41.114.122,1437'
            $h['Database']       | Should -Be 'IMS4MB_BYB_PRD'
            $h['Username']       | Should -Be 'wmsuser'
            $h['Password']       | Should -Be 'fakepass-NOTREAL'
            $h['Query']          | Should -Be 'SELECT 1'
        } finally {
            $env:WMS_KILLIOS_DB_PASSWORD = $prev
        }
    }
}

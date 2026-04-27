Describe 'Logger helpers (privados)' {
    BeforeAll {
        $modulePath = (Resolve-Path "$PSScriptRoot/../../src/WmsBrainClient.psd1").Path
        Import-Module $modulePath -Force
        $script:loggerPath = (Resolve-Path "$PSScriptRoot/../../src/Private/_Logger.ps1").Path
        . $script:loggerPath
    }

    Context 'ConvertTo-WmsBrainSafeString' {
        It 'redacta el valor de WMS_KILLIOS_DB_PASSWORD si esta seteado' {
            $orig = $env:WMS_KILLIOS_DB_PASSWORD
            try {
                $env:WMS_KILLIOS_DB_PASSWORD = 'super-secret-password-1234'
                $out = ConvertTo-WmsBrainSafeString -InputString 'connection: User Id=foo, Password=super-secret-password-1234'
                $out | Should -Not -Match 'super-secret-password-1234'
                $out | Should -Match '<WMS_KILLIOS_DB_PASSWORD:redacted>'
            } finally {
                $env:WMS_KILLIOS_DB_PASSWORD = $orig
            }
        }

        It 'no rompe con string vacio' {
            { ConvertTo-WmsBrainSafeString -InputString '' } | Should -Not -Throw
        }

        It 'devuelve el input intacto si no hay vars secretas seteadas con valor > 4 chars' {
            $orig = $env:WMS_KILLIOS_DB_PASSWORD
            try {
                $env:WMS_KILLIOS_DB_PASSWORD = 'abc'
                $out = ConvertTo-WmsBrainSafeString -InputString 'no secrets here'
                $out | Should -Be 'no secrets here'
            } finally {
                $env:WMS_KILLIOS_DB_PASSWORD = $orig
            }
        }
    }

    Context 'Write-WmsBrainLog' {
        It 'no tira para los 5 niveles' {
            { Write-WmsBrainLog -Cmdlet 'X' -Level 'INFO' -Message 'm' } | Should -Not -Throw
            { Write-WmsBrainLog -Cmdlet 'X' -Level 'WARN' -Message 'm' } | Should -Not -Throw
            { Write-WmsBrainLog -Cmdlet 'X' -Level 'ERROR' -Message 'm' } | Should -Not -Throw
            { Write-WmsBrainLog -Cmdlet 'X' -Level 'DEBUG' -Message 'm' } | Should -Not -Throw
            { Write-WmsBrainLog -Cmdlet 'X' -Level 'OK' -Message 'm' } | Should -Not -Throw
        }
        It 'rechaza nivel invalido' {
            { Write-WmsBrainLog -Cmdlet 'X' -Level 'TRACE' -Message 'm' } | Should -Throw
        }
    }
}

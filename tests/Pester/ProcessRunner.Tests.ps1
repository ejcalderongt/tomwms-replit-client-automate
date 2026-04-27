Describe 'ConvertTo-WmsBrainProcessArgString (Win32 quoting, PS 5.1 + 7)' {
    BeforeAll {
        $modulePath = (Resolve-Path "$PSScriptRoot/../../src/WmsBrainClient.psd1").Path
        Import-Module $modulePath -Force
        . (Resolve-Path "$PSScriptRoot/../../src/Private/_NodeRunner.ps1").Path
    }

    It 'devuelve string vacio cuando no hay argumentos' {
        ConvertTo-WmsBrainProcessArgString -Arguments @() | Should -Be ''
    }

    It 'no quotea args sin espacios ni comillas' {
        ConvertTo-WmsBrainProcessArgString -Arguments @('notify', '--exchange-repo', 'C:\repos\exch') |
            Should -Be 'notify --exchange-repo C:\repos\exch'
    }

    It 'quotea args con espacios' {
        ConvertTo-WmsBrainProcessArgString -Arguments @('--from-event-file', 'C:\Program Files\evt.json') |
            Should -Be '--from-event-file "C:\Program Files\evt.json"'
    }

    It 'escapa comillas internas como \"' {
        $r = ConvertTo-WmsBrainProcessArgString -Arguments @('--message', 'di "hola"')
        $r | Should -Be '--message "di \"hola\""'
    }

    It 'duplica backslashes que preceden a un cierre de quote' {
        $r = ConvertTo-WmsBrainProcessArgString -Arguments @('C:\path with space\')
        $r | Should -Be '"C:\path with space\\"'
    }

    It 'mantiene backslashes interiores sin tocar' {
        $r = ConvertTo-WmsBrainProcessArgString -Arguments @('a\b\c with space')
        $r | Should -Be '"a\b\c with space"'
    }
}

Describe 'Runners no invocan ProcessStartInfo.ArgumentList (incompat PS 5.1)' {
    # El regex apunta al uso real de API (`$psi.ArgumentList...`) y no al
    # texto suelto "ArgumentList" para no chocar con comentarios que
    # documentan justamente esta incompatibilidad.

    It '_NodeRunner.ps1 no usa $psi.ArgumentList' {
        $path = (Resolve-Path "$PSScriptRoot/../../src/Private/_NodeRunner.ps1").Path
        $content = Get-Content -LiteralPath $path -Raw
        $content | Should -Not -Match '\$psi\.ArgumentList'
    }
    It '_GitHelpers.ps1 no usa $psi.ArgumentList' {
        $path = (Resolve-Path "$PSScriptRoot/../../src/Private/_GitHelpers.ps1").Path
        $content = Get-Content -LiteralPath $path -Raw
        $content | Should -Not -Match '\$psi\.ArgumentList'
    }
    It '_NodeRunner.ps1 setea Arguments (string) en ProcessStartInfo' {
        $path = (Resolve-Path "$PSScriptRoot/../../src/Private/_NodeRunner.ps1").Path
        (Get-Content -LiteralPath $path -Raw) | Should -Match '\$psi\.Arguments\s*='
    }
    It '_GitHelpers.ps1 setea Arguments (string) en ProcessStartInfo' {
        $path = (Resolve-Path "$PSScriptRoot/../../src/Private/_GitHelpers.ps1").Path
        (Get-Content -LiteralPath $path -Raw) | Should -Match '\$psi\.Arguments\s*='
    }
}

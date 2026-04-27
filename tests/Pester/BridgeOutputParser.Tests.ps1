Describe 'ConvertFrom-WmsBrainBridgeListOutput' {
    BeforeAll {
        $modulePath = (Resolve-Path "$PSScriptRoot/../../src/WmsBrainClient.psd1").Path
        Import-Module $modulePath -Force
        . (Resolve-Path "$PSScriptRoot/../../src/Private/_BridgeOutputParser.ps1").Path
    }

    It 'devuelve array vacio para stdout vacio' {
        $r = ConvertFrom-WmsBrainBridgeListOutput -StdOut ''
        @($r).Count | Should -Be 0
    }

    It 'parsea formato real key=value del bridge (status=pending, type=apply_succeeded)' {
        $sample = @'
20260427-1845-EJC  type=apply_succeeded  status=pending  source=apply_bundle  message="v23 OK"
20260427-1846-EJC  type=directive        status=analyzed source=openclaw      message="Q-001 review"
'@
        $r = @(ConvertFrom-WmsBrainBridgeListOutput -StdOut $sample)
        $r.Count | Should -Be 2
        $r[0].id      | Should -Be '20260427-1845-EJC'
        $r[0].type    | Should -Be 'apply_succeeded'
        $r[0].status  | Should -Be 'pending'
        $r[0].source  | Should -Be 'apply_bundle'
        $r[0].message | Should -Be 'v23 OK'
        $r[1].id      | Should -Be '20260427-1846-EJC'
        $r[1].type    | Should -Be 'directive'
        $r[1].status  | Should -Be 'analyzed'
    }

    It 'salta headers, separadores y lineas vacias' {
        $sample = @'
ID                   TYPE              STATUS    SOURCE        MESSAGE
-------------------- ----------------- --------- ------------- -------------
20260427-1845-EJC  type=apply_succeeded  status=pending source=apply_bundle  message="ok"

'@
        $r = @(ConvertFrom-WmsBrainBridgeListOutput -StdOut $sample)
        $r.Count | Should -Be 1
        $r[0].id | Should -Be '20260427-1845-EJC'
    }

    It 'parsea JSON array si el bridge devuelve --json' {
        $sample = '[{"id":"20260427-1900-EJC","type":"apply_succeeded","status":"pending"}]'
        $r = @(ConvertFrom-WmsBrainBridgeListOutput -StdOut $sample)
        $r.Count | Should -Be 1
        $r[0].id     | Should -Be '20260427-1900-EJC'
        $r[0].type   | Should -Be 'apply_succeeded'
        $r[0].status | Should -Be 'pending'
    }

    It 'parsea formato tabular legacy id status type summary' {
        $sample = '20260427-1845-EJC  pending  apply_succeeded  v23 fix ajuste borrador'
        $r = @(ConvertFrom-WmsBrainBridgeListOutput -StdOut $sample)
        $r.Count | Should -Be 1
        $r[0].id      | Should -Be '20260427-1845-EJC'
        $r[0].status  | Should -Be 'pending'
        $r[0].type    | Should -Be 'apply_succeeded'
        $r[0].summary | Should -Match 'ajuste'
    }

    It 'JSON malformado cae a parser de texto sin tirar' {
        $sample = '[{not json'
        { ConvertFrom-WmsBrainBridgeListOutput -StdOut $sample } | Should -Not -Throw
        $r = @(ConvertFrom-WmsBrainBridgeListOutput -StdOut $sample)
        $r.Count | Should -Be 0
    }
}

Describe 'ConvertFrom-WmsBrainHelloSyncOutput' {
    BeforeAll {
        $modulePath = (Resolve-Path "$PSScriptRoot/../../src/WmsBrainClient.psd1").Path
        Import-Module $modulePath -Force
        . (Resolve-Path "$PSScriptRoot/../../src/Private/_BridgeOutputParser.ps1").Path
    }

    It 'parsea formato real OK rama=... head=... bundles=...' {
        $sample = 'OK  rama=wms-brain  head=abcd1234  bundles=12  ultimo_producido=v23  ultimo_aplicado=v22  pendiente=v23'
        $r = ConvertFrom-WmsBrainHelloSyncOutput -StdOut $sample
        $r.ExchangeBranch     | Should -Be 'wms-brain'
        $r.ExchangeHead       | Should -Be 'abcd1234'
        $r.BundlesEncontrados | Should -Be 12
        $r.UltimoProducido    | Should -Be 'v23'
        $r.UltimoAplicado     | Should -Be 'v22'
        @($r.Pendiente).Count | Should -Be 1
        $r.Pendiente[0]       | Should -Be 'v23'
    }

    It 'parsea formato legacy verboso `Rama: ...` / `HEAD: ...`' {
        $sample = @'
Rama: wms-brain
HEAD: abcd1234
Bundles encontrados: 12
Ultimo producido: v23
Ultimo aplicado: v22
Pendientes: v23, v24
'@
        $r = ConvertFrom-WmsBrainHelloSyncOutput -StdOut $sample
        $r.ExchangeBranch     | Should -Be 'wms-brain'
        $r.ExchangeHead       | Should -Be 'abcd1234'
        $r.BundlesEncontrados | Should -Be 12
        $r.UltimoProducido    | Should -Be 'v23'
        $r.UltimoAplicado     | Should -Be 'v22'
        @($r.Pendiente).Count | Should -Be 2
        $r.Pendiente -contains 'v24' | Should -BeTrue
    }

    It 'tolera multiples lineas key=value y devuelve campos vacios si faltan' {
        $sample = 'OK  rama=main  head=deadbeef'
        $r = ConvertFrom-WmsBrainHelloSyncOutput -StdOut $sample
        $r.ExchangeBranch  | Should -Be 'main'
        $r.ExchangeHead    | Should -Be 'deadbeef'
        $r.UltimoProducido | Should -Be ''
        @($r.Pendiente).Count | Should -Be 0
    }

    It 'devuelve campos vacios para stdout vacio sin tirar' {
        { ConvertFrom-WmsBrainHelloSyncOutput -StdOut '' } | Should -Not -Throw
        $r = ConvertFrom-WmsBrainHelloSyncOutput -StdOut ''
        $r.ExchangeBranch | Should -Be ''
        $r.ExchangeHead   | Should -Be ''
    }

    It 'soporta sinonimos branch / last_produced / pending' {
        $sample = 'OK  branch=wms-brain  head=abc  bundles=5  last_produced=v10  last_applied=v9  pending=v10'
        $r = ConvertFrom-WmsBrainHelloSyncOutput -StdOut $sample
        $r.ExchangeBranch  | Should -Be 'wms-brain'
        $r.UltimoProducido | Should -Be 'v10'
        $r.UltimoAplicado  | Should -Be 'v9'
        $r.Pendiente[0]    | Should -Be 'v10'
    }
}

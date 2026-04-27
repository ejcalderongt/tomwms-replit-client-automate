@{
    RootModule           = 'WmsBrainClient.psm1'
    ModuleVersion        = '0.2.0'
    GUID                 = 'b0a2e6f3-7f5d-4d69-8a7d-2c1f6e8d9a01'
    Author               = 'Erik Calderon (PrograX24)'
    CompanyName          = 'PrograX24'
    Copyright            = '(c) 2026 PrograX24. Uso interno.'
    Description          = 'Cliente operativo PowerShell del ecosistema TOMWMS Brain. Wrappea brain_bridge.mjs / apply_bundle.mjs / hello_sync.mjs y la conexion SQL a las BDs productivas (read-only).'
    PowerShellVersion    = '5.1'
    CompatiblePSEditions = @('Desktop', 'Core')
    FunctionsToExport    = @(
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
    CmdletsToExport      = @()
    VariablesToExport    = @()
    AliasesToExport      = @('wmsbc')
    PrivateData          = @{
        PSData = @{
            Tags         = @('WMS', 'TOMWMS', 'Brain', 'PrograX24', 'SqlServer', 'Bridge')
            ProjectUri   = 'https://github.com/ejcalderongt/tomwms-replit-client-automate/tree/wms-brain-client'
            ReleaseNotes = 'v0.2.0 — alineado al brain_bridge real (SCHEMA_VERSION="1") + propuesta v2.'
            ExpectedBridgeSchemaVersion = '1'
        }
    }
}

# Brain Query MVP
param(
  [Parameter(Mandatory=$true)][string]$Question,
  [string]$ExchangeRepo = "C:\tomwms-exchange",
  [string]$BrainRepo = "C:\tomwms-brain"
)

Write-Host "Question: $Question"
Write-Host "Brain cache: $BrainRepo"
Write-Host "Exchange: $ExchangeRepo"
Write-Host "(MVP) Aqui se conectará el índice del brain y la capa SQL en el siguiente paso."

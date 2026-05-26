#Requires -Version 5.1

$ErrorActionPreference = "Stop"

Write-Host "=== Instalando Ollama ==="
irm https://ollama.com/install.ps1 | iex

Write-Host "=== Verificando Python ==="
python --version

Write-Host "=== Instalando Aider ==="
python -m pip install --upgrade pip
python -m pip install aider-install
aider-install

Write-Host "=== Configurando Ollama API ==="
setx OLLAMA_API_BASE "http://127.0.0.1:11434"

Write-Host "=== Descargando modelo qwen2.5-coder:14b ==="
ollama pull qwen2.5-coder:14b

Write-Host "=== Probando modelo ==="
ollama run qwen2.5-coder:14b "Responde solo: OK modelo local funcionando"

Write-Host "=== Listo ==="
Write-Host "Cierra y abre PowerShell de nuevo antes de usar Aider."
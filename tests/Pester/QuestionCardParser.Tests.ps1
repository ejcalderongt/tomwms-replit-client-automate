Describe 'QuestionCardParser (privado)' {
    BeforeAll {
        $modulePath = (Resolve-Path "$PSScriptRoot/../../src/WmsBrainClient.psd1").Path
        Import-Module $modulePath -Force
        $script:parserPath = (Resolve-Path "$PSScriptRoot/../../src/Private/_QuestionCardParser.ps1").Path
        . $script:parserPath
        $script:tmp = Join-Path ([System.IO.Path]::GetTempPath()) ("WmsCard-" + [Guid]::NewGuid().ToString('N').Substring(0,8))
        New-Item -ItemType Directory -Path $script:tmp -Force | Out-Null
    }
    AfterAll {
        if (Test-Path -LiteralPath $script:tmp) { Remove-Item -LiteralPath $script:tmp -Recurse -Force -ErrorAction SilentlyContinue }
    }

    It 'Test-WmsBrainYamlModule devuelve booleano sin tirar' {
        $r = Test-WmsBrainYamlModule
        $r | Should -BeOfType ([bool])
    }

    It 'Read-WmsBrainYamlFrontMatter devuelve {Path, Yaml, Body} crudos' {
        $cardPath = Join-Path $script:tmp 'Q-999-test.md'
        @"
---
id: Q-999
title: Pregunta de test
priority: high
tags:
  - test
  - parser
---

# Cuerpo

Contenido libre del .md.
"@ | Set-Content -LiteralPath $cardPath -Encoding UTF8

        $fm = Read-WmsBrainYamlFrontMatter -Path $cardPath
        $fm.Path | Should -Be $cardPath
        $fm.Yaml | Should -Match 'id:\s*Q-999'
        $fm.Yaml | Should -Match 'title:\s*Pregunta de test'
        $fm.Body | Should -Match '# Cuerpo'
    }

    It 'Read-WmsBrainQuestionCard parsea YAML como propiedades del objeto' {
        $hasYaml = Test-WmsBrainYamlModule
        if (-not $hasYaml) {
            Set-ItResult -Skipped -Because 'powershell-yaml no esta instalado en este host'
            return
        }
        $cardPath = Join-Path $script:tmp 'Q-997-full.md'
        @"
---
id: Q-997
title: Card completa
priority: medium
tags:
  - test
  - parser
---

cuerpo
"@ | Set-Content -LiteralPath $cardPath -Encoding UTF8
        $card = Read-WmsBrainQuestionCard -Path $cardPath
        $card.id       | Should -Be 'Q-997'
        $card.title    | Should -Be 'Card completa'
        $card.priority | Should -Be 'medium'
        @($card.tags).Count | Should -Be 2
        $card.Path     | Should -Be $cardPath
        $card.Body     | Should -Match 'cuerpo'
    }

    It 'Read-WmsBrainYamlFrontMatter tira si no hay front-matter' {
        $cardPath = Join-Path $script:tmp 'Q-998-noyaml.md'
        '# Solo cuerpo, sin front-matter' | Set-Content -LiteralPath $cardPath -Encoding UTF8
        { Read-WmsBrainYamlFrontMatter -Path $cardPath } | Should -Throw
    }

    It 'Get-WmsBrainQuestionsDir resuelve a <repo>/questions cuando el dir existe' {
        $qdir = Join-Path $script:tmp 'questions'
        New-Item -ItemType Directory -Path $qdir -Force | Out-Null
        $d = Get-WmsBrainQuestionsDir -ClientRepo $script:tmp
        $d | Should -Be $qdir
    }

    It 'Get-WmsBrainQuestionsDir tira [2] si <repo>/questions no existe' {
        $repoNoQ = Join-Path $script:tmp 'no-questions-here'
        New-Item -ItemType Directory -Path $repoNoQ -Force | Out-Null
        { Get-WmsBrainQuestionsDir -ClientRepo $repoNoQ } | Should -Throw '*No existe directorio de questions*'
    }
}

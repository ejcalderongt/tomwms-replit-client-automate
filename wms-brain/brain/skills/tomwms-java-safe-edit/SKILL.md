---
name: tomwms-java-safe-edit
description: Safely inspect and edit TOMWMS/TOMHH2025 Java source files when encoding, mojibake, CRLF/LF, Logcat tags, or repeated patch failures are a risk. Use for Android HH Java changes, especially files containing Spanish text, legacy comments, or prior mojibake.
---

# TOMWMS Java Safe Edit

Use this skill before modifying TOMHH2025 Java files that contain Spanish text, legacy comments, or fragile encoding. The goal is to preserve the file's existing bytes as much as possible while making small, reviewable edits.

## Encoding Policy

- Prefer UTF-8 without BOM for Java/Android source files.
- Do not add a BOM to Java files unless the repo already standardizes on BOM for that file family.
- Do not normalize an entire legacy file just to make a small code change.
- Preserve existing line endings unless the task explicitly asks for normalization.
- Treat mojibake as existing content unless the user asks to repair text encoding across the file.

## Workflow

1. Inspect the target file before editing:

```powershell
powershell -NoProfile -ExecutionPolicy Bypass -File C:\Users\carol\.codex\skills\tomwms-java-safe-edit\scripts\Inspect-JavaEncoding.ps1 -Path <absolute-java-file>
```

2. Prefer `apply_patch` for code edits. Keep context lines ASCII when possible if the file contains mojibake.

3. Avoid matching patch context against corrupted Spanish strings. Anchor patches around stable Java syntax, method names, tags, or ASCII comments.

4. Add or update inline tags using the current task initials, for example `//#EJC20260702_LC001`, but keep comments short.

5. Validate after editing:

```powershell
powershell -NoProfile -ExecutionPolicy Bypass -File C:\Users\carol\.codex\skills\tomwms-java-safe-edit\scripts\Test-JavaEdit.ps1 -Path <absolute-java-file> -GradleProject C:\Users\carol\StudioProjects\TOMHH2025
```

6. If the patch repeatedly fails because of mojibake, inspect exact line numbers with PowerShell and patch using nearby ASCII-only context.

## Guardrails

- Do not rewrite Java files with `Set-Content`, `Out-File`, Python, or broad formatters for ordinary edits.
- Do not convert all comments or strings to repair mojibake unless explicitly asked.
- Do not place generated logs, backups, or reports inside the repo unless the user asks. Use `%TEMP%`.
- If a change touches WebService flow, validate with `.\gradlew.bat :app:compileDebugJavaWithJavac`.
- For HH flow guards, prefer state flags released on real callback completion, not fixed timers.

## Useful Patterns

For exact line inspection without rewriting:

```powershell
$p = "<absolute-java-file>"
$i = 0
Get-Content $p | ForEach-Object {
  $i++
  if ($i -ge 100 -and $i -le 130) { "{0,5}: {1}" -f $i,$_ }
}
```

For byte inspection of a suspicious line:

```powershell
$line = (Get-Content <absolute-java-file>)[123]
[System.Text.Encoding]::UTF8.GetBytes($line) | ForEach-Object { $_.ToString("X2") }
```

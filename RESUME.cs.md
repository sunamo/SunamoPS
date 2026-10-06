---
schema_version: 11
type: my-library
category_override: none
file_count: 56
file_extensions: cs:37, md:8, csproj:3, noext:3, json:1, jsonanddelete:1, old:1, slnx:1, txt:1, yml:1
file_extensions_updated: 2026-10-04
avg_lines_per_file: 54
total_lines: 3109
metrics_lm: 2026-09-30 15:11:44
move_to_legacy_percent: 10
description_updated: 2026-09-30
links_updated: 2026-09-30
github_source_url: not run
origin_status: pending
origin_checked: not run
article_source_url: not run
article_status: pending
article_checked: not run
last_build_ok: not run
last_build_date: not run
last_tests_run_date: not run
covered_lines: not run
---

## Description

Knihovna pro práci s PowerShell 7: spouštění příkazů a skriptů (`PowershellRunner`, `PowershellBuilder`), parsování a zpracování výstupů (`PowershellParser`, `PsOutput`, `ErrorRecordHelper`). Staví na `Microsoft.PowerShell.SDK` a `System.Management.Automation`.
Balíček je self-contained: kód dříve referencovaných balíčků (SunamoExceptions, SunamoStringGetLines, SunamoStringSplit, SunamoDictionary, SunamoInterfaces aj.) je zkopírován do `_sunamo\` jako internal; jiné Sunamo balíčky z projektu SunamoPS nereferencuje (pomocný projekt RunnerPS má ještě project reference).

## Původ zdrojáků

Původ zdrojáků se zatím nezjišťoval (`origin_status: pending`).

## Doporučení přesunu do legacy

Doporučení přesunu do legacy: **10 %** — hodnota převzata ze starší verze souboru, důvod nebyl zapsán.

## Vazby na moje repa

- Submoduly: not run
- ProjectReference / PackageReference: not run
